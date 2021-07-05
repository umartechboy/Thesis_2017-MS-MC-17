using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;           
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;  
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using Physics;

namespace RoboSim
{
    public delegate void DebugPoint(Point3D p, int ind);
    public class Robot
    {
        public double SimLifeMillis = 0;
        double gA = -9.817;
        public Robot lastState;
        public virtual void Initialize()
        { }
        public ServoActuator[] Actuators;

        internal virtual Robot Clone()
        {
            Robot r = new Robot();
            r.Actuators = new ServoActuator[Actuators.Length];
            for (int i = 0; i < r.Actuators.Length; i++)
            {
                r.Actuators[i] = Actuators[i].Clone();
            }
            return r;
        }

        public void ThreadStep(double dt)
        {
            // Set last State = current here and then make the step
            for (int i = 0; i < Actuators.Length; i++)
            {
                Actuators[i].ThreadStep(dt);
            }
            SimLifeMillis += dt * 1000;
        }
        protected Model3DGroup gm = null;
        public Model3DGroup Model
        {
            get { return gm; }
        }
        public void sync3DView()
        {
            //Change Colors here if neccessary
        }
        public virtual void ShowSolution(RobotSolution solution)
        { }
        public virtual bool TargetAchievable(RobotSolution solution)
        {
            throw new NotImplementedException();
        }
    }
    public class SphericalRobot:Robot
    {
        public event DebugPoint OnDebugPoint;
        public event doubleArrayChangedHandler OnLengthChanged;
        public event doubleArrayChangedHandler OnPositionChanged;
        public event doubleArrayChangedHandler OnTargetChanged;
        public void Debug(Point3D p, int ind)
        {
            OnDebugPoint?.Invoke(p, ind);
        }     
        public double L0 { get { return armModelsTT[0].OffsetZ; } set { armModelsTT[0].OffsetZ = value;  resetModel(); OnLengthChanged?.Invoke(0, value); } }
        public double L1 { get { return armModelsTT[1].OffsetZ; } set { armModelsTT[1].OffsetZ = value; resetModel(); OnLengthChanged?.Invoke(1, value); } }
        public double L2 { get { return armModelsTT[2].OffsetZ * 2; } set { armModelsTT[2].OffsetZ = value / 2; resetModel(); OnLengthChanged?.Invoke(2, value); } }
        public double L3 { get { return armModelsTT[3].OffsetZ * 2; } set { armModelsTT[3].OffsetZ = value / 2; resetModel(); OnLengthChanged?.Invoke(3, value); } }
        public double L4 { get { return armModelsTT[4].OffsetZ * 2; } set { armModelsTT[4].OffsetZ = value / 2; resetModel(); OnLengthChanged?.Invoke(3, value); } }
        public SphericalRobot()
        {
            double l0 = 0.4, l1 = 1.2, l2 = 1, l3 = 0.4, l4 = 0.2;

            Actuators = new ServoActuator[7];
            Actuators[0] = new StepperMotor(500 * 50, 0.001, -Math.PI, 2 * Math.PI, -Math.PI/4, Axis.Z, 0);
            Actuators[1] = new StepperMotor(500 * 50, 0.001, -Math.PI / 2, Math.PI, -Math.PI/4, Axis.X, 1);
            Actuators[2] = new StepperMotor(500 * 50, 0.001, -2 * Math.PI/3, 4 * Math.PI / 3, 0, Axis.X, 2);
            Actuators[3] = new StepperMotor(500 * 50, 0.001, -Math.PI, 2 * Math.PI, 0, Axis.Z, 3);
            Actuators[4] = new StepperMotor(500 * 50, 0.001, -2 * Math.PI / 3, 4 * Math.PI / 3, 0, Axis.X, 4);
            Actuators[5] = new StepperMotor(500 * 50, 0.001, -2 * Math.PI, 4 * Math.PI, 0, Axis.Z, 5);
            Actuators[6] = new ConstantForceStepperRack(0, 100, 0, l4, 100, 6);
            foreach (var m in Actuators)
            {
                m.OnPositionChanged += M_OnPositionChanged;
                m.OnTargetChanged += M_OnTargetChanged;   
            }

            armModelsTT = new TranslateTransform3D[]
            {
                new TranslateTransform3D(0, 0, l0),
                 new TranslateTransform3D(0, 0, l1),
                  new TranslateTransform3D(0, 0, l2 / 2),
                   new TranslateTransform3D(0, 0, l3 / 2),
                    new TranslateTransform3D(0, 0, l4)
            };
            resetModel();
        }


        internal override Robot Clone()
        {            
            SphericalRobot  r = new SphericalRobot();
            r.Actuators = new ServoActuator[Actuators.Length];
            for (int i = 0; i < r.Actuators.Length; i++)
            {
                r.Actuators[i] = Actuators[i].Clone();
            }
            return r;
        }
        private void M_OnTargetChanged(object sender, double value)
        {
            OnTargetChanged?.Invoke(((ServoActuator)sender).ID, value);
        }          
        private void M_OnPositionChanged(object sender, double value)
        {
            OnPositionChanged?.Invoke(((ServoActuator)sender).ID, value);
        }          
        public EulerAngleOrientation CurrentPosition
        {
            get
            {
                var ans = new EulerAngleOrientation();
                ans.Offset =  new Point3D();
                return ans;
            }
        }
        void resetModel()
        {
            for (int i = 0; i < 7; i++) 
            {
                if (armModels[i] == null)
                    armModels[i] = new Model3DGroup();
                armModels[i].Children.Clear();
            }
            armModels[0].Children.Add(simCalc.CylinderToUJoint(L0, 0.4, 0.28, 0.3, 0.5, 0, System.Windows.Media.Colors.Red));
            armModels[1].Children.Add(simCalc.UjointToUJoint(L1, 0.39, 0.25, 0.3, 0, 0.5, 0.5, 0, System.Windows.Media.Colors.Green));
            armModels[2].Children.Add(simCalc.CylinderToUJoint(L2 / 2, 0.3, 0.28, 0.20, 0, 0.5, System.Windows.Media.Colors.Red, true));
            armModels[3].Children.Add(simCalc.CylinderToUJoint(L2 / 2, 0.3, 0.28, 0.20, 0.5, 0, System.Windows.Media.Colors.Red, false));
            armModels[4].Children.Add(simCalc.CylinderToUJoint(L3 / 2, 0.24, 0.23, 0.20, 0, 0.5, System.Windows.Media.Colors.Green, true));
            armModels[5].Children.Add(simCalc.meshToGeometry(simCalc.CylenderMesh(L3 / 2, 0.2, false), System.Windows.Media.Colors.White));
            armModels[6].Children.Add(simCalc.CircleToRectangle(0.15, 0.08, 0.005, L4, System.Windows.Media.Colors.White));
        }
        private TranslateTransform3D[] armModelsTT;
        Model3DGroup [] armModels = new Model3DGroup[7];
        public override void Initialize()
        {     
            //Initialize all the elements. Dimensions. Initial states etc.
            gm = new Model3DGroup();
            var link0 = new Model3DGroup();
            var link1 = new Model3DGroup();
            var link2 = new Model3DGroup();
            var link3 = new Model3DGroup();
            var link4 = new Model3DGroup();
            var link5 = new Model3DGroup();
            var link6 = new Model3DGroup();

            link0.Children.Add(armModels[0]);
            link0.Children.Add(link1);
            

            link1.Children.Add(armModels[1]);
            link1.Children.Add(link2);

            link2.Children.Add(armModels[2]);
            link2.Children.Add(link3);

            link3.Children.Add(armModels[3]);
            link3.Children.Add(link4);

            link4.Children.Add(armModels[4]);
            link4.Children.Add(link5);

            link5.Children.Add(armModels[5]);
            link5.Children.Add(link6);

            link6.Children.Add(armModels[6]);

            // add tranformations
            link0.Transform = new RotateTransform3D(((StepperMotor)Actuators[0]).VisualRotation);
            var tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((StepperMotor)Actuators[1]).VisualRotation));
            tg.Children.Add(armModelsTT[0]);
            link1.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((StepperMotor)Actuators[2]).VisualRotation));            
            tg.Children.Add(armModelsTT[1]);
            link2.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((StepperMotor)Actuators[3]).VisualRotation));
            tg.Children.Add(armModelsTT[2]);
            link3.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((StepperMotor)Actuators[4]).VisualRotation));
            tg.Children.Add(armModelsTT[2]);
            link4.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((StepperMotor)Actuators[5]).VisualRotation));
            tg.Children.Add(armModelsTT[3]);
            link5.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(((ConstantForceStepperRack)Actuators[6]).VisualScale);
            tg.Children.Add(armModelsTT[4]);
            link6.Transform = tg;

            // Make the link models now. Add motors represenations if needed                                                        
            gm.Children.Add(link0);
        }

        public override bool TargetAchievable(RobotSolution solution)
        {                           
            var a = ((SphericalRobotSolution)solution).MotorAngles;
            for (int i = 0; i < 6; i++)
                if (!Actuators[i].TargetAchievable(a[i]))
                    return false;
            return true;

        }
        public override void ShowSolution(RobotSolution solution)
        {
            var a = ((SphericalRobotSolution)solution).MotorAngles;
            for (int i = 0; i < 6; i++)
                Actuators[i].Target = a[i];
        }
        EulerAngleOrientation currentee = new EulerAngleOrientation();
        public EulerAngleOrientation CurrentEE { get { return currentee; } }
        public EulerAngleOrientation ComputeCurrentEE()
        {
            var ee = new ChainTranformationMatrix();
            ee.Children.Add(new RotateTransformationMatrix(Actuators[0].CurrentPosition, Axis.Z));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L0));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[1].CurrentPosition, Axis.X));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L1));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[2].CurrentPosition, Axis.X));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[3].CurrentPosition, Axis.Z));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[4].CurrentPosition, Axis.X));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[5].CurrentPosition, Axis.Z));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L3 + L4));
            currentee = EulerAngleOrientation.FromTransformationMatrix(ee);
            return currentee;
        }
    }
    public class RobotSolution
    {                       
    }
    public class SphericalRobotSolution : RobotSolution
    {                                         
        public double[] MotorAngles { get; protected set; } = new double[6];

        public static SphericalRobotSolution Solution1(SphericalRobot robot, EulerAngleOrientation target)
        {
            //target = new EulerAngleOrientation(target.Offset, target.A + Math.PI, -target.B + Math.PI, -target.G + Math.PI / 2);
            var RT = new ChainTranformationMatrix();
            RT.Children.Add(target);
            RT.Children.Add(new TranslateTransformationMatrix(0, 0, (robot.L3 + robot.L4)));

            var P3 = RT * new Point3D();  

            var X3b = Math.Sqrt(P3.X * P3.X + P3.Y * P3.Y);
            var Y3b = P3.Z;
            var a1 = robot.L1;
            var a2 = -robot.L2;
            var a3 = X3b;
            var b3 = Y3b - robot.L0;
            var Th4b = 2 * Math.Atan((2 * a2 * b3 + Math.Sqrt((-(a1 * a1) + 2 * a1 * a2 - a2 * a2 + a3 * a3 + b3 * b3) * (a1 * a1 + 2 * a1 * a2 + a2 * a2 - a3 * a3 - b3 * b3))) / (-a1 * a1 + a2 * a2 + 2 * a2 * a3 + a3 * a3 + b3 * b3));

            var phi = Math.Atan2(Y3b - robot.L0, X3b);
            var c = Math.Sqrt(X3b * X3b + Math.Pow(Y3b - robot.L0, 2));
            var alpha1 = Math.Acos((robot.L1 * robot.L1 + c * c - robot.L2 * robot.L2) / (2 * robot.L1 * c));
            var alpha2 = -alpha1;
            var Th1 = Math.Atan2(P3.Y, P3.X) + Math.PI / 2;
            var Th2 = Math.PI / 2 - phi + alpha1;


            ChainTranformationMatrix p2Trans = new ChainTranformationMatrix();
            p2Trans.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L0));
            p2Trans.Children.Add(new RotateTransformationMatrix(Th1, Axis.Z));
            p2Trans.Children.Add(new RotateTransformationMatrix(Th2, Axis.X));
            p2Trans.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L1));

            var P2 = p2Trans * new Point3D();
            //robot.Debug(P2, 2);
            var pd24 = new Point3D(
                P2.X - target.X,
                P2.Y - target.Y,
                P2.Z - target.Z);

            var y23b = P2.Z - P3.Z;
            var x23b = X3b - robot.L1 * Math.Sin(Th2);
            var phi2 = Math.Atan2(y23b, x23b);
            var Th3 = Math.PI / 2 + phi2 - Th2;

            var d24 = Math.Sqrt(pd24.X * pd24.X + pd24.Y * pd24.Y + pd24.Z * pd24.Z);
            var k = Math.Round((robot.L2 * robot.L2 + (robot.L3 + robot.L4) * (robot.L3 + robot.L4) - d24 * d24) / (2 * robot.L2 * (robot.L3 + robot.L4)), 8);
            var Th5 = Math.PI - Math.Acos(k);

            var H300T = new ChainTranformationMatrix();
            H300T.Children.Add(new RotateTransformationMatrix(0, Axis.Z)); // 0 for Th4
            H300T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L2));
            H300T.Children.Add(new RotateTransformationMatrix(-Th3, Axis.X));
            H300T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L1));
            H300T.Children.Add(new RotateTransformationMatrix(-Th2, Axis.X));
            H300T.Children.Add(new RotateTransformationMatrix(-Th1, Axis.Z));
            H300T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L0));
            H300T.Children.Add(target);
            var P300T = H300T * new Point3D();

            var Th4 = Math.PI / 2 + Math.Atan2(P300T.Y, P300T.X);

            var H400T = new ChainTranformationMatrix();
            H400T.Children.Add(new RotateTransformationMatrix(0, Axis.Z)); // 0 for Th6 (No effect)
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -(robot.L3 + robot.L4)));
            H400T.Children.Add(new RotateTransformationMatrix(-Th5, Axis.X));
            H400T.Children.Add(new RotateTransformationMatrix(-Th4, Axis.Z));
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L2));
            H400T.Children.Add(new RotateTransformationMatrix(-Th3, Axis.X));
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L1));
            H400T.Children.Add(new RotateTransformationMatrix(-Th2, Axis.X));
            H400T.Children.Add(new RotateTransformationMatrix(-Th1, Axis.Z));
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L0));
            H400T.Children.Add(target);
            var P400T = H400T * new Point3D(1, 0, 0);
            var Th6 = Math.PI / 2 + Math.Atan2(P400T.Y, P400T.X);

            SphericalRobotSolution s1 = new SphericalRobotSolution();
            s1.MotorAngles[0] = Th1;
            s1.MotorAngles[1] = Th2;
            s1.MotorAngles[2] = Th3;
            s1.MotorAngles[3] = Th4;
            s1.MotorAngles[4] = Th5;
            s1.MotorAngles[5] = Th6;
            for (int i = 0; i < 6; i++)
            {
                if (s1.MotorAngles[i] < Math.PI)
                    s1.MotorAngles[i] += Math.PI * 2;
                if (s1.MotorAngles[i] > Math.PI)
                    s1.MotorAngles[i] -= Math.PI * 2;
            }
            return s1;
        }
        public static SphericalRobotSolution Solution2(SphericalRobot robot, EulerAngleOrientation target)
        {
            //SphericalRobotSolution solution = new SphericalRobotSolution();
            //Point3D P3 = ((TransformationMatrix)target) * (new Point3D());
            //solution.MotorAngles[0] = Math.Atan2(P3.Y, P3.X);
            //var c = Math.Sqrt(P3.X * P3.X + P3.Y * P3.Y + Math.Pow(P3.Z - robot.L0, 2));
            //solution.MotorAngles[2] = Math.Acos((robot.L1 * robot.L1 + robot.L2 * robot.L2 - c * c) / (2 * robot.L1 * robot.L2));

            //return solution;
            //target = new EulerAngleOrientation(target.Offset, target.A + Math.PI, -target.B + Math.PI, -target.G + Math.PI / 2);
            var RT = new ChainTranformationMatrix();
            RT.Children.Add(target);
            RT.Children.Add(new TranslateTransformationMatrix(0, 0, (robot.L3 + robot.L4)));
            var P3 = RT * new Point3D();

            var X3b = Math.Sqrt(P3.X * P3.X + P3.Y * P3.Y);
            var Y3b = P3.Z;
            var a1 = robot.L1;
            var a2 = -robot.L2;
            var a3 = X3b;
            var b3 = Y3b - robot.L0;
            var Th4b = 2 * Math.Atan((2 * a2 * b3 + Math.Sqrt((-(a1 * a1) + 2 * a1 * a2 - a2 * a2 + a3 * a3 + b3 * b3) * (a1 * a1 + 2 * a1 * a2 + a2 * a2 - a3 * a3 - b3 * b3))) / (-a1 * a1 + a2 * a2 + 2 * a2 * a3 + a3 * a3 + b3 * b3));

            var phi = Math.Atan2(Y3b - robot.L0, X3b);
            var c = Math.Sqrt(X3b * X3b + Math.Pow(Y3b - robot.L0, 2));
            if (c > robot.L1 + robot.L2)
            { }
            var alpha1 = Math.Acos((robot.L1 * robot.L1 + c * c - robot.L2 * robot.L2) / (2 * robot.L1 * c));
            var alpha2 = -alpha1;
            var Th1 = Math.Atan2(P3.Y, P3.X) + Math.PI / 2;
            var Th2 = Math.PI / 2 + (-phi - alpha1);


            ChainTranformationMatrix p2Trans = new ChainTranformationMatrix();
            p2Trans.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L0));
            p2Trans.Children.Add(new RotateTransformationMatrix(Th1, Axis.Z));
            p2Trans.Children.Add(new RotateTransformationMatrix(Th2, Axis.X));
            p2Trans.Children.Add(new TranslateTransformationMatrix(0, 0, robot.L1));

            var P2 = p2Trans * new Point3D();
            var pd24 = new Point3D(
                P2.X - target.X,
                P2.Y - target.Y,
                P2.Z - target.Z);

            var y23b = P2.Z - P3.Z;
            var x23b = X3b - robot.L1 * Math.Sin(Th2);
            var phi2 = Math.Atan2(y23b, x23b);
            var Th3 = Math.PI / 2 + phi2 - Th2;

            var d24 = Math.Sqrt(pd24.X * pd24.X + pd24.Y * pd24.Y + pd24.Z * pd24.Z);
            var k = Math.Round((robot.L2 * robot.L2 + (robot.L3 + robot.L4) * (robot.L3 + robot.L4) - d24 * d24) / (2 * robot.L2 * (robot.L3 + robot.L4)), 8);
            var Th5 = Math.PI - Math.Acos(k);

            var H300T = new ChainTranformationMatrix();
            H300T.Children.Add(new RotateTransformationMatrix(0, Axis.Z)); // 0 for Th4
            H300T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L2));
            H300T.Children.Add(new RotateTransformationMatrix(-Th3, Axis.X));
            H300T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L1));
            H300T.Children.Add(new RotateTransformationMatrix(-Th2, Axis.X));
            H300T.Children.Add(new RotateTransformationMatrix(-Th1, Axis.Z));
            H300T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L0));
            H300T.Children.Add(target);
            var P300T = H300T * new Point3D();

            var Th4 = Math.PI / 2 + Math.Atan2(P300T.Y, P300T.X);

            var H400T = new ChainTranformationMatrix();
            H400T.Children.Add(new RotateTransformationMatrix(0, Axis.Z)); // 0 for Th6 (No effect)
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -(robot.L3 + robot.L4)));
            H400T.Children.Add(new RotateTransformationMatrix(-Th5, Axis.X));
            H400T.Children.Add(new RotateTransformationMatrix(-Th4, Axis.Z));
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L2));
            H400T.Children.Add(new RotateTransformationMatrix(-Th3, Axis.X));
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L1));
            H400T.Children.Add(new RotateTransformationMatrix(-Th2, Axis.X));
            H400T.Children.Add(new RotateTransformationMatrix(-Th1, Axis.Z));
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -robot.L0));
            H400T.Children.Add(target);
            var P400T = H400T * new Point3D(1, 0, 0);
            var Th6 = Math.PI / 2 + Math.Atan2(P400T.Y, P400T.X);

            SphericalRobotSolution s1 = new SphericalRobotSolution();
            s1.MotorAngles[0] = Th1;
            s1.MotorAngles[1] = Th2;
            s1.MotorAngles[2] = Th3;
            s1.MotorAngles[3] = Th4;
            s1.MotorAngles[4] = Th5;
            s1.MotorAngles[5] = Th6;
            for (int i = 0; i < 5; i++)
            {
                if (s1.MotorAngles[i] < Math.PI)
                    s1.MotorAngles[i] += Math.PI * 2;
                if (s1.MotorAngles[i] > Math.PI)
                    s1.MotorAngles[i] -= Math.PI * 2;
            }
            return s1;
        }
        public static List<RobotSolution> Solve(Robot robot_, EulerAngleOrientation target)
        {
            List<RobotSolution> solutions = new List<RoboSim.RobotSolution>();
            solutions.Add(Solution1((SphericalRobot)robot_, target));
            solutions.Add(Solution2((SphericalRobot)robot_, target));
            // we need to check the solutions for range check yet. Also, add step-by-step checks for singularity and target being out of workspace
            return solutions;   
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                var v = MotorAngles[i];
                var d = v * 180 / Math.PI;
                var str = Math.Round(v / Math.PI, 3) + "𝜋 (" + Math.Round(d, 2) + "°)";
                sb.Append(str);
            }
            return sb.ToString().Trim(new char[] { ',' });
        }
    }

}
