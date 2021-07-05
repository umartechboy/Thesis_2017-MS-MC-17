using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    public delegate void doubleArrayChangedHandler(int index, double value);
    public delegate void doubleChangedHandler(object sender, double value);
    public class ServoActuator
    {
        public int ID { get; }
        public virtual event doubleChangedHandler OnPositionChanged;
        public virtual event doubleChangedHandler OnTargetChanged;
        protected virtual void PositionChanged(double value)
        {
            OnPositionChanged?.Invoke(this, value);
        }
        public virtual bool IsOnTarget
        { get { return CurrentPosition == Target; } }
        public virtual bool TargetAchievable(double value)
        {
            if (value < MinimumPosition)
                return false;
            if (value > MinimumPosition + PositionRange)
                return false;
            return true;
        }

        protected ServoActuator(double min, double range, double startingPosition, int id)
        {
            MinimumPosition = min;
            PositionRange = range;
            curPos = startingPosition;
            Target = startingPosition;
            ID = id;
        }
        ServoActuator ls;
        public ServoActuator LastState
        {
            get { return ls; }
            protected set
            {
                ls = value.Clone();
            }

        }
        public virtual void ThreadStep(double dt)
        {
            throw new Exception("Step must define how a motor is going to behave with time.");
        }
        public virtual ServoActuator Clone()
        {
            return (ServoActuator)this.MemberwiseClone();
        }
        protected double curPos = 0, target = 0;
        public virtual void ForceSetCurrentPosition(double value)
        { curPos = value; }
        public virtual double CurrentPosition
        {
            get { return curPos; }
            protected set
            {
                curPos = value;
                PositionChanged(value);
            }
        }
        public double PositionRange { get; set; }
        public double MinimumPosition { get; protected set; }
        public virtual double Target
        {
            get { return target; }
            set
            {
                double bkp = target;
                target = value;
                if (target < MinimumPosition)
                    target = MinimumPosition;
                if (target > MinimumPosition + PositionRange)
                    target = MinimumPosition + PositionRange;
                if (target != bkp)
                    OnTargetChanged?.Invoke(this, target);
            }
        }
    }
    public class ServoMotor :ServoActuator
    {            
        public System.Windows.Media.Media3D.AxisAngleRotation3D VisualRotation { get; protected set; }
        public override double CurrentPosition
        {          
            protected set
            {
                base.CurrentPosition = value;  
                var bkp = curPos;
                curPos = value;
                if (curPos != bkp)
                    PositionChanged(curPos);
            }
        }

        protected override void PositionChanged(double value)
        {
            base.PositionChanged(value);
            VisualRotation.Angle = CurrentPosition * 180 / Math.PI;
        }
        public override void ThreadStep(double dt)
        {
            // maximum gain controller
            CurrentPosition = target;
        }                    
        public override double Target
        {     
            set
            {                              
                while (value <= -Math.PI)
                    value += 2 * Math.PI;
                while (value > Math.PI)
                    value -= 2 * Math.PI;
                base.Target = value;
            }
        }
        public ServoMotor(double min, double range, double startingAngle, Axis rotationAxis, int id):base(min, range, startingAngle, id)
        {                     
            if (rotationAxis == Axis.X)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(1, 0, 0), 0);
            else if (rotationAxis == Axis.Y)
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 1, 0), 0);
            else if (rotationAxis == Axis.Z)                                                                                                                    
                VisualRotation = new System.Windows.Media.Media3D.AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 0, 1), 0);
                             
        }
        public override string ToString()
        {
            return "Current: " + CurrentPosition + ", Target: " + target + ", Range: " + PositionRange;
        }
    }
    public class StepperMotor : ServoMotor
    {
        public int Resolution { get; protected set; } = 1;
        Axis axisBkp = Axis.X;
        public StepperMotor(int stepsPerRevolution, double stepTime, double min, double range, double startingAngle, Axis axis, int id) : base(min, range, startingAngle, axis, id)
        {
            axisBkp = axis;
            StepTime = stepTime;
            Resolution = stepsPerRevolution;

        }
        public override double CurrentPosition
        {
            get
            {
                var v = (double)totalSteps / Resolution * 2 * Math.PI;
                if (v == -Math.PI)
                    return Math.PI;
                return v;
            }
        }
        public override void ForceSetCurrentPosition(double value)
        {                                                          
            totalSteps = (int)Math.Round(value * (Resolution / Math.PI / 2));
        }
        /// <summary>
        /// Maximum time required to do one step
        /// </summary>
        public double StepTime { get; protected set; }
        public double Life { get; protected set; } = 0;
        long totalSteps = 0;
        double lastStepAt = 0;
        
        public override void ThreadStep(double dt)
        {                 
            if (CurrentPosition != Target)
            {
                while (CurrentPosition != Target)
                {
                    if (Life - lastStepAt > StepTime)
                    {
                        if (Target < CurrentPosition)
                            totalSteps--;
                        else
                            totalSteps++;
                        lastStepAt += StepTime;
                    }
                    else
                        break;
                }
                // This will force a Visual refresh
                PositionChanged(CurrentPosition);
            }
            Life += dt;
            LastState = this;
        }
        public override bool IsOnTarget
        {
            get
            {
                double cv = Math.Abs(CurrentPosition - Target);
                if (cv >= 2 * Math.PI - Math.PI * 2 / Resolution)
                    return true;
                if (cv <= -2 * Math.PI + Math.PI * 2 / Resolution)
                    return true;
                return cv <= Math.PI * 2 / Resolution;
            }
        }
        public override ServoActuator Clone()
        {
            StepperMotor sm = new StepperMotor(Resolution, StepTime, MinimumPosition, PositionRange, CurrentPosition, axisBkp, ID);
            sm.Life = Life;
            sm.totalSteps = totalSteps;
            sm.target = target;
            return sm;
        }
    }

    public class ConstantForceStepperRack : ServoActuator
    {               
        public System.Windows.Media.Media3D.ScaleTransform3D VisualScale { get; protected set; }
        public double SpringConstant { get; protected set; }
        double basicLength = 0;
        public ConstantForceStepperRack(double min, double range, double force, double fixedLength, double K, int id) : base(min, range, force, id)
        {
            SpringConstant = K;
            VisualScale = new System.Windows.Media.Media3D.ScaleTransform3D(1, 1, 1);
            VisualScale.CenterZ = -fixedLength / 2;
            basicLength = fixedLength;
        }

        protected override void PositionChanged(double value)
        {
            base.PositionChanged(value);
            VisualScale.ScaleZ = (CurrentPosition / SpringConstant + basicLength) / basicLength;
        }
        int internalLength = 0;
        public override void ThreadStep(double dt)
        {
            // infinite gain controller
            curPos = target;
            PositionChanged(curPos);
        }
    }
}
