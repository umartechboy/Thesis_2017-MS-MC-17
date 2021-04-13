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
    public class DiscretePlannedTrajectory
    {
        public List<TrajectoryAction> Actions;
        public List<ChangeTarget> TargetSteps
        {
            get
            {
                List<ChangeTarget> t = new List<ChangeTarget>();
                foreach (var s in Actions)
                    if (s is ChangeTarget)
                        t.Add((ChangeTarget)s);
                return t;
            }
        }
        //public static DiscretePlannedTrajectory FromTargets(List<EulerAngleOrientation[]> computedTargetsForWritting,  SphericalRobot robot, double dt, double retract)
        //{
        //    robot = (SphericalRobot)robot.Clone();
        //    var traj = new DiscretePlannedTrajectory();
        //    traj.Actions = new List<TrajectoryAction>();

        //    List<EulerAngleOrientation[]> withOffset = new List<EulerAngleOrientation[]>();
        //    foreach (var s in computedTargetsForWritting)
        //    {
        //        var a = s.ToList();
        //        for (double f = 0; f <= retract; f += retract / 10)
        //        {
        //            var zero = ((ChainTranformationMatrix)s[0]);
        //            var end = ((ChainTranformationMatrix)s[s.Length-1]);
        //            zero.Children.Add(new TranslateTransformationMatrix(0, 0, f));
        //            a.Insert(0, EulerAngleOrientation.FromTransformationMatrix(zero));

        //            end.Children.Add(new TranslateTransformationMatrix(0, 0, f));
        //            a.Add(EulerAngleOrientation.FromTransformationMatrix(end));
        //        }
        //        withOffset.Add(a.ToArray());
        //    }
        //    computedTargetsForWritting = withOffset;
        //    var solution = SphericalRobotSolution.Solution2(robot, computedTargetsForWritting[0][0]);
        //    traj.Actions.Add(new GoToHome(computedTargetsForWritting[0][0], solution.MotorAngles));
        //    for (int i = 0; i < solution.MotorAngles.Length; i++)
        //    {
        //        robot.Actuators[i].ForceSetCurrentPosition(solution.MotorAngles[i]);
        //        robot.Actuators[i].Target = solution.MotorAngles[i];
        //    }
        //    foreach (var s in computedTargetsForWritting)
        //    {
        //        foreach (var t in s)
        //        {

        //            // now simulate this step
        //            solution = SphericalRobotSolution.Solution2(robot, t);
        //            traj.Actions.Add(new ChangeTarget(solution.MotorAngles));
        //            double [] requiredTime = new double[solution.MotorAngles.Length];
        //            for (int i = 0; i < solution.MotorAngles.Length;i++)
        //            {
        //                var anglePerStep = Math.PI * 2 / ((StepperMotor)robot.Actuators[i]).Resolution;
        //                var requiredTicks = (int)Math.Ceiling(Math.Abs(robot.Actuators[i].CurrentPosition - solution.MotorAngles[i]) / anglePerStep);
        //                if (Math.Abs(Math.Round(robot.Actuators[i].CurrentPosition, 5)) == Math.Round(Math.PI, 5) &&
        //                    Math.Abs(Math.Round(robot.Actuators[i].Target, 5)) == Math.Round(Math.PI, 5))
        //                    requiredTime[i] = 0;
        //                else
        //                    requiredTime[i] = requiredTicks * ((StepperMotor)robot.Actuators[i]).StepTime;
        //            }
        //            double minRequiredTime = requiredTime.Max();
        //            long minRequiredSteps = (long)Math.Ceiling(minRequiredTime / dt);
        //            //traj.Actions.Add(new WaitSimSteps(minRequiredSteps));
        //            for (int i = 0; i < 6; i++)
        //            {
        //                if (solution.MotorAngles[i] < -20 || solution.MotorAngles[i] > 20 || double.IsNaN(solution.MotorAngles[i]))
        //                {
        //                    System.Windows.Forms.MessageBox.Show("One of the points in the tarjectory cannot be achieved. The process will abort now");
        //                    return new RoboSim.DiscretePlannedTrajectory();
        //                }
        //            }
        //            traj.Actions.Add(new WaitTarget(solution.MotorAngles));

        //            for (int i = 0; i < solution.MotorAngles.Length; i++)
        //            {
        //                robot.Actuators[i].ForceSetCurrentPosition(solution.MotorAngles[i]);
        //            }
        //        }

        //    }
        //    return traj;
        //}
    }
    public class TrajectoryAction
    { }
    public class WaitSimSteps : TrajectoryAction
    {
        public long TotalSteps { get; set; }
        public WaitSimSteps(long steps)
        { TotalSteps = steps; }
        public override string ToString()
        {
            return "Sim: " + TotalSteps;
        }

    }
    public class ChangeTarget: TrajectoryAction
    {
        public double[] Target { get; protected set; }
        public ChangeTarget(double [] target)
        { Target = target; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Target.Length; i++)
                sb.Append(Math.Round(Target[i], 3).ToString() + ", ");
            return sb.ToString().Trim(new char[] { ' ', ',' });
        }
    }
    public class WaitTarget : TrajectoryAction
    {
        public double[] Target { get; protected set; }
        public WaitTarget(double[] target)
        { Target = target; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Target.Length; i++)
                sb.Append(Math.Round(Target[i], 3).ToString() + ", ");
            return sb.ToString().Trim(new char[] { ' ', ',' });
        }
    }
    public class GoToHome: TrajectoryAction
    {
        public EulerAngleOrientation Home { get; protected set; }
        public double[] Targets;
        public GoToHome(EulerAngleOrientation home, double [] targets)
        {
            Home = home;
            Targets = targets;
        }
    }
}
