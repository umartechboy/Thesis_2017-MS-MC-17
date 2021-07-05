using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SplineDesigner
{
    [Serializable]
    public class SplineCollectionBoard:Panel
    {                                                                       
        public List<BezierSpline> Splines = new List<BezierSpline>();
        public SplineCollectionBoard()
        {
            DoubleBuffered = true;
            MouseDown += SplineCollectionBoard_MouseDown;
            MouseMove += SplineCollectionBoard_MouseMove;
            MouseUp += SplineCollectionBoard_MouseUp;
            MouseDoubleClick += SplineCollectionBoard_MouseDoubleClick;
        }

        private void SplineCollectionBoard_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ActiveSpline == null)
                return;
            SplineEditorForm sef = new SplineDesigner.SplineEditorForm();
            sef.splineEditor1.bezierBoard1.Spline = ActiveSpline;
            sef.ShowDialog();
            Invalidate();
        }

        ops Op = ops.None;
        private void SplineCollectionBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (Op == ops.None)
            {
                if (ActiveSpline != null)
                {
                    if (e.Button == MouseButtons.Left)
                        Op = ops.Moving;
                    else if (e.Button == MouseButtons.Right)
                        Op = ops.Scaling;
                    DownAt = LastCursor;
                }
            }
            else
                Op = ops.None;
        }
                                       
        BezierSpline ActiveSpline = null;
        Point LastCursor = new Point();
        Point DownAt = new Point();
        double lastScaleD = 0;
        private void SplineCollectionBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (Op == ops.None)
            {
                bool contains = false;
                foreach (var spline in Splines)
                {
                    if (spline.Contains(e.Location))
                    {
                        Cursor = Cursors.Hand;
                        ActiveSpline = spline;
                        contains = true;
                    }                    
                }
                if (!contains)               
                    Cursor = Cursors.Default;
            }
            else if (Op == ops.Moving)
            {
                int changeX = e.Location.X - LastCursor.X;
                int changeY = e.Location.Y - LastCursor.Y;
                foreach (var ap in ActiveSpline.Anchors)
                {
                    ap.P1.X += changeX;
                    ap.a1.X += changeX;
                    ap.a2.X += changeX;
                    ap.r1.X += changeX;
                    ap.r2.X += changeX;

                    ap.P1.Y += changeY;
                    ap.a1.Y += changeY;
                    ap.a2.Y += changeY;
                    ap.r1.Y += changeY;
                    ap.r2.Y += changeY;
                }
                Invalidate();
            }
            else if (Op == ops.Scaling)
            {
                double d1 = Math.Sqrt(Math.Pow(e.Location.X - 0, 2) +
                Math.Pow(e.Location.Y - 0, 2));    
                double fac = d1 / 5000 + 1;
                if (d1 < lastScaleD)
                    fac = -d1 / 5000 + 1;
                lastScaleD = d1;
                                   

                PointD center = new SplineDesigner.PointD();
                foreach (var ap in ActiveSpline.Anchors)
                {
                    center.X += ap.P1.X;
                    center.Y += ap.P1.Y;
                }
                center.X /= ActiveSpline.Anchors.Count;
                center.Y /= ActiveSpline.Anchors.Count;
                ActiveSpline.FlatTipThickness *= (float)fac;
                foreach (var ap in ActiveSpline.Anchors)
                {
                    ap.P1 = PointD.Intermediate(center, ap.P1, 1 - fac);
                    ap.a1 = PointD.Intermediate(center, ap.a1, 1 - fac);
                    ap.a2 = PointD.Intermediate(center, ap.a2, 1 - fac);
                    ap.r1 = PointD.Intermediate(center, ap.r1, 1 - fac);
                    ap.r2 = PointD.Intermediate(center, ap.r2, 1 - fac);
                }
                Invalidate();
            }
            LastCursor = e.Location;
        }

        private void SplineCollectionBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (Op == ops.Moving)
            {
                Op = ops.None;
            }
            else if (Op == ops.Scaling)
            {
                Op = ops.None;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var spline in Splines)
            {
                spline.Draw(e.Graphics, System.Drawing.Pens.Blue, false);
            }
        }
        enum ops { Move, Scale, None, Moving, Scaling};
    }
}
