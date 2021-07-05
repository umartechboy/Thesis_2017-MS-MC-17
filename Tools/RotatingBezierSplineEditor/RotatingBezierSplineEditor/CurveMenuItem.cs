using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class CurveMenuItem : UserControl
    {
        public RotatingBezierSpline Spline { get; set; }

        public CurveMenuItem()
        {
            var st = DateTime.Now;
            InitializeComponent();
            var s1 = DateTime.Now - st;
            try
            {
                //delTC.SetImage(Image.FromFile("Resources\\delete.png"), 23);
                //appearanceTC.SetImage(Image.FromFile("Resources\\appearance.png"), 23);
            }
            catch { }
            var s2 = DateTime.Now - st;
        }

        public CurveMenuItem(RotatingBezierSpline spline, Image visibleIcon, Image activeIcon, Image visibleIconDull, Image activeIconDull, int sz):this()
        {
            var st = DateTime.Now;
            visibleTC.SetImage(visibleIcon, sz, visibleIconDull);
            activeTC.SetImage(activeIcon, sz, activeIconDull);
            var s1 = DateTime.Now - st;
            this.Spline = spline;
            spline.OnAnchorAdded += Spline_OnAnchorAdded;
            spline.WidthChangeRequest += Spline_WidthChangeRequest;
            
            Spline_OnAnchorAdded(spline, new EventArgs());
            var s2 = DateTime.Now - st;
        }

        private void Spline_WidthChangeRequest(object sender, EventArgs e)
        {
            NeedsToRedrawPreview = true;
        }

        public bool NeedsToRedrawPreview { get; set; } = false;
        private void Spline_OnAnchorAdded(object sender, EventArgs e)
        {
            NeedsToRedrawPreview = true;
            foreach (var a in Spline.Anchors)
            {
                a.OnShapeChanged -= A_OnShapeChanged;
                a.OnShapeChanged += A_OnShapeChanged;
            }
        }

        private void A_OnShapeChanged(object sender, EventArgs e)
        {
            NeedsToRedrawPreview = true;
        }

        internal void RecomputePreview()
        {
            if (Spline.BoundingRectangle().Width == 0) return;
            if (Spline.BoundingRectangle().Height == 0) return;
            var bmp = new Bitmap((int)Spline.BoundingRectangle().Width, (int)Spline.BoundingRectangle().Height);
            var g = Graphics.FromImage(bmp);
            g.ScaleTransform(1, -1);
            g.TranslateTransform(0, -bmp.Height);
            g.TranslateTransform(-Spline.BoundingRectangle().X , -Spline.BoundingRectangle().Y);
            float widBkp = Spline.FlatTipWidth;
            if (Spline.FlatTipWidth < 2)
                Spline.FlatTipWidth = 2;
            Spline.Draw(g, InkDrawMode.Ink, AnchorDrawMode.None, null, null);
            Spline.FlatTipWidth = widBkp;
            prevP.BackgroundImage = bmp;
            prevP.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void visibleTC_OnActivated(object sender, EventArgs e)
        {
            Spline.Visible = visibleTC.Active;
        }

        private void activeTC_OnActivated(object sender, EventArgs e)
        {
            Spline.Locked = !activeTC.Active;
        }

        private void appearnceP_MouseClick(object sender, MouseEventArgs e)
        {
            Spline.ChangeAppearance();
        }

        private void delP_MouseClick(object sender, MouseEventArgs e)
        {
            Spline.SelfRemoveRequest();
        }

        private void prevP_MouseEnter(object sender, EventArgs e)
        {
            Spline.MouseState = MouseState.Hover;
            Spline.Board.Invalidate();
        }

        private void CurveMenuItem_MouseEnter(object sender, EventArgs e)
        {
            Spline.MouseState = MouseState.Hover;
            Spline.Board.Invalidate();

        }

        private void prevP_MouseLeave(object sender, EventArgs e)
        {
            Spline.MouseState = MouseState.None;
            Spline.Board.Invalidate();

        }

        private void CurveMenuItem_MouseLeave(object sender, EventArgs e)
        {
            Spline.MouseState = MouseState.None;
            Spline.Board.Invalidate();

        }
    }
}
