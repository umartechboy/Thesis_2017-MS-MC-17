using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public delegate void IncrementLocationHandler(float x, float y);
    public enum MouseState
    {
        None,
        Hover,
        Held,
        Selected,
    }
    public class BezierBoardItem
    {
        public string Tag = "";
        public event EventHandler OnSelected;
        public event IncrementLocationHandler IncrementLocationRequest;
        public event EventHandler LocationResetRequest;
        public virtual float Left { get; set; } = 0;
        public virtual float Top { get; set; } = 0;
        public virtual float Width { get; set; } = 0;
        public virtual float Height { get; set; } = 0;
        public virtual RectangleF BoundsOnTopParent
        {
            get
            { return new RectangleF(Left, Top, Width, Height); }
        }
        BezierBoardItem sisterItemUnderMouseMove;
        PointF lastMouseMove;
        PointF MouseDownAt;
        public void NotifyLocationIncrementRequest(float dx, float dy)
        { IncrementLocationRequest?.Invoke(dx, dy); }
        public void NotifySelected()
        {
            OnSelected?.Invoke(this, new EventArgs());
        }
        public bool CanBeSelected { get; set; } = false;
        public BezierBoardItem()
        {
            OnMouseEnter += (s, e) => { MouseState = MouseState.Hover; };
            OnMouseLeave += (s, e) => { if (!CanBeSelected) MouseState = MouseState.None; };
            OnMouseDown += (s, e) => { MouseState = MouseState.Held; MouseDownAt = e.Location; };
            OnMouseUp += (s, e) =>
            {
                if (MouseState == MouseState.Held)
                    LocationResetRequest?.Invoke(this, new EventArgs());
                if (CanBeSelected)
                {
                    MouseState = MouseState.Selected;
                    NotifySelected();
                }
                else
                    MouseState = MouseState.Hover;
            };
            OnMouseMove += (s, e) =>
            {
                if (MouseState == MouseState.Held)
                {
                    NotifyLocationIncrementRequest(e.X - lastMouseMove.X, e.Y - lastMouseMove.Y);
                }
                lastMouseMove = e.Location;
            };
        }
        public virtual List<BezierBoardItem> GetChildren()
        { throw new NotImplementedException(); }
        public MouseState MouseState { get; set; } = MouseState.None;
        public virtual Cursor Cursor { get; set; } = Cursors.No;
        public PointF MousePosition;
        bool wentDownInBounds = false;
        bool lastwasOutside = true;
        Cursor cursorBkp;
        public event MouseEventHandler OnMouseLeave;
        public event MouseEventHandler OnMouseMove;
        public event MouseEventHandler OnMouseEnter;
        public event MouseEventHandler OnMouseDown;
        public event MouseEventHandler OnMouseUp;
        public void NotifyMouseLeave(object sender, MouseEventArgs e, BezierBoardItem Parent, Control ParentControl)
        {
            lastwasOutside = true;
            OnMouseLeave?.Invoke(sender, e);
            if (Parent.Cursor == Cursors.No)
                ParentControl.Cursor = Cursors.Default;
            else ParentControl.Cursor = Parent.Cursor;
        }
        public void NotifyMouseDown(object sender, MouseEventArgs e)
        {
            lastMouseMove = e.Location;
            wentDownInBounds = true;
            lastwasOutside = false;
            OnMouseDown?.Invoke(sender, e);
        }
        public void NotifyMouseUp(object sender, MouseEventArgs e) { OnMouseUp?.Invoke(sender, e); }
        public void NotifyMouseMove(object sender, MouseEventArgs e) { OnMouseMove?.Invoke(sender, e); }
        public void NotifyMouseEnter(object sender, MouseEventArgs e) { OnMouseEnter?.Invoke(sender, e); }
        public virtual bool ProcessMouseMove(MouseEventArgs e, BezierBoardItem filter, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            // cache the position.
            if (this.Tag == "C")
                ;
            else if (Tag == "obj")
                ;
            else if (Tag == "anchor")
                ;
            MousePosition = new PointF(e.X, e.Y);
            // resolve any debts to previous relationships
            if (!CanDraw(inkDrawMode, anchorDrawMode))
                return false;
            if (TopParentBoundsContains(e.Location))
            {
                // before returning, call mouse leave on sister controls that had mouse hover
                if (Parent != null)
                {
                    if (Parent.sisterItemUnderMouseMove != null && Parent.sisterItemUnderMouseMove != this)
                        Parent.sisterItemUnderMouseMove.NotifyMouseLeave(this, e, this, ParentControl);
                    Parent.sisterItemUnderMouseMove = this;
                }
            }
            // don't receive an event if not visible.

            if (filter != this && !wentDownInBounds)
            {
                // ask the children in reverse order
                var controls = GetChildren();
                for (int i = controls.Count - 1; i >= 0; i--)
                {
                    if (controls[i].ProcessMouseMove(e, filter, inkDrawMode, anchorDrawMode, this, ParentControl))
                    {
                        return true;
                    }
                }
            }
            sisterItemUnderMouseMove = null;
            if (filter == null || filter == this || wentDownInBounds)
            {
                if (this is CenterPoint)
                    ;
                var e2 = new MouseEventArgs(e.Button, e.Clicks, (int)(e.X - BoundsOnTopParent.X), (int)(e.Y - BoundsOnTopParent.Y), e.Delta);
                if (TopParentBoundsContains(e.Location) || wentDownInBounds)
                {
                    if (lastwasOutside)
                    {
                        lastwasOutside = false;
                        cursorBkp = ParentControl.Cursor;
                        NotifyMouseEnter(this, e2);
                        if (Cursor == Cursors.No)
                            ParentControl.Cursor = Cursors.Default;
                        else ParentControl.Cursor = Cursor;
                    }
                    NotifyMouseMove(this, e);
                    return true;
                }
                else if (!lastwasOutside)
                {
                    lastwasOutside = true;
                    NotifyMouseLeave(this, e2, this, ParentControl);
                    NotifyMouseMove(this, e2);

                    if (Parent == null)
                        ParentControl.Cursor = Cursors.Default;
                    else if (Parent.Cursor == Cursors.No)
                        ParentControl.Cursor = Cursors.Default;
                    else ParentControl.Cursor = Parent.Cursor;
                    // don't return true.
                }
            }
            return false;
        }
        public virtual BezierBoardItem ProcessMouseDown(MouseEventArgs e, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            // devide if we need to receive mouse down and up events.
            if (!CanDraw(inkDrawMode, anchorDrawMode))
                return null;

            // first ask the children

            var controls = GetChildren();
            for (int i = controls.Count - 1; i >= 0; i--)
            {
                var cr = controls[i].ProcessMouseDown(e, inkDrawMode, anchorDrawMode, this, ParentControl);
                if (cr != null)
                    return cr;
            }
            if (TopParentBoundsContains(e.Location))
            {
                NotifyMouseDown(this, e);
                return this;
            }
            return null;
        }

        public virtual void ProcessMouseUp(object sender, MouseEventArgs e, BezierBoardItem filter, BezierBoardItem Parent, Control ParentControl)
        {
            if (filter != this)
            {
                var controls = GetChildren();
                for (int i = controls.Count - 1; i >= 0; i--)
                {
                    controls[i].ProcessMouseUp(this, e, filter, this, ParentControl);
                }
            }
            if (filter == this || filter == null)
            {
                if (TopParentBoundsContains(e.Location) || wentDownInBounds)
                {
                    NotifyMouseUp(sender, e);
                }
            }
            wentDownInBounds = false;
        }
        public virtual bool TopParentBoundsContains(PointF p)
        {
            return BoundsOnTopParent.Contains(p);
        }
        public virtual void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        { throw new NotImplementedException(); }
        public virtual bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
    }
    public class CenterPoint : CPoint
    {
        public override Cursor Cursor => Cursors.No;
        public CenterPoint(float x, float y) : base(x, y)
        {
            CanBeSelected = true;
        }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return anchorDrawMode.HasFlag(AnchorDrawMode.Centers) & inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            float sz = 4F;
            if (MouseState == MouseState.Hover)
                sz = 6;
            g.FillEllipse((MouseState == MouseState.Held || MouseState == MouseState.Selected) ? Brushes.Black : Brushes.Gray, (float)X - sz, (float)Y - sz, sz * 2, sz * 2);
        }
    }
    public class RotationHandlePoint : CPoint
    {
        Cursor _c;
        public override Cursor Cursor { get { if (_c == null) _c = new Cursor("resources\\rotation_icon.ico"); return _c; } }
        public static float HandleLength = 50;
        public RotationHandlePoint(double x, double y) : base((float)x, (float)y)
        { }
        public RotationHandlePoint(CenterPoint cp, double angle) : base(cp.X + HandleLength * Math.Cos(angle) , cp.Y + HandleLength * Math.Sin(angle))
        { }
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            float sz = 3F;
            if (MouseState == MouseState.Hover)
                sz = 6;

            g.DrawEllipse((MouseState == MouseState.Held || MouseState == MouseState.Selected) ? Pens.Black : Pens.Gray, (float)X - sz, (float)Y - sz, sz * 2, sz * 2);
        }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return anchorDrawMode.HasFlag(AnchorDrawMode.RotaionHandles) & inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
    }
    public class CurvatureHandlePoint : CPoint
    {
        public override Cursor Cursor => Cursors.Hand;
        public CurvatureHandlePoint(PointF p) : this(p.X, p.Y)
        { }
        public CurvatureHandlePoint(float x, float y) : base(x, y)
        { }
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            float sz = 3F;
            if (MouseState == MouseState.Hover)
                sz = 6;
            if (MouseState == MouseState.Held || MouseState == MouseState.Selected)
                g.FillRectangle(Brushes.Black, (float)X - sz, (float)Y - sz, sz * 2, sz * 2);
            else
                g.DrawRectangle(Pens.DarkGray, (float)X - sz, (float)Y - sz, sz * 2, sz * 2);
        }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return anchorDrawMode.HasFlag(AnchorDrawMode.CurvatureHandles) & inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
    }
    public class CPoint:BezierBoardItem
    {
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public override float Left { get { return X - 4; } set { X = value + 4; }  }
        public override float Top { get { return Y - 4; } set { Y = value + 4; } }
        public override float Width { get; set; } = 8;
        public override float Height { get; set; } = 8;
        public override List<BezierBoardItem> GetChildren()
        {
            return new List<BezierBoardItem>();
        }
        public static implicit operator PointF(CPoint p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }
        public static implicit operator Point(CPoint p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
        public bool IsCloseBy(PointF P, int tol = 3)
        {
            return Math.Sqrt(Math.Pow(P.X - X, 2) + Math.Pow(P.Y - Y, 2)) < 3;
        }
        public CPoint(double x = 0, double y = 0)
        {
            X = (float)x; Y = (float)y;
        }
        public static CPoint Intermediate(CPoint a, CPoint b, double frac = 0.5)
        {
            return new CPoint(a.X * frac + b.X * (1 - frac), a.Y * frac + b.Y * (1 - frac));
        }
        public double DistanceFrom(CPoint p)
        {
            return DistanceBetween(this, p);
        }
        public static double DistanceBetween(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
        }
        public static double DistanceBetween(PointF p1, PointF p2)
        {
            return DistanceBetween(p1.X, p1.Y, p2.X, p2.Y);
        }
        public static double DistanceBetween(CPoint p1, CPoint p2)
        {
            return DistanceBetween(p1.X, p1.Y, p2.X, p2.Y);
        }
        public double AngleBetween(CPoint p, CPoint c)
        {
            return Math.Atan2(p.Y - c.Y, p.X - c.X);
        }
        public double AngleAbout(CPoint c)
        {
            return AngleBetween(this, c);
        }
        public void Increment(float dx, float dy)
        {
            X += dx;
            Y += dy;
        }

        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            g.DrawEllipse(Pens.Black, (float)X - 3, (float)Y - 3, 6, 6);
        }

    }
    
    public class RotatingBezierSplineAnchor : BezierBoardItem
    {
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
        public override List<BezierBoardItem> GetChildren()
        {
            return new BezierBoardItem[] { P, C1, C2, R1, /*R2*/ }.ToList();
        }
        public CenterPoint P { get; private set; }
        public CurvatureHandlePoint C1 { get; private set; }
        public CurvatureHandlePoint C2 { get; private set; }
        public RotationHandlePoint R1 { get; private set; }
        //public RotationHandlePoint R2 { get; private set; }

        public CPoint[] Points;
        int rotationsOffset = 0;
        public bool BindCurvatureHandlesLength { get; set; } = false;
        public double R { get{ return rotationsOffset * 2 * Math.PI + Math.Atan2(R1.Y - P.Y, R1.X - P.X); } }
        public RotatingBezierSplineAnchor(float x, float y) : this(new PointF(x, y)) { }
        public RotatingBezierSplineAnchor(PointF P) : this(P, new PointF(P.X, P.Y), 0, 0) { }
        public RotatingBezierSplineAnchor(PointF p, PointF a1, double a2Length, double rotation)
        {
            this.P = new CenterPoint(p.X, p.Y);
            this.C1 = new CurvatureHandlePoint(a1.X, a1.Y);
            var ang = C1.AngleAbout(P) + Math.PI;
            this.C2 = new CurvatureHandlePoint(new PointF((float)a2Length * (float)Math.Cos(ang) + P.X, (float)a2Length * (float)Math.Sin(ang) + P.Y));
            // do we need to add rotation to R as well?
            while (rotation > Math.PI)
            {
                rotation -= 2 * Math.PI;
                rotationsOffset--;
            } 
            while (rotation < -Math.PI)
            {
                rotation += 2 * Math.PI;
                rotationsOffset++;
            }
            R1 = new RotationHandlePoint(this.P, rotation);
            //R2 = new RotationHandlePoint(this.P, rotation + Math.PI);
            Points = new CPoint[] { this.P, C1, C2, R1, /*R2*/ };
            P.IncrementLocationRequest += (dx, dy) =>
              {
                  P.Increment(dx, dy);
                  C1.Increment(dx, dy);
                  C2.Increment(dx, dy);
                  R1.Increment(dx, dy);
                  //R2.Increment(dx, dy);
              };
            R1.LocationResetRequest += (s, e) =>
            {
                double newAngle = R1.AngleAbout(P);
                R1.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Cos(newAngle));
                R1.Y = (float)(P.Y + RotationHandlePoint.HandleLength * Math.Sin(newAngle));
            };
            //R2.LocationResetRequest += (s, e) =>
            //{
            //    double newAngle = R2.AngleAbout(P);
            //    R2.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Cos(newAngle));
            //    R2.Y = (float)(P.Y + RotationHandlePoint.HandleLength * Math.Sin(newAngle));
            //};
            R1.IncrementLocationRequest += (dx, dy) =>
            {
                double oldAngle = R1.AngleAbout(P);
                R1.Increment(dx, dy);
                double newAngle = R1.AngleAbout(P);
                double dTh = newAngle - oldAngle;
                while (dTh > Math.PI)
                {
                    dTh -= 2 * Math.PI;
                    rotationsOffset--;
                }
                while (dTh < -Math.PI)
                {
                    dTh += 2 * Math.PI;
                    rotationsOffset++;
                }
                newAngle = oldAngle + dTh;
                //// do the complete rotation compensation here
                //R1.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Cos(newAngle));
                //R1.Y = (float)(P.Y + RotationHandlePoint.HandleLength * Math.Sin(newAngle));
                //R2.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Cos(newAngle + Math.PI));
                //R2.Y = (float)(P.Y + RotationHandlePoint.HandleLength * Math.Sin(newAngle + Math.PI));
            };
            //R2.IncrementLocationRequest += (dx, dy) =>
            //{
            //    //double oldAngle = R2.AngleAbout(P);
            //    //R2.Increment(dx, dy);
            //    //double newAngle = R2.AngleAbout(P);
            //    ////// do the complete rotation compensation here
            //    ////R2.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Cos(newAngle));
            //    ////R2.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Sin(newAngle));
            //    //R1.X = (float)(P.X + RotationHandlePoint.HandleLength * Math.Cos(newAngle + Math.PI));
            //    //R1.Y = (float)(P.Y + RotationHandlePoint.HandleLength * Math.Sin(newAngle + Math.PI));
            //};
            C1.IncrementLocationRequest += (dx, dy) =>
              {
                  double oldAngle = C1.AngleAbout(P);
                  C1.Increment(dx, dy);
                  double newAngle = C1.AngleAbout(P);
                  // keep the radius fixed
                  double r2 = C2.DistanceFrom(P);
                  if (BindCurvatureHandlesLength)
                      r2 = C1.DistanceFrom(P);
                  // do the complete rotation compensation here
                  C2.X = (float)(P.X + r2 * Math.Cos(newAngle + Math.PI));
                  C2.Y = (float)(P.Y + r2 * Math.Sin(newAngle + Math.PI));
              };
            C2.IncrementLocationRequest += (dx, dy) =>
            {
                double oldAngle = C2.AngleAbout(P);
                C2.Increment(dx, dy);
                double newAngle = C2.AngleAbout(P);
                double r2 = C1.DistanceFrom(P);
                if (BindCurvatureHandlesLength)
                    r2 = C2.DistanceFrom(P);
                // do the complete rotation compensation here
                C1.X = (float)(P.X + r2 * Math.Cos(newAngle + Math.PI));
                C1.Y = (float)(P.Y + r2 * Math.Sin(newAngle + Math.PI));
            };

        }

        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (anchorDrawMode.HasFlag(AnchorDrawMode.Centers))
                P.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
            if (anchorDrawMode.HasFlag(AnchorDrawMode.CurvatureHandles))
            {
                // draw the lines
                g.DrawLine(Pens.Black, C1, P);
                g.DrawLine(Pens.Black, C2, P);
                C1.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl); C2.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
            }
            if (anchorDrawMode.HasFlag(AnchorDrawMode.RotaionHandles))
            {
                g.DrawLine(Pens.Black, R1, P);
                //g.DrawLine(Pens.Black, R2, P);
                R1.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
                //R2.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
            }

        }
        //public RotatingBezierSplineAnchor YInvertedClone()
        //{
        //    RotatingBezierSplineAnchor a = Clone();
        //    a.P.Y *= -1;
        //    a.ch1.Y *= -1;
        //    a.ch2.Y *= -1;
        //    a.rh1.Y *= -1;
        //    a.rh2.Y *= -1;

        //    return a;
        //}
    }
    public class BezierCurveCellWithRotation
    {

        public RotatingBezierSplineAnchor A1 { get; private set; }
        public RotatingBezierSplineAnchor A2 { get; private set; }

        public BezierCurveCellWithRotation(RotatingBezierSplineAnchor a1, RotatingBezierSplineAnchor a2)
        {
            this.A1 = a1;
            this.A2 = a2;
        }
        public bool InkContains(PointF p)
        {
            if (psInk == null) return false;
            return IsPointInPolygon4(psInk, p);
        }
        /// <summary>
        /// Determines if the given point is inside the polygon
        /// </summary>
        /// <param name="polygon">the vertices of polygon</param>
        /// <param name="testPoint">the given point</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        public static bool IsPointInPolygon4(PointF[] polygon, PointF testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
        PointF[] psInk;
        PointF[] psSpline;
        public void Draw(Graphics g, float thickness, MouseState mouseState, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            // draw the spline
            int divisions = 20;
            psSpline = new PointF[divisions + 1];
            CPoint pPrevious = null;
            double r1 = this.A1.R;
            double r2 = this.A2.R;
            psInk = new PointF[divisions * 4];
            for (int i = 0; i <= divisions; i++)
            {
                double f = i / (double)divisions;
                CPoint pl1_1 = CPoint.Intermediate(A1.P, A1.C2, f);
                CPoint pl1_2 = CPoint.Intermediate(A1.C2, A2.C1, f);
                CPoint pl1_3 = CPoint.Intermediate(A2.C1, A2.P, f);

                CPoint pl2_1 = CPoint.Intermediate(pl1_1, pl1_2, f);
                CPoint pl2_2 = CPoint.Intermediate(pl1_2, pl1_3, f);

                CPoint P = CPoint.Intermediate(pl2_1, pl2_2, f);

                if (i > 0 && thickness > 0)
                {
                    double fp = (i - 1) / (double)divisions;
                    CPoint pp1 = new CPoint(
                        pPrevious.X + thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y + thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)));

                    CPoint pp2 = new CPoint(
                        pPrevious.X - thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y - thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)));
                    CPoint pc1 = new CPoint(
                        P.X + thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y + thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)));

                    CPoint pc2 = new CPoint(
                        P.X - thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y - thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)));
                    psInk[(i - 1) * 2] = pp1;
                    psInk[(i - 1) * 2 + 1] = pc1;
                    psInk[psInk.Length - ((i - 1) * 2) - 1] = pp2;
                    psInk[psInk.Length - ((i - 1) * 2 + 1) - 1] = pc2;
                }
                psSpline[i] = P;
                pPrevious = P;
            }


            if (mouseState == MouseState.None)
            {
                if (thickness > 0 && inkDrawMode.HasFlag(InkDrawMode.Ink))
                    g.FillPolygon(Brushes.DarkGray, psInk);
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                    g.DrawLines(inkDrawMode.HasFlag(InkDrawMode.Ink) ? Pens.White : Pens.Gray, psSpline);
            }
            else if (mouseState == MouseState.Hover)
            {
                if (thickness > 0 && inkDrawMode.HasFlag(InkDrawMode.Ink))
                {
                    g.FillPolygon(Brushes.DarkGray, psInk);
                    g.DrawPolygon(new Pen(Color.DarkBlue, 3), psInk);
                }
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                {
                    g.DrawLines(Pens.Black, psSpline);
                    g.DrawLines(new Pen(Color.FromArgb(100, 0, 0, 0), 3), psSpline);
                }
            }
            else if (mouseState == MouseState.Held)
            {
                if (thickness > 0 && inkDrawMode.HasFlag(InkDrawMode.Ink))
                {
                    g.FillPolygon(Brushes.DarkGray, psInk);
                    g.DrawPolygon(new Pen(Color.DarkBlue, 1), psInk);
                }
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                {
                    g.DrawLines(Pens.Black, psSpline);
                    g.DrawLines(new Pen(Color.FromArgb(100, 0, 0, 0), 2), psSpline);
                }
            }

            if (inkDrawMode.HasFlag(InkDrawMode.Spline)) // draw the handles as well
            {
                A1.Draw(g, inkDrawMode, anchorDrawMode, Parent, ParentControl);
                A2.Draw(g, inkDrawMode, anchorDrawMode, Parent, ParentControl);
            }
        }

        internal bool SplineIntersects(PointF p, int distance)
        {
            if (psSpline == null)
                return false;
            foreach (var ps in psSpline)
            {
                if (CPoint.DistanceBetween(p, ps) <= distance)
                    return true;
            }
            return false;
        }

        //public RectangleF GetBounds(double thickness)
        //{
        //    int divisions = 15;
        //    CPoint pp = null;
        //    double A1 = Math.Atan2(P1.Y - r1.Y, P1.X - r1.X);
        //    double A2 = Math.Atan2(P2.Y - r2.Y, P2.X - r2.X) + Math.PI + 0.001;
        //    if (A2 > 2 * Math.PI)
        //        A2 -= 2 * Math.PI;
        //    if (A1 < 0)
        //        A1 += 2 * Math.PI;
        //    if (A2 < 0)
        //        A2 += 2 * Math.PI;
        //    PointF[] psInk = new PointF[divisions * 4];
        //    for (int i = 0; i <= divisions; i++)
        //    {
        //        double f = i / (double)divisions;
        //        CPoint pl1_1 = CPoint.Intermediate(P1, a1, f);
        //        CPoint pl1_2 = CPoint.Intermediate(a1, a2, f);
        //        CPoint pl1_3 = CPoint.Intermediate(a2, P2, f);

        //        CPoint pl2_1 = CPoint.Intermediate(pl1_1, pl1_2, f);
        //        CPoint pl2_2 = CPoint.Intermediate(pl1_2, pl1_3, f);

        //        CPoint P = CPoint.Intermediate(pl2_1, pl2_2, f);

        //        if (i > 0)
        //        {
        //            double fp = (i - 1) / (double)divisions;
        //            CPoint pp1 = new CPoint(
        //                pp.X + thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
        //                pp.Y + thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));

        //            CPoint pp2 = new CPoint(
        //                pp.X - thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
        //                pp.Y - thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));
        //            CPoint pc1 = new CPoint(
        //                P.X + thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
        //                P.Y + thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));

        //            CPoint pc2 = new CPoint(
        //                P.X - thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
        //                P.Y - thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));
        //            psInk[(i - 1) * 2] = pp1;
        //            psInk[(i - 1) * 2 + 1] = pc1;
        //            psInk[psInk.Length - ((i - 1) * 2) - 1] = pp2;
        //            psInk[psInk.Length - ((i - 1) * 2 + 1) - 1] = pc2;
        //        }
        //        pp = P;
        //    }

        //    float minX = psInk.Min(p => p.X);
        //    float maxX = psInk.Max(p => p.X);
        //    float minY = psInk.Min(p => p.Y);
        //    float maxY = psInk.Max(p => p.Y);
        //    return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        //}
        //public bool Contains(Point p, double thickness)
        //{
        //    return GetBounds(thickness).Contains(p);
        //}
        //static bool IsInPolygon(PointF[] poly, Point point)
        //{
        //    var coef = poly.Skip(1).Select((p, i) =>
        //                                    (point.Y - poly[i].Y) * (p.X - poly[i].X)
        //                                  - (point.X - poly[i].X) * (p.Y - poly[i].Y))
        //                            .ToList();

        //    if (coef.Any(p => p == 0))
        //        return true;

        //    for (int i = 1; i < coef.Count(); i++)
        //    {
        //        if (coef[i] * coef[i - 1] < 0)
        //            return false;
        //    }
        //    return true;
        //}

        //internal void MakeTargets(double inc, List<EulerAngleOrientation> targets)
        //{
        //    // draw the spline                      
        //    CPoint pp = null;
        //    double A1 = Math.Atan2(P1.Y - r1.Y, P1.X - r1.X);
        //    double A2 = Math.Atan2(P2.Y - r2.Y, P2.X - r2.X) + Math.PI + 0.001;
        //    if (A2 > 2 * Math.PI)
        //        A2 -= 2 * Math.PI;
        //    if (A1 < 0)
        //        A1 += 2 * Math.PI;
        //    if (A2 < 0)
        //        A2 += 2 * Math.PI;

        //    for (double f = 1; f >= 0; f -= inc)
        //    {
        //        CPoint pl1_1 = CPoint.Intermediate(P1, a1, f);
        //        CPoint pl1_2 = CPoint.Intermediate(a1, a2, f);
        //        CPoint pl1_3 = CPoint.Intermediate(a2, P2, f);

        //        CPoint pl2_1 = CPoint.Intermediate(pl1_1, pl1_2, f);
        //        CPoint pl2_2 = CPoint.Intermediate(pl1_2, pl1_3, f);

        //        CPoint P = CPoint.Intermediate(pl2_1, pl2_2, f);

        //        var twist = A1 * f + A2 * (1 - f);

        //        if (f == 1) // first                                                 
        //        {
        //            if (targets.Count > 0)
        //            {
        //                if (new EulerAngleOrientation(twist, 0, 0, P.X, P.Y, 0) != targets[targets.Count - 1])
        //                    targets.Add(new EulerAngleOrientation(0, 0, twist, P.X, P.Y, 0));
        //            }
        //            else
        //                targets.Add(new EulerAngleOrientation(0, 0, twist, P.X, P.Y, 0));
        //        }
        //        else
        //        {
        //            double d = Math.Sqrt(Math.Pow(targets[targets.Count - 1].X - P.X, 2) + Math.Pow(targets[targets.Count - 1].Y - P.Y, 2));
        //            if (d >= inc)
        //                targets.Add(new EulerAngleOrientation(0, 0, twist, P.X, P.Y, 0));
        //        }
        //        // add
        //        pp = P;
        //    }
        //}
    }
  
    [Serializable]
    public class RotatingBezierSpline : BezierBoardItem
    {
        public RotatingBezierSpline()
        {
            IncrementLocationRequest += (dx, dy) => {
                foreach (var a in Anchors)
                {
                    a.P.NotifyLocationIncrementRequest(dx, dy);
                }
            };
        }
        public override Cursor Cursor { get => Cursors.SizeAll; }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return true;
        }
        public override List<BezierBoardItem> GetChildren()
        {
            return Anchors.Select(a => (BezierBoardItem)a).ToList();
        }
        List<RotatingBezierSplineAnchor> Anchors { get; set; } = new List<RotatingBezierSplineAnchor>();

        public float FlatTipWidth { get; internal set; }

        //public static List<RotatingBezierSpline> FromFile(string fileName)
        //{
        //    var lines = System.IO.File.ReadAllLines(fileName);
        //    List<RotatingBezierSpline> Splines = new List<RotatingBezierSpline>();
        //    RotatingBezierSpline Spline = new RotatingBezierSpline();
        //    foreach (var line in lines)
        //    {
        //        var parts = line.Split(new char[] { ';', ',' });
        //        if (parts.Length == 1)
        //        {
        //            if (line == "%")
        //            {
        //                if (Spline.Anchors.Count > 0)
        //                    Splines.Add(Spline);
        //                Spline = new RotatingBezierSpline();
        //                continue;
        //            }
        //            Spline.FlatTipWidth = Convert.ToSingle(line);
        //            continue;
        //        }
        //        RotatingBezierSplineAnchor a = new RotatingBezierSplineAnchor(
        //            new CPoint(Convert.ToDouble(parts[0]), Convert.ToDouble(parts[1])),
        //            new CPoint(Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3])),
        //            new CPoint(Convert.ToDouble(parts[4]), Convert.ToDouble(parts[5]))
        //            );
        //        a.rh1 = new CPoint(Convert.ToDouble(parts[6]), Convert.ToDouble(parts[7]));
        //        a.rh2 = new CPoint(Convert.ToDouble(parts[8]), Convert.ToDouble(parts[9]));
        //        Spline.Anchors.Add(a);
        //    }

        //    if (Spline.Anchors.Count > 0)
        //        Splines.Add(Spline);
        //    return Splines;
        //}
        public float Thickness { get; set; } = 0;
        List<BezierCurveCellWithRotation> lastCurveCellCache;
        InkDrawMode inkDrawModeCache;
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            inkDrawModeCache = inkDrawMode;
            lastCurveCellCache = new List<BezierCurveCellWithRotation>();
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                var cell = new BezierCurveCellWithRotation(Anchors[i], Anchors[i + 1]);
                lastCurveCellCache.Add(cell);
                cell.Draw(g, Thickness, MouseState, inkDrawMode, anchorDrawMode, this, ParentControl);
            }
        }
        public override bool TopParentBoundsContains(PointF p)
        {
            if (lastCurveCellCache == null) return false;
            else
                foreach (var c in lastCurveCellCache)
                {
                    if (inkDrawModeCache.HasFlag(InkDrawMode.Ink))
                    {
                        if (c.InkContains(p))
                            return true;
                    }
                    else if (inkDrawModeCache.HasFlag(InkDrawMode.Spline))
                    {
                        if (c.SplineIntersects(p, 10))
                            return true;
                    }
                }
            return false;
        }
        public bool CanReceiveAnchorAtStart
        {
            get
            {
                if (Anchors.Count == 0) return false;
                if (Anchors.First().P.MouseState == MouseState.Selected)
                    return true;
                return false;
            }
        }
        public bool CanReceiveAnchorAtEnd
        {
            get
            {
                if (Anchors.Count == 0) return false;
                if (Anchors.Last().P.MouseState == MouseState.Selected)
                    return true;
                return false;
            }
        }
        public CurvatureHandlePoint AddAnchor(RotatingBezierSplineAnchor a)
        {
            a.P.OnSelected += (s, e) =>
            {
                foreach (var _a in Anchors)
                {
                    if (_a == a)
                        continue;
                    _a.P.MouseState = MouseState.None;
                }
                this.NotifySelected();
            };
            if (CanReceiveAnchorAtStart)
            {
                Anchors.Insert(0, a);
                return a.C1;
            }
            else // force add
            {
                Anchors.Add(a);
                return a.C2;
            }
        }

        public void UnselectAllAnchors()
        {
            foreach (var a in Anchors)
            {
                a.P.MouseState = MouseState.None;
            }
        }
        //public RectangleF GetBounds()
        //{
        //    if (Anchors.Count < 2)
        //        return new RectangleF();

        //    float minX = float.PositiveInfinity, minY = float.PositiveInfinity;
        //    float maxX = float.NegativeInfinity, maxY = float.NegativeInfinity;
        //    for (int i = 0; i < Anchors.Count - 1; i++)
        //    {
        //        var b = new BezierCurveCellWithRotation(
        //            Anchors[i].P,
        //            Anchors[i + 1].P,
        //            Anchors[i].C2,
        //            Anchors[i + 1].C1,
        //            Anchors[i].R2,
        //            Anchors[i + 1].R1
        //            ).GetBounds(FlatTipWidth);
        //        if (b.X < minX)
        //            minX = b.X;
        //        if (b.Y < minY)
        //            minY = b.Y;
        //        if (b.X + b.Width > maxX)
        //            maxX = b.X + b.Width;
        //        if (b.Y + b.Height > maxY)
        //            maxY = b.Y + b.Height;
        //    }
        //    if (Anchors.Count < 2)
        //        return new RectangleF();
        //    return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        //}
        //public bool Contains(Point p)
        //{
        //    for (int i = 0; i < Anchors.Count - 1; i++)
        //    {
        //        if (
        //        new BezierCurveCellWithRotation(
        //            Anchors[i].P,
        //            Anchors[i + 1].P,
        //            Anchors[i].C2,
        //            Anchors[i + 1].C1,
        //            Anchors[i].R2,
        //            Anchors[i + 1].R1
        //            ).Contains(p, FlatTipWidth))
        //            return true;
        //    }
        //    return false;
        //}

        //public List<EulerAngleOrientation> MakeTargets(double inc, double scale, double xOffset, double yOffset)
        //{
        //    List<EulerAngleOrientation> targets = new List<EulerAngleOrientation>();
        //    SplineAnchor[] anchors = new SplineAnchor[Anchors.Count];

        //    for (int i = 0; i < Anchors.Count; i++)
        //    {
        //        anchors[i] = Anchors[i].Clone();
        //    }

        //    foreach (var a in anchors)
        //    {
        //        a.P1.X = (a.P1.X - xOffset) * scale;
        //        a.P1.Y = (a.P1.Y - yOffset) * scale;
        //        a.a1.X = (a.a1.X - xOffset) * scale;
        //        a.a1.Y = (a.a1.Y - yOffset) * scale;
        //        a.a2.X = (a.a2.X - xOffset) * scale;
        //        a.a2.Y = (a.a2.Y - yOffset) * scale;
        //        a.r1.X = (a.r1.X - xOffset) * scale;
        //        a.r1.Y = (a.r1.Y - yOffset) * scale;
        //        a.r2.X = (a.r2.X - xOffset) * scale;
        //        a.r2.Y = (a.r2.Y - yOffset) * scale;
        //    }
        //    for (int i = 0; i < anchors.Length - 1; i++)
        //    {
        //        new BezierCurveWithRotation(
        //            anchors[i].P1,
        //            anchors[i + 1].P1,
        //            anchors[i].a2,
        //            anchors[i + 1].a1,
        //            anchors[i].r2,
        //            anchors[i + 1].r1
        //            ).MakeTargets(inc, targets);
        //    }
        //    return targets;
        //}

        //internal string ToSaveString()
        //{

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(FlatTipWidth.ToString());
        //    foreach (var anchor in Anchors)
        //    {
        //        sb.Append(anchor.P.X.ToString() + "," + anchor.P.Y.ToString() + ";");
        //        sb.Append(anchor.C1.X.ToString() + "," + anchor.C1.Y.ToString() + ";");
        //        sb.Append(anchor.C2.X.ToString() + "," + anchor.C2.Y.ToString() + ";");
        //        sb.Append(anchor.R1.X.ToString() + "," + anchor.R1.Y.ToString() + ";");
        //        sb.AppendLine(anchor.R2.X.ToString() + "," + anchor.R2.Y.ToString());
        //    }
        //    return sb.ToString();
        //}
    }
}