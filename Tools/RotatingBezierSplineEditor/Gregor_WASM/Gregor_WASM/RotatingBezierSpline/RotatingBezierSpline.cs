using Gregor_WASM;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static RotatingBezierSplineEditor.BezierBoard;

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
        bool TopParentBoundsContains(PointF p, PointF offsetG, float scale);
        void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode,  AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl);
        bool CanDraw(InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode);
        bool CanBeSelected { get; set; }
        RectangleF BoundsOnTopParent { get; }
    }
    public class BezierBoardItem:IBezierBoardItem
    {
        public string Tag = "";
        public BezierBoard Board { get; set; }
        bool _v = true, _l = false;
        public bool Visible { get { return _v; } set { _v = value; Board.Invalidate(); } }
        public bool Locked { get { return _l; } set { _l = value; Board.Invalidate(); } }
        public event EventHandler OnSelected;
        public event IncrementLocationHandler IncrementLocationRequest;
        public event EventHandler LocationResetRequest;
        public event EventHandler OnSelfRemoveRequest;
        public void SelfRemoveRequest(bool dontAsk = false)
        {
            throw new NotImplementedException();
            //if (this is RotatingBezierSpline && !dontAsk)
            //    if (MessageBox.Show("Do you want to delete this spline?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            //        return;
            //OnSelfRemoveRequest?.Invoke(this, new EventArgs());
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
        public int Index { get; internal set; }

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
        public virtual bool ProcessMouseMove(MouseEventArgsF e, PointF offsetG, float scale, BezierBoardItem filter, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, BezierBoard ParentControl)
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
            if (TopParentBoundsContains(e.Location, offsetG, scale))
            {
                // before returning, call mouse leave on sister controls that had mouse hover
                if (Parent != null)
                {
                    if (Parent.GetChildren().Find(c => c.MouseState == MouseState.Held && c != this) != null) // a sister control is being held and moved
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
                    if (controls[i].ProcessMouseMove(e, offsetG, scale, filter, inkDrawMode, anchorDrawMode, this, ParentControl))
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

                if (TopParentBoundsContains(e.Location, offsetG, scale) || wentDownInBounds)
                {
                    Trace.WriteLine("B1 " + MouseState + ": " + e.Location);
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
                else
                {
                    if (!lastwasOutside)
                    {
                        if (!TopParentBoundsContains(e.Location, offsetG, scale))
                        {
                            NotifyMouseLeave(this, e2, this, ParentControl);
                            lastwasOutside = true;
                        }
                        NotifyMouseMove(this, e2);

                        if (Parent == null)
                            ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
                        else if (Parent.Cursor == Cursors.No)
                            ParentControl.Cursor = ((BezierBoard)ParentControl).DefaultCursor;
                        else ParentControl.Cursor = Parent.Cursor;
                        // don't return true.
                    }
                }
            }
            return false;
        }
        public virtual BezierBoardItem ProcessMouseDown(MouseEventArgsF e, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, BezierBoard ParentControl)
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
                var cr = controls[i].ProcessMouseDown(e, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
                if (cr != null)
                    return cr;
            }
            if (TopParentBoundsContains(e.Location, offsetG, scale))
            {
                NotifyMouseDown(this, e);
                return this;
            }
            return null;
        }

        public virtual void ProcessMouseUp(object sender, PointF offsetG, float scale, MouseEventArgsF e, BezierBoardItem filter, BezierBoardItem Parent, BezierBoard ParentControl)
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
                    controls[i].ProcessMouseUp(this, offsetG, scale, e, filter, this, ParentControl);
                }
            }
            if (filter == this || filter == null)
            {
                if (TopParentBoundsContains(e.Location, offsetG, scale) || wentDownInBounds)
                {
                    NotifyMouseUp(sender, e);
                }
            }
            wentDownInBounds = false;
        }
        public virtual bool TopParentBoundsContains(PointF p, PointF offsetG, float scale)
        {
            return BoundsOnTopParent.Contains(p);
        }
        public virtual void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
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
		public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            float sz = 4F;
            if (MouseState == MouseState.Hover)
                sz = 6;
            var inner = new RectangleF((float)X * scale - sz + offsetG.X,
                (float)Y * scale - sz + offsetG.Y,
                sz * 2, sz * 2);
            var outer = inner;
            outer.Inflate(.5F, .5F);
            if (MouseState == MouseState.Held || MouseState == MouseState.Selected)
                g.FillEllipse(Color.Gray, outer);
            else
                g.FillEllipse(Color.Black, outer);
            g.FillEllipse((MouseState == MouseState.Held || MouseState == MouseState.Selected) ? Color.Black : Color.Gray, 
               inner);
            if (MouseState == MouseState.Held)
                base.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, Parent, ParentControl);
        }
    }
    public class RotationHandlePoint : RBSPoint
    {
        Cursor _c;
        public override Cursor Cursor
        {
            get
            {
                //if (_c == null)
                //    _c = new Cursor(Path.Combine(Application.StartupPath, "resources\\rotation_icon.ico"));
                //return _c;
                return Cursors.Default;
            }
        }
        float _hl = 50;
        public float HandleLength
        {
            get { return _hl; }
            set
            {
                _hl = value; 
                if (_hl < 1)
                    _hl = .0001F;
            }
        }
        public RotationHandlePoint(CenterPoint cp, double angle, float HandleLength, bool mouseEvents) : base(cp.X + HandleLength * Math.Cos(angle), cp.Y + HandleLength * Math.Sin(angle), mouseEvents)
        {
            this.HandleLength = HandleLength;
        }
        public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            float sz = 6F;
            if (MouseState == MouseState.Hover)
                sz = 8;

            g.DrawEllipse(new Gregor_WASM.Pen((MouseState == MouseState.Held || MouseState == MouseState.Selected) ? Color.Gray : Color.Black, 4),
                (float)X * scale - sz + offsetG.X, (float)Y * scale - sz + offsetG.Y, sz * 2, sz * 2);

            g.DrawEllipse(new Gregor_WASM.Pen((MouseState == MouseState.Held || MouseState == MouseState.Selected) ? Color.Black : Color.Gray, 3), 
                (float)X*scale - sz + offsetG.X, (float)Y*scale - sz + offsetG.Y, sz * 2, sz * 2);
            base.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, Parent, ParentControl);
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
        public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            float sz = 6F;
            if (MouseState == MouseState.Hover)
                sz = 7;
            if (MouseState == MouseState.Held || MouseState == MouseState.Selected)
            {
                g.FillRectangle(Color.Gray, (float)X * scale - sz + offsetG.X - 1, (float)Y * scale - sz - 1 + offsetG.Y, sz * 2 + 2, sz * 2 + 2);
                g.FillRectangle(Color.Black, (float)X * scale - sz + offsetG.X, (float)Y * scale - sz + offsetG.Y, sz * 2, sz * 2);
            }
            else
            {
                g.DrawRectangle(new Gregor_WASM.Pen(Color.Black, 3), (float)X * scale - sz + offsetG.X, (float)Y * scale - sz + offsetG.Y, sz * 2, sz * 2);
                g.DrawRectangle(new Gregor_WASM.Pen(Color.Gray, 2), (float)X * scale - sz + offsetG.X, (float)Y * scale - sz + offsetG.Y, sz * 2, sz * 2);
            }

            base.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, Parent, ParentControl);
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
        /// <summary>
        /// Use only for calculating points to be drawn.
        /// </summary>
        /// <param name="offsetG"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public RBSPoint Transform(PointF offsetG, float scale)
        {
            return new RBSPoint(X * scale + offsetG.X , Y * scale + offsetG.Y, false);
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

        public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            //g.DrawEllipse(Pens.Black, (float)X - 3, (float)Y - 3, 6, 6); 
            if (MouseState == MouseState.Held || MouseState == MouseState.Hover)
            {
                g.TranslateTransform(offsetG.X, offsetG.Y);
                g.ScaleTransform(scale, scale);
                g.ScaleTransform(1, -1);
                g.DrawString("{" + X + ", " + Y + "}", new Gregor_WASM.Font("CONSOLA", 20 / scale), Color.Black, X + 10, -Y);
                g.ScaleTransform(1, -1);
                g.ScaleTransform(1 / scale, 1 / scale);
                g.TranslateTransform(-offsetG.X, -offsetG.Y);
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
        public override bool TopParentBoundsContains(PointF p, PointF offsetG, float scale)
        {
            // we need to scale down because the size of the displayed points in normalized.
            var bounds = new RectangleF(
                BoundsOnTopParent.X + (BoundsOnTopParent.Width - BoundsOnTopParent.Width / scale) / 2,
                BoundsOnTopParent.Y + (BoundsOnTopParent.Height - BoundsOnTopParent.Height/ scale) / 2, 
                BoundsOnTopParent.Width / scale, BoundsOnTopParent.Height / scale);
            return bounds.Contains(p);
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
        public RotatingBezierSplineAnchor(float x, float y, float flatTipWidth, bool mouseEvents) : this(new PointF(x, y), flatTipWidth, mouseEvents) 
        { 
        }
        public RotatingBezierSplineAnchor(PointF P, float flatTipWidth, bool mouseEvents) : this(P, new PointF(P.X, P.Y), 0, 0, flatTipWidth, mouseEvents)
        {
        }
        public RotatingBezierSplineAnchor(PointF p, PointF a1, double a2Length, double rotation, float flatTipWidth, bool mouseEvents) : base(mouseEvents)
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
            R1 = new RotationHandlePoint(this.P, rotation, flatTipWidth / 2, mouseEvents);
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
                R1.X = (float)(P.X + R1.HandleLength * Math.Cos(newAngle));
                R1.Y = (float)(P.Y + R1.HandleLength * Math.Sin(newAngle));
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

        public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (anchorDrawMode.HasFlag(AnchorDrawMode.Centers))
                P.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
            if (anchorDrawMode.HasFlag(AnchorDrawMode.CurvatureHandles))
            {
                // draw the lines
                g.DrawLine(new Gregor_WASM.Pen(Color.White, 2), C1.Transform(offsetG, scale), P.Transform(offsetG, scale));
                g.DrawLine(Color.Black, 1, C1.Transform(offsetG, scale), P.Transform(offsetG, scale));
                g.DrawLine(new Gregor_WASM.Pen(Color.White, 2), C2.Transform(offsetG, scale), P.Transform(offsetG, scale));
                g.DrawLine(Color.Black, 1, C2.Transform(offsetG, scale), P.Transform(offsetG, scale));
                C1.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
                C2.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
            }
            if (anchorDrawMode.HasFlag(AnchorDrawMode.RotaionHandles))
            {
                g.DrawLine(new Gregor_WASM.Pen(Color.White, 2), R1.Transform(offsetG, scale), P.Transform(offsetG, scale));
                g.DrawLine(Color.Black, 1, R1.Transform(offsetG, scale), P.Transform(offsetG, scale));
                //g.DrawLine(Pens.Black, R2, P);
                R1.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
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
        public static RotatingBezierSplineAnchor Parse(XmlElement node, float flatTipWidth)
        {
            var ret = new RotatingBezierSplineAnchor(0, 0, flatTipWidth, true);
            ret.rotationsOffset = int.Parse(((XmlText)(node.GetElementsByTagName("rotationoffset")[0].FirstChild)).Data);

            for (int i = 0; i < ret.Points.Length; i++)
                ret.Points[i].ExtractCoordinates((XmlElement)(node.GetElementsByTagName(names[i])[0]));
            return ret;
        }

        internal void ResetRotationHandleLength()
        {
            R1.X = (float)(P.X + R1.HandleLength * Math.Cos(R));
            R1.Y = (float)(P.Y + R1.HandleLength * Math.Sin(R));
        }

        internal RotatingBezierSplineAnchor MakeCopy(float flatTipWidth, float xOffset, float yOffset)
        {
            var anchor = new RotatingBezierSplineAnchor(new PointF(this.P.X, this.P.Y), flatTipWidth, true);
            anchor.P.X = this.P.X + xOffset;
            anchor.P.Y = this.P.Y + yOffset;
            anchor.C1.X = this.C1.X + xOffset;
            anchor.C1.Y = this.C1.Y + yOffset;
            anchor.C2.X = this.C2.X + xOffset;
            anchor.C2.Y = this.C2.Y + yOffset;
            anchor.R1.X = this.R1.X + xOffset;
            anchor.R1.Y = this.R1.Y + yOffset;
            anchor.rotationsOffset = this.rotationsOffset;
            return anchor;
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
        public object RasterizedData { get; internal set; }

        public BezierCurveCellWithRotation(RotatingBezierSplineAnchor a1, RotatingBezierSplineAnchor a2)
        {
            this.A1 = a1;
            this.A2 = a2;
        }
        public bool InkContains(PointF p, PointF offsetG, float scale)
        {
            if (psInk == null) return false;
            return IsPointInPolygon4(psInk, new PointF(
                p.X * scale + offsetG.X,
                p.Y * scale + offsetG.Y));
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
        public static RBSPoint BezierInterpolate(double f, RotatingBezierSplineAnchor A1, RotatingBezierSplineAnchor A2)
        {
            RBSPoint pl1_1 = RBSPoint.Intermediate(A1.P, A1.C2, f, false);
            RBSPoint pl1_2 = RBSPoint.Intermediate(A1.C2, A2.C1, f, false);
            RBSPoint pl1_3 = RBSPoint.Intermediate(A2.C1, A2.P, f, false);

            RBSPoint pl2_1 = RBSPoint.Intermediate(pl1_1, pl1_2, f, false);
            RBSPoint pl2_2 = RBSPoint.Intermediate(pl1_2, pl1_3, f, false);

            RBSPoint P = RBSPoint.Intermediate(pl2_1, pl2_2, f, false);
            return P;
        }
        //public RectangleF BoundingRectangle(int resterPointCount, float width, Func<float, bool> progressUpdate)
        //{
        //    var rSpline = Rasterize(resterPointCount, width, progressUpdate);
        //    var maxX = (float)rSpline.X.Max();
        //    var minX = (float)rSpline.X.Min();
        //    var maxY = (float)rSpline.Y.Max();
        //    var minY = (float)rSpline.Y.Min();
        //    return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        //}
        //public RasterizedRotatingBezierSpline Rasterize(int count, float thickness, Func<float, bool> progressUpdate)
        //{
        //    List<double> x = new List<double>();
        //    List<double> y = new List<double>();
        //    List<double> t = new List<double>();
        //    double r1 = this.A1.R;
        //    double r2 = this.A2.R;
        //    double f = 0;

        //    for (int i =0; i <= count -1;i++)
        //    {
        //        var XY = BezierInterpolate((i / (double)(count - 1)), A1, A2);
        //        x.Add(XY.X);
        //        y.Add(XY.Y);
        //        t.Add(r1 * f + r2 * (1 - f));
        //    }
        //    return new RasterizedRotatingBezierSpline() { X = x.ToArray(), Y = y.ToArray(), T = t.ToArray() };
        //}
        PointF[] psInk;
        PointF[] psSpline;
        public void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, float thickness, Color normalColor, MouseState mouseState, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            // draw the spline
            thickness *= scale;
            int divisions = 20;

                psSpline = new PointF[divisions + 1];
                RBSPoint pPrevious = null;
                double r1 = this.A1.R;
                double r2 = this.A2.R;
                psInk = new PointF[divisions * 4];
            for (int i = 0; i <= divisions; i++)
            {
                double f = i / (double)divisions;
                RBSPoint pl1_1 = RBSPoint.Intermediate(A1.P.Transform(offsetG, scale), A1.C2.Transform(offsetG, scale), f, false);
                RBSPoint pl1_2 = RBSPoint.Intermediate(A1.C2.Transform(offsetG, scale), A2.C1.Transform(offsetG, scale), f, false);
                RBSPoint pl1_3 = RBSPoint.Intermediate(A2.C1.Transform(offsetG, scale), A2.P.Transform(offsetG, scale), f, false);

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
                {
                    if (BezierBoard.FlatTipRenderAlgorithm == FlatTipRenderAlgorithm.Polygon)
                        g.FillPolygon(Color.FromArgb((int)(255 * BezierBoard.Fill), normalColor), psInk);
                    else
                    {
                        for (int i = 0; i < psInk.Length / 2 - 1; i++)
                        {
                            g.FillPolygon(Color.FromArgb((int)(255 * BezierBoard.Fill), normalColor),
                                new PointF[] {
                                    psInk[i], psInk[psInk.Length - i - 1],psInk[psInk.Length - i - 2], psInk[i + 1],
                                }
                                );
                            g.DrawLine(new Gregor_WASM.Pen(Color.FromArgb((int)(255 * BezierBoard.Fill), normalColor), 2),
                                    psInk[i], psInk[psInk.Length - i - 1]
                                );
                        }
                    }
                }
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                    g.DrawLines(new Gregor_WASM.Pen(inkDrawMode.HasFlag(InkDrawMode.Ink) ? Color.White : Color.Gray, 2), psSpline);
            }
            else if (mouseState == MouseState.Hover)
            {
                if (thickness > 0 && inkDrawMode.HasFlag(InkDrawMode.Ink))
                {
                    g.FillPolygon(Color.FromArgb((int)(255 * BezierBoard.Fill), Color.DarkGray), psInk);
                    g.DrawPolygon(new Gregor_WASM.Pen(Color.DarkBlue, 3), psInk);
                }
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                {
                    g.DrawLines(Color.Black, 1, psSpline);
                    g.DrawLines(new Gregor_WASM.Pen(Color.FromArgb(100, 255, 255, 255), 2), psSpline);
                    g.DrawLines(new Gregor_WASM.Pen(Color.FromArgb(100, 0, 0, 0), 1), psSpline);
                }
            }
            else if (mouseState == MouseState.Held)
            {
                if (thickness > 0 && inkDrawMode.HasFlag(InkDrawMode.Ink))
                {
                    g.FillPolygon(Color.FromArgb((int)(255 * BezierBoard.Fill), normalColor), psInk);
                    g.DrawPolygon(new Gregor_WASM.Pen(Color.DarkBlue, 1), psInk);
                }
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                {
                    g.DrawLines(Color.Black, 1, psSpline);
                    g.DrawLines(new Gregor_WASM.Pen(Color.FromArgb(100, 0, 0, 0), 2), psSpline);
                }
            }

            if (inkDrawMode.HasFlag(InkDrawMode.Spline)) // draw the handles as well
            {
                A1.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, Parent, ParentControl);
                A2.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, Parent, ParentControl);
            }
        }

        internal bool SplineIntersects(PointF p, int distance, PointF offsetG, float scale)
        {
            if (psSpline == null)
                return false;
            foreach (var ps in psSpline)
            {
                if (RBSPoint.DistanceBetween(new PointF(
                        p.X * scale + offsetG.X,
                        p.Y * scale + offsetG.Y), 
                        ps
                    ) <= distance)
                    return true;
            }
            return false;
        }

    }
  
    [Serializable]
    public class RotatingBezierSpline : BezierBoardItem
    {
        public string Label { get; set; } = "";
        public event EventHandler WidthChangeRequest;
        public event EventHandler OnShowCurveMenuRequest;
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
                    OnShowCurveMenuRequest(this, new EventArgs());
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
        public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (!Visible)
                return;
            if (FlatTipWidth < 1)
                inkDrawMode &= ~(InkDrawMode.Ink);
            inkDrawModeCache = inkDrawMode;
            lastCurveCellCache = new List<BezierCurveCellWithRotation>();
            if (Anchors.Count == 1) // adding new spline
                Anchors[0].Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                var cell = new BezierCurveCellWithRotation(Anchors[i], Anchors[i + 1]);
                lastCurveCellCache.Add(cell);
                var col = NormalColor;
                if (BezierBoard.ForceSingleColorSplines)
                    col = BezierBoard.ForcedInkColor;
                cell.Draw(g, offsetG, scale, FlatTipWidth, col, MouseState, inkDrawMode, Locked ? AnchorDrawMode.None : anchorDrawMode, this, ParentControl);
            }
        }
        public override bool TopParentBoundsContains(PointF p, PointF offsetG, float scale)
        {
            if (Locked)
                return false;
            if (!BezierBoard.SplinesCanBeSelected)
                return false;
            if (lastCurveCellCache == null) return false;
            else
                foreach (var c in lastCurveCellCache)
                {
                    if (inkDrawModeCache.HasFlag(InkDrawMode.Ink))
                    {
                        if (c.InkContains(p, offsetG, scale))
                            return true;
                    }
                    else if (inkDrawModeCache.HasFlag(InkDrawMode.Spline))
                    {
                        if (c.SplineIntersects(p, 10, offsetG, scale))
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
                {
                    Anchors.Remove(a);
                    if (Anchors.Count == 0) // spline was removed
                    {
                        SelfRemoveRequest(true);
                    }
                }
            };
            a.C1.OnMouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    a.C1.X = a.P.X;
                    a.C1.Y = a.P.Y;
                }
                a.P.MouseState = MouseState.Selected;
                a.P.NotifySelected();
            }; 
            a.C2.OnMouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    a.C2.X = a.P.X;
                    a.C2.Y = a.P.Y;
                }
                a.P.MouseState = MouseState.Selected;
                a.P.NotifySelected();
            };
            a.R1.OnMouseClick += (s, e) =>
            {
                a.P.MouseState = MouseState.Selected;
                a.P.NotifySelected();
            };
            if (CanReceiveAnchorAtStart && Anchors.Count > 1)
            {
                var r = a.R1.DistanceFrom(a.P);
                Anchors.Insert(0, a);
                OnAnchorAdded?.Invoke(this, new EventArgs());
                return a.C1;
            }
            else // force add
            {
                if (Anchors.Count > 0)
                {
                    var r = a.R1.DistanceFrom(a.P);
                }
                Anchors.Add(a);
                OnAnchorAdded?.Invoke(this, new EventArgs());
                return a.C2;
            }
            a.P.MouseState = MouseState.Selected;
            a.P.NotifySelected();
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

            try
            {
                sRet.Label = ((XmlText)(spline.GetElementsByTagName("Label")[0].FirstChild)).Data;
            }
            catch { }
            sRet.FlatTipWidth = float.Parse(((XmlText)(spline.GetElementsByTagName("FlatTipWidth")[0].FirstChild)).Data);
            sRet.NormalColor = Color.FromArgb(int.Parse(((XmlText)(spline.GetElementsByTagName("Color")[0].FirstChild)).Data));
            foreach (XmlElement obj in spline.GetElementsByTagName("anchor"))
                sRet.AddAnchor(RotatingBezierSplineAnchor.Parse(obj, sRet.FlatTipWidth));
            return sRet;
        }
        public override void Save(XmlNode node)
        {
            var doc = node.OwnerDocument;
            var spline = doc.CreateElement("spline");
            node.AppendChild(spline);
            var lab = doc.CreateElement("Label");
            var v = doc.CreateTextNode(Label);
            lab.AppendChild(v);
            spline.AppendChild(lab);
            var wid = doc.CreateElement("FlatTipWidth");
            v = doc.CreateTextNode(FlatTipWidth.ToString());
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
            throw new NotImplementedException();
            //SplineAppearanceEditor apf = new SplineAppearanceEditor();
            //apf.widthTB.Value = (int)FlatTipWidth;
            //apf.colorP.BackColor = NormalColor;
            //apf.colorP.Click += (s2, e2) =>
            //{
            //    var cd = new ColorDialog();
            //    cd.Color = apf.colorP.BackColor;
            //    if (cd.ShowDialog() == DialogResult.OK)
            //    {
            //        apf.colorP.BackColor = cd.Color;
            //        NormalColor = cd.Color;
            //        WidthChangeRequest?.Invoke(this, new EventArgs());
            //        Board.Invalidate(); Application.DoEvents();
            //    }
            //};
            //apf.widthTB.ValueChanged += (s2, e2) =>
            //{
            //    FlatTipWidth = apf.widthTB.Value;
            //    WidthChangeRequest?.Invoke(this, new EventArgs());
            //    foreach (var a in Anchors)
            //    {
            //        a.R1.HandleLength = FlatTipWidth / 2;
            //        a.ResetRotationHandleLength();
            //    }
            //    Board.Invalidate(); Application.DoEvents();
            //};
            //apf.ShowDialog();
        }
        public override string ToString()
        {
            return Index.ToString() + ": " + Label;
        }

        internal RotatingBezierSpline MakeCopy(BezierBoard board)
        {
            var spline = new RotatingBezierSpline(board, true);
            foreach (var a in Anchors)
                spline.AddAnchor(a.MakeCopy(FlatTipWidth, 
                    (Anchors.Max(an => an.GetChildren().Select(bi => (RBSPoint)bi).Max(rp => rp.X)) -
                    Anchors.Min(an => an.GetChildren().Select(bi => (RBSPoint)bi).Min(rp => rp.X))) * 1.2F, 0));
            spline.Label = this.Label + " - Duplicate";
            spline.FlatTipWidth = this.FlatTipWidth;
            return spline;
        }
    }
    public class ImageItem : BezierBoardItem
    {
        public SKImage SourceImage { get; private set; }
        CenterPoint center;
        CurvatureHandlePoint size;
        ImageItem(bool mouseEvents = false):base(mouseEvents) { }
        public ImageItem(BezierBoard board, SKImage img, float x, float y, bool mouseEvents):base(mouseEvents)
        {
            Board = board;
            this.SourceImage = img;
            center = new CenterPoint(x, y, mouseEvents);
            size = new CurvatureHandlePoint(x - img.Width / 2, y - img.Height / 2, true);
            center.IncrementLocationRequest += (dx, dy) =>
            {
                size.X += dx;
                size.Y += dy;
                center.X += dx;
                center.Y += dy;
            };
            center.OnMouseClick += (s, e) =>
            {
                if (Locked)
                    return;
                if (e.Button == MouseButtons.Right)
                {
                    SelfRemoveRequest();
                }
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
        public override void Draw(Gregor_WASM.Graphics g, PointF offsetG, float scale, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode, BezierBoardItem Parent, Control ParentControl)
        {
            if (!Visible) return;
            if (inkDrawMode.HasFlag(InkDrawMode.Images))
            {
                PointF[] destinationPoints = {
          new PointF(size.X, size.Y),   // destination for upper-left point of original
          new PointF(size.X + 2 * (float)Math.Abs(size.X - center.X), size.Y),  // destination for upper-right point of original
          new PointF(size.X, size.Y - 2 * (size.Y - center.Y))};  // destination for lower-left point of original


                // Draw the image mapped to the parallelogram.
                //TBD
                //g.DrawImage(SourceImage, destinationPoints);
                //g.DrawImage(img, size.X, size.Y, 2 * (float)Math.Abs(size.X - center.X), 2 * (float)Math.Abs(size.Y - center.Y));
                size.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
                center.Draw(g, offsetG, scale, inkDrawMode, anchorDrawMode, this, ParentControl);
            }
        }

        internal static BezierBoardItem Parse(BezierBoard board, XmlElement spline)
        {
            var sRet = new ImageItem(board, stringToImage(((XmlText)(spline.GetElementsByTagName("ImageAdress")[0].FirstChild)).Data), 0, 0, true);
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
            var imagedata = imageToString(SourceImage);
            var v = doc.CreateTextNode(imagedata);
            imageAddress.AppendChild(v);
            spline.AppendChild(imageAddress);
            center.Save(spline, "center");
            size.Save(spline, "size");
        }
        static SKImage stringToImage(string str)
        {
            return SKImage.FromEncodedData(new MemoryStream(str.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(s => byte.Parse(s, System.Globalization.NumberStyles.HexNumber)).ToArray()));
        }
        static string imageToString(SKImage img)
        {
            //var imagedata = Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".jpg";
            //img.Save(imagedata, System.Drawing.Imaging.ImageFormat.Jpeg);
            //return imagedata;
            MemoryStream ms = new MemoryStream();
            img.Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);
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

        internal static ImageItem FromFile(BezierBoard board, string fileName)
        {
            var img = SKImage.FromEncodedData(File.ReadAllBytes(fileName));
            // TBD
            //img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return new ImageItem(board, img, 0, 0,true);
        }

        internal static ImageItem FromClipBoard(BezierBoard board)
        {
            throw new NotImplementedException();
            //try
            //{
            //    return new ImageItem(board, Clipboard.GetImage(), 0, 0, true);
            //}
            //catch { return null; }
        }

        internal static ImageItem FromImage(SKImage bitmap, BezierBoard board)
        {
            try
            {
                return new ImageItem(board, bitmap, 0, 0, true);
            }
            catch { return null; }
        }
    }
}