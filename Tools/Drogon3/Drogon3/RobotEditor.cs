using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboSim
{
    public partial class RobotEditor : UserControl
    {
        public RobotEditor(SphericalRobot robot)
        {
            this.robot = robot;
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "l" + i + "TB";
                tb.Left = 50;
                tb.Top = 10 + i * tb.Height;
                tb.TextChanged += LTB_TextChanged;
                var l = new Label();
                l.Top = 14 + i * tb.Height;
                l.Text = "L" + i;
                l.AutoSize = true;
                l.Left = 30;
                Controls.Add(tb);
                Controls.Add(l);
            }
            int y = 110;
            for (int i = 0; i < 6; i++)
            {
                robot.Actuators[i].Editor.Top = y;
                y += robot.Actuators[i].Editor.Height;
                Controls.Add(robot.Actuators[i].Editor);
            }                                                  
        }
        int animInd = -1;
        int animFrom = 0, animTo = 0, animCrsr = 0;
        bool goingUp = true;

        private void rangeTB_TextChanged(object sender, EventArgs e)
        {
            if (dontTriggerTextChange) return;
            try
            {
                int ind = Convert.ToInt16(((Control)sender).Name.Substring(1, 1));
                double v = Convert.ToDouble(((TextBox)sender).Text) * Math.PI / 180;
                try
                {
                    Robot.Actuators[ind].Range = v;
                    animInd = ind;
                    animCrsr = 0;
                    goingUp = true;
                }
                catch { }
            }
            catch { }
        }

        private void minTB_TextChanged(object sender, EventArgs e)
        {
            if (dontTriggerTextChange) return;
            try
            {
                int ind = Convert.ToInt16(((Control)sender).Name.Substring(1, 1));
                double v = Convert.ToDouble(((TextBox)sender).Text) * Math.PI / 180;
                try
                {
                    Robot.Actuators[ind].Reference = v;
                    Robot.Actuators[ind].Reference = v;
                }
                catch { }
            }
            catch { }
        }

        private void LTB_TextChanged(object sender, EventArgs e)
        {
            if (dontTriggerTextChange) return;
            try
            {
                int ind = Convert.ToInt16(((Control)sender).Name.Substring(1, 1));
                double v = Convert.ToDouble(((TextBox)sender).Text);
                try
                {
                    if (ind == 0) Robot.L0 = v;
                    else if (ind == 1) Robot.L1 = v;
                    else if (ind == 2) Robot.L2 = v;
                    else if (ind == 3) Robot.L3 = v;
                }
                catch { }
            }
            catch { } 
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (animInd >= 0)
            {
                if (goingUp)
                {
                    double d = (animTo - animFrom) + (double)animCrsr / 10;
                    if (d < Robot.Actuators[animInd].Range)
                    {
                        Robot.Actuators[animInd].Reference = Robot.Actuators[animInd].Minimum + d;
                        animCrsr++;
                    }
                    else
                    {
                        goingUp = false;
                        animCrsr = 0;
                    }
                }
                else
                {
                    double d = (animTo - animFrom) + (double)(10 - animCrsr) / 10;
                    if (d > Robot.Actuators[animInd].Range)
                    {
                        Robot.Actuators[animInd].Reference = Robot.Actuators[animInd].Minimum - d;
                        animCrsr++;
                    }
                    else
                    {
                        animInd = -1;
                        animCrsr = 0;
                    }
                }
            }
        }

        SphericalRobot robot;

        private void RobotEditor_Load(object sender, EventArgs e)
        {
            robot.OnPositionChanged += Robot_OnPositionChanged;
        }

        private void Robot_OnPositionChanged(int index, double value)
        {
            dontTriggerTextChange = true;
            //((TextBox)Controls.Find("m" + index + "MinTB", false)[0]).Text = value.ToString();
            dontTriggerTextChange = false;
        }

        bool dontTriggerTextChange = false;
        public SphericalRobot Robot
        {
            get { return robot; }
            set
            {
                dontTriggerTextChange = true;
                robot = value;

                ((TextBox)Controls.Find("l0TB", false)[0]).Text = robot.L0.ToString();
                ((TextBox)Controls.Find("l1TB", false)[0]).Text = robot.L1.ToString();
                ((TextBox)Controls.Find("l2TB", false)[0]).Text = robot.L2.ToString();
                ((TextBox)Controls.Find("l3TB", false)[0]).Text = robot.L3.ToString();

                for (int i = 0; i < 6; i++)
                {
                    ((TextBox)Controls.Find("m" + i + "MinTB", false)[0]).Text = robot.Actuators[i].Minimum.ToString();
                    ((TextBox)Controls.Find("m" + i + "RangeTB", false)[0]).Text = robot.Actuators[i].Range.ToString();
                }
                dontTriggerTextChange = false;
            }
        }
    }
}
