using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RoboSim
{
    public class ExplorablePanelExt
    {
        public PointF Translation;
        Point panPanelLastCursor = new Point();
        bool panPanelMouseIsDown = false;
        public bool InvertY = false;
        public bool LockX = false;
        public bool LockY = false;
        public void SubscribePanel(Panel p, PointF startingTranslation, bool invertY_ = true)
        {
            Translation = startingTranslation;
            p.MouseEnter += MouseEnter;
            p.MouseLeave += MouseLeave;
            p.MouseMove += MouseMove;
            p.MouseDown += MouseDown;
            p.MouseUp += MouseUp;
            InvertY = invertY_;
        }
        public void UnsubscribePanel(Panel p)
        {                                     
            p.MouseEnter -= MouseEnter;
            p.MouseLeave -= MouseLeave;
            p.MouseMove -= MouseMove;
            p.MouseDown -= MouseDown;
            p.MouseUp -= MouseUp;
        }

        private void MouseEnter(object sender, EventArgs e)
        {
            ((Panel)sender).Cursor = Cursors.Cross;
        }

        private void MouseLeave(object sender, EventArgs e)
        {
            ((Panel)sender).Cursor = Cursors.Default;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (panPanelMouseIsDown)
            {
                if (InvertY)
                    Translation = new PointF(
                    Translation.X + (LockX ? 0 : (e.X - panPanelLastCursor.X)),
                    Translation.Y - (LockY ? 0 : (e.Y - panPanelLastCursor.Y)));
                else
                    Translation = new PointF(
                        Translation.X + (LockX ? 0 : (e.X - panPanelLastCursor.X)),
                        Translation.Y + (LockY ? 0 : (e.Y - panPanelLastCursor.Y)));
            }
            panPanelLastCursor = e.Location;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panPanelMouseIsDown = true;
                ((Panel)sender).Cursor = Cursors.Hand;
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            panPanelMouseIsDown = false;
            ((Panel)sender).Cursor = Cursors.Cross;
        }
    }
}
