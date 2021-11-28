using Physics;
using RoboSim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drogon3
{
    public partial class EndEffectorControl : UserControl
    {
        public event ObjectShareHandler OnTargetPreviewRequested;
        public EndEffectorControl()
        {
            InitializeComponent();
        }
        SphericalRobot Robot;
        public EndEffectorControl(SphericalRobot robot):this()
        {
            Robot = robot;
        }

        public EulerAngleOrientation Target = new EulerAngleOrientation(0, 0, 0, 1, 1, 0);
        SphericalRobotSolution Solution;
        private void joyStickReader_Tick(object sender, EventArgs e)
        {
            if (Robot.ControlSource == RobotControlSource.JoyStick)
            {
                Target.X += xyEEJS.DX / 100;
                Target.Y += xyEEJS.DY / 100;
                Target.Z += zEEJS.DY / 100;
                Target.A += aEEJS.DY / 100;
                Target.B += bEEJS.DY / 100;
                Target.G += gEEJS.DY / 20;

                var solBkp = (SphericalRobotSolution)Solution?.Clone();
                Solution = SphericalRobotSolution.Solution(Solution, 0, Robot, Target);

                for (int i = 0; i < Robot.Actuators.Length; i++)
                    if (
                        Solution.MotorAngles[i] > 100 ||
                        Solution.MotorAngles[i] < -100 ||
                        double.IsNaN(Solution.MotorAngles[i]) ||
                        double.IsNegativeInfinity(Solution.MotorAngles[i]) || 
                        double.IsPositiveInfinity(Solution.MotorAngles[i]))
                    {
                        Target.X -= xyEEJS.DX / 100;
                        Target.Y -= xyEEJS.DY / 100;
                        Target.Z -= zEEJS.DY / 100;
                        Target.A -= aEEJS.DY / 100;
                        Target.B -= bEEJS.DY / 100;
                        Target.G -= gEEJS.DY / 20;
                        Solution = solBkp;
                        return;
                    }
                OnTargetPreviewRequested(this, Target);
                for (int i = 0; i < Robot.Actuators.Length; i++)
                    Robot.Actuators[i].Reference = Solution.MotorAngles[i];
            }
            else
            {
                if (Robot.CurrentTarget != null)
                    Target = Robot.CurrentTarget?.Clone();
            }
        }

        private void controlTypeRB_CheckedChanged(object sender, EventArgs e)
        {
            if (calligraphyRB.Checked)
                Robot.ControlSource = RobotControlSource.BezierSpline;
            else if (joystickControlRB.Checked)
                Robot.ControlSource = RobotControlSource.JoyStick;
            else
                Robot.ControlSource = RobotControlSource.SolutionTester;
        }
    }
}
