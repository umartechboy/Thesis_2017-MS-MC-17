using Drogon3;
using RoboSim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Physics
{
    public delegate void doubleArrayChangedHandler(int index, double value);
    public delegate void doubleChangedHandler(object sender, double value);
    public interface IServoMotor
    {
        UserControl Editor { get; }
        event doubleChangedHandler OnMovement;
        int ID { get; set; }
        void ThreadStep(double dt);
        double Reference { get; set; }
        double Current { get; }
        double Minimum { get; }
        double Range { get; set; }
        System.Windows.Media.Media3D.AxisAngleRotation3D VisualRotation { get; }

        bool TargetAchieved();
    }

    public class StepperMotor : IServoMotor
    {
        double _resolution = 1;
        public System.Windows.Media.Media3D.AxisAngleRotation3D VisualRotation { get; protected set; }
        public Axis Axis { get; private set; }
        public double Resolution { get { return _resolution; } protected set { _resolution = value; SMEditor.resolution.Text = value.ToString(); } }
        public UserControl Editor { get { return SMEditor; } }
        public StepperMotorEditor SMEditor { get; private set; }
        public StepperMotor(int id,
            int stepsPerRevolution,
            double stepTime,
            double min, double range,
            double startingAngle,
            Axis rotationAxis
            )
        {
            ID = id;

            SMEditor = new StepperMotorEditor(id);
            SMEditor.minimum.Leave += (s, e) =>
            {
                try { SMEditor.minimum.Text = double.Parse(SMEditor.minimum.Text).ToString(); }
                catch { SMEditor.minimum.Text = "0"; }
                Minimum = double.Parse(SMEditor.minimum.Text);
                Range = Maximum - Minimum;
                if (Range < 0)
                {
                    Range = 0;
                    Maximum = Minimum;
                }
            };
            SMEditor.maximum.Leave += (s, e) =>
            {
                try { SMEditor.maximum.Text = double.Parse(SMEditor.maximum.Text).ToString(); }
                catch { SMEditor.maximum.Text = "0"; }
                Maximum = double.Parse(SMEditor.maximum.Text);
                Range = Maximum - Minimum;
                if (Range < 0)
                {
                    Range = 0;
                    Minimum = Maximum;
                }
            };
            SMEditor.range.Leave += (s, e) =>
            {
                try { SMEditor.range.Text = double.Parse(SMEditor.range.Text).ToString(); }
                catch { SMEditor.range.Text = "0"; }
                Range = double.Parse(SMEditor.range.Text);
                Maximum = Minimum + Range;
                if (Range < 0)
                {
                    Range = 0;
                    Maximum = Minimum;
                }
            };
            Resolution = stepsPerRevolution;
            StepTime = stepTime;
            Minimum = min;
            Range = range;
            Maximum = Maximum;
            Axis = rotationAxis;
            if (rotationAxis == Axis.X)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(1, 0, 0), 0);
            else if (rotationAxis == Axis.Y)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 1, 0), 0);
            else if (rotationAxis == Axis.Z)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 0, 1), 0);
            Current = startingAngle;
            Reference = startingAngle;

        }

        double _stepTime;
        /// <summary>
        /// Maximum time required to do one step
        /// </summary>
        public double StepTime { get { return _stepTime; } protected set { _stepTime = value; SMEditor.maxStepTime.Text = value.ToString(); } }
        public double AssumedLife { get; protected set; } = 0;
        public int ID { get; set; }
        public double Reference { get; set; }

        public double Current
        {
            get
            {
                return TotalSteps / (double)Resolution * 2 * Math.PI;
            }
            protected set
            {
                TotalSteps = (long)Math.Round((double)value / (2 * Math.PI) * Resolution);
            }
        }

        double _min, _range;
        public double Minimum { get { return _min; } protected set { SMEditor.minimum.Text = value.ToString(); _min = value; } }
        public double Maximum { get { return Minimum + Range; } protected set { Minimum = value - Range; SMEditor.maximum.Text = value.ToString(); } }
        public double Range { get { return _range; } set { _range = value; SMEditor.range.Text = Range.ToString(); } }

        long _steps = 0;
        long TotalSteps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                VisualRotation.Angle = Current * 180 / Math.PI;
            }
        }
        double lastStepAt = 0;


        public event doubleChangedHandler OnMovement;

        public void ThreadStep(double dt)
        {
            double supposedLife = AssumedLife + dt;
            int dir = 0;
            for (; AssumedLife < supposedLife; AssumedLife += StepTime)
             {
                if (Reference < Current && dir <= 0)
                { 
                    TotalSteps--;
                    if (Reference > Current)
                        TotalSteps++; // undo
                    else
                        // This will force a Visual refresh
                        OnMovement(this, Current);
                    dir = -1; 
                }
                else if (Reference > Current && dir >= 0)
                {
                    TotalSteps++;
                    if (Reference < Current)
                        TotalSteps--; // undo
                    else
                        // This will force a Visual refresh
                        OnMovement(this, Current);
                    dir = 1;
                }
            }
        }

        public bool TargetAchieved()
        {
            var TotalSteps = this.TotalSteps;
            // emulate a sim step to see it it would have moved the motor.
            if (Reference < TotalSteps / (double)Resolution * 2 * Math.PI)
            {
                TotalSteps--;
                if (Reference > TotalSteps / (double)Resolution * 2 * Math.PI)
                    TotalSteps++; // undo
            }
            else if (Reference > TotalSteps / (double)Resolution * 2 * Math.PI)
            {
                TotalSteps++;
                if (Reference < TotalSteps / (double)Resolution * 2 * Math.PI)
                    TotalSteps--; // undo
            }
            if (TotalSteps == this.TotalSteps)
                return true;
            return false;
        }
    }
    public class ServoMotor : IServoMotor
    {
        public System.Windows.Media.Media3D.AxisAngleRotation3D VisualRotation { get; protected set; }
        public Axis Axis { get; private set; }
        public PID PositionPID { get; private set; } = new PID();
        public double Omega { get; set; } = 0;
        double[] OmegaHistory = new double[3];
        public double P { get { return PositionPID.Kp; } set { PositionPID.Kp = value; SMEditor.P.Text = value.ToString(); } } 
        public double I { get { return PositionPID.Ki; } set { PositionPID.Ki = value; SMEditor.I.Text = value.ToString(); } }
        public double D { get { return PositionPID.Kd; } set { PositionPID.Kd = value; SMEditor.D.Text = value.ToString(); } }
        double _pw = 0;
        public double MaximumPower { get { return _pw; } set { _pw = value; PositionPID.Min = -value; PositionPID.Max = value; } }
        public double MaximumTorque { get; set; }
        public UserControl Editor { get { return SMEditor; } }
        public ServoMotorEditor SMEditor { get; private set; }
        SphericalRobot Robot;
        public ServoMotor(int id,
            SphericalRobot robot,
            double min, double range,
            double startingAngle,
            double p, double i, double d,
            double power,
            double torque,
            Axis rotationAxis
            )
        {
            ID = id;
            this.Robot = robot;
            SMEditor = new ServoMotorEditor(id);
            SMEditor.minimum.Leave += (s, e) =>
            {
                try { SMEditor.minimum.Text = double.Parse(SMEditor.minimum.Text).ToString(); }
                catch { SMEditor.minimum.Text = "0"; }
                Minimum = double.Parse(SMEditor.minimum.Text);
                Range = Maximum - Minimum;
                if (Range < 0)
                {
                    Range = 0;
                    Maximum = Minimum;
                }
            };
            SMEditor.maximum.Leave += (s, e) =>
            {
                try { SMEditor.maximum.Text = double.Parse(SMEditor.maximum.Text).ToString(); }
                catch { SMEditor.maximum.Text = "0"; }
                Maximum = double.Parse(SMEditor.maximum.Text);
                Range = Maximum - Minimum;
                if (Range < 0)
                {
                    Range = 0;
                    Minimum = Maximum;
                }
            };
            SMEditor.range.Leave += (s, e) =>
            {
                try { SMEditor.range.Text = double.Parse(SMEditor.range.Text).ToString(); }
                catch { SMEditor.range.Text = "0"; }
                Range = double.Parse(SMEditor.range.Text);
                Maximum = Minimum + Range;
                if (Range < 0)
                {
                    Range = 0;
                    Maximum = Minimum;
                }
            };
            SMEditor.P.Leave += (s, e) =>
            {
                try { SMEditor.P.Text = double.Parse(SMEditor.P.Text).ToString(); }
                catch { SMEditor.P.Text = "0"; }
                P = double.Parse(SMEditor.P.Text);
            };
            SMEditor.I.Leave += (s, e) =>
            {
                try { SMEditor.I.Text = double.Parse(SMEditor.I.Text).ToString(); }
                catch { SMEditor.I.Text = "0"; }
                I = double.Parse(SMEditor.I.Text);
            };
            SMEditor.D.Leave += (s, e) =>
            {
                try { SMEditor.D.Text = double.Parse(SMEditor.D.Text).ToString(); }
                catch { SMEditor.D.Text = "0"; }
                D = double.Parse(SMEditor.D.Text);
            };

            SMEditor.power.Leave += (s, e) =>
            {
                try { SMEditor.power.Text = double.Parse(SMEditor.power.Text).ToString(); }
                catch { SMEditor.power.Text = "0"; }
                MaximumPower = double.Parse(SMEditor.power.Text);
                PositionPID.Max = MaximumPower;
            };
            Minimum = min;
            Range = range;
            Maximum = Maximum;
            Axis = rotationAxis;
            if (rotationAxis == Axis.X)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(1, 0, 0), 0);
            else if (rotationAxis == Axis.Y)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 1, 0), 0);
            else if (rotationAxis == Axis.Z)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 0, 1), 0);
            //Current = startingAngle;
            Reference = startingAngle;
            P = p; I = i; D = d; 
            MaximumPower = power;
            MaximumTorque = torque;
        }

        public int ID { get; set; }
        public double Reference { get; set; }

        public double AppliedPower { get; private set; }
        double _cur = 0;
        public double Current
        {
            get { return _cur; }
            set
            {
                _cur = value;
                VisualRotation.Angle = Current * 180 / Math.PI;
            }
        }

        double _min, _range;
        public double Minimum { get { return _min; } protected set { SMEditor.minimum.Text = value.ToString(); _min = value; } }
        public double Maximum { get { return Minimum + Range; } protected set { Minimum = value - Range; SMEditor.maximum.Text = value.ToString(); } }
        public double Range { get { return _range; } set { _range = value; SMEditor.range.Text = Range.ToString(); } }

        long _steps = 0;
        long TotalSteps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                VisualRotation.Angle = Current * 180 / Math.PI;
            }
        }
        public double AppliedTorque { get; private set; }
        public double TorqueRate { get; private set; } = 1000; // kgm/sec
        public event doubleChangedHandler OnMovement;
        public void ThreadStep(double dt) // implement the PID step here.
        {
            var startingTh = Current;
            var pow = PositionPID.Signal(Reference, Current, dt);
            var tau = pow / Math.Abs(Omega);
            if (Omega == 0)
            {
                if (pow > 0)
                    tau = MaximumTorque;
                else if (pow < 0)
                    tau = -MaximumTorque;
                else
                    tau = 0;
            }
            if (tau > MaximumTorque)
                tau = MaximumTorque;
            else if (tau < -MaximumTorque)
                tau = -MaximumTorque;
            if (tau > AppliedTorque)
            {
                AppliedTorque += TorqueRate * dt;
                if (AppliedTorque > tau)
                    AppliedTorque = tau;
            }
            else if (tau < AppliedTorque)
            {
                AppliedTorque -= TorqueRate * dt;
                if (AppliedTorque < tau)
                    AppliedTorque = tau;
            }
            // we need to do a mechanical thread step here.
            // lets find out what load is the motor up against.
            var I = Robot.GetInertialLoadOnMotor(ID);
            var TG = Robot.GetGravitationalLoadOnMotor(ID);
            var alpha = AppliedTorque / I;
            var vS = Omega * dt;
            var aS = (((alpha * dt) * dt) / 2);
            var ds = vS + aS;
            AppliedPower = AppliedTorque * Omega;
            Current += ds; 

            var endingTh = Current;
            var thisOmega = (endingTh - startingTh) / dt;
            for (int i = 0; i < OmegaHistory.Length - 1; i++)
                OmegaHistory[i] = OmegaHistory[i + 1];
            OmegaHistory[OmegaHistory.Length - 1] = thisOmega;
            Omega = OmegaHistory.Average();
            OnMovement(this, Current);
        }

        public bool TargetAchieved()
        {
            throw new NotImplementedException();
        }
    }
}
