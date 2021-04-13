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
        public event ObjectShareHandler OnWritingPadOrientationChangeRequested;
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
        public EulerAngleOrientation WritingPadOrientation = new EulerAngleOrientation(0, 0, 0, 1, 1, 0);
        SphericalRobotSolution Solution;
        private void joyStickReader_Tick(object sender, EventArgs e)
        {
            if (manualControlRB.Checked)
            {
                Target.X += xyEEJS.DX / 100;
                Target.Y += xyEEJS.DY / 100;
                Target.Z += zEEJS.DY / 100;
                Target.A += aEEJS.DY / 100;
                Target.B += bEEJS.DY / 100;
                Target.G += gEEJS.DY / 20;
                OnTargetPreviewRequested(this, Target);
                Solution = SphericalRobotSolution.Solution(Solution, 0, Robot, Target);
                for (int i = 0; i < Robot.Actuators.Length; i++)
                    Robot.Actuators[i].Reference = Solution.MotorAngles[i];
                WritingPadOrientation.X += xyWPJS.DX / 100;
                WritingPadOrientation.Y += xyWPJS.DY / 100;
                WritingPadOrientation.Z += zJPJS.DY / 100;
                WritingPadOrientation.A += aGPJS.DY / 100;
                WritingPadOrientation.B += bGPJS.DY / 100;
                WritingPadOrientation.G += gGPJS.DY / 20;
                OnWritingPadOrientationChangeRequested(this, WritingPadOrientation);
                xyWPL.Text = WritingPadOrientation.X.ToString() + ", " + WritingPadOrientation.Y;
                zWPL.Text = WritingPadOrientation.Z.ToString();
                aWPL.Text = WritingPadOrientation.A.ToString();
                gWPL.Text = WritingPadOrientation.G.ToString();
                bWPL.Text = WritingPadOrientation.B.ToString();
            }
        }
    }
}
