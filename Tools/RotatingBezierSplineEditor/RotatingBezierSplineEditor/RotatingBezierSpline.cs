using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
    public interface IBezierBoardItem
    {
        bool TopParentBoundsContains(PointF p);
        void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl);
        bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode);
        bool CanBeSelected { get; set; }
        RectangleF BoundsOnTopParent { get; }
    }
    public class BezierBoardItem:IBezierBoardItem
    {
        public string Tag = "";
        public event EventHandler OnSelected;
        public event IncrementLocationHandler IncrementLocationRequest;
        public event EventHandler LocationResetRequest;
        public event EventHandler OnSelfRemoveRequest;
        public void SelfRemoveRequest()
        {
            OnSelfRemoveRequest?.Invoke(this, new EventArgs());
        }
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
            OnMouseEnter += (s, e) => {
                MouseState = MouseState.Hover; 
            };
            OnMouseLeave += (s, e) =>
            {
                if (!CanBeSelected)
                    MouseState = MouseState.None;
            };
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
        public MouseState MouseState { get; set; }
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
        public event MouseEventHandler OnMouseClick;
        public void NotifyMouseLeave(object sender, MouseEventArgs e, BezierBoardItem Parent, Control ParentControl)
        {
            lastwasOutside = true;
            OnMouseLeave?.Invoke(sender, e);
            if (Parent.Cursor == Cursors.No)
                ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
            else ParentControl.Cursor = Parent.Cursor;
        }
        public void NotifyMouseClick(Point location, MouseButtons button)
        {
            if (MouseDownAt == MousePosition)
                OnMouseClick?.Invoke(this, new MouseEventArgs(button, 1, location.X, location.Y, 0));
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
            MousePosition = new PointF(e.X, e.Y);
            // resolve any debts to previous relationships
            if (!CanDraw(inkDrawMode, anchorDrawMode))
                return false;
            if (TopParentBoundsContains(e.Location))
            {
                // before returning, call mouse leave on sister controls that had mouse hover
                if (Parent != null)
                {
                    if (Parent.GetChildren().Find(c => c.MouseState == MouseState.Held) != null) // a sister control is being held and moved
                        return false;
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
            if (filter == this)
                ;
            if (filter == null || filter == this || wentDownInBounds)
            {
                var e2 = new MouseEventArgs(e.Button, e.Clicks, (int)(e.X - BoundsOnTopParent.X), (int)(e.Y - BoundsOnTopParent.Y), e.Delta);
                if (TopParentBoundsContains(e.Location) || wentDownInBounds)
                {
                    if (lastwasOutside)
                    {
                        lastwasOutside = false;
                        cursorBkp = ParentControl.Cursor;
                        NotifyMouseEnter(this, e2);
                        if (Cursor == Cursors.No)
                            ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
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
                        ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
                    else if (Parent.Cursor == Cursors.No)
                        ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
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

        public virtual void Save(XmlNode node)
        {
            //throw new NotImplementedException();
        }
    }
    public class CenterPoint : RBSPoint
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
            if (MouseState == MouseState.Held)
                base.Draw(g, inkDrawMode, anchorDrawMode, Parent, ParentControl);
        }
    }
    public class RotationHandlePoint : RBSPoint
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
            base.Draw(g, inkDrawMode, anchorDrawMode, Parent, ParentControl);
        }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return anchorDrawMode.HasFlag(AnchorDrawMode.RotaionHandles) & inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
    }
    public class CurvatureHandlePoint : RBSPoint
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

            base.Draw(g, inkDrawMode, anchorDrawMode, Parent, ParentControl);
        }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return anchorDrawMode.HasFlag(AnchorDrawMode.CurvatureHandles) & inkDrawMode.HasFlag(InkDrawMode.Spline);
        }
    }
    public class RBSPoint:BezierBoardItem
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
        public static implicit operator PointF(RBSPoint p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }
        public static implicit operator Point(RBSPoint p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
        public bool IsCloseBy(PointF P, int tol = 3)
        {
            return Math.Sqrt(Math.Pow(P.X - X, 2) + Math.Pow(P.Y - Y, 2)) < 3;
        }
        public RBSPoint(double x = 0, double y = 0)
        {
            X = (float)x; Y = (float)y;
        }
        public static RBSPoint Intermediate(RBSPoint a, RBSPoint b, double frac = 0.5)
        {
            return new RBSPoint(a.X * frac + b.X * (1 - frac), a.Y * frac + b.Y * (1 - frac));
        }
        public double DistanceFrom(RBSPoint p)
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
        public static double DistanceBetween(RBSPoint p1, RBSPoint p2)
        {
            return DistanceBetween(p1.X, p1.Y, p2.X, p2.Y);
        }
        public double AngleBetween(RBSPoint p, RBSPoint c)
        {
            return Math.Atan2(p.Y - c.Y, p.X - c.X);
        }
        public double AngleAbout(RBSPoint c)
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
            //g.DrawEllipse(Pens.Black, (float)X - 3, (float)Y - 3, 6, 6); 
            if (MouseState == MouseState.Held || MouseState == MouseState.Hover)
            {
                g.ScaleTransform(1, -1);
                g.DrawString("{" + X + ", " + Y + "}", new Font("CONSOLA", 20), Brushes.Black, X + 10, -Y);
                g.ScaleTransform(1, -1);
            }
        }
        public void ExtractCoordinates(XmlNode node)
        {
            var xy = ((XmlText)(node.FirstChild)).Data.Split(new char[] { ',' });
            X = float.Parse(xy[0]);
            Y = float.Parse(xy[1]);
        }
        public void Save(XmlNode node, string title)
        {
            var doc = node.OwnerDocument;
            var pointNode = node.OwnerDocument.CreateElement(title);
            var c = this;
            var v = doc.CreateTextNode(c.X.ToString() + ", " + c.Y.ToString());
            pointNode.AppendChild(v);
            node.AppendChild(pointNode);
        }
    }
    
    public class RotatingBezierSplineAnchor : BezierBoardItem
    {
        public delegate void WidthChanegHandler(float width);
        public event WidthChanegHandler WidthChangeRequest;
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

        public RBSPoint[] Points;
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
            Points = new RBSPoint[] { this.P, C1, C2, R1, /*R2*/ };
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
                float newWid = (float)R1.DistanceFrom(P);
                //WidthChangeRequest?.Invoke(newWid); // not a good way to change the width
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
        static string[] names = { "P", "C1", "C2", "R1" };
        public override void Save(XmlNode node)
        {
            var doc = node.OwnerDocument;
            var anchor = node.OwnerDocument.CreateElement("anchor");
            var er = doc.CreateElement("rotationoffset");
            var v = doc.CreateTextNode(rotationsOffset.ToString());
            er.AppendChild(v);
            anchor.AppendChild(er);
            for (int i = 0; i < Points.Length; i++)
                Points[i].Save(anchor, names[i]);
            node.AppendChild(anchor);
        }
        public static RotatingBezierSplineAnchor Parse(XmlElement node)
        {
            var ret = new RotatingBezierSplineAnchor(0, 0);
            ret.rotationsOffset = int.Parse(((XmlText)(node.GetElementsByTagName("rotationoffset")[0].FirstChild)).Data);

            for (int i = 0; i < ret.Points.Length; i++)
                ret.Points[i].ExtractCoordinates((XmlElement)(node.GetElementsByTagName(names[i])[0]));
            return ret;
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
        public void Draw(Graphics g, float thickness, Color normalColor, MouseState mouseState, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            // draw the spline
            int divisions = 20;
            psSpline = new PointF[divisions + 1];
            RBSPoint pPrevious = null;
            double r1 = this.A1.R;
            double r2 = this.A2.R;
            psInk = new PointF[divisions * 4];
            for (int i = 0; i <= divisions; i++)
            {
                double f = i / (double)divisions;
                RBSPoint pl1_1 = RBSPoint.Intermediate(A1.P, A1.C2, f);
                RBSPoint pl1_2 = RBSPoint.Intermediate(A1.C2, A2.C1, f);
                RBSPoint pl1_3 = RBSPoint.Intermediate(A2.C1, A2.P, f);

                RBSPoint pl2_1 = RBSPoint.Intermediate(pl1_1, pl1_2, f);
                RBSPoint pl2_2 = RBSPoint.Intermediate(pl1_2, pl1_3, f);

                RBSPoint P = RBSPoint.Intermediate(pl2_1, pl2_2, f);

                if (i > 0 && thickness > 0)
                {
                    double fp = (i - 1) / (double)divisions;
                    RBSPoint pp1 = new RBSPoint(
                        pPrevious.X + thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y + thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)));

                    RBSPoint pp2 = new RBSPoint(
                        pPrevious.X - thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y - thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)));
                    RBSPoint pc1 = new RBSPoint(
                        P.X + thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y + thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)));

                    RBSPoint pc2 = new RBSPoint(
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
                    g.FillPolygon(new SolidBrush(normalColor), psInk);
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
                    g.FillPolygon(new SolidBrush(normalColor), psInk);
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
                if (RBSPoint.DistanceBetween(p, ps) <= distance)
                    return true;
            }
            return false;
        }
    }
  
    [Serializable]
    public class RotatingBezierSpline : BezierBoardItem
    {
        public RotatingBezierSpline(BezierBoard board)
        {
            IncrementLocationRequest += (dx, dy) => {
                foreach (var a in Anchors)
                {
                    a.P.NotifyLocationIncrementRequest(dx, dy);
                }
            };
            OnMouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    SplineAppearanceEditor apf = new SplineAppearanceEditor();
                    apf.widthTB.Value = (int)FlatTipWidth;
                    apf.colorP.BackColor = NormalColor;
                    apf.colorP.Click += (s2, e2) =>
                    {
                        var cd = new ColorDialog();
                        cd.Color = apf.colorP.BackColor;
                        if (cd.ShowDialog() == DialogResult.OK)
                        {
                            apf.colorP.BackColor = cd.Color;
                            NormalColor = cd.Color;
                            board.Invalidate(); Application.DoEvents();
                        }
                    };
                    apf.widthTB.ValueChanged += (s2, e2) =>
                    {
                        FlatTipWidth = apf.widthTB.Value;
                        board.Invalidate(); Application.DoEvents();
                    };
                    apf.ShowDialog();
                }
                else
                {
                    SelfRemoveRequest();
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
        Color NormalColor = Color.DarkGray;
        public float FlatTipWidth { get; set; } = 0;
        List<BezierCurveCellWithRotation> lastCurveCellCache;
        InkDrawMode inkDrawModeCache;
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (FlatTipWidth < 1)
                inkDrawMode &= ~(InkDrawMode.Ink);
            inkDrawModeCache = inkDrawMode;
            lastCurveCellCache = new List<BezierCurveCellWithRotation>();
            if (Anchors.Count == 1) // adding new spline
                Anchors[0].Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                var cell = new BezierCurveCellWithRotation(Anchors[i], Anchors[i + 1]);
                lastCurveCellCache.Add(cell);
                cell.Draw(g, FlatTipWidth, NormalColor, MouseState, inkDrawMode, anchorDrawMode, this, ParentControl);
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
            //a.WidthChangeRequest += (w) => { FlatTipWidth = w; }; // not using this
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
            a.P.OnMouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    Anchors.Remove(a);
            };
            if (CanReceiveAnchorAtStart && Anchors.Count > 1)
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
        internal static BezierBoardItem Parse(XmlElement spline, BezierBoard board)
        {
            var sRet = new RotatingBezierSpline(board);

            sRet.FlatTipWidth = float.Parse(((XmlText)(spline.GetElementsByTagName("FlatTipWidth")[0].FirstChild)).Data);
            sRet.NormalColor = Color.FromArgb(int.Parse(((XmlText)(spline.GetElementsByTagName("Color")[0].FirstChild)).Data));
            foreach (XmlElement obj in spline.GetElementsByTagName("anchor"))
                sRet.AddAnchor(RotatingBezierSplineAnchor.Parse(obj));
            return sRet;
        }
        public override void Save(XmlNode node)
        {
            var doc = node.OwnerDocument;
            var spline = doc.CreateElement("spline");
            node.AppendChild(spline);
            var wid = doc.CreateElement("FlatTipWidth");
            var v = doc.CreateTextNode(FlatTipWidth.ToString());
            wid.AppendChild(v);
            spline.AppendChild(wid);
            var col = doc.CreateElement("Color");
            v = doc.CreateTextNode(NormalColor.ToArgb().ToString());
            col.AppendChild(v);
            spline.AppendChild(col);
            foreach (var c in Anchors)
                c.Save(spline);
        }
    }
    public class ImageItem : BezierBoardItem
    {
        Image img;
        CenterPoint center;
        CurvatureHandlePoint size;
        ImageItem() { }
        public ImageItem(Image img, float x, float y)
        {
            this.img = img;
            center = new CenterPoint(x, y);
            size = new CurvatureHandlePoint(x - img.Width / 2, y - img.Height / 2);
            center.IncrementLocationRequest += (dx, dy) =>
            {
                size.X += dx;
                size.Y += dy;
                center.X += dx;
                center.Y += dy;
            }; 
            size.IncrementLocationRequest += (dx, dy) =>
            {
                size.X += dx;
                size.Y += dy;
            };
        }
        public override bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            return true;
        }
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (inkDrawMode.HasFlag(InkDrawMode.Images))
            {
                PointF[] destinationPoints = {
          new PointF(size.X, size.Y),   // destination for upper-left point of original
          new PointF(size.X + 2 * (float)Math.Abs(size.X - center.X), size.Y),  // destination for upper-right point of original
          new PointF(size.X, size.Y - 2 * (size.Y - center.Y))};  // destination for lower-left point of original


                // Draw the image mapped to the parallelogram.
                g.DrawImage(img, destinationPoints);
                //g.DrawImage(img, size.X, size.Y, 2 * (float)Math.Abs(size.X - center.X), 2 * (float)Math.Abs(size.Y - center.Y));
                size.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
                center.Draw(g, inkDrawMode, anchorDrawMode, this, ParentControl);
            }
        }

        internal static BezierBoardItem Parse(XmlElement spline)
        {
            var sRet = new ImageItem(stringToImage(((XmlText)(spline.GetElementsByTagName("ImageAdress")[0].FirstChild)).Data), 0, 0);
            sRet.center.ExtractCoordinates(spline.GetElementsByTagName("center")[0]);
            sRet.size.ExtractCoordinates(spline.GetElementsByTagName("size")[0]);

            return sRet;
        }
        public override void Save(XmlNode node)
        {
            var doc = node.OwnerDocument;
            var spline = doc.CreateElement("image");
            node.AppendChild(spline);
            var imageAddress = doc.CreateElement("ImageAdress");
            var imagedata = imageToString(img);
            var v = doc.CreateTextNode(imagedata);
            imageAddress.AppendChild(v);
            spline.AppendChild(imageAddress);
            center.Save(spline, "center");
            size.Save(spline, "size");
        }
        static Image stringToImage(string str)
        {
            return Image.FromStream(new MemoryStream(str.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(s => byte.Parse(s, System.Globalization.NumberStyles.HexNumber)).ToArray()));
        }
        static string imageToString(Image img)
        {
            //var imagedata = Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".jpg";
            //img.Save(imagedata, System.Drawing.Imaging.ImageFormat.Jpeg);
            //return imagedata;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bytes = ms.ToArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i % 256 == 0)
                    sb.AppendLine();
                sb.Append(bytes[i].ToString("x").PadLeft(2, '0').ToUpper() + " ");
            }
            return sb.ToString();
        }
        public override List<BezierBoardItem> GetChildren()
        {
            return new BezierBoardItem[] { center, size }.ToList();
        }
        public override Cursor Cursor { get => Cursors.SizeAll; }
        public override bool TopParentBoundsContains(PointF p)
        {
            return false;
            //return new RectangleF(size.X, size.Y, 2 * (float)Math.Abs(size.X - center.X), 2 * (float)Math.Abs(size.Y - center.Y)).Contains(p);
        }

        internal static BezierBoardItem FromFile(string fileName)
        {
            return new ImageItem(Image.FromFile(fileName), 0, 0);
        }

        internal static BezierBoardItem FromClipBoard()
        {
            try
            {
                return new ImageItem(Clipboard.GetImage(), 0, 0);
            }
            catch { return null; }
        }
    }
}