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
    public class MagPendulumVisualizer:Panel
    {
        public MagPendulumVisualizer()
        {
            DoubleBuffered = true;
            Timer t = new Timer();
            t.Interval = 20;
            t.Tick += T_Tick;
            t.Enabled = true;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (needsRefresh)
                Invalidate();
            needsRefresh = false;
        }

        public float mAngle = 0, pAngle;
        public float MotorAngle { get { return mAngle; } set { mAngle = value;  needsRefresh = true; } }
        public float PendulumAngle { get { return pAngle; } set { pAngle = value; needsRefresh = true; } }
        bool needsRefresh = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.ScaleTransform(0.3F, 0.3F);
            g.Clear(BackColor);
            float crankR = 10;
            PointF motorCenter = new PointF(41.25F, 73.25F);
                      
            float xMainOffset = crankR * (float)Math.Cos(mAngle) - crankR;
            //Base
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 17, 373, 417, 11);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 17, 373, 417, 11);
            //Column 1
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 39, 43, 6, 330);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 39, 43, 6, 330);
            //Column 2
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 225, 43, 7, 330);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 225, 43, 7, 330);
            // Top Panel
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 19, 35, 265, 77);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 19, 35, 265, 77);
            // Motor
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 28F, 60F, 26.5F, 26.5F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 28F, 60F, 26.5F, 26.5F);
            // Piston Rod 1
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 56, 44, 269, 5);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 56, 44, 269, 5);
            // Piston Rod 2
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 56, 96, 269, 6);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 56, 96, 269, 6);
            // Slide Top Right
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 242.9377F, 39F, 15F, 15F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 242.9377F, 39F, 15F, 15F);
            // Slide Top Left
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 90F, 39F, 15F, 15F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 90F, 39F, 15F, 15F);
            // Slide Bottom Right
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 242.9377F, 90.93774F, 15F, 15F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 242.9377F, 90.93774F, 15F, 15F);
            // Slide Bottom Left
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 90, 90.93774F, 15F, 15F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 90, 90.93774F, 15F, 15F);
            // Crank Holder
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 50, 39, 6, 68);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 50, 39, 6, 68);
            // Slide Support            
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 157, 40, 37, 66);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 157, 40, 37, 66);
            // Angle Sensor
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 324, 37, 32, 74);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 324, 37, 32, 74);
            float PendulumLength = 218 - 72;
            float x2o = (float)(PendulumLength* Math.Sin(pAngle));
            float y2o = (float)(PendulumLength * Math.Cos(pAngle)) - PendulumLength;
            // Pendulum
            g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 339, 72, xMainOffset + 339 + x2o, 72 + PendulumLength + y2o);
            //PendulumMagnet
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 329F + x2o, 207.7805F + y2o, 18.43909F, 18.43909F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 329F + x2o, 207.7805F + y2o, 18.43909F, 18.43909F);
            //Pendulum Hinge
            g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), xMainOffset + 332.9172F, 65.91724F, 12.16553F, 12.16553F);
            g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), xMainOffset + 332.9172F, 65.91724F, 12.16553F, 12.16553F);
            ////Jack
            //g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 267, 261, 145, 16);
            //g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 267, 261, 145, 16);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 283, 278, 371, 318);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 371, 319, 288, 372);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 307, 278, 397, 318);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 397, 318, 310, 373);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 370, 319, 384, 311);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 397, 277, 355, 299);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 373, 277, 343, 293);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 332, 300, 293, 321);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 292, 321, 330, 345);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 339, 354, 368, 371);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 391, 371, 352, 345);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 341, 338, 304, 315);
            //g.DrawLine(new Pen(Color.FromArgb(0, 0, 0), 0), 314, 323, 344, 306);
            ////Jack
            //g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 339F, 342F, 6F, 6F);
            //g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 339F, 342F, 6F, 6F);
            ////Jack
            //g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 341.8377F, 296.8377F, 6.324555F, 6.324555F);
            //g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 341.8377F, 296.8377F, 6.324555F, 6.324555F);
            ////Jack
            //g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 378.8377F, 316.8377F, 6.324555F, 6.324555F);
            //g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 378.8377F, 316.8377F, 6.324555F, 6.324555F);
            ////Jack
            //g.FillEllipse(new SolidBrush(Color.FromArgb(240, 240, 240)), 301.8377F, 318.8377F, 6.324555F, 6.324555F);
            //g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 0), 0), 301.8377F, 318.8377F, 6.324555F, 6.324555F);
            ////Jack
            //g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 275, 360, 123, 13);
            //g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 275, 360, 123, 13);
            //Magnet
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 329, 245, 23, 16);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 329, 245, 23, 16);
            // holder
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 335, 261, 11, 16);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 335, 261, 11, 50);
            // holder 2
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), 330, 311, 21, 62);
            g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0), 0), 330, 311, 21, 62);


        }
    }
}
