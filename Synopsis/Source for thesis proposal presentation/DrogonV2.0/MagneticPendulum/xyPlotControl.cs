using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagneticPendulum
{
    public partial class xyPlotControl : UserControl
    {
        public event ValueChangeHandler XOffsetChanged;
        public event ValueChangeHandler XScaleChanged;
        public event EventHandler DataCleared;
        public xyPlot Plot { get { return dataPlot; } }
        public override string Text
        {
            get
            {
                return titleL.Text;
            }

            set
            {
                titleL.Text = value;
            }
        }
        public override bool AutoScroll
        {
            get
            {
                return dataPlot.AutoScroll;
            }

            set
            {
                dataPlot.AutoScroll = value;
            }
        }
        public xyPlotControl()
        {
            dataPlot = new xyPlot();
            InitializeComponent();
            sfd = new SaveFileDialog();
            sfd.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.bmp;*.jpg;*.png|Formated Data(*.dat)|*.dat|Unformatted Formated Data(*.dat)|*.bin";
            dataPlot.ContinuousAutoScale = true;
        }

        public bool UniqueXAxisStamps { get { return  dataPlot.UniqueXAxisStamps; } set { dataPlot.UniqueXAxisStamps = value; } }
        public void Add(PointF p)
        {
            dataPlot.Add(p);
        }
        public void SetXOffsetG(float v)
        {
            dataPlot.SetXOffsetG(v);
        }
        public void SetXScale(float v)
        {
            dataPlot.SetXScale(v);
        }
        SaveFileDialog sfd;
        
        string GetSaveableString(bool addHeader)
        {
            StringBuilder sw = new StringBuilder();
            if (addHeader)
                sw.AppendLine("Sr.No.\t" + XLabel + "\t" + YLabel);
            int ind = 1;
            foreach (var p in dataPlot.DataPoints)
            {
                if (addHeader)
                    sw.Append(ind + "\t");
                sw.Append(Math.Round(p.X, 5) + "\t");
                sw.AppendLine(Math.Round(p.Y, 5).ToString());
                ind++;
            }
            return sw.ToString();
        }
        public string XLabel { get; set; } = "x axis";
        public string YLabel { get; set; } = "y axis";

        private void dataPlot_XOffsetChanged(float currentValue)
        {
            XOffsetChanged?.Invoke(currentValue);
        }

        private void dataPlot_XScaleChanged(float currentValue)
        {
            XScaleChanged?.Invoke(currentValue);
        }

        private void autoScaleCB_CheckedChanged(object sender, EventArgs e)
        {
            dataPlot.ContinuousAutoScale = autoScaleCB.Checked;
        }
    }
}
