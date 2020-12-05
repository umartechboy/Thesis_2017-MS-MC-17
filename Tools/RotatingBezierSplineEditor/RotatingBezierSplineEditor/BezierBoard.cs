using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public class BezierBoard : Panel
    {
        AnchorDrawMode _AnchorDrawMode = AnchorDrawMode.None;
        InkDrawMode _InkDrawMode = InkDrawMode.Ink | InkDrawMode.Spline;
        public InkDrawMode InkDrawMode { get { return _InkDrawMode; } set { InkDrawMode = value; Invalidate(); } }
        public AnchorDrawMode AnchorDrawMode { get { return _AnchorDrawMode; } set { _AnchorDrawMode = value; Invalidate(); } }
        public List<RotatingBezierSpline> Paths = new List<RotatingBezierSpline>();
        public BezierBoard()
        {
            DoubleBuffered = true;
            MouseMove += BezierBoard_MouseMove;
            MouseDown += BezierBoard_MouseDown;
            MouseUp += BezierBoard_MouseUp;
        }


        private void BezierBoard_MouseUp(object sender, MouseEventArgs e)
        {
            // let's find if an element wants to have the focus
            foreach (var path in Paths)
            {
                path.MouseState = MouseState.None;
                foreach (var anchor in path.Anchors)
                {
                    foreach (var anchorPart in anchor.Points)
                        if (anchorPart.MouseState == MouseState.Hover)
                        {
                            anchorPart.MouseState = MouseState.Selected;
                            path.MouseState = MouseState.Selected;
                            Invalidate();
                            return;
                        }
                }
            }
        }

        private void BezierBoard_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var path in Paths)
            {
                if (path.MouseState == MouseState.Hover) // latch on
                {
                    path.NitifyMouseDown(e.Location);
                    Invalidate();
                    return;
                }
                foreach (var anchor in path.Anchors)
                    foreach (var anchorPart in anchor.Points)
                        if (anchorPart.MouseState == MouseState.Held)
                        {
                            anchorPart.NitifyMouseDown(e.Location);
                            Invalidate();
                            return;
                        }
            }
            foreach (var path in Paths)
            {
                if (path.Anchors.Count > 0)
                {
                    if (path.Anchors.First().P.MouseState == MouseState.Selected && !path.Anchors.First().ContainsMouse(e.Location))
                    {
                        path.Anchors.First().P.MouseState = MouseState.None;
                        path.Anchors.Insert(0, new RotatingBezierSplineAnchor(e.Location));
                        path.Anchors.First().C1.MouseState = MouseState.Held;
                    }
                }
            }
        }

        PointF lastMousePosition;
        private void BezierBoard_MouseMove(object sender, MouseEventArgs e)
        {
            PointF lastMousePositionBkp = lastMousePosition;
            lastMousePosition = e.Location;
            foreach (var path in Paths)
            {
                if (path.MouseState == MouseState.Held) // can't initiate a new hold/hover now
                {
                    path.ProcessMouse(e.Location, e.X - lastMousePositionBkp.X, e.Y - lastMousePositionBkp.Y);
                    return;
                }
                foreach (var anchor in path.Anchors)
                    foreach (var anchorPart in anchor.Points)
                        if (anchorPart.MouseState == MouseState.Held)
                        {
                            anchorPart.ProcessMouse(e.Location, e.X - lastMousePositionBkp.X, e.Y - lastMousePositionBkp.Y);
                            return;
                        }
            }
            // let's find if an element wants to have the focus
            foreach (var path in Paths)
            {
                path.MouseState = MouseState.None;
                foreach (var anchor in path.Anchors)
                    foreach (var anchorPart in anchor.Points)
                        anchorPart.MouseState = MouseState.None;
            }
            foreach (var path in Paths)
            {
                if (path.ContainsMouse(e.Location))
                {
                    path.MouseState = MouseState.Hover;
                    Invalidate();
                    return;
                }
                foreach (var anchor in path.Anchors)
                    foreach (var anchorPart in anchor.Points)
                        if (anchorPart.ContainsMouse(e.Location))
                        {
                            anchorPart.MouseState = MouseState.Hover;
                            Invalidate();
                            return;
                        }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var curve in Paths)
                curve.Draw(e.Graphics, InkDrawMode, AnchorDrawMode);
        }

    }
    public enum InkDrawMode
    {
        None = 0,
        Spline = 1,
        Ink = 2,
        Both = Spline | Ink
    }
    public enum AnchorDrawMode
    {
        None = 0,
        Centers = 1,
        RotaionHandles = 2,
        CurvatureHandles = 4,
    }
}
