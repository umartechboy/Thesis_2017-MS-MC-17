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
    public partial class CurveMenuItem : BezierBoardItemMenuItem
    {
        public new RotatingBezierSpline Item { get { return (RotatingBezierSpline)base.Item; } }

        public CurveMenuItem(BezierBoardItem item = null):base(item)
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

        public CurveMenuItem(RotatingBezierSpline spline, Image visibleIcon, Image activeIcon, Image visibleIconDull, Image activeIconDull, int sz):this(spline)
        {
            SplineVisible.SetImage(visibleIcon, sz, visibleIconDull);
            SplineEnabled.SetImage(activeIcon, sz, activeIconDull);
            this.textBox1.Text = spline.Label;
            spline.OnAnchorAdded += Spline_OnAnchorAdded;
            spline.WidthChangeRequest += Spline_WidthChangeRequest;
            
            Spline_OnAnchorAdded(spline, new EventArgs());
        }

        private void Spline_WidthChangeRequest(object sender, EventArgs e)
        {
            NeedsToRedrawPreview = true;
        }

        public bool NeedsToRedrawPreview { get; set; } = false;
        public int Index { get; internal set; }

        private void Spline_OnAnchorAdded(object sender, EventArgs e)
        {
            NeedsToRedrawPreview = true;
            foreach (var a in Item.Anchors)
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
            if (Item.BoundingRectangle().Width == 0) return;
            if (Item.BoundingRectangle().Height == 0) return;
            try
            {
                var bmp = new Bitmap((int)Item.BoundingRectangle().Width, (int)Item.BoundingRectangle().Height);
                var g = Graphics.FromImage(bmp);
                g.ScaleTransform(1, -1);
                g.TranslateTransform(0, -bmp.Height);
                g.TranslateTransform(-Item.BoundingRectangle().X, -Item.BoundingRectangle().Y);
                float widBkp = Item.FlatTipWidth;
                if (Item.FlatTipWidth < 2)
                    Item.FlatTipWidth = 2;
                Item.Draw(g, new PointF(), 1, InkDrawMode.Ink, AnchorDrawMode.None, null, null);
                Item.FlatTipWidth = widBkp;
                prevP.BackgroundImage = bmp;
                prevP.BackgroundImageLayout = ImageLayout.Zoom;
            }
            catch { }
        }

        private void visibleTC_OnActivated(object sender, EventArgs e)
        {
            Item.Visible = SplineVisible.Active;
        }

        private void activeTC_OnActivated(object sender, EventArgs e)
        {
            Item.Locked = !SplineEnabled.Active;
        }

        private void appearnceP_MouseClick(object sender, MouseEventArgs e)
        {
            Item.ChangeAppearance();
        }

        private void delP_MouseClick(object sender, MouseEventArgs e)
        {
            Item.SelfRemoveRequest();
        }

        private void prevP_MouseEnter(object sender, EventArgs e)
        {
            Item.MouseState = MouseState.Hover;
            Item.Board.Invalidate();
        }

        private void CurveMenuItem_MouseEnter(object sender, EventArgs e)
        {
            Item.MouseState = MouseState.Hover;
            Item.Board.Invalidate();

        }

        private void prevP_MouseLeave(object sender, EventArgs e)
        {
            Item.MouseState = MouseState.None;
            Item.Board.Invalidate();

        }

        private void CurveMenuItem_MouseLeave(object sender, EventArgs e)
        {
            Item.MouseState = MouseState.None;
            Item.Board.Invalidate();

        }

        private void prevP_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyRequestToShowAll();
        }

        private void unlockAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyRequestToUnlockAll();
        }

        private void showOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyRequestShowOnly();
        }

        private void lockAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyRequestToUnlockOnly();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Item.Label = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NotifyMoveUpRequest();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NotifyMoveDownRequest();
        }
    }
}
