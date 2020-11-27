using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public class ToolControl : Panel
    {
        public event EventHandler OnActivated;
        public ToolControl()
        {
            DoubleBuffered = true;
            MouseEnter += (s, e) => { containsMouse = true; Invalidate(); };
            MouseLeave += (s, e) => { containsMouse = false; Invalidate(); };
            Click += (s, e) =>
            {
                Active = true;
                OnActivated?.Invoke(this, new EventArgs());
            };
        }
        Image icon;
        Image dull;
        bool _active = false;
        public bool Active
        {
            get { return _active; }
            set
            {
                if (value && Parent != null)
                    foreach (Control c in Parent.Controls)
                        if (c is ToolControl)
                            ((ToolControl)c).Active = false;
                _active = value;
                Invalidate();
            }
        }
        int size;
        public void SetImage(Image Image, int size)
        {
            this.size = size;
            icon = Image;
            var bmp = new Bitmap(Image);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    var c = bmp.GetPixel(x,y);
                    var g = (c.R + c.G + c.B) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(c.A == 0 ? 0 : 60, g, g, g));
                }
            }
            dull = bmp;
        }
        bool containsMouse = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            if (icon == null)
                return;
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, size, (size * icon.Height) / icon.Width);
            rect.Inflate(-3, -3);
            if (containsMouse)
                rect.Inflate(3, 3);
            g.DrawImage(Active ? icon : dull, Width / 2 - rect.Width / 2, Height / 2 - rect.Height / 2, rect.Width, rect.Height);
        }
    }
}
