using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MagneticPendulum
{
    public class ChartPanel:Panel
    {
        public ChartPanel()
        {
            DoubleBuffered = true;
            Timer t = new Timer();
            t.Interval = 20;
            t.Tick += T_Tick;
            t.Enabled = true;
        }

        bool needsRefresh = true;
        private void T_Tick(object sender, EventArgs e)
        {
            if (needsRefresh) return;
            Invalidate();
            Application.DoEvents();
            needsRefresh = false;
        }

        PointF[] Data = new PointF[1000 * 60 * 360];
        int count = 0;
        public void Add(double x, double y)
        {
            Data[count] = new PointF((float)x, (float)y);
            count++;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
        }
    }
}
