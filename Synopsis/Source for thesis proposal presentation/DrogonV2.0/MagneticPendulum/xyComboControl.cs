using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MagneticPendulum
{
    public delegate xyPlot GetPhasePortraitHandler();
    public partial class xyComboControl : UserControl
    {
        public event GetPhasePortraitHandler PhasePortraitRequest;
        public event EventHandler ClearPhasePortraitRequest;
        public xyComboControl()
        {
            InitializeComponent();
            dPlot.XOffsetChanged += DPlot_XOffsetChanged;
            dPlot.XScaleChanged += DPlot_XScaleChanged;
            vPlot.XOffsetChanged += VPlot_XOffsetChanged;
            vPlot.XScaleChanged += VPlot_XScaleChanged;
            Size = Size;
            sfd = new SaveFileDialog();
            sfd.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.bmp;*.jpg;*.png|Formated Data(*.dat)|*.dat";

        }
        private void DPlot_XScaleChanged(float currentValue)
        {
            vPlot.SetXScale(currentValue);
        }

        private void DPlot_XOffsetChanged(float currentValue)
        {
            vPlot.SetXOffsetG(currentValue);
        }
        private void VPlot_XScaleChanged(float currentValue)
        {
            dPlot.SetXScale(currentValue);
        }

        private void VPlot_XOffsetChanged(float currentValue)
        {
            dPlot.SetXOffsetG(currentValue);
        }

        public void AddDPoint(PointF p)
        {
            dPlot.Add(p);
        }
        public void AddVPoint(PointF p)
        {
            vPlot.Add(p);
        }
        private void ComboTimePlot_SizeChanged(object sender, EventArgs e)
        {
            panel1.Height = (label3.Top) / 2;
            panel2.Height = dPlot.Height;
            panel2.Top = dPlot.Height;
        }

        private void dPlot_XOffsetChanged(float currentValue)
        {
            vPlot.SetXOffsetG(currentValue);
        }

        private void dPlot_XScaleChanged(float currentValue)
        {
            vPlot.SetXScale(currentValue);
        }

        private void vPlot_XOffsetChanged(float currentValue)
        {
            dPlot.SetXOffsetG(currentValue);
        }

        private void vPlot_XScaleChanged(float currentValue)
        {
            dPlot.SetXScale(currentValue);
        }

        private void autoScrollCB_CheckedChanged(object sender, EventArgs e)
        {
            dPlot.AutoScroll = autoScrollCB.Checked;
            vPlot.AutoScroll = autoScrollCB.Checked;
        }

        private void clearAllB_Click(object sender, EventArgs e)
        {
            dPlot.ClearAll();
            vPlot.ClearAll();
            ClearPhasePortraitRequest?.Invoke(this, new EventArgs());
        }

        private void recordButton1_Click(object sender, EventArgs e)
        {   
            recordButton1.SetAutoTimerMS(recordDurationNUD.Value * 1000);
        }

        private void recordButton1_RecordingStateChanged(bool state)
        {
            if (recordButton1.RecordingState)
            {
                dPlot.ClearAll();
                vPlot.ClearAll();
                ClearPhasePortraitRequest?.Invoke(this, new EventArgs());
            }
            else
            {
                Save(Math.Max(dPlot.MaxX.X - recordButton1.GetAutoTimerMS(), 0));
            }
        }
        SaveFileDialog sfd;
        public bool Save(float timeToStartFrom)
        {
            int sind = 0;
            for (int i = 0; i < vPlot.DataPoints.Count; i++)
            {
                if (vPlot.DataPoints[i].X >= timeToStartFrom)
                {
                    sind = i;
                    break;
                }
            }
            if (timeToStartFrom < 0)
                timeToStartFrom = 0;
            var strWithFormat = GetSaveableString(true, timeToStartFrom);
            var strWithoutFormat = GetSaveableString(false, timeToStartFrom);
            var dbmp = dPlot.GetImage(sind, "Pendulum angle with time", "Time (s)", "ϴ (rad)");
            var vbmp = vPlot.GetImage(sind, "Pendulum angular velocity with time", "Time (s)", "dϴ (rad/s)");
            
            var ppbmp = PhasePortraitRequest().GetImage(sind, "Phase Portrait", "ϴ (rad)", "dϴ (rad/s)");

            if (sfd.ShowDialog() != DialogResult.OK)
                return false;

            if (System.IO.Path.GetExtension(sfd.FileName).ToLower().Contains("bmp") ||
                System.IO.Path.GetExtension(sfd.FileName).ToLower().Contains("jpg") ||
                System.IO.Path.GetExtension(sfd.FileName).ToLower().Contains("png"))
            {
                dbmp.Save(Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileNameWithoutExtension(sfd.FileName) + "_d" + Path.GetExtension(sfd.FileName)));
                vbmp.Save(Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileNameWithoutExtension(sfd.FileName) + "_v" + Path.GetExtension(sfd.FileName)));
                ppbmp.Save(Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileNameWithoutExtension(sfd.FileName) + "_PhasePortrait" + Path.GetExtension(sfd.FileName)));
            }
            else
            {
                //bool addHeader = !System.IO.Path.GetExtension(sfd.FileName).ToLower().Contains("bin");
                bool addHeader = true;
                string fname = Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileNameWithoutExtension(sfd.FileName) + ".dat");
                if (addHeader)
                    File.WriteAllText(fname, strWithFormat);
                else
                    File.WriteAllText(fname, strWithoutFormat);
            }
            return true;
        }
        string GetSaveableString(bool addHeader, float timeToStartFrom)
        {
            StringBuilder sw = new StringBuilder();
            if (addHeader)
                sw.AppendLine("Sr.No.\ttime_s\ttheta_degree\tomega_rad_p_s");
            int srNo = 1;
            int startInd = -1;
            for (int i =0; i < dPlot.DataPoints.Count; i++)
            {
                if (dPlot.DataPoints[i].X < timeToStartFrom)
                    continue;
                if (addHeader)
                    sw.Append(srNo + "\t");
                if (startInd < 0)
                    startInd = i;
                sw.Append(Math.Round(dPlot.DataPoints[i].X - dPlot.DataPoints[startInd].X, 5) + "\t");
                sw.Append(Math.Round(dPlot.DataPoints[i].Y, 5) + "\t");
                sw.AppendLine(Math.Round(vPlot.DataPoints[i].Y, 5) + "\t");
                srNo++;
            }
            return sw.ToString();
        }

        private void recordDurationNUD_ValueChanged(float currentValue)
        {
            recordButton1.SetAutoTimerMS((int)currentValue * 1000);
        }

        private void saveB_Click(object sender, EventArgs e)
        {
            Save(dPlot.MaxX.X - saveDurationNUD.Value);
        }

        private void xyComboControl_Load(object sender, EventArgs e)
        {
            recordButton1.SetAutoTimerMS(recordDurationNUD.Value * 1000);
        }
    }
}
