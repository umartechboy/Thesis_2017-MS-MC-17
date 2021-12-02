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
using System.Threading;

namespace RoboSim
{
    public delegate void DebugPoint(Point3D p, int ind);
    public class Robot
    {
        protected Thread graphicsUpdateThread;
        public void Terminate()
        { graphicsUpdateThread?.Abort(); }
        public RobotControlSource ControlSource { get; set; } = RobotControlSource.SolutionTester;
        public EulerAngleOrientation CurrentTarget { get; set; } = null;
        public event EventHandler AfterThreadStep, BeforeThreadStep;
        public double SimLifeinMillis { get; private set; }
        double gA = -9.817;
        public Robot lastState;
        public bool RealTimeTickEnabled { get; set; } = false;
        public virtual void Initialize()
        { }
        public IServoMotor[] Actuators;
        public RobotSolution LastAppliedSolution = null;
        public void ThreadStep(double dt)
        {
            BeforeThreadStep?.Invoke(this, new EventArgs());
            // update target if in automatic path mode
            if (ControlSource == RobotControlSource.BezierSpline && Program!= null)
            {
                // determine if we even need to do this 
                if (Program.Index < Program.Codes.Count)
                {
                    if (Program.UnderWorking is GCode)
                    {
                        if (Program.UnderWorking is G90)
                        {
                            // determine if we are at a switching point
                            bool targetAchieved = false;
                            // positional error based check
                            if ((CurrentEndEffectorPosition - ((G90)Program.UnderWorking).Target.Offset).Length < 0.0005)
                                targetAchieved = true;

                            // Actuator target based check
                            targetAchieved = TargetAchieved();

                            if (targetAchieved)
                            {
                                Program.Index++;
                            }
                            if (Program.UnderWorking is G90) // 
                            // re assert the target
                            {
                                if (Program.UnderWorking != null) // end of program
                                {
                                    if (Program.UnderWorking is GCode)
                                    {
                                        if (Program.UnderWorking is G90)
                                        {
                                            if (Program.LastSolvedTarget != Program.UnderWorking)
                                            {
                                                //CurrentTarget = ((G90)Program.UnderWorking).Target;

                                                Program.LastSolvedTarget = (GCode)Program.UnderWorking;
                                                try
                                                {
                                                    var solution = SphericalRobotSolution.Solution(LastAppliedSolution, 0, this, ((G90)Program.UnderWorking).Target);
                                                    var t = ((SphericalRobotSolution)solution).MotorAngles[5];
                                                    solution.ApplyAsTarget(this);
                                                }
                                                catch { }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            throw new NotImplementedException();
                    }
                    else if (Program.UnderWorking is ToolChangeCommand)
                    {
                        var tcc = (ToolChangeCommand)Program.UnderWorking;
                        if (this is SphericalRobot)
                            ((SphericalRobot)this).ToolWidth = tcc.ToolSize;

                        Program.Index++;
                    }
                    else
                        throw new NotImplementedException();
                }
            }
            // Set last State = current here and then make the step
            for (int i = 0; i < Actuators.Length; i++)
                Actuators[i].ThreadStep(dt);
            AfterThreadStep?.Invoke(this, new EventArgs());
            SimLifeinMillis += dt * 1000;
        }

        public virtual bool TargetAchieved()
        {
            throw new NotImplementedException();
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
        public virtual Point3D CurrentEndEffectorPosition { get; }
        public virtual ChainTranformationMatrix CurrentEndEffectorTransformationMatrix { get; }
        public MachineProgram Program { get; set; }
    }
    public class SphericalRobot : Robot
    {
        public event DebugPoint OnDebugPoint;
        public event doubleArrayChangedHandler OnLengthChanged;
        public event doubleArrayChangedHandler OnPositionChanged;
        public event doubleArrayChangedHandler OnTargetChanged;
        public override Point3D CurrentEndEffectorPosition
        {
            get
            {
                var H400T = new ChainTranformationMatrix();
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L0));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[0].Current, Axis.Z));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[1].Current, Axis.X));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L1));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[2].Current, Axis.X));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[3].Current, Axis.Z));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[4].Current, Axis.X));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L4));
                return H400T * new Point3D(0, 0, 0);
            }
        }
        public override ChainTranformationMatrix CurrentEndEffectorTransformationMatrix 
        {
            get
            {
                var H400T = new ChainTranformationMatrix();
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L0));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[0].Current, Axis.Z));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[1].Current, Axis.X));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L1));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[2].Current, Axis.X));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[3].Current, Axis.Z));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[4].Current, Axis.X));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                H400T.Children.Add(new TranslateTransformationMatrix(0, 0, L4));
                H400T.Children.Add(new RotateTransformationMatrix(Actuators[5].Current, Axis.Z));
                return H400T;
            }
        }
        public override bool TargetAchieved()
        {
            foreach (var motor in Actuators)
            {
                if (!motor.TargetAchieved())
                    return false;
            }
            return true;
        }
        public void Debug(Point3D p, int ind)
        {
            OnDebugPoint?.Invoke(p, ind);
        }
        public double L0 { get { return armModelsTT[0].OffsetZ; } set { armModelsTT[0].OffsetZ = value; resetModel(); OnLengthChanged?.Invoke(0, value); } }
        public double L1 { get { return armModelsTT[1].OffsetZ; } set { armModelsTT[1].OffsetZ = value; resetModel(); OnLengthChanged?.Invoke(1, value); } }
        public double L2 { get { return armModelsTT[2].OffsetZ * 2; } set { armModelsTT[2].OffsetZ = value / 2; armModelsTT[3].OffsetZ = value / 2; resetModel(); OnLengthChanged?.Invoke(2, value); } }
        public double L3 { get { return armModelsTT[4].OffsetZ; } set { armModelsTT[4].OffsetZ = value; resetModel(); OnLengthChanged?.Invoke(3, value); } }
        // the pen
        public double L4 { get { return armModelsTT[5].OffsetZ; } set { armModelsTT[5].OffsetZ = value; resetModel(); OnLengthChanged?.Invoke(3, value); } }
        double _tw = 0.005;
        public double ToolWidth { get { return _tw; } set { _tw = value; resetModel(); OnLengthChanged?.Invoke(3, value); } }

        public SphericalRobot()
        {
            double l0 = 0.4, l1 = 1.2, l2 = 1, l3 = 0.1, l4 = 0.4;

            Actuators = new IServoMotor[6];
            Actuators[0] = new StepperMotor(0, 1000 * 50, 0.0002, -Math.PI, 2 * Math.PI, -Math.PI/4, Axis.Z);
            Actuators[1] = new StepperMotor(1, 1000 * 50, 0.0002, -Math.PI / 2, Math.PI, 0, Axis.X);
            Actuators[2] = new StepperMotor(2, 1000 * 50, 0.0003, -2 * Math.PI / 3, 4 * Math.PI / 3, 0, Axis.X);
            Actuators[3] = new StepperMotor(3, 800 * 50, 0.0005, -Math.PI, 2 * Math.PI, 0, Axis.Z);
            Actuators[4] = new StepperMotor(4, 500 * 50, 0.0005, -2 * Math.PI / 3, 4 * Math.PI / 3, 0, Axis.X);
            Actuators[5] = new StepperMotor(5, 2000, 0.0005, -2 * Math.PI, 4 * Math.PI, 0, Axis.Z);
            //Actuators[0] = new ServoMotor(0, this, -Math.PI, 2 * Math.PI, 0, 4000, 0, 3200, 70, 70, Axis.Z);
            //Actuators[1] = new ServoMotor(1, this, -Math.PI, 2 * Math.PI, 0, 2000, 0, 3500, 150, 170, Axis.X);
            //Actuators[2] = new ServoMotor(2, this, -Math.PI, 2 * Math.PI, -Math.PI / 4, 2000, 0, 2000, 100, 110, Axis.X);
            ////Actuators[3] = new ServoMotor(3, this, -Math.PI, 2 * Math.PI, -Math.PI / 4, 500, 0, 500, 80, 8, Axis.Z);
            //Actuators[4] = new StepperMotor(4, 500 * 50, 0.0005, -2 * Math.PI / 3, 4 * Math.PI / 3, 0, Axis.X);
            //Actuators[5] = new StepperMotor(5, 500 * 50, 0.0005, -2 * Math.PI, 4 * Math.PI, 0, Axis.Z);

            // Actuators[6] = new ConstantForceStepperRack(0, 100, 0, l4, 100, 6);
            foreach (var m in Actuators)
            {
                m.OnMovement += M_OnPositionChanged;
                //m.OnTargetChanged += M_OnTargetChanged;   
            }

            armModelsTT = new TranslateTransform3D[]
            {
                new TranslateTransform3D(0, 0, l0),
                new TranslateTransform3D(0, 0, l1),
                new TranslateTransform3D(0, 0, l2 / 2),
                new TranslateTransform3D(0, 0, l2 / 2),
                new TranslateTransform3D(0, 0, l3 ),
                new TranslateTransform3D(0, 0, l4)
            };
            resetModel();
        }


        private void M_OnTargetChanged(object sender, double value)
        {
            OnTargetChanged?.Invoke(((IServoMotor)sender).ID, value);
        }
        private void M_OnPositionChanged(object sender, double value)
        {
            OnPositionChanged?.Invoke(((IServoMotor)sender).ID, value);
        }
        void resetModel()
        {
            for (int i = 0; i < 6; i++)
            {
                if (armModels[i] == null)
                    armModels[i] = new Model3DGroup();
                armModels[i].Children.Clear();
            }
            _LinkSizes = new Size3D[] {
                new Size3D(0.4, 0.28, L0),
                new Size3D(0.4, 0.4, L1),
                new Size3D(0.3, 0.28, L2),
                new Size3D(0.24, 0.23, L3),
                new Size3D(ToolWidth, ToolWidth * 0.1, L4),
            };
            _LinkPoints = new HollowBlock[4];
            double pointSize = 0.02;
            for (int i = 0; i < 4; i++)
            {
                var sz = _LinkSizes[i];
                int nX = (int)Math.Round(sz.X / pointSize);
                int nY = (int)Math.Round(sz.Y / pointSize);
                int nZ = (int)Math.Round(sz.Z / pointSize);
                if (nX <= 0) nX = 1;
                if (nY <= 0) nY = 1;
                if (nZ <= 0) nZ = 1;
                _LinkPoints[i] = new HollowBlock(nZ);
                var st = _sheetThicknesses[i];
                double V = (sz.X * sz.Y * sz.Z) - (st * sz.X * 2 + st * sz.Y * 2 + st * st * 4) * sz.Z;
                double density = 2700;
                double M = V * density;
                for (int iz = 0; iz < nZ; iz++)
                {
                    _LinkPoints[i].Layers[iz] = new LayerOfHollowSquarePipe(nX, nY);
                    for (int ix = 0; ix < nX; ix++)
                    {
                        double sX = sz.X / nX;
                        double sY = st;
                        double sZ = sz.Z / nZ;

                        double x = -sz.X / 2 + sX * ix + sX / 2;
                        double y = -sz.Y / 2 + st / 2;
                        double z = sz.Z / nZ * iz + sz.Z / nZ / 2;

                        _LinkPoints[i].Layers[iz].X0[ix] = new MatterPoint()
                        {
                            Location = new Point3D(x, y, z),
                            Size = new Size3D(sX, sY, sZ),
                            m = sX * sY * sZ / V * M
                        };
                        y = sz.Y / 2 - st / 2;
                        _LinkPoints[i].Layers[iz].X1[ix] = new MatterPoint()
                        {
                            Location = new Point3D(x, y, z),
                            Size = new Size3D(sX, sY, sZ),
                            m = sX * sY * sZ / V * M
                        };
                    }
                    for (int iy = 0; iy < nY; iy++)
                    {
                        double sX = st;
                        double sY = (sz.Y - st * 2) / nY;
                        double sZ = sz.Z / nZ;

                        double x = -sz.Y / 2 + st / 2;
                        double y = -(sz.Y - st * 2) / 2 + sY * iy + sY / 2 + st;
                        double z = sz.Z / nZ * iz + sz.Z / nZ / 2;

                        _LinkPoints[i].Layers[iz].Y0[iy] = new MatterPoint()
                        {
                            Location = new Point3D(x, y, z),
                            Size = new Size3D(sX, sY, sZ),
                            m = sX * sY * sZ / V * M
                        };
                        x = sz.Y / 2 - st / 2;
                        _LinkPoints[i].Layers[iz].Y1[iy] = new MatterPoint()
                        {
                            Location = new Point3D(x, y, z),
                            Size = new Size3D(sX, sY, sZ),
                            m = sX * sY * sZ / V * M
                        };
                    }
                }
            }
            armModels[0].Children.Add(simCalc.RectangulToUJoint(_LinkSizes[0].Z, _LinkSizes[0].X, _LinkSizes[0].Y, 0.5, 0, System.Windows.Media.Colors.Red));
            armModels[1].Children.Add(simCalc.UjointToUJoint(_LinkSizes[1].Z, _LinkSizes[1].X, _LinkSizes[1].Y, 0.3, 0, 0.5, 0.5, 0, System.Windows.Media.Colors.Green));
            armModels[2].Children.Add(simCalc.RectangulToUJoint(_LinkSizes[2].Z / 2, _LinkSizes[2].X, _LinkSizes[2].Y, 0, 0.5, System.Windows.Media.Colors.Red, true));
            armModels[3].Children.Add(simCalc.RectangulToUJoint(_LinkSizes[2].Z / 2, _LinkSizes[2].X, _LinkSizes[2].Y, 0.5, 0, System.Windows.Media.Colors.Red, false));
            armModels[4].Children.Add(simCalc.RectangulToUJoint(_LinkSizes[3].Z, _LinkSizes[3].X, _LinkSizes[3].Y, 0, 0.5, System.Windows.Media.Colors.Green, true));
            //armModels[5].Children.Add(simCalc.meshToGeometry(simCalc.CylenderMesh(_LinkSizes[3].Z / 2, 0.1, false), System.Windows.Media.Colors.White));
            armModels[5].Children.Add(simCalc.CircleToRectangle(_LinkSizes[3].X / 2, _LinkSizes[4].X, _LinkSizes[4].Y, _LinkSizes[4].Z, System.Windows.Media.Colors.White));

        }
        private TranslateTransform3D[] armModelsTT;
        Model3DGroup[] armModels = new Model3DGroup[6];
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

            //link6.Children.Add(armModels[6]);

            // add tranformations
            link0.Transform = new RotateTransform3D(((IServoMotor)Actuators[0]).VisualRotation);
            var tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((IServoMotor)Actuators[1]).VisualRotation));
            tg.Children.Add(armModelsTT[0]);
            link1.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((IServoMotor)Actuators[2]).VisualRotation));
            tg.Children.Add(armModelsTT[1]);
            link2.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((IServoMotor)Actuators[3]).VisualRotation));
            tg.Children.Add(armModelsTT[2]);
            link3.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((IServoMotor)Actuators[4]).VisualRotation));
            tg.Children.Add(armModelsTT[3]);
            link4.Transform = tg;

            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(((IServoMotor)Actuators[5]).VisualRotation));
            tg.Children.Add(armModelsTT[4]);
            link5.Transform = tg;

            //tg = new Transform3DGroup();
            //tg.Children.Add(((ConstantForceStepperRack)Actuators[6]).VisualScale);
            //tg.Children.Add(armModelsTT[4]);
            //link6.Transform = tg;

            // Make the link models now. Add motors represenations if needed                                                        
            gm.Children.Add(link0);
            graphicsUpdateThread = new Thread(() => {
                while (true)
                {
                    Thread.Sleep(30);

                }
            });
            graphicsUpdateThread.Start();
        }

        public override bool TargetAchievable(RobotSolution solution)
        {
            var a = ((SphericalRobotSolution)solution).MotorAngles;
            //for (int i = 0; i < 6; i++)
            //    if (!Actuators[i].TargetAchievable(a[i]))
            //        return false;
            return true;

        }
        public override void ShowSolution(RobotSolution solution)
        {
            var a = ((SphericalRobotSolution)solution).MotorAngles;
            for (int i = 0; i < 6; i++)
                Actuators[i].Reference = a[i];
        }
        EulerAngleOrientation currentee = new EulerAngleOrientation();
        public EulerAngleOrientation CurrentEE { get { return currentee; } }
        public EulerAngleOrientation ComputeCurrentEE()
        {
            var ee = new ChainTranformationMatrix();
            ee.Children.Add(new RotateTransformationMatrix(Actuators[0].Current, Axis.Z));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L0));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[1].Current, Axis.X));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L1));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[2].Current, Axis.X));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[3].Current, Axis.Z));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[4].Current, Axis.X));
            ee.Children.Add(new RotateTransformationMatrix(Actuators[5].Current, Axis.Z));
            ee.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
            currentee = EulerAngleOrientation.FromTransformationMatrix(ee);
            return currentee;
        }
        Size3D[] _LinkSizes = null;
        double[] _sheetThicknesses = new double[] { 0.005, 0.004, 0.003, 0.002 };
        public class MatterPoint
        {
            public Point3D Location { get; set; }
            public Size3D Size { get; set; }
            public double m { get; set; }
            //https://onlinemschool.com/math/library/analytic_geometry/p_line/
            internal double ComputeMomentOfInertia(Vector3D M1, Vector3D M2)
            {
                var M0 = new Vector3D(Location.X, Location.Y, Location.Z);
                var S = M2 - M1;
                var M0M1 = M1 - M0;
                var r = Vector3D.CrossProduct(M0M1, S).Length / S.Length;
                return m * r * r;
            }
        }
        public class LayerOfHollowSquarePipe
        {
            public LayerOfHollowSquarePipe()
            { }
            public LayerOfHollowSquarePipe(int nX, int nY)
            {
                X0 = new MatterPoint[nX]; X1 = new MatterPoint[nX];
                Y0 = new MatterPoint[nY]; Y1 = new MatterPoint[nY];
            }
            public MatterPoint[] X0{ get; set; }
            public MatterPoint[] X1 { get; set; }
            public MatterPoint[] Y0 { get; set; }
            public MatterPoint[] Y1 { get; set; }
        }
        public class HollowBlock
        {
            HollowBlock()
            {
            }
            public HollowBlock(int nZ)
            {
                Layers = new LayerOfHollowSquarePipe[nZ];
            }
            public LayerOfHollowSquarePipe[] Layers { get; private set; }

            public HollowBlock Transform(ChainTranformationMatrix h)
            {
                return new HollowBlock()
                {
                    Layers =
                Layers.Select(l => new LayerOfHollowSquarePipe()
                {
                    X0 = l.X0.Select(lx0 => new MatterPoint() { Location = h * lx0.Location, Size = lx0.Size, m = lx0.m }).ToArray(),
                    X1 = l.X1.Select(lx1 => new MatterPoint() { Location = h * lx1.Location, Size = lx1.Size, m = lx1.m }).ToArray(),
                    Y0 = l.X0.Select(ly0 => new MatterPoint() { Location = h * ly0.Location, Size = ly0.Size, m = ly0.m }).ToArray(),
                    Y1 = l.X1.Select(ly1 => new MatterPoint() { Location = h * ly1.Location, Size = ly1.Size, m = ly1.m }).ToArray()
                }
                ).ToArray()
                };
            }
            internal double ComputeMomentOfInertia(Point3D l0_t, Point3D l0_h)
            {
                return ComputeMomentOfInertia((Vector3D)l0_t, (Vector3D)l0_h);
            }
            internal double ComputeMomentOfInertia(Vector3D l0_t, Vector3D l0_h)
            {
                return Layers.Sum(l =>
                    l.X0.Sum(p => p.ComputeMomentOfInertia(l0_t, l0_h)) +
                    l.X1.Sum(p => p.ComputeMomentOfInertia(l0_t, l0_h)) +
                    l.Y0.Sum(p => p.ComputeMomentOfInertia(l0_t, l0_h)) +
                    l.Y1.Sum(p => p.ComputeMomentOfInertia(l0_t, l0_h)));
            }

            Point3D MidOf(Point3D p1, Point3D p2)
            {
                return new Point3D((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2, (p1.Z + p2.Z) / 2);
            }
            public double ComputeMass()
            {
                return Layers.Sum(l =>
                    l.X0.Sum(p => p.m) +
                    l.X1.Sum(p => p.m) +
                    l.Y0.Sum(p => p.m) +
                    l.Y1.Sum(p => p.m));
            }
            internal Point3D ComputeCenterOfMass()
            {
                return
                    MidOf(
                        MidOf(MidOf(Layers[0].X0.First().Location, Layers[0].X0.Last().Location), MidOf(Layers[0].X1.First().Location, Layers[0].X1.Last().Location)),
                        MidOf(MidOf(Layers.Last().X0.First().Location, Layers.Last().X0.Last().Location), MidOf(Layers.Last().X1.First().Location, Layers.Last().X1.Last().Location)));
            }
        }
        HollowBlock[] _LinkPoints = null;

        internal double GetInertialLoadOnMotor(int id)
        {
            // we are not adding up the motor torques in it yet.
            var LP = _LinkPoints;
            var sz = _LinkSizes;

            if (id == 0) // base motor.
            {
                var uz_t = new Point3D(0, 0, 0);
                var uz_h = new Point3D(0, 0, 1);
                double I = 0;
                var H = new ChainTranformationMatrix();
                // link 0 in a vector pointing upwards, 
                var M0_t = H * uz_t;
                var M0_h = H * uz_h;
                I += LP[0].Transform(H).ComputeMomentOfInertia(M0_t, M0_h);
                var masses = LP.Select(lp => lp.ComputeMass()).ToArray();
                // transform the vector on to Link 1
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                var L1_t = H * uz_t;
                var L1_h = H * uz_h;
                I += LP[1].Transform(H).ComputeMomentOfInertia(M0_t, M0_h);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                var L2_t = H * uz_t;
                var L2_h = H * uz_h;
                I += LP[2].Transform(H).ComputeMomentOfInertia(M0_t, M0_h);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                var L3_t = H * uz_t;
                var L3_h = H * uz_h;
                I += LP[3].Transform(H).ComputeMomentOfInertia(M0_t, M0_h);
                //H.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                // transform the vector on to Link 3

                // Use the parallel axis theorem to compute the inertia of the links on this motor.
                return I;
            }
            else if (id == 1) // main lifter motor
            {
                double I = 0;
                var ux_t = new Point3D(0, 0, 0);
                var ux_h = new Point3D(1, 0, 0);
                var H = new ChainTranformationMatrix();
                // transform the vector on to Link 1
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                var M1_t = H * ux_t;
                var M1_h = H * ux_h;
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                I += LP[1].Transform(H).ComputeMomentOfInertia(M1_t, M1_h);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                I += LP[2].Transform(H).ComputeMomentOfInertia(M1_t, M1_h);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                I += LP[3].Transform(H).ComputeMomentOfInertia(M1_t, M1_h);
                //H.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                // transform the vector on to Link 3

                // Use the parallel axis theorem to compute the inertia of the links on this motor.
                return I;
            }
            else if (id == 2)
            {
                double I = 0;
                var H = new ChainTranformationMatrix();
                var ux_t = new Point3D(0, 0, 0);
                var ux_h = new Point3D(1, 0, 0);
                // transform the vector on to Link 1
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                var M2_t = H * ux_t;
                var M2_h = H * ux_h;
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                I += LP[2].Transform(H).ComputeMomentOfInertia(M2_t, M2_h);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                I += LP[3].Transform(H).ComputeMomentOfInertia(M2_t, M2_h);
                //H.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                // transform the vector on to Link 3

                // Use the parallel axis theorem to compute the inertia of the links on this motor.
                return I;
            }
            else if (id == 3)
            {
                double I = 0;
                var H = new ChainTranformationMatrix();
                var ux_t = new Point3D(0, 0, 0);
                var ux_h = new Point3D(0, 0, 1);
                // transform the vector on to Link 1
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                var M3_t = H * ux_t;
                var ML3_h = H * ux_h;
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                I += LP[2].Transform(H).ComputeMomentOfInertia(M3_t, ML3_h);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                I += LP[3].Transform(H).ComputeMomentOfInertia(M3_t, ML3_h);
                //H.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                // transform the vector on to Link 3

                // Use the parallel axis theorem to compute the inertia of the links on this motor.
                return I;
            }
            else if (id == 4)
            {
                double I = 0;
                var H = new ChainTranformationMatrix();
                var ux_t = new Point3D(0, 0, 0);
                var ux_h = new Point3D(1, 0, 0);
                // transform the vector on to Link 1
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                var M3_t = H * ux_t;
                var ML3_h = H * ux_h;
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                I += LP[3].Transform(H).ComputeMomentOfInertia(M3_t, ML3_h);
                //H.Children.Add(new TranslateTransformationMatrix(0, 0, L3));
                // transform the vector on to Link 3

                // Use the parallel axis theorem to compute the inertia of the links on this motor.
                return I;
            }

            // 5 is a stepper motor

            throw new NotImplementedException();
        }

        internal object GetGravitationalLoadOnMotor(int id)
        {
            var LP = _LinkPoints;
            var sz = _LinkSizes;
            if (id == 0)
            {
                return 0; // no g load
            }
            else if (id == 1) // Main Lift Motor
            {
                var g = 9.812;
                Vector3D T = new Vector3D();
                var H = new ChainTranformationMatrix();
                // transform the vector on to Link 1
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                var c1_t = H * new Point3D(0, 0, 0); // center of M1
                var c1_h = H * new Point3D(1, 0, 0); // center of M1
                var mcl1 = LP[1].Transform(H).ComputeCenterOfMass();
                var r1 = mcl1 - c1_t;
                var F1 = new Vector3D(0, 0, -LP[1].ComputeMass() * g);
                T += Vector3D.CrossProduct(r1, F1);

                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                var mcl2 = LP[2].Transform(H).ComputeCenterOfMass();
                var r2 = mcl2 - c1_t;
                var F2 = new Vector3D(0, 0, -LP[2].ComputeMass() * g);
                T += Vector3D.CrossProduct(r2, F2);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                var mcl3 = LP[3].Transform(H).ComputeCenterOfMass();
                var r3 = mcl3 - c1_t;
                var F3 = new Vector3D(0, 0, -LP[3].ComputeMass() * g);
                T += Vector3D.CrossProduct(r3, F3);
                // Take the projection of this torque on M1
                // https://www.ck12.org/book/ck-12-college-precalculus/section/9.6/
                var v = T;
                var u = c1_h - c1_t;
                var T1 = Vector3D.DotProduct(v, u) / u.Length * (u / u.Length);

                //I method: For nonzero vectors a and b, if a×b=0, then they are in the same direction.
                //II method: If the absolute value of dot product a.b |=| a |.| b|, then they are in the same direction, and if a.b = -| a |.| b |, then they are in opposite directions.
                if (Vector3D.DotProduct(u, T1) < 0)
                    return -T1.Length;
                else
                    return T1.Length;
            }
            else if (id == 2 || id == 3)
            {
                var g = 9.812;
                Vector3D T = new Vector3D();
                var H = new ChainTranformationMatrix();
                // transform the vector on to Link 2
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                var c2_t = H * new Point3D(0, 0, 0); // center of M2/M3
                var c2_h = H * new Point3D(1, 0, 0); // center of M2/M3
                if (id == 3)
                    c2_h = H * new Point3D(0, 0, 1); // center of M2/3
                var mcl2 = LP[2].Transform(H).ComputeCenterOfMass();
                var r2 = mcl2 - c2_t;
                var F2 = new Vector3D(0, 0, -LP[2].ComputeMass() * g);
                T += Vector3D.CrossProduct(r2, F2);
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                var mcl3 = LP[3].Transform(H).ComputeCenterOfMass();
                var r3 = mcl3 - c2_t;
                var F3 = new Vector3D(0, 0, -LP[3].ComputeMass() * g);
                T += Vector3D.CrossProduct(r3, F3);
                // Take the projection of this torque on M2/M3
                // https://www.ck12.org/book/ck-12-college-precalculus/section/9.6/
                var v = T;
                var u = c2_h - c2_t;
                var T2 = Vector3D.DotProduct(v, u) / u.Length * (u / u.Length);

                //I method: For nonzero vectors a and b, if a×b=0, then they are in the same direction.
                //II method: If the absolute value of dot product a.b |=| a |.| b|, then they are in the same direction, and if a.b = -| a |.| b |, then they are in opposite directions.
                if (Vector3D.DotProduct(u, T2) < 0)
                    return -T2.Length;
                else
                    return T2.Length;
            }
            else if (id == 4)
            {
                var g = 9.812;
                Vector3D T = new Vector3D();
                var H = new ChainTranformationMatrix();
                // transform the vector on to Link 2
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L0)); // Tz(L0)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[0].Current, Axis.Z));  // Rz(th1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[1].Current, Axis.X));  // Rx(th2)
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L1)); // Tz(L1)
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[2].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[3].Current, Axis.Z));
                H.Children.Add(new TranslateTransformationMatrix(0, 0, L2));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[4].Current, Axis.X));
                H.Children.Add(new RotateTransformationMatrix(this.Actuators[5].Current, Axis.Z));
                var c4_t = H * new Point3D(0, 0, 0); // center of M4
                var c4_h = H * new Point3D(1, 0, 0); // center of M4
                var mcl4 = LP[2].Transform(H).ComputeCenterOfMass();
                var r4 = mcl4 - c4_t;
                var F4 = new Vector3D(0, 0, -LP[3].ComputeMass() * g);
                T += Vector3D.CrossProduct(r4, F4);
                // Take the projection of this torque on M2/M3
                // https://www.ck12.org/book/ck-12-college-precalculus/section/9.6/
                var v = T;
                var u = c4_h - c4_t;
                var T2 = Vector3D.DotProduct(v, u) / u.Length * (u / u.Length);

                //I method: For nonzero vectors a and b, if a×b=0, then they are in the same direction.
                //II method: If the absolute value of dot product a.b |=| a |.| b|, then they are in the same direction, and if a.b = -| a |.| b |, then they are in opposite directions.
                if (Vector3D.DotProduct(u, T2) < 0)
                    return -T2.Length;
                else
                    return T2.Length;
            }
            // 5 is a stepper motor
            throw new NotImplementedException();
        }
    }
    public class RobotSolution
    {
        public virtual RobotSolution Clone()
        { throw new NotImplementedException(); }

        public virtual void ApplyAsTarget(Robot robot)
        {
            throw new NotImplementedException();
        }
    }
    public class SphericalRobotSolution : RobotSolution
    {
        public double[] MotorAngles { get; protected set; } = new double[6];
        public override RobotSolution Clone()
        {
            return new SphericalRobotSolution() { MotorAngles = MotorAngles.ToArray() };
        }
        public override void ApplyAsTarget(Robot robot)
        {
            for (int i = 0; i < 6; i++)
                ((SphericalRobot)robot).Actuators[i].Reference = MotorAngles[i];
        }
        public static RobotSolution Solution(RobotSolution previousSolution_, int ind, Robot robot_, EulerAngleOrientation target)
        {
            var robot = (SphericalRobot)robot_;
            var previousSolution = (SphericalRobotSolution)previousSolution_;
            var newSolution = SolutionX(ind, robot, target);
            if (previousSolution != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    while (newSolution.MotorAngles[i] < previousSolution.MotorAngles[i] - Math.PI)
                        newSolution.MotorAngles[i] += Math.PI * 2;
                    while (newSolution.MotorAngles[i] > previousSolution.MotorAngles[i] + Math.PI)
                        newSolution.MotorAngles[i] -= Math.PI * 2;
                }
            }
            return newSolution;
            throw new NotImplementedException();
        }
        public static SphericalRobotSolution SolutionX(int solutionID, SphericalRobot robot, EulerAngleOrientation target)
        {
            if (solutionID == 0) return Solution1(robot, target);
            if (solutionID == 1) return Solution2(robot, target);
            throw new NotImplementedException();
        }

        public static SphericalRobotSolution Solution2(SphericalRobot robot, EulerAngleOrientation target)
        {
            var l3 = robot.L3 + robot.L4;
            //target = new EulerAngleOrientation(target.Offset, target.A + Math.PI, -target.B + Math.PI, -target.G + Math.PI / 2);
            var RT = new ChainTranformationMatrix();
            RT.Children.Add(target);
            RT.Children.Add(new TranslateTransformationMatrix(0, 0, (l3)));

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
            var k = Math.Round((robot.L2 * robot.L2 + l3 * l3 - d24 * d24) / (2 * robot.L2 * l3), 8);
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
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -l3));
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
                while (s1.MotorAngles[i] > robot.Actuators[i].Minimum + robot.Actuators[i].Range)
                    s1.MotorAngles[i] -= Math.PI * 2;
                while (s1.MotorAngles[i] < robot.Actuators[i].Minimum)
                    s1.MotorAngles[i] += Math.PI * 2;
            }
            return s1;
        }
        public static SphericalRobotSolution Solution1(SphericalRobot robot, EulerAngleOrientation target)
        {
            //SphericalRobotSolution solution = new SphericalRobotSolution();
            //Point3D P3 = ((TransformationMatrix)target) * (new Point3D());
            //solution.MotorAngles[0] = Math.Atan2(P3.Y, P3.X);
            //var c = Math.Sqrt(P3.X * P3.X + P3.Y * P3.Y + Math.Pow(P3.Z - robot.L0, 2));
            //solution.MotorAngles[2] = Math.Acos((robot.L1 * robot.L1 + robot.L2 * robot.L2 - c * c) / (2 * robot.L1 * robot.L2));

            //return solution;
            //target = new EulerAngleOrientation(target.Offset, target.A + Math.PI, -target.B + Math.PI, -target.G + Math.PI / 2);
            var l3 = robot.L3 + robot.L4;
            var RT = new ChainTranformationMatrix();
            RT.Children.Add(target);
            RT.Children.Add(new TranslateTransformationMatrix(0, 0, l3));
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
            var k = Math.Round((robot.L2 * robot.L2 + l3 * l3 - d24 * d24) / (2 * robot.L2 * l3), 8);
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
            H400T.Children.Add(new TranslateTransformationMatrix(0, 0, -l3));
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
                while (s1.MotorAngles[i] > robot.Actuators[i].Minimum + robot.Actuators[i].Range)
                    s1.MotorAngles[i] -= Math.PI * 2;
                while (s1.MotorAngles[i] < robot.Actuators[i].Minimum)
                    s1.MotorAngles[i] += Math.PI * 2;
            }
            //for (int i = 0; i < 5; i++)
            //{
            //    if (s1.MotorAngles[i] < Math.PI)
            //        s1.MotorAngles[i] += Math.PI * 2;
            //    if (s1.MotorAngles[i] > Math.PI)
            //        s1.MotorAngles[i] -= Math.PI * 2;
            //}
            return s1;
        }
        public static List<RobotSolution> Solve(Robot robot_, EulerAngleOrientation target)
        {
            List<RobotSolution> solutions = new List<RoboSim.RobotSolution>();
            solutions.Add(Solution2((SphericalRobot)robot_, target));
            solutions.Add(Solution1((SphericalRobot)robot_, target));
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

        public bool IsValid()
        {
            for (int i = 0; i < 6; i++)
            {
                if (double.IsNaN(MotorAngles[i]) || double.IsInfinity(MotorAngles[i]))
                    return false;
            }
            return true;
        }
    }

    public enum RobotControlSource
    {
        SolutionTester,
        JoyStick,
        BezierSpline
    }
}
