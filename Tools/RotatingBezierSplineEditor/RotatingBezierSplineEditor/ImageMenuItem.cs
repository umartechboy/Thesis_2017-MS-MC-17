using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class ImageMenuItem : BezierBoardItemMenuItem
    {
        public new ImageItem Item { get { return (ImageItem)base.Item; } }
        public ImageMenuItem(ImageItem imageItem):base(imageItem)
        {
            InitializeComponent();
        }
        public ImageMenuItem(ImageItem imageItem, Image visibleIcon, Image activeIcon, Image visibleIconDull, Image activeIconDull, int sz) : this(imageItem)
        {
            SplineVisible.SetImage(visibleIcon, sz, visibleIconDull);
            SplineEnabled.SetImage(activeIcon, sz, activeIconDull);
            prevP.BackgroundImage = new Bitmap(imageItem.SourceImage);
            prevP.BackgroundImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            prevP.BackgroundImageLayout = ImageLayout.Zoom;
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
