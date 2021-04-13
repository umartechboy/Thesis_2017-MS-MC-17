using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drogon3
{
    public partial class SplineRasterizationProgress : Form
    {
        public SplineRasterizationProgress()
        {
            InitializeComponent();

        }
        public double Max { get; set; } = 1;
        public int UnitsCount { get; set; }
        int mV = 0;
        int uV = 0;
        public void Update(int ind, float f)
        {
            var tF = ind * Max + f;
            var mF = tF / (UnitsCount * Max);
            mV = (int)(mF * 100);
            uV = (int)(f * 100);
            Application.DoEvents();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                pm.Value = mV;
                pcm.Text = pm.Value.ToString();
                pu.Value = uV;
                pcu.Text = pu.Value.ToString();
            }));
        }

        private void SplineRasterizationProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
