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
using System.Windows.Media.Media3D;

namespace Drogon3
{
    public partial class InverseKinematicsSolver : UserControl
    {
        private SphericalRobot robot;

        public event ObjectShareHandler OnTargetPreviewRequested;
        public InverseKinematicsSolver()
        {
            InitializeComponent();
        }
        
        SphericalRobotSolution []solutions;
        TextBox[][] actuatorOutputs;
        Button[] solveBs;
        public InverseKinematicsSolver(SphericalRobot robot) : this()
        {
            this.robot = robot;
            int yo = solveB.Bottom + 10;
            int xo = x.Left, dx = y.Left - x.Left;
            var currentOutputs = new SpeedChangingTextBox[robot.Actuators.Length];
            for (int i = 0; i < robot.Actuators.Length; i++)
            {
                var lb = new Label();
                lb.Text = "A" + i;
                lb.AutoSize = false;
                lb.Width = x.Width;
                lb.Height = 15;
                lb.Left = xo;
                lb.Top = yo;

                var tb = new SpeedChangingTextBox();
                //tb.ReadOnly = true;
                tb.Width = x.Width;
                tb.Left = xo;
                tb.Top = lb.Bottom + 5;
                Controls.Add(lb);
                Controls.Add(tb);
                currentOutputs[i] = tb;
                xo += dx;
            }
            Timer t = new Timer();
            t.Interval = 50;
            t.Tick += (s, e) =>
              {
                  for (int i = 0; i < robot.Actuators.Length; i++)
                      currentOutputs[i].Text = robot.Actuators[i].Current.ToString();
              };
            t.Enabled = true;
            yo += 50;
            xo = x.Left;


            var currentRefs = new SpeedChangingTextBox[robot.Actuators.Length];
            for (int i = 0; i < robot.Actuators.Length; i++)
            {
                var tb = new SpeedChangingTextBox();
                //tb.ReadOnly = true;
                tb.Width = x.Width;
                tb.Left = xo;
                tb.Top = yo;
                Controls.Add(tb);
                currentRefs[i] = tb;
                xo += dx;
            }
            t = new Timer();
            t.Interval = 50;
            t.Tick += (s, e) =>
            {
                for (int i = 0; i < robot.Actuators.Length; i++)
                    currentRefs[i].Text = robot.Actuators[i].Reference.ToString();
            };
            t.Enabled = true;
            yo += 100;

            actuatorOutputs = new TextBox[2][];
            solveBs = new Button[2];
            solutions = new SphericalRobotSolution[2];
            for (int j = 0; j < 2; j++)
            {
                xo = x.Left;
                dx = y.Left - x.Left;
                actuatorOutputs[j] = new TextBox[robot.Actuators.Length];
                for (int i = 0; i < robot.Actuators.Length; i++)
                {
                    var lb = new Label();
                    lb.Text = "A" + i;
                    lb.AutoSize = false;
                    lb.Width = x.Width;
                    lb.Height = 15;
                    lb.Left = xo;
                    lb.Top = yo;

                    var tb = new TextBox();
                    tb.Name = "tb" + j + i;
                    //tb.ReadOnly = true;
                    tb.Width = x.Width;
                    tb.Left = xo;
                    tb.Top = lb.Bottom + 5;
                    Controls.Add(lb);
                    Controls.Add(tb);
                    actuatorOutputs[j][i] = tb;
                    xo += dx;
                }
                var prevB = new Button();
                prevB.Size = solveB.Size;
                prevB.Text = "Apply";
                prevB.Left = solveB.Left;
                prevB.Top = yo + 50;
                int finalJ = j;
                prevB.Click += (s, e) => {
                    if (robot.ControlSource == RobotControlSource.SolutionTester)
                    {
                        for (int i = 0; i < robot.Actuators.Length; i++)
                            try
                            {
                                solutions[finalJ].MotorAngles[i] = double.Parse(Controls.Find("tb" + finalJ + i, false)[0].Text);
                            }
                            catch { return; }
                        for (int k = 0; k < robot.Actuators.Length; k++)
                            robot.Actuators[k].Reference = solutions[finalJ].MotorAngles[k];
                    }
                };
                Controls.Add(prevB);
                //var applyB = new Button();
                //applyB.Size = solveB.Size;
                //applyB.Text = "Apply";
                //applyB.Left = prevB.Right + 5;
                //applyB.Top = yo + 50;
                //Controls.Add(applyB);
                yo += 100;
            }
        }

        EulerAngleOrientation SetTarget;
        SphericalRobotSolution [] lastSolutions = new SphericalRobotSolution[2];
        private void solveB_Click(object sender, EventArgs e)
        {
            SetTarget = null;
            try
            {
                SetTarget = new EulerAngleOrientation(
                    double.Parse(a.Text), double.Parse(b.Text), double.Parse(g.Text),
                    double.Parse(x.Text), double.Parse(y.Text), double.Parse(z.Text));
            }
            catch { MessageBox.Show("Kindly enter a valid target"); }
            OnTargetPreviewRequested(this, SetTarget);
            solutions = new SphericalRobotSolution[2];
            for (int j = 0; j < 2; j++)
            {
                try
                {
                    solutions[j] = SphericalRobotSolution.Solution(lastSolutions[j], j, robot, SetTarget);
                    lastSolutions[j] = solutions[j];
                    for (int i = 0; i < solutions[j].MotorAngles.Length; i++)
                    {
                        actuatorOutputs[j][i].Text = solutions[j].MotorAngles[i].ToString();
                        if (solutions[j].MotorAngles[i] < robot.Actuators[i].Minimum || solutions[j].MotorAngles[i] > robot.Actuators[i].Minimum + robot.Actuators[i].Range)
                            actuatorOutputs[j][i].BackColor = Color.FromArgb(255, 200, 200);
                        else
                            actuatorOutputs[j][i].BackColor = Color.FromArgb(200, 255, 200);
                    }
                }
                catch { }
            }
        }

        DateTime lastTime = DateTime.Now;
        double lastD = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(SetTarget, null)) return;
            var dt = (DateTime.Now - lastTime).TotalSeconds;
            lastTime = DateTime.Now;
            var pos = robot.CurrentEndEffectorPosition;
            var tar = SetTarget.Offset;
            var d = (tar - pos).Length;
            pErrorTB.Text = d.ToString();
            var dd = d - lastD;
            var v = dd / dt;
            speedTB.Text = v.ToString();
            lastD = d;

        }
    }
    public delegate void ObjectShareHandler(object sender, object package);
}
