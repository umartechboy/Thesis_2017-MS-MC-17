using Physics;
using PhysLogger2;
using PhysLogger2.Hardware;
using PhysLogger2.PhysLoggerMath.Quantity;
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
    public partial class PIDPerformanceControl : PhysLogger2.Forms.DXForm.DXFormEmulator
    {
        public SoftLoggerTerminalQuantity timeQ { get; private set; }

        public SoftLoggerTerminalQuantity[] motorPosQs { get; private set; }
        public SoftLoggerTerminalQuantity[] motorRefQs { get; private set; }
        public RoboSim.Robot Robot { get; private set; }
        public PhysLogger2.Widgets.Plotting.XYPlot pidPerformanceXYP { get; private set; }
        public PIDPerformanceControl()
        {
            InitializeComponent();
        }
        public void Setup(RoboSim.Robot robot)
        {
            Robot = robot;
            var dxForm = PhysLogger2.Forms.DXForm.RunInControl(this);
            var ThemedResources = new PhysLogger2.ThemedResources(dxForm);
            pidPerformanceXYP = new PhysLogger2.Widgets.Plotting.XYPlot("pid", ThemedResources, 0, 20, 500, 500);
            pidPerformanceXYP.Font = new PhysLogger2.Graphics.DXFont() { Height = 12 };
            timeQ = new TimeSoftwareQuantity(ThemedResources, null);
            Color[] colors = { Color.Red, Color.DarkBlue, Color.DarkGreen, Color.Gray, Color.Black, Color.GreenYellow };
            motorPosQs = robot.Actuators.Select(a => new ConstantSoftwareQuantity(ThemedResources, UnitCollection.UnitTypesEnum.Angle) { Color = colors[a.ID] }).ToArray();
            motorRefQs = robot.Actuators.Select(a => new ConstantSoftwareQuantity(ThemedResources, UnitCollection.UnitTypesEnum.Angle) { Color = Color.FromArgb(100, colors[a.ID]), LineThickness = 1 }).ToArray();
            foreach (ActuatorListBoxItem item in robot.Actuators.Select(a => new ActuatorListBoxItem(a)))
                actuatorsList.Items.Add(item.ToString(), item.Actuator is ServoMotor);
            actuatorsList.ItemCheck += (s, e) =>
              {
                  foreach (int selInd in actuatorsList.CheckedIndices)
                  {
                      if (pidPerformanceXYP.YAxis.DataSeries.Find(ds => ds.BindingQuantity == motorPosQs[selInd]) == null)
                      {
                          pidPerformanceXYP.AddYSource(motorPosQs[selInd]);
                          pidPerformanceXYP.AddYSource(motorRefQs[selInd]);
                      }
                  }
                  for (int i = 0; i < motorPosQs.Length; i++)
                  {
                      if (!actuatorsList.CheckedIndices.Contains(i))
                          if (pidPerformanceXYP.YAxis.DataSeries.Find(ds => ds.BindingQuantity == motorPosQs[i]) != null)
                          {
                              pidPerformanceXYP.RemoveYSource(motorPosQs[i], pidPerformanceXYP.YAxis);
                              pidPerformanceXYP.RemoveYSource(motorRefQs[i], pidPerformanceXYP.YAxis);
                          }
                  }
              };
            pidPerformanceXYP.SetXSource(timeQ);
            foreach (var q in motorPosQs)
                pidPerformanceXYP.AddYSource(q);
            foreach (var q in motorRefQs)
                pidPerformanceXYP.AddYSource(q);
            SizeChanged += (s2, e2) =>
            {
                pidPerformanceXYP.VisualStates.SetWidth(this.Width + 200 - actuatorsList.Width);
                pidPerformanceXYP.VisualStates.SetHeight(this.Height - 50);
                pidPerformanceXYP.ResetAxisBounds();
            };
            dxForm.AddDXControl(pidPerformanceXYP);
            dxForm.NotifyLoaded();
        }

        private void actuatorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PT.Text = "";
            IT.Text = "";
            DT.Text = "";
            PMax.Text = "";
            refT.Text = "";
            posT.Text = "";
        }

        private void T_Leave(object sender, EventArgs e)
        {
            if (actuatorsList.SelectedIndices.Count != 1)
                return;
            try
            {
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).P = double.Parse(PT.Text);
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).I = double.Parse(IT.Text);
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).D = double.Parse(DT.Text);
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).MaximumPower = double.Parse(PMax.Text);
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).MaximumTorque = double.Parse(tauMaxT.Text);
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Reference = double.Parse(refT.Text);
                ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Current = double.Parse(posT.Text);
            }
            catch { ((TextBox)sender).Focus(); }
        }

        private void resetP_Click(object sender, EventArgs e)
        {
            if (actuatorsList.SelectedIndices.Count != 1) return;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Reference = 0;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Current = 0;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Omega = 0;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).PositionPID.Reset();
        }

        private void stopB_Click(object sender, EventArgs e)
        {
            if (actuatorsList.SelectedIndices.Count != 1) return;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Reference = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Current;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Omega = 0;
            ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).PositionPID.Reset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (actuatorsList.SelectedIndices.Count != 1) return;
            if (Robot.Actuators[actuatorsList.SelectedIndex] is ServoMotor)
            {
                if (!PT.Focused) PT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).P.ToString();
                if (!IT.Focused) IT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).I.ToString();
                if (!DT.Focused) DT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).D.ToString();
                if (!PMax.Focused) PMax.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).MaximumPower.ToString();
                if (!tauMaxT.Focused) tauMaxT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).MaximumTorque.ToString();
                if (!refT.Focused) refT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Reference.ToString();
                if (!posT.Focused) posT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Current.ToString();
                PCurrentT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).AppliedPower.ToString();
                tauAppliedT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).AppliedTorque.ToString();
                omegaT.Text = ((ServoMotor)Robot.Actuators[actuatorsList.SelectedIndex]).Omega.ToString();
            }
        }
    }
    public class ActuatorListBoxItem
    {
        public IServoMotor Actuator { get; private set; }

        public ActuatorListBoxItem(IServoMotor a)
        {
            this.Actuator = a;
        }

        public override string ToString()
        {
            return Actuator.ID + " " + Actuator.ToString().Split(new char[] { '.' }).Last();
        }
    }
    public class ConstantSoftwareQuantity : SoftLoggerTerminalQuantity
    {
        public ConstantSoftwareQuantity(ThemedResources themedResources, UnitCollection.UnitTypesEnum unit) : base(themedResources, null)
        {
            Unit = UnitCollection.Create(unit);
        }
        //object offset = null;
        public override float getValue()
        {
            //    if (offset == null)
            //        offset = DateTime.Now;
            //    return (float)((DateTime.Now - ((DateTime)offset)).TotalSeconds);
            return cache;
        }
    }
    public class TimeSoftwareQuantity : SoftLoggerTerminalQuantity
    {
        public TimeSoftwareQuantity(ThemedResources themedResources, PhysLoggerHW HW) : base(themedResources, HW)
        {
            Unit = UnitCollection.Create(UnitCollection.UnitTypesEnum.Time);
        }
        //object offset = null;
        public override float getValue()
        {
            //    if (offset == null)
            //        offset = DateTime.Now;
            //    return (float)((DateTime.Now - ((DateTime)offset)).TotalSeconds);
            return cache;
        }
    }
    public class SpeedChangingTextBox : TextBox
    {
        float last = 0;
        public override string Text
        {
            get => base.Text;
            set
            {
                var v = 0.0F;
                try
                {
                    //v = 50;
                    v = float.Parse(value);
                }
                catch { }
                var dv = v - last;
                var change = dv / last;
                if (last != 0)
                {
                    float ci = change * 100;
                    int r = 255 - (int)Math.Max(Math.Min(ci, 255), 0);
                    int g = 255 - (int)Math.Max(Math.Min(-ci, 255), 0);
                    int b = Math.Max(255 - (255 - r) - (255 - g), 0);
                    BackColor = Color.FromArgb(r, g, b);
                }
                else
                    BackColor = Color.White;
                base.Text = value;
                last = v;
            }
        }
    }
}
