using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static RotatingBezierSplineEditor.BezierBoard;

namespace RotatingBezierSplineEditor
{
    public class RasterizedRotatingBezierSpline
    {
        public double[] X;
        public double[] Y;
        public double[] T;
    }
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
            if (this is RotatingBezierSpline)
                if (MessageBox.Show("Do you want to delete this spline?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
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
        public BezierBoardItem(bool mouseEvents)
        {
            if (mouseEvents)
            {
                OnMouseEnter += (s, e) =>
                {
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
        }
        public virtual List<BezierBoardItem> GetChildren()
        { throw new NotImplementedException(); }
        public MouseState MouseState { get; set; }
        public virtual Cursor Cursor { get; set; } = Cursors.No;
        public PointF MousePosition;
        bool wentDownInBounds = false;
        bool lastwasOutside = true;
        Cursor cursorBkp;
        public event MouseEventHandlerF OnMouseLeave;
        public event MouseEventHandlerF OnMouseMove;
        public event MouseEventHandlerF OnMouseEnter;
        public event MouseEventHandlerF OnMouseDown;
        public event MouseEventHandlerF OnMouseUp;
        public event MouseEventHandlerF OnMouseClick;
        public void NotifyMouseLeave(object sender, MouseEventArgsF e, BezierBoardItem Parent, Control ParentControl)
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
                OnMouseClick?.Invoke(this, new MouseEventArgsF(button, 1, location.X, location.Y, 0));
        }
        public void NotifyMouseDown(object sender, MouseEventArgsF e)
        {
            lastMouseMove = e.Location;
            wentDownInBounds = true;
            lastwasOutside = false;
            OnMouseDown?.Invoke(sender, e);
        }
        public void NotifyMouseUp(object sender, MouseEventArgsF e) { OnMouseUp?.Invoke(sender, e); }
        public void NotifyMouseMove(object sender, MouseEventArgsF e) { OnMouseMove?.Invoke(sender, e); }
        public void NotifyMouseEnter(object sender, MouseEventArgsF e) { OnMouseEnter?.Invoke(sender, e); }
        public virtual bool ProcessMouseMove(MouseEventArgsF e, BezierBoardItem filter, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (this is RotatingBezierSpline)
            {
                if (((RotatingBezierSpline)this).Locked || !((RotatingBezierSpline)this).Visible)
                    return false;
            }
            else if (this is RBSPoint)
            {
            }
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
                var e2 = new MouseEventArgsF(e.Button, e.Clicks, (e.X - BoundsOnTopParent.X), (e.Y - BoundsOnTopParent.Y), e.Delta);
                if (TopParentBoundsContains(e.Location) || wentDownInBounds)
                {
                    if (lastwasOutside)
                    {
                        lastwasOutside = false;
                        cursorBkp = ParentControl.Cursor;
                        NotifyMouseEnter(this, e2);
                        Trace.WriteLine("BI1  " + ToString() + ": " + e.Location.ToString());
                        if (Cursor == Cursors.No)
                            ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
                        else ParentControl.Cursor = Cursor;
                    }
                    Trace.WriteLine("BI3  " + ToString() + ": " + e.Location.ToString());
                    NotifyMouseMove(this, e);
                    return true;
                }
                else if (!lastwasOutside)
                {
                    lastwasOutside = true;
                    NotifyMouseLeave(this, e2, this, ParentControl);
                    NotifyMouseMove(this, e2);

                    Trace.WriteLine("BI2  " + ToString() + ": " + e.Location.ToString());
                    if (Parent == null)
                        ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
                    else if (Parent.Cursor == Cursors.No)
                        ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
                    else ParentControl.Cursor = Parent.Cursor;
                    // don't return true.
                }
                else
                    Trace.WriteLine("BI4  " + ToString() + ": " + e.Location.ToString());
            }
            return false;
        }
        public virtual BezierBoardItem ProcessMouseDown(MouseEventArgsF e, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            // devide if we need to receive mouse down and up events.
            if (!CanDraw(inkDrawMode, anchorDrawMode))
                return null;
            if (this is RotatingBezierSpline)
            {
                if (((RotatingBezierSpline)this).Locked || !((RotatingBezierSpline)this).Visible)
                    return null;
            }

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

        public virtual void ProcessMouseUp(object sender, MouseEventArgsF e, BezierBoardItem filter, BezierBoardItem Parent, Control ParentControl)
        {
            if (this is RotatingBezierSpline)
            {
                if (((RotatingBezierSpline)this).Locked || !((RotatingBezierSpline)this).Visible)
                    return;
            }
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
        public CenterPoint(float x, float y, bool mouseEvents) : base(x, y, mouseEvents)
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
        public RotationHandlePoint(double x, double y, bool mouseEvents) : base((float)x, (float)y, mouseEvents)
        { }
        public RotationHandlePoint(CenterPoint cp, double angle, bool mouseEvents) : base(cp.X + HandleLength * Math.Cos(angle), cp.Y + HandleLength * Math.Sin(angle), mouseEvents)
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
        public CurvatureHandlePoint(PointF p, bool mouseEvents) : this(p.X, p.Y, mouseEvents)
        { }
        public CurvatureHandlePoint(float x, float y, bool mouseEvents) : base(x, y, mouseEvents)
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
        //public static implicit operator PointF(RBSPoint p)
        //{
        //    return new PointF((int)p.X, (int)p.Y);
        //}
        public bool IsCloseBy(PointF P, int tol = 3)
        {
            return Math.Sqrt(Math.Pow(P.X - X, 2) + Math.Pow(P.Y - Y, 2)) < 3;
        }
        public RBSPoint(double x , double y, bool mouseEvents):base(mouseEvents)
        {
            X = (float)x; Y = (float)y;
        }
        public static RBSPoint Intermediate(RBSPoint a, RBSPoint b, double frac, bool mouseEvents)
        {
            return new RBSPoint(a.X * frac + b.X * (1 - frac), a.Y * frac + b.Y * (1 - frac), mouseEvents);
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
        public event EventHandler OnShapeChanged;
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
        public RotatingBezierSplineAnchor(float x, float y, bool mouseEvents) : this(new PointF(x, y), mouseEvents) { }
        public RotatingBezierSplineAnchor(PointF P, bool mouseEvents) : this(P, new PointF(P.X, P.Y), 0, 0, mouseEvents)
        {
        }
        public RotatingBezierSplineAnchor(PointF p, PointF a1, double a2Length, double rotation, bool mouseEvents) : base(mouseEvents)
        {
            this.P = new CenterPoint(p.X, p.Y, mouseEvents);
            this.C1 = new CurvatureHandlePoint(a1.X, a1.Y, mouseEvents);
            var ang = C1.AngleAbout(P) + Math.PI;
            this.C2 = new CurvatureHandlePoint(new PointF((float)a2Length * (float)Math.Cos(ang) + P.X, (float)a2Length * (float)Math.Sin(ang) + P.Y), mouseEvents);
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
            R1 = new RotationHandlePoint(this.P, rotation, mouseEvents);
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
            this.P.IncrementLocationRequest += (s, e) =>
            OnShapeChanged?.Invoke(this, new EventArgs());
            C1.IncrementLocationRequest += (s, e) => OnShapeChanged?.Invoke(this, new EventArgs());
            C2.IncrementLocationRequest += (s, e) => OnShapeChanged?.Invoke(this, new EventArgs());
            R1.IncrementLocationRequest += (s, e) => OnShapeChanged?.Invoke(this, new EventArgs());
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
            var ret = new RotatingBezierSplineAnchor(0, 0, true);
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
        static RBSPoint BezierInterpolate(double f, RotatingBezierSplineAnchor A1, RotatingBezierSplineAnchor A2)
        {
            RBSPoint pl1_1 = RBSPoint.Intermediate(A1.P, A1.C2, f, false);
            RBSPoint pl1_2 = RBSPoint.Intermediate(A1.C2, A2.C1, f, false);
            RBSPoint pl1_3 = RBSPoint.Intermediate(A2.C1, A2.P, f, false);

            RBSPoint pl2_1 = RBSPoint.Intermediate(pl1_1, pl1_2, f, false);
            RBSPoint pl2_2 = RBSPoint.Intermediate(pl1_2, pl1_3, f, false);

            RBSPoint P = RBSPoint.Intermediate(pl2_1, pl2_2, f, false);
            return P;
        }
        static RBSPoint moveOn(ref double f, double distance, double scale, double tol, RotatingBezierSplineAnchor A1, RotatingBezierSplineAnchor A2)
        {
            var p = BezierInterpolate(f, A1, A2);

            double f0 = f;
            double f1 = f + .01;
            var p0 = BezierInterpolate(f0, A1, A2);
            var p1 = BezierInterpolate(f1, A1, A2);
            double d0 = p0.DistanceFrom(p0);
            double d1 = p1.DistanceFrom(p0);
            double m = (f1 - f0) / (d1 - d0);
            double c = f0;
            double testF = distance * m + c;
            var pTest = BezierInterpolate(testF, A1, A2); ;
            var dTest = pTest.DistanceFrom(p0);
            f = testF;
            return pTest;


            //while (f < 1)
            //{
            //    double testF = f + inc;
            //    while (testF > 1)
            //    {
            //        inc /= 2;
            //        testF = f + inc;
            //    } 
            //    p2 = BezierInterpolate(f, A1, A2);
            //    d = (float)(p1.DistanceFrom(p2) * scale);
            //    if (d < distance - tol) // f is still too small. lets finalize this f
            //    {
            //        if (testF > 1)
            //            return p2;
            //    }
            //    {
            //        if (dir > 0)
            //            inc /= 2;
            //        dir = -1;
            //        f += inc;
            //        if (f > 1)
            //        {
            //            f = 1;
            //            inc /= 2;
            //        }
            //    }
            //    else if (d > distance + tol) // f is too big
            //    {
            //        if (dir < 0)
            //            inc /= 2;
            //        dir = 1;
            //        f -= inc;
            //        if (f < startF)
            //        {
            //            f = startF;
            //            inc /= 2;
            //        }
            //    }
            //    else
            //        break; // never gonna hapen
            //}
            //lastInc = Math.Min(inc * 4, 0.5);
            //return p2;
        }
        public RectangleF BoundingRectangle(int resterPointCount, float width, Func<float, bool> progressUpdate)
        {
            var rSpline = Rasterize(resterPointCount, width, progressUpdate);
            var maxX = (float)rSpline.X.Max();
            var minX = (float)rSpline.X.Min();
            var maxY = (float)rSpline.Y.Max();
            var minY = (float)rSpline.Y.Min();
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }
        public RasterizedRotatingBezierSpline Rasterize(int count, float thickness, Func<float, bool> progressUpdate)
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> t = new List<double>();
            double r1 = this.A1.R;
            double r2 = this.A2.R;
            double f = 0;

            for (int i =0; i <= count -1;i++)
            {
                var XY = BezierInterpolate((i / (double)(count - 1)), A1, A2);
                x.Add(XY.X);
                y.Add(XY.Y);
                t.Add(r1 * f + r2 * (1 - f));
            }
            return new RasterizedRotatingBezierSpline() { X = x.ToArray(), Y = y.ToArray(), T = t.ToArray() };
        }
        public RasterizedRotatingBezierSpline Rasterize(double resolution, double scale, List<double> x, List<double> y, List<double> t, float thickness, Func<float, bool> progressUpdate)
        {
            double r1 = this.A1.R;
            double r2 = this.A2.R;
            double f = 0;
            while (f <= 1 - resolution)
            {
                double bkpf = f;
                var XY = moveOn(ref f, resolution, scale, resolution * 0.05, A1, A2);
                if (f <= bkpf)
                    ;
                var T = r1 * f + r2 * (1 - f);
                x.Add(XY.X * scale);
                y.Add(XY.Y * scale);
                t.Add(T);
                progressUpdate((float)f);
            }
            return new RasterizedRotatingBezierSpline() { X = x.ToArray(), Y = y.ToArray(), T = t.ToArray() };
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
                RBSPoint pl1_1 = RBSPoint.Intermediate(A1.P, A1.C2, f, false);
                RBSPoint pl1_2 = RBSPoint.Intermediate(A1.C2, A2.C1, f, false);
                RBSPoint pl1_3 = RBSPoint.Intermediate(A2.C1, A2.P, f, false);

                RBSPoint pl2_1 = RBSPoint.Intermediate(pl1_1, pl1_2, f, false);
                RBSPoint pl2_2 = RBSPoint.Intermediate(pl1_2, pl1_3, f, false);

                RBSPoint P = RBSPoint.Intermediate(pl2_1, pl2_2, f, false);

                if (i > 0 && thickness > 0)
                {
                    double fp = (i - 1) / (double)divisions;
                    RBSPoint pp1 = new RBSPoint(
                        pPrevious.X + thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y + thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)), true);

                    RBSPoint pp2 = new RBSPoint(
                        pPrevious.X - thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y - thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)), true);
                    RBSPoint pc1 = new RBSPoint(
                        P.X + thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y + thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)), true);

                    RBSPoint pc2 = new RBSPoint(
                        P.X - thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y - thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)), true);
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
                    g.FillPolygon(new SolidBrush(Color.FromArgb((int)(255 * BezierBoard.Fill), normalColor)), psInk);
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
                    g.FillPolygon(new SolidBrush(Color.FromArgb((int)(255 * BezierBoard.Fill), normalColor)), psInk);
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
        public event EventHandler WidthChangeRequest;
        bool _v = true, _l = false;
        public bool Visible { get { return _v; } set { _v = value;  Board.Invalidate(); } } 
        public bool Locked { get { return _l; } set { _l = value; Board.Invalidate(); } }
        BezierBoard Board { get; set; }
        public RotatingBezierSpline(BezierBoard board, bool mouseEvents) :base(mouseEvents)
        {
            Board = board;
            IncrementLocationRequest += (dx, dy) => {
                if (Locked || !Visible) return;
                foreach (var a in Anchors)
                {
                    a.P.NotifyLocationIncrementRequest(dx, dy);
                }
            };
            OnMouseClick += (s, e) =>
            {
                if (Locked || !Visible) return;
                if (e.Button == MouseButtons.Left)
                {
                    ChangeAppearance();
                }
                else
                {
                    SelfRemoveRequest();
                }
            };
        }
        public RectangleF BoundingRectangle()
        {
            if (Anchors.Count == 0)
                return new RectangleF();
            var maxX = (float)Anchors.Max(a => a.P.X) + FlatTipWidth;
            var minX = (float)Anchors.Min(a => a.P.X) - FlatTipWidth;
            var maxY = (float)Anchors.Max(a => a.P.Y) + FlatTipWidth;
            var minY = (float)Anchors.Min(a => a.P.Y) - FlatTipWidth;
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
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
        public List<RotatingBezierSplineAnchor> Anchors { get; private set; } = new List<RotatingBezierSplineAnchor>();
        Color NormalColor = Color.DarkGray;
        public float FlatTipWidth { get; set; } = 0;
        List<BezierCurveCellWithRotation> lastCurveCellCache;
        InkDrawMode inkDrawModeCache;
        public override void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (!Visible)
                return;
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
                cell.Draw(g, FlatTipWidth, NormalColor, MouseState, inkDrawMode, Locked ? AnchorDrawMode.None : anchorDrawMode, this, ParentControl);
            }
        }
        public RasterizedRotatingBezierSpline Rasterize(double resolution, double scale, Func<float, bool> progressUpdate)
        {
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> T = new List<double>();

            int ind = 0;
            bool Progress(float f)
            {
                f /= (Anchors.Count - 1);
                f += ind / (float)(Anchors.Count - 1);
                progressUpdate(f);
                if (f > 1)
                    f = 1;
                return true;
            }
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                var cell = new BezierCurveCellWithRotation(Anchors[i], Anchors[i + 1]);
                var rast = cell.Rasterize(resolution, scale, X, Y, T, FlatTipWidth, Progress);
                X.AddRange(rast.X);
                Y.AddRange(rast.Y);
                T.AddRange(rast.T);
                ind++;
            }
            return new RasterizedRotatingBezierSpline() { X = X.ToArray(), Y = Y.ToArray(), T = T.ToArray() };
        }
        public override bool TopParentBoundsContains(PointF p)
        {
            if (Locked)
                return false;
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
        public event EventHandler OnAnchorAdded;
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
                var r = a.R1.DistanceFrom(a.P);
                a.R1.X = (float)(a.P.X + r * Math.Cos(Anchors.First().R1.AngleAbout(Anchors.First().P)));
                a.R1.Y = (float)(a.P.Y + r * Math.Sin(Anchors.First().R1.AngleAbout(Anchors.First().P)));
                Anchors.Insert(0, a);
                OnAnchorAdded?.Invoke(this, new EventArgs());
                return a.C1;
            }
            else // force add
            {
                if (Anchors.Count > 0)
                {
                    var r = a.R1.DistanceFrom(a.P);
                    a.R1.X = (float)(a.P.X + r * Math.Cos(Anchors.Last().R1.AngleAbout(Anchors.Last().P)));
                    a.R1.Y = (float)(a.P.Y + r * Math.Sin(Anchors.Last().R1.AngleAbout(Anchors.Last().P)));
                }
                Anchors.Add(a);
                OnAnchorAdded?.Invoke(this, new EventArgs());
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
            var sRet = new RotatingBezierSpline(board, true);

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

        internal void ChangeAppearance()
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
                    WidthChangeRequest?.Invoke(this, new EventArgs());
                    Board.Invalidate(); Application.DoEvents();
                }
            };
            apf.widthTB.ValueChanged += (s2, e2) =>
            {
                FlatTipWidth = apf.widthTB.Value;
                WidthChangeRequest?.Invoke(this, new EventArgs());
                Board.Invalidate(); Application.DoEvents();
            };
            apf.ShowDialog();
        }
    }
    public class ImageItem : BezierBoardItem
    {
        Image img;
        CenterPoint center;
        CurvatureHandlePoint size;
        ImageItem(bool mouseEvents = false):base(mouseEvents) { }
        public ImageItem(Image img, float x, float y, bool mouseEvents):base(mouseEvents)
        {
            this.img = img;
            center = new CenterPoint(x, y, mouseEvents);
            size = new CurvatureHandlePoint(x - img.Width / 2, y - img.Height / 2, true);
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
            var sRet = new ImageItem(stringToImage(((XmlText)(spline.GetElementsByTagName("ImageAdress")[0].FirstChild)).Data), 0, 0, true);
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
            var img = Image.FromFile(fileName);
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return new ImageItem(img, 0, 0,true);
        }

        internal static BezierBoardItem FromClipBoard()
        {
            try
            {
                return new ImageItem(Clipboard.GetImage(), 0, 0, true);
            }
            catch { return null; }
        }
    }
}