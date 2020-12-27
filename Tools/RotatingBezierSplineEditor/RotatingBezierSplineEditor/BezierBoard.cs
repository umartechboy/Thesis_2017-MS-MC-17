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
    public class BezierBoard : Panel
    {
        AnchorDrawMode _AnchorDrawMode = AnchorDrawMode.Centers;
        InkDrawMode _InkDrawMode = InkDrawMode.All;
        public InkDrawMode InkDrawMode { get { return _InkDrawMode; } set { _InkDrawMode = value; Invalidate(); } }
        public AnchorDrawMode AnchorDrawMode { get { return _AnchorDrawMode; } set { _AnchorDrawMode = value; Invalidate(); } }
        List<BezierBoardItem> Objects = new List<BezierBoardItem>();

        MouseButtons MouseButtonsThatWentDown = MouseButtons.None;
        Point MouseLocationAtDown;
        public BezierBoard()
        {
            DoubleBuffered = true;
            MouseMove += BezierBoard_MouseMove;
            MouseDown += BezierBoard_MouseDown;
            MouseUp += BezierBoard_MouseUp;
            SizeChanged += BezierBoard_SizeChanged;
            Click += BezierBoard_Click;
        }
        bool _ge = true, _se = true, _xyl = true;
        public bool GridEnabled { get{ return _ge; }set { _ge = value; BezierBoard_SizeChanged(null, null);Invalidate(); } }
        public bool ScaleEnabled { get { return _se; } set { _se = value; BezierBoard_SizeChanged(null, null); Invalidate(); } }
        public bool XYLinesEnabled { get { return _xyl & GridEnabled; } set { _xyl = value; Invalidate(); } }
        public int  XAxisHeight { get { return ScaleEnabled? 20:0; } }
        public int YAxisWidth { get { return ScaleEnabled ? 50 : 0; } }
        private void BezierBoard_SizeChanged(object sender, EventArgs e)
        {
            PlotBounds = new RectangleF(0, 0, Width - YAxisWidth, Height - XAxisHeight);
            YAxisBounds = new RectangleF(Width - YAxisWidth, 0, YAxisWidth, Height - XAxisHeight);
            XAxisBounds = new RectangleF(0, Height - XAxisHeight, Width - YAxisWidth, XAxisHeight);
        }

        public BezierBoardItem AddItem(BezierBoardItem item)
        {
            if (item == null) return null;
            Objects.Add(item);
            item.OnSelfRemoveRequest += (s, e) => { Objects.Remove(item); Invalidate(); };
            if (item is RotatingBezierSpline)
            {
                var sitem = (RotatingBezierSpline)item;
                sitem.OnSelected += (s, e) => { foreach (var obj in Objects)
                        if (obj is RotatingBezierSpline)
                        {
                            if (obj == sitem)
                                continue;
                            ((RotatingBezierSpline)obj).UnselectAllAnchors();
                        }
                };
            }
            return item;
        }
        
        private void BezierBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseAtDownForScale != null)
            { MouseAtDownForScale = null; return; }
            if (BoardMoveStartedAt != null)
            { BoardMoveStartedAt = null; return; }
            var pT = GtoV(e.Location);
            e = new MouseEventArgs(e.Button, e.Clicks, pT.X, pT.Y, e.Delta);
            for (int i = Objects.Count - 1; i >= 0; i--)
                Objects[i].ProcessMouseUp(sender, e, currentControlUnderMouse, null, this);
            currentControlUnderMouse = null;
            Invalidate();
        }
        public new Cursor DefaultCursor = Cursors.Default;
        PointF OffsetGAtMouseDown; // for panning
        object BoardMoveStartedAt = null;
        object MouseAtDownForScale = null; // for scaling
        PointF VatMouseDown; // for scaling
        Point lastMouseG;
        float ppuAtMouseDown = 1;
        public bool clickIsForAdding = true;
        private void BezierBoard_MouseDown(object sender, MouseEventArgs e)
        {
            MouseButtonsThatWentDown = e.Button;
            MouseLocationAtDown = e.Location;
            var pT = GtoV(e.Location);
            var eBkp = new Point(e.X, e.Y);
            e = new MouseEventArgs(e.Button, e.Clicks, pT.X, pT.Y, e.Delta);
            var controls = Objects;
            for (int i = controls.Count - 1; i >= 0; i--)
            {
                currentControlUnderMouse = controls[i].ProcessMouseDown(e, InkDrawMode, AnchorDrawMode, null, this);
                if (currentControlUnderMouse != null)
                {
                    Invalidate();
                    return;
                }
            }
            // we reach here only only when no controls are under the mouse.
            foreach(var o in Objects)
            {
                if (o is RotatingBezierSpline)
                {
                    var so = (RotatingBezierSpline)o;
                    if (so.CanReceiveAnchorAtEnd || so.CanReceiveAnchorAtStart)
                    {
                        if (e.Button == MouseButtons.Left && clickIsForAdding) // this is not an append operation, but a cancel op.
                        {
                            continuousAnchorAddition(e, so);
                            return;
                        }
                        else
                        {
                            foreach (var o_ in Objects)
                            {
                                if (o_ is RotatingBezierSpline)
                                { ((RotatingBezierSpline)o_).UnselectAllAnchors(); }
                            }
                            return;
                        }
                    }
                }
            }
            // we move here only when no spline is being appended to
            if (e.Button == MouseButtons.Right) // initiate scale
            {
                //GAtMouseDown = eG.X;
                VatMouseDown = GtoV(eBkp);
                MouseAtDownForScale = eBkp;
                ppuAtMouseDown = PPU;
            }
            else // move or add new curve
            {
                if (clickIsForAdding)
                {
                    var so = new RotatingBezierSpline(this);
                    AddItem(so);
                    continuousAnchorAddition(e, so);
                    Invalidate();
                    //RotatingBezierSpline newSpline = ;
                    //var newSpline.AddAnchor(new RotatingBezierSplineAnchor(e.Location))
                }
                else
                {
                    BoardMoveStartedAt = eBkp;
                    OffsetGAtMouseDown = OffsetG;
                }
            }
            lastMouseG = eBkp;
        }
        public void ForceBeginDragItem(BezierBoardItem item)
        {
            Cursor.Position = new Point(0, 0);
            ForceBeginDragItem(item, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }
        public void ForceBeginDragItem(BezierBoardItem item, MouseEventArgs e)
        {
            if (item == null) return;
            currentControlUnderMouse = item;
            item.NotifyMouseDown(this, e);
            item.MouseState = MouseState.Held;
        }
        void continuousAnchorAddition(MouseEventArgs e, RotatingBezierSpline so)
        {
            var a1 = e.Location;
            a1.Offset(1, 1);
            anchorToAdd = new RotatingBezierSplineAnchor(e.Location, a1, 0, 0);
            anchorToAdd.BindCurvatureHandlesLength = true;
            CurvatureHandlePoint pointToMoveWhenAdding = so.AddAnchor(anchorToAdd);
            so.UnselectAllAnchors();
            ForceBeginDragItem(pointToMoveWhenAdding, e);
            void unbindEv(object ss, EventArgs ee)
            {
                anchorToAdd.BindCurvatureHandlesLength = false;
                pointToMoveWhenAdding.OnMouseUp -= unbindEv;
                anchorToAdd.P.MouseState = MouseState.Selected;
            }
            pointToMoveWhenAdding.OnMouseUp += unbindEv;
        }
        BezierBoardItem currentControlUnderMouse = null;
        RotatingBezierSplineAnchor anchorToAdd;

        public float VxtoG(float v, float offsetG, float ppu)
        {
            return v * ppu + offsetG;
        }
        public float GxtoV(float vG, float offsetG, float ppu)
        {
            return (vG - offsetG) / ppu;
        }
        public float VytoG(float v, float offsetG, float ppu)
        {
            return -(v * ppu + offsetG) + Height - XAxisHeight;
        }
        public float GytoV(float vG, float offsetG, float ppu)
        {
            return ((Height - XAxisHeight - vG) - offsetG) / ppu;
        }
        public Point GtoV(Point p)
        {
            return new Point(
                (int)GxtoV(p.X, OffsetG.X, PPU), 
                (int)GytoV(p.Y, OffsetG.Y, PPU));
        }
        public Point VtoG(Point p)
        {
            return new Point(
                (int)VxtoG(p.X, OffsetG.X, PPU), 
                (int)VytoG(p.Y, OffsetG.Y, PPU));
        }

        public void ScaleChanged(PointF latestPoint, PointF lastPoint)
        {
            OffsetG = new PointF(
                ((Point)MouseAtDownForScale).X - VatMouseDown.X * PPU,
                (Height - XAxisHeight - ((Point)MouseAtDownForScale).Y) - VatMouseDown.Y * PPU);
        }

        private void BezierBoard_Click(object sender, EventArgs e)
        {
            if (currentControlUnderMouse != null)
                currentControlUnderMouse.NotifyMouseClick(MouseLocationAtDown, MouseButtonsThatWentDown);
        }

        private void BezierBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseAtDownForScale != null)
            {
                var ec = ((Point)MouseAtDownForScale);
                var er = new Point((Width - (int)YAxisWidth) / 2, (Height - XAxisHeight) / 2);
                var refD = RBSPoint.DistanceBetween(ec.X, ec.Y, er.X, er.Y);
                PPU = ppuAtMouseDown * (float)(RBSPoint.DistanceBetween(e.X, e.Y, er.X, er.Y) / refD);
                if (PPU < .01F)
                    PPU = 0.01F;
                else if (PPU > 100)
                    PPU = 100;
                ScaleChanged(e.Location, lastMouseG);
                lastMouseG = e.Location;
                Invalidate();
                return;
            }
            if (BoardMoveStartedAt != null)
            {
                OffsetG = new PointF(
                    OffsetGAtMouseDown.X + e.X - ((Point)BoardMoveStartedAt).X,
                    OffsetGAtMouseDown.Y - e.Y + ((Point)BoardMoveStartedAt).Y
                    );
                lastMouseG = e.Location;
                Invalidate();
                return;
            }
            var pT = GtoV(e.Location);
            e = new MouseEventArgs(e.Button, e.Clicks, pT.X, pT.Y, e.Delta);
            var controls = Objects;
            for (int i = controls.Count - 1; i >= 0; i--)
            {
                if (controls[i] == null)
                    continue;
                if (controls[i].ProcessMouseMove(e, currentControlUnderMouse, InkDrawMode, AnchorDrawMode, null, this))
                {
                    break;
                }
            }
            Invalidate();
        }

        internal void ImportObjects(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            foreach (XmlElement obj in doc.GetElementsByTagName("image"))
                AddItem(ImageItem.Parse(obj));
            foreach (XmlElement obj in doc.GetElementsByTagName("spline"))
                AddItem(RotatingBezierSpline.Parse(obj, this));
        }

        internal void ClearObjects()
        {
            Objects.Clear();
        }

        internal void SaveObjects(string fileName, params int [] indices)
        {
            if (indices == null) indices = new int[0];
            if (indices.Length == 0)
            {
                indices = new int[Objects.Count];
                for (int i = 0; i < indices.Length; i++)
                    indices[i] = i;
            }
            indices = indices.Distinct().ToArray();

            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            var main = doc.CreateElement("objects");
            foreach (var ind in indices)
                Objects[ind].Save(main);
            doc.AppendChild(main);
            doc.Save(fileName);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawAxisAndGrid_Horizontal(g);
            DrawAxisAndGrid_Vertical(g);

            g.SetClip(new RectangleF(0, 0, Width - YAxisWidth, Height - XAxisHeight));
            g.DrawRectangle(Pens.DarkSlateBlue, 0, 0, PlotBounds.Width - 1, PlotBounds.Height - 1);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.ScaleTransform(1, -1);
            g.TranslateTransform(0 + OffsetG.X, -Height + OffsetG.Y + XAxisHeight);
            g.ScaleTransform(PPU, PPU);
            foreach (var obj in Objects.FindAll(o => o is ImageItem))
                obj?.Draw(e.Graphics, InkDrawMode, AnchorDrawMode, null, this);
            foreach (var obj in Objects.FindAll(o => !(o is ImageItem)))
                obj?.Draw(e.Graphics, InkDrawMode, AnchorDrawMode, null, this);

            g.ResetTransform();
            g.ResetClip();
            if (MouseAtDownForScale != null)
            {
                var lineC = PPU < ppuAtMouseDown ? Color.DarkGreen : Color.DarkRed;
                var line = new Pen(lineC, 3);
                var p1 = new Point((Width - YAxisWidth) / 2, (Height - XAxisHeight) / 2);
                var p2 = lastMouseG;
                var p3 = (Point)MouseAtDownForScale;
                g.DrawLine(line, p1, p2);
                g.DrawRectangle(new Pen(lineC,1), p1.X - 3, p1.Y - 3, 6, 6);
                g.DrawRectangle(new Pen(lineC, 1), p2.X - 3, p2.Y - 3, 6, 6);
                g.DrawLine(Pens.Red, p3.X, p3.Y - 20, p3.X, p3.Y + 20);
                g.DrawLine(Pens.Red, p3.X - 20, p3.Y, p3.X + 20, p3.Y);
                g.DrawRectangle(Pens.Red, p3.X - 40, p3.Y - 40, 80, 80);
                var r = (float)RBSPoint.DistanceBetween(p1.X, p1.Y, p3.X, p3.Y);
                g.DrawEllipse(Pens.Black, p1.X - r, p1.Y - r, 2 * r, 2 * r);
            }
            if (BoardMoveStartedAt != null)
            {
                var p1 = (Point)BoardMoveStartedAt;
                var p2 = lastMouseG;
                g.DrawLine(Pens.DarkBlue, p1, p2);
                g.DrawRectangle(Pens.DarkBlue, p1.X - 3, p1.Y - 3, 6, 6);
                g.DrawRectangle(Pens.DarkBlue, p2.X - 3, p2.Y - 3, 6, 6);
            }

        }
        float PPU = 1F;
        PointF OffsetG = new PointF(100,100);
        Font tickFont = new Font("ARIAL", 10);
        RectangleF PlotBounds, YAxisBounds, XAxisBounds;
        public void DrawAxisAndGrid_Horizontal(Graphics g)
        {
            if (!ScaleEnabled && !GridEnabled)
                return;
            float xs = PPU;
            float xog = OffsetG.X;
            // X Axis

            var axisP = new Pen(Color.Black, 1.5F);
            var majLine = new Pen(Color.DarkGray, 1F);
            var minLine = new Pen(Color.LightGray, 1F);

            double unitX = 1e-8F;
            double multF = 5;
            // determine scale first
            while (unitX * xs < tickFont.Height * 2.2F)
            {
                unitX *= multF;
                multF = multF == 2 ? 5 : 2;
            }

            double minX = 0, maxX = 0;
            while (minX * xs < -xog)
            {
                if (double.IsPositiveInfinity(minX + unitX))
                { minX = float.MaxValue; break; }
                minX += unitX;
            }
            while (minX * xs > -xog)
            {
                if (double.IsNegativeInfinity(minX - unitX))
                { minX = double.MinValue; break; }
                minX -= unitX;
            }

            while (maxX * xs > PlotBounds.Width - xog)
            {
                if (double.IsNegativeInfinity(maxX - unitX))
                { minX = double.MinValue; break; }
                maxX -= unitX;
            }
            while (maxX * xs < PlotBounds.Width - xog)
            {
                if (double.IsPositiveInfinity(maxX + unitX))
                { maxX = float.MaxValue; break; }
                maxX += unitX;
            }



            int xaHei = (tickFont.Height * 15 / 10);
            bool isMinLine = false;

            var xSigFiguresAfterD = 0;
            var totalFigs = (unitX / 2 - Math.Floor(unitX / 2)).ToString().Length - 2;

            while (Math.Round(unitX, xSigFiguresAfterD) == Math.Round(unitX / 2, xSigFiguresAfterD)
                && xSigFiguresAfterD <= totalFigs)
                xSigFiguresAfterD++;

            for (double i = minX; i <= maxX; i += unitX / 2)
            {
                //PointF drawableMid = VtoG(new PointF(i, 0), xog / xs, xs, 1, 0);
                //drawableMid = new PointF(drawableMid.X, h);

                PointF drawable1 = new PointF((float)i * xs + xog + PlotBounds.X, PlotBounds.Y);
                PointF drawable2 = new PointF((float)i * xs + xog + PlotBounds.X, PlotBounds.Y + PlotBounds.Height);
                //if (grid)
                //drawable1 = new PointF(drawable1.X, 0);
                //if (grid)
                //drawable2 = new PointF(drawable2.X, h - xaHei);
                string sNum = NumberUtils.roundedFrac((float)i, xSigFiguresAfterD);
                var xyo = g.MeasureString(sNum, tickFont);
                PointF drawableStrPos = new PointF(drawable2.X - xyo.Width / 2, XAxisBounds.Y + 8);
                if (!isMinLine)
                {
                    drawable2 = new PointF((float)i * xs + xog + PlotBounds.X, PlotBounds.Y + PlotBounds.Height + 5);
                    if (drawable1.X < PlotBounds.Width && drawable1.X >= 0)
                    {
                        if (GridEnabled)
                            g.DrawLine(majLine, drawable1, drawable2);
                        g.DrawString(sNum, tickFont, Brushes.Black, drawableStrPos.X, drawableStrPos.Y);
                    }
                }
                else
                {
                    if (drawable1.X < PlotBounds.Width && drawable1.X > 0)
                    {
                        if (GridEnabled)
                            g.DrawLine(minLine, drawable1, drawable2);
                    }
                }
                isMinLine = !isMinLine;
            }
            if (XYLinesEnabled)
                if (xog < PlotBounds.Width && xog > 0)
                    g.DrawLine(axisP, xog, 0, xog, PlotBounds.Height);

            //g.DrawLine(axisP, AxisBounds.X, AxisBounds.Y, AxisBounds.X + AxisBounds.Width, AxisBounds.Y);

            //// axis labels are buttons now. Dont draw their strings
            //var unitStr = new DXString() { Text = Unit, Color = Color.Black, DXFont = tickFont };
            //var unitSize = g.MeasureString(unitStr);
            //g.DrawString(unitStr, AxisBounds.X + AxisBounds.Width / 2 - unitSize.Width / 2, AxisBounds.Y + tickFont.Height * 0.9F);
        }

        public void DrawAxisAndGrid_Vertical(Graphics g)
        {
            if (!ScaleEnabled && !GridEnabled)
                return;
            YAxisBounds.Height = PlotBounds.Height;
            YAxisBounds.Y = PlotBounds.Y;
            float ys = PPU;
            float yog = OffsetG.Y;

            // X Axis
            var axisP = new Pen(Color.Black, 1.5F);
            var majLine = new Pen(Color.DarkGray, 1F);
            var minLine = new Pen(Color.LightGray, 1F);

            // Y Axis
            double unitY = 1e-8F;
            double multF = 5;
            multF = 5;
            // determine scale first
            while (unitY * ys < tickFont.Height * 1.5F)
            {
                if (double.IsNegativeInfinity(unitY * multF))
                { unitY = double.MinValue; break; }
                if (double.IsPositiveInfinity(unitY * multF))
                { unitY = double.MaxValue; break; }
                unitY *= multF;
                multF = multF == 2 ? 5 : 2;
            }
            //if (unitY < 1e-7 || unitY > 1e7)
            //    return drawingRect;

            double minY = 0, maxY = 0;
            while (minY * ys < -yog)
            {
                if (double.IsPositiveInfinity(minY + unitY))
                { minY = double.MaxValue; break; }
                minY += unitY;
            }
            while (minY * ys + yog > 0)
            {
                if (double.IsNegativeInfinity(minY - unitY))
                { minY = double.MinValue; break; }
                minY -= unitY;
            }

            while (maxY * ys + yog > YAxisBounds.Height)
            {
                if (double.IsNegativeInfinity(maxY - unitY))
                { minY = double.MinValue; break; }
                maxY -= unitY;
            }
            while (maxY * ys + yog < YAxisBounds.Height)
            {
                if (double.IsPositiveInfinity(maxY + unitY))
                { maxY = double.MaxValue; break; }
                maxY += unitY;
            }


            bool isMinLine = false;
            var ySigFiguresAfterD = 0;
            var totalFigs = (unitY / 2 - Math.Floor(unitY / 2)).ToString().Length - 2;

            while (Math.Round(unitY, ySigFiguresAfterD) == Math.Round(unitY / 2, ySigFiguresAfterD)
                && ySigFiguresAfterD <= totalFigs)
                ySigFiguresAfterD++;
            for (double i = minY; i <= maxY; i += unitY / 2)
            {
                //PointF drawableMid = VtoG(new PointF(0, i), 1, 1, yog / ys, ys, PlotBounds.Height);
                //drawableMid = new PointF(PlotBounds.Width, drawableMid.Y);

                PointF drawable1 = new PointF(PlotBounds.X, (float)PlotBounds.Height - (float)(PlotBounds.Y + i * ys + yog));
                PointF drawable2 = new PointF(PlotBounds.X + PlotBounds.Width, (float)PlotBounds.Height - (float)(PlotBounds.Y + i * ys + yog));
                if (!isMinLine)
                {
                    drawable2 = new PointF(PlotBounds.X + PlotBounds.Width + 5, (float)PlotBounds.Height - (float)(PlotBounds.Y + i * ys + yog));

                    string s = NumberUtils.roundedFrac((float)i, ySigFiguresAfterD);
                    var xyo = g.MeasureString(s, tickFont);
                    PointF drawableStrPos = new PointF(YAxisBounds.X + 6, drawable2.Y - xyo.Height / 2);
                    if (drawable2.Y < PlotBounds.Y + PlotBounds.Height && drawable2.Y > PlotBounds.Y)
                    {
                        if (GridEnabled)
                            g.DrawLine(majLine, drawable1, drawable2);
                        g.DrawString(s, tickFont, Brushes.Black, drawableStrPos.X, drawableStrPos.Y);
                    }
                }
                else
                {
                    if (drawable2.Y < PlotBounds.Height && drawable2.Y > 0)
                        if (GridEnabled)
                            g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }

            // zero line
            if (XYLinesEnabled)
                if (yog < YAxisBounds.Height && yog > 0)
                    g.DrawLine(axisP, PlotBounds.X, PlotBounds.Y + PlotBounds.Height - yog, PlotBounds.X + PlotBounds.Width, PlotBounds.Y + PlotBounds.Height - yog);

            // draw border
            //g.DrawLine(axisP, AxisBounds.X, AxisBounds.Y, AxisBounds.X, AxisBounds.Y + AxisBounds.Height);

            // axis labels are buttons now. Dont draw their strings
            //var unitStr = new DXString() { Text = Unit, Color = Color.Black, DXFont = Font };
            //var unitSize = g.MeasureString(unitStr);

            //g.DrawString(unitStr, AxisBounds.X + Font.Height * 2.5F, AxisBounds.Y + AxisBounds.Height/2 + unitSize.Width/2, -90);
        }
    }
    public enum InkDrawMode
    {
        None = 0,
        Spline = 1,
        Ink = 2,
        Images = 4,
        CompleteSpline = Spline | Ink,
        All = CompleteSpline | Images
    }
    public enum AnchorDrawMode
    {
        None = 0,
        Centers = 1,
        RotaionHandles = 2 ,
        CurvatureHandles = 4,
    }
    public class NumberUtils
    {
        static string prefixes = "afpum kMTPA";
        public static string roundedFrac(float number, int significantFigures)
        {
            var isNeg = number < 0;
            if (isNeg)
                number = -number;

            if (number == 0)
                return "0";
            int multi = 5;
            while (number < 1 && multi > 0)
            { number *= 1000; multi--; }
            while (number >= 1000 && multi < 10)
            { number /= 1000; multi++; }

            return ((Math.Round(number, 3) * (isNeg ? -1 : 1)) + (multi != 5 ? prefixes[multi].ToString() : "").ToString());
        }
    }
}
