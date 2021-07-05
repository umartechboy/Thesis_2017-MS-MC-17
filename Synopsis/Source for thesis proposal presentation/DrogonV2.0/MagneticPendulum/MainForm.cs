using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using FivePointNine.Windows.Controls;

namespace MagneticPendulum
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            phasePortraitP.dataPlot.AutoScroll = false;
            phasePortraitP.dataPlot.UniqueXAxisStamps = false;
        }

        private void angularFreqTrB_Scroll(object sender, EventArgs e)
        {
            dataPort.Send(new FivePointNine.Windows.IO.PacketCommand(0, angularFreqTrB.Value.ToString(), dataPort.ID, 1));
            angularFreqTB.TextChanged -= angularFreqTB_TextChanged;
            angularFreqTB.Text = Math.Round(angularFreqTrB.Value * Math.PI / 60 * 2, 1).ToString();
            angularFreqTB.TextChanged += angularFreqTB_TextChanged;
        }

        double LastPendulumThD = double.NaN;
        double LastSampleT = double.NaN;
        bool hasFormUpdates = false;
        double minMaxLabel1_Max = 0, minMaxLabel1_Min, minMaxLabel1_Value, minMaxLabel2_Value, minMaxLabel2_Max, minMaxLabel2_Min;
        private void spEventPoll_Tick(object sender, EventArgs e)
        {
            if (!hasFormUpdates)
                return;
            hasFormUpdates = false;
            minMaxLabel1.Max = minMaxLabel1_Max;
            minMaxLabel1.Min = minMaxLabel1_Min;
            minMaxLabel2.Max = minMaxLabel2_Max;
            minMaxLabel2.Min = minMaxLabel2_Min;
            minMaxLabel1.Value = minMaxLabel1_Value;
            minMaxLabel2.Value = minMaxLabel2_Value;
            timeElapsedL.Text = Math.Round(dvPlot.dPlot.MaxX.X, 1).ToString();
        }
        private void serialChannelControl1_PacketReceived(FivePointNine.Windows.IO.PacketCommand command)
        {
            if (command.PacketID == FivePointNine.Windows.IO.PacketCommandID.Ping)
            {
                angularFreqTrB.Value = Convert.ToInt32(command.PayLoadString);
                angularFreqTB.TextChanged -= angularFreqTB_TextChanged;
                angularFreqTB.TextChanged += angularFreqTB_TextChanged;
            }
            else if (command.PacketID == (FivePointNine.Windows.IO.PacketCommandID)1)
            {
                var millis  = BitConverter.ToUInt32(command.PayLoad, 0);
                float ThRaw_ = BitConverter.ToSingle(command.PayLoad, 4);
                var motorTh = BitConverter.ToUInt16(command.PayLoad, 8);

                float ThD = (float)Calibrate(ThRaw_);
                //float ThD = ThRaw_;

                ProcessIncomingData(millis, ThD, motorTh);
            }
        }
        void ProcessIncomingData(uint millis, float ThD, ushort motorTh)
        {
            hasFormUpdates = true;
            var t = millis / 1000.0F;
            var ThR = (float)(ThD / 180 * Math.PI);
            var LastPendulumThR = LastPendulumThD * Math.PI / 180.0F;
            magPendulumVisualizer1.PendulumAngle = ThR;
            if (ThD > minMaxLabel1.Max)
                minMaxLabel1_Max = ThD;
            else if (ThD < minMaxLabel1.Min)
                minMaxLabel1_Min = ThD;

            magPendulumVisualizer1.MotorAngle = (float)(motorTh / 400.0F * 2.0F * Math.PI);
            minMaxLabel1_Value = ThD;
            minMaxLabel2_Value = (ThR - LastPendulumThR) / (t - LastSampleT);

            if (!double.IsNaN(LastPendulumThD) && LastSampleT != t)
            {
                if (dvPlot.dPlot.DataPoints.Count == 0)
                {
                    dvPlot.dPlot.ShiftStartXOffset(t);
                    dvPlot.vPlot.ShiftStartXOffset(t);
                }
                var dThR = (float)((ThR - LastPendulumThR) / (t - LastSampleT));
                dvPlot.AddDPoint(new PointF(t, (float)ThD));
                dvPlot.AddVPoint(new PointF(t, dThR));
                phasePortraitP.Add(new PointF((float)ThR, dThR));
                if (dThR > minMaxLabel2.Max)
                    minMaxLabel2_Max = dThR;
                else if (dThR < minMaxLabel2.Min)
                    minMaxLabel2_Min = dThR;
            }

            LastPendulumThD = ThD;
            LastSampleT = t;
        }

        double p1 = -4.598e-15;
       double p2 = 8.02e-12;
       double p3 = -4.608e-09;
       double p4 = 9.521e-07;
       double p5 = -0.0001255;
       double p6 = -0.09126;
       double p7 = 90.63;
        private double Calibrate(double th_)
        {
            double v = p1 * Math.Pow(th_, 6) + p2 * Math.Pow(th_, 5) + p3 * Math.Pow(th_, 4) + p4 * Math.Pow(th_, 3) + p5 * Math.Pow(th_, 2) + p6 * th_ + p7;
            if (v <= -40)
                return v * v * v* 0.0005683 + v * v * 0.1026 + v * 7.116 + 116.7;
            else
               return v;
        }

        private void angularFreqTB_TextChanged(object sender, EventArgs e)
        {
            if (angularFreqTB.Text == "")
                angularFreqTB.Text = "0";
            int v = 0;
            try { v = (int)Math.Round(Math.Min(Math.Max(Convert.ToDouble(angularFreqTB.Text) / Math.PI / 2 * 60, 0), 200) ); }
            catch
            { return; }
            angularFreqTrB.Value = v;
            angularFreqTrB.Scroll -= angularFreqTrB_Scroll;
            //dataPort.Send(new FivePointNine.Windows.IO.PacketCommand(0, angularFreqTrB.Value.ToString(), dataPort.ID, 1));
           
            angularFreqTrB.Scroll += angularFreqTrB_Scroll;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void dataPort_Load(object sender, EventArgs e)
        {

        }

        private void homeMotorB_Click(object sender, EventArgs e)
        {
            string pl = Math.Round((double)motorHomeOffset.Value / 360 * 400, 0).ToString();
            dataPort.Send(new FivePointNine.Windows.IO.PacketCommand((FivePointNine.Windows.IO.PacketCommandID)1, pl, dataPort.ID, 1));
        }

        private void applySpeed_Click(object sender, EventArgs e)
        {
            if (sender == stopB)
                angularFreqTB.Text = "0";
            if (angularFreqTB.Text == "")
                angularFreqTB.Text = "0";
            int v = 0;
            try { v = (int)Math.Round(Math.Min(Math.Max(Convert.ToDouble(angularFreqTB.Text) / Math.PI / 2 * 60, 0), 200)); }
            catch
            { return; }
            angularFreqTrB.Value = v;
            angularFreqTrB.Scroll -= angularFreqTrB_Scroll;
            dataPort.Send(new FivePointNine.Windows.IO.PacketCommand(0, angularFreqTrB.Value.ToString(), dataPort.ID, 1));
            angularFreqTrB.Scroll += angularFreqTrB_Scroll;
        }

        float dummyMillis = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            float millis = dummyMillis;
            var t = millis / 1000;
            var Th = (float)(Math.Sin(2 * Math.PI * t) * 180 / Math.PI);
            ProcessIncomingData((uint)millis, Th, 0);
            dummyMillis += timer1.Interval;
        }

        private void controlGP_Enter(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
           Close();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void dvPlot_Load(object sender, EventArgs e)
        {

        }

        private xyPlot dvPlot_PhasePortraitRequest()
        {
            return phasePortraitP.Plot;
        }

        private void dvPlot_ClearPhasePortraitRequest(object sender, EventArgs e)
        {
            phasePortraitP.Plot.ClearAll();
        }

        private void autoScalePhasePortraitB_Click(object sender, EventArgs e)
        {
        }

        private void dataPort_Connected(object sender, EventArgs e)
        {
            //dvPlot.Plot1.ClearAll();
            //dvPlot.Plot2.ClearAll();
        }

        private void phasePortraitP_SizeChanged(object sender, EventArgs e)
        {
        }
    }
}
