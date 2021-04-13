using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace Drogon3
{
    public class xyJoyStick:DBPanel
    {
        public xyJoyStick()
        {
            MouseMove += XyJoyStick_MouseMove;
            MouseDown += XyJoyStick_MouseDown;
            MouseUp += XyJoyStick_MouseUp;
            HandleLocation = Origin;
            HandleLocation.Offset(0, 0, 20);
            SizeChanged += XyJoyStick_SizeChanged;
        }
        Point3D DragOffset;
        private void XyJoyStick_SizeChanged(object sender, EventArgs e)
        {
            HandleLocation = Origin;
        }

        private void XyJoyStick_MouseUp(object sender, MouseEventArgs e)
        {
            HeldFrom = null;
            HandleLocation = Origin;
            HandleHeight += 10;
            DX = 0;
            DY = 0;
            Invalidate();
        }
        public double DX { get; private set; }
        public double DY { get; private set; }
        Point3D cursorLocation;
        Point3D HandleLocation;
        float HandleHeight = 10;
        private void XyJoyStick_MouseMove(object sender, MouseEventArgs e)
        {
            cursorLocation = new Point3D(e.X, e.Y, 0);
            if (HeldFrom != null)
            {
                var delta = cursorLocation - new Point3D(((Point3D)HeldFrom).X, ((Point3D)HeldFrom).Y, Origin.Z);
                var r = Math.Min(delta.Length, (Width - HandleDia) / 2);
                DX = delta.X / ((Width - HandleDia) / 2);
                DY = -delta.Y / ((Width - HandleDia) / 2);
                var theta = Math.Atan2(delta.Y, delta.X);
                HandleLocation = new Point3D(r * Math.Cos(theta)+Origin.X , r * Math.Sin(theta) + Origin.Y, HandleLocation.Z);

                Invalidate();
            }
            else
            {
                if ((HandleLocation - cursorLocation).Length < HandleDia / 2)
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;
            }
        }

        double HandleDia = 50;
        object HeldFrom = null;
        Point3D Origin { get { return new Point3D(Width / 2, Height / 2, 0); } }
        private void XyJoyStick_MouseDown(object sender, MouseEventArgs e)
        {
            if ((Origin - cursorLocation).Length < HandleDia / 2)
            {
                HeldFrom = cursorLocation;
                HandleLocation = Origin;
                HandleHeight -= 10;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);
            g.DrawEllipse(Pens.Black, 0, 0, Width - 1, Height - 1);

            DrawEllipse(g, (float)Origin.X, (float)Origin.Y, Color.FromArgb(255, 150, 170, 180), Color.FromArgb(255, 60, 75, 90), (float)this.Width);
            DrawEllipse(g, (float)HandleLocation.X, (float)HandleLocation.Y, Color.FromArgb(150, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), (float)this.HandleDia  + HandleHeight * 3);
            DrawEllipse(g, (float)HandleLocation.X, (float)HandleLocation.Y, Color.FromArgb(255, 250, 70, 70), Color.FromArgb(255, 170, 16, 16), (float)this.HandleDia + HandleHeight);
        }
        static void DrawEllipse(Graphics g, float x, float y, Color inner, Color outer, float HandleDia)
        {
            g.TranslateTransform(x, y);
            var rect = new RectangleF(-(float)HandleDia / 2, -(float)HandleDia / 2, (float)HandleDia, (float)HandleDia);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rect);

            // Use the path to construct a brush.
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);

            // Set the color at the center of the path to blue.
            pthGrBrush.CenterColor = inner;

            // Set the color along the entire boundary
            // of the path to aqua.
            Color[] colors = { outer };
            pthGrBrush.SurroundColors = colors;

            g.FillEllipse(pthGrBrush, rect);
            g.TranslateTransform(-x, -y);
        }
    }
}
