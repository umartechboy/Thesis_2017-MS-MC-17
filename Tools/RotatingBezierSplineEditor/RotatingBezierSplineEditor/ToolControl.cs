using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    /// <summary>
    /// This control is similar to RadioButton. Only one control of this type can be "Active" in one container. A Click on a control will Activate it, thus changing the images.
    /// Whenever the active state is changed, an "OnActivated" event is called.
    /// It doesn't use Panel's own Background image. Background image drawing is handles outside OnPaint.
    /// Instead, use SetImage to set the icon image. This method also creates a dull loking version of the supplied image 
    /// by changing the R,G,B components of all the non-transparent pixels to a mean Gray value lower opacity. 
    /// Size is implemented to support fit resizing of images that are bigger than control. This resizing should be automatic.
    /// The control also supports a hover indication scheme. The icons are enlarged by a few pixels while mouse curse is inside the bounds
    /// </summary>
    public class ToolControl : Panel
    {
        public event EventHandler OnActivated;
        public ToolControl()
        {
            DoubleBuffered = true;

            // to handle mouse hover, we need to know when the mouse enters and leaves.
            MouseEnter += (s, e) => { containsMouseCursor = true; Invalidate(); };
            MouseLeave += (s, e) => { containsMouseCursor = false; Invalidate(); };
            // to implement "Active" changing like radio buttons
            Click += (s, e) => Active = true;
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
                if (value != _active)
                {
                    // change the value first
                    _active = value;
                    // redraw using OnPaint
                    Invalidate();
                    // raise the events
                    OnActivated?.Invoke(this, new EventArgs());
                }
            }
        }
        int size;
        /// <summary>
        /// This is alternate to BackgroundImage creates a dull looking copy of the image as well.
        /// </summary>
        /// <param name="Image">Image to be used base icon</param>
        /// <param name="size">size of the rendered icon</param>
        public void SetImage(Image Image, int size)
        {
            this.size = size;
            icon = Image;
            // make new memory space for the dull image
            var bmp = new Bitmap(Image);
            // this method can be made fast by using Unsafe access. Create BitmapData using the specs of this bmp, "UnlockBits" on the bitmap using the new bitmap data. Read Scan0 of bmpData using Marshal.Copy, alter it, put back the data by locking the bits.
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    var c = bmp.GetPixel(x,y);
                    var g = (c.R + c.G + c.B) / 3;
                    // get a gray level
                    bmp.SetPixel(x, y, Color.FromArgb(c.A == 0 /*Do it only on transparent pixels*/? 0 : 60, g, g, g));
                }
            }
            dull = bmp;
        }
        bool containsMouseCursor = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            if (icon == null) // this is needed for the UI Editor which is not familiar with our objects and will call this function regardless of the values to show in the editor.
                return;
            var g = e.Graphics;
            // smoothing is slower but needed for resizing lines
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, size, (size * icon.Height) / icon.Width);
            rect.Inflate(-3, -3);
            if (containsMouseCursor)
                rect.Inflate(3, 3);
            g.DrawImage(Active ? icon : dull, Width / 2 - rect.Width / 2, Height / 2 - rect.Height / 2, rect.Width, rect.Height);
        }
    }
    public class AnchorEditToolControl : ToolControl
    {
        public AnchorParts TargetPart { get; set; }
    }
}
