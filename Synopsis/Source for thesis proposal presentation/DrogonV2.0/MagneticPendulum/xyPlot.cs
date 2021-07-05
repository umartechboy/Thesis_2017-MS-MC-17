using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using FivePointNine.Numbers;

namespace MagneticPendulum
{
    public delegate void ValueChangeHandler(float currentValue);
    public class xyPlot : Panel
    {
        public event ValueChangeHandler XOffsetChanged;
        public event ValueChangeHandler XScaleChanged;
        Size LastSize;
        public bool LastPointHighlight { get; set; } = false;
        public Color LineColor { get; set; } = Color.Black;
        public float LineThickness { get; set; } = 3;
        public float LineOpacity { get; set; } = 1;
        public xyPlot()
        {
            DoubleBuffered = true;
            XPPU = Width; // 1 sec per width
            YPPU = 0.5F; // 1 degree per pixel
            xOffsetG = Width; // right of the screen.
            yOffsetG = Height / 2; // center of the screen
            LastSize = Size;
            SizeChanged += TimePlot_SizeChanged;
            MouseDown += TimePlot_MouseDown;
            MouseUp += TimePlot_MouseUp;
            MouseMove += TimePlot_MouseMove;
            Timer t = new Timer();
            t.Tick += T_Tick;
            t.Interval = 20;
            t.Enabled = true;
            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Items.Add("Line thickness");
            ContextMenuStrip.Items.Add("Line color");
            ContextMenuStrip.Items.Add("Opacity color");
            //ContextMenuStrip.Items.Add("Line style");
            for (int i = 0; i < 5; i++)
            {
                ((ToolStripMenuItem)ContextMenuStrip.Items[0]).DropDownItems.Add(i.ToString());
                ((ToolStripMenuItem)ContextMenuStrip.Items[0]).DropDownItems[i].Click += LineThicknessItemClick;
            }
            int ind = 0;
            foreach (var c in CommonColors())
            {
                ((ToolStripMenuItem)ContextMenuStrip.Items[1]).DropDownItems.Add("");
                ((ToolStripMenuItem)ContextMenuStrip.Items[1]).DropDownItems[ind].BackColor = c;
                ((ToolStripMenuItem)ContextMenuStrip.Items[1]).DropDownItems[ind].Click += LineColorItemClick;
                ind++;
            }
            for (int i = 10; i <= 100; i += 10)
            {
                ((ToolStripMenuItem)ContextMenuStrip.Items[2]).DropDownItems.Add(i.ToString() + "%");
                ((ToolStripMenuItem)ContextMenuStrip.Items[2]).DropDownItems[i/10 - 1].Click += LineOpacityItemClick; ;
            }
        }

        private void LineOpacityItemClick(object sender, EventArgs e)
        {
            LineOpacity = Convert.ToSingle(((ToolStripItem)sender).Text.TrimEnd(new char[] { '%'}))/100;
        }

        private void LineColorItemClick(object sender, EventArgs e)
        {
            LineColor = ((ToolStripItem)sender).BackColor;
        }

        private void LineThicknessItemClick(object sender, EventArgs e)
        {
            LineThickness = Convert.ToInt16(((ToolStripItem)sender).Text);
            Invalidate();
        }

        Color[] CommonColors()
        {
            UInt32 [] vs = new UInt32[] { 0x0, 0xC0C0C0, 0x808080, 0xFFFFFF, 0x800000, 0xFF0000, 0x800080, 0xFF00FF, 0x8000, 0x0FF00, 0x808000, 0xFFFF00, 0x80, 0x0000FF, 0x8080, 0x00FFFF};

            List<Color> colorsList = new List<Color>();
            foreach (var v in vs)
            {
                colorsList.Add(Color.FromArgb((int)(v%256), (int)((v >> 8)%256), (int)((v>>16)%256)));
            }
            //var colors = Enum.GetValues(typeof(KnownColor));
            //foreach (KnownColor knowColor in colors)
            //{
            //    var c = Color.FromKnownColor(knowColor);
            //    if (c.A != 255)
            //        continue;
            //    colorsList.Add(c);
            //}
            return colorsList.ToArray();
        }
        
        private void T_Tick(object sender, EventArgs e)
        {
            if (!needsRefresh)
                return;
            needsRefresh = false;
            Invalidate();
        }

        MoveOp CurrentMoveOp = MoveOp.None;
        MoveOp TentativeOp = MoveOp.None;
        Point LastMouse = new Point();
        Point MouseDownAt = new Point();
        public void AutoScale()
        {
            if (MinX.X == MaxX.X)
                return;
            if (MinY.Y == MaxY.Y)
                return;
            xppu = Width / (MaxX.X - MinX.X) * 0.8F;
            xOffsetG = -MinX.X * xppu + (MaxX.X - MinX.X) * 0.2F * xppu/2;
            YPPU = Height / (MaxY.Y - MinY.Y) * 0.8F;
            yOffsetG = -MinY.Y * YPPU + (MaxY.Y - MinY.Y) * 0.2F * YPPU / 2;
        }
        private void TimePlot_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentMoveOp == MoveOp.None)
            {
                if (e.Location.Y > Height - 15)
                {
                    Cursor = Cursors.SizeWE;
                    TentativeOp = MoveOp.xZoom;
                }
                else if (e.Location.X > Width - 30)
                {
                    Cursor = Cursors.SizeNS;
                    TentativeOp = MoveOp.yZoom;
                }
                else
                {
                    Cursor = Cursors.Default;
                    TentativeOp = MoveOp.xyPan;
                }
            }
            else if (CurrentMoveOp == MoveOp.xZoom)
            {
                float totalShownV = Width / XPPU;
                float changeV = -(e.X - LastMouse.X) / XPPU;
                float newTotalV = totalShownV + changeV;
                XPPU = Width / newTotalV;
                //xOffsetV += changeV;
                needsRefresh = true;
            }
            else if (CurrentMoveOp == MoveOp.yZoom)
            {
                float totalShownV = Height / YPPU;
                float changeV = -(e.Y - LastMouse.Y) / YPPU;
                float newTotalV = totalShownV + changeV;
                YPPU = Height / newTotalV;
                //xOffsetV += changeV;
                needsRefresh = true;
            }
            else if (CurrentMoveOp == MoveOp.xyPan)
            {
                xOffsetG += (e.X - LastMouse.X);
                yOffsetG += -(e.Y - LastMouse.Y);
                needsRefresh = true;
            }
            LastMouse = e.Location;
        }

        private void TimePlot_MouseUp(object sender, MouseEventArgs e)
        {
            CurrentMoveOp = MoveOp.None;
            TentativeOp = MoveOp.None;
        }

        private void TimePlot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CurrentMoveOp = TentativeOp;
                if (CurrentMoveOp == MoveOp.xyPan)
                    Cursor = Cursors.NoMove2D;
            }
            MouseDownAt = LastMouse;
        }

        private void TimePlot_SizeChanged(object sender, EventArgs e)
        {
            xOffsetG += (Size.Width - LastSize.Width) / 2;
            yOffsetG += (Size.Height - LastSize.Height) / 2;
            needsRefresh = true;
            LastSize = Size;
        }
        public void SetXOffsetG(float v)
        {
            xOffsetG_ = v;
            needsRefresh = true;
        }
        public void SetXScale(float v)
        {
            xppu = v;
            needsRefresh = true;
        }
        public List<PointF> DataPoints { get; set; } = new List<PointF>();
        public PointF MinX { get; set; } = new PointF();
        public PointF MinY { get; set; } = new PointF();

        public PointF MaxX { get; set; } = new PointF();
        public PointF MaxY  { get; set; } = new PointF();
        public float xppu, YPPU, xOffsetG_, yOffsetG;
        protected float xOffsetG { get { return xOffsetG_; } set { if (xOffsetG_ == value) return; xOffsetG_ = value; XOffsetChanged?.Invoke(xOffsetG_); } }
        protected float XPPU { get { return xppu; } set { if (xppu == value) return; xppu = value; XScaleChanged?.Invoke(xppu); } }
        protected float xOffsetV { get { return xOffsetG / XPPU; } set { xOffsetG = value * XPPU; } }
        protected float yOffsetV { get { return yOffsetG / YPPU; } set { yOffsetG = value * YPPU; } }
        public override bool AutoScroll { get; set; } = true;
        public void ShiftStartXOffset(float value)
        {
            xAddOffset = value;
            xOffsetG = Width;
            MaxX = new PointF(-0.000001F, 0);
            MinX = new PointF();
        }
        public void ClearAll()
        {
            if (DataPoints == null)
                DataPoints = new List<PointF>();
            DataPoints.Clear();
            if (UniqueXAxisStamps)
            {
                ShiftStartXOffset(MaxX.X + xAddOffset);
            }
        }
        public bool UniqueXAxisStamps { get; set; } = true;
        public bool ContinuousAutoScale { get; internal set; } = false;

        float xAddOffset = 0;
        bool needsRefresh = true;
        public virtual void Add(PointF p)
        {
            if (UniqueXAxisStamps)
            {
                p = new PointF(p.X - xAddOffset, p.Y);
            }
            var lastMaxX = MaxX;
            if (p.X < MinX.X)
                MinX = p;
            if (p.Y < MinY.Y)
                MinY = p;
            if (p.X > MaxX.X)
                MaxX = p;
            if (p.Y > MaxY.Y)
                MaxY = p;
            if (AutoScroll)
                xOffsetG_ -= (p.X - lastMaxX.X) * XPPU;
            if (UniqueXAxisStamps)
            {
                if (p.X >= lastMaxX.X)
                    DataPoints.Add(p);
                else
                    for (int i = 0; i < DataPoints.Count; i++)
                    {
                        if (DataPoints[i].X < p.X)
                        {
                            DataPoints.Insert(i + 1, p);
                            break;
                        }
                        else if (DataPoints[i].X == p.X)
                        {
                            DataPoints[i] = p;
                            break;
                        }
                    }
            }
            else
            {
                if (DataPoints == null)
                    DataPoints = new List<PointF>();
                DataPoints.Add(p);
            }
            if (ContinuousAutoScale)
                AutoScale();
            needsRefresh = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawAll(g, Width,Height,xOffsetG, yOffsetG, xppu, YPPU, 0);
        }
        public Image GetImage(int sind, string title, string xl, string yl)
        {
            if (UniqueXAxisStamps)
            {
                Font f = new Font("ARIAL", 12);
                float startTimeS = DataPoints[sind].X;
                float rangeXG = (MaxX.X - startTimeS) * xppu;
                float rangeYG = Height;
                int p = 3;
                int xm = (int)measureString(yl, f).Width + p * 2,
                    ym = ((int)measureString(xl, f).Height + p * 2) * 2;
                Bitmap bmp = new Bitmap((int)Math.Round(rangeXG, 0) + xm, (int)Math.Round(rangeYG, 0) + ym);
                Graphics g = Graphics.FromImage(bmp);
                g.TranslateTransform(0, ym / 2);
                
                DrawAll(g, bmp.Width - xm, bmp.Height - ym, 0, yOffsetG, xppu, YPPU, sind);
                g.TranslateTransform(0, bmp.Height - ym);
                g.ScaleTransform(1, -1);
                g.TranslateTransform(0, -ym / 2);
                g.Clip = new Region(new RectangleF(0, 0, bmp.Width, bmp.Height));
                drawCenterString(g, title, f, bmp.Width, new PointF(bmp.Width / 2, p));
                drawCenterString(g, xl, f, bmp.Width, new PointF(bmp.Width / 2, bmp.Height - p - measureString(xl, f).Height));
                drawCenterString(g, yl, f, xm, new PointF(bmp.Width - xm / 2, (bmp.Height - ym) / 2));
                return bmp;
            }
            else
            {
                Font f = new Font("ARIAL", 12);
                int p = 3;
                int xm = (int)measureString(yl, f).Width + p * 2,
                    ym = ((int)measureString(xl, f).Height + p * 2) * 2;
                Bitmap bmp = new Bitmap(Width + xm, Height + ym);
                Graphics g = Graphics.FromImage(bmp);
                g.TranslateTransform(0, ym / 2);
                DrawAll(g, Width, Height, xOffsetG, yOffsetG, xppu, YPPU, sind);
                g.TranslateTransform(0, bmp.Height - ym);
                g.ScaleTransform(1, -1);
                g.TranslateTransform(0, -ym / 2);
                g.Clip = new Region(new RectangleF(0, 0, bmp.Width, bmp.Height));
                drawCenterString(g, title, f, bmp.Width, new PointF(bmp.Width / 2, p));
                drawCenterString(g, xl, f, bmp.Width, new PointF(bmp.Width / 2, bmp.Height - p - measureString(xl, f).Height));
                drawCenterString(g, yl, f, xm, new PointF(bmp.Width - xm / 2, (bmp.Height - ym) / 2));
                return bmp;
            }
        }
        public static SizeF measureString(string s, Font f)
        {
            return Graphics.FromImage(new Bitmap(1, 1)).MeasureString(s, f);
        }
        public static void drawCenterString(Graphics g, string s, Font f, int w, PointF p)
        {
            var sz = g.MeasureString(s, f);
            g.DrawString(s, f, Brushes.Black, p.X - sz.Width / 2, p.Y);
        }
        public void DrawAll(Graphics g, int width, int height, float xog, float yog, float xs, float ys, int sInd)
        {
            try
            {
                g.Clear(BackColor);
                drawXYAxis(g, width, height, xs, ys, xog, yog, true, BackColor);
                g.ScaleTransform(1, -1);
                g.TranslateTransform(0, -height);
                g.Clip = new Region(new RectangleF(0, 12, width - 30, height- 12));

                g.SmoothingMode = SmoothingMode.AntiAlias;
                List<PointF> ps = new List<PointF>();
                if (DataPoints.Count > 2)
                {
                    if (UniqueXAxisStamps)
                    {
                        for (int i = sInd; i < DataPoints.Count; i++)
                        {
                            if ((DataPoints[i].X - DataPoints[sInd].X) * xs + xog >= 0 && (DataPoints[i].X - DataPoints[sInd].X) * xs + xog <= width)
                            {
                                ps.Add(new PointF((DataPoints[i].X - DataPoints[sInd].X) * xs + xog, DataPoints[i].Y * ys+ yog));
                            }
                        }
                    }
                    else
                    {
                        for (int i = sInd; i < DataPoints.Count; i++)
                        {
                            ps.Add(new PointF(DataPoints[i].X * xs + xog, DataPoints[i].Y * ys + yog));
                        }
                    }
                }
                Color c = Color.FromArgb((int)(LineOpacity * 255), LineColor);
                Pen PlotP = new Pen(c, LineThickness);
                if (ps.Count > 1)
                    g.DrawLines(PlotP, ps.ToArray());
                if (LastPointHighlight && ps.Count > 0)
                {
                    var p = ps[ps.Count - 1];
                    g.FillEllipse(Brushes.Black, p.X - LineThickness * 2, p.Y - LineThickness * 2, LineThickness * 4, LineThickness * 4);
                }
            }
            catch
            { }
        }
        public static void drawXYAxis(Graphics g, float w, float h, float xs, float ys, float xog, float yog, bool grid, Color backColor)
        {
            // X Axis
            var axisP = new Pen(Color.DarkGray, 1.5F);
            var majLine = new Pen(Color.FromArgb(155, 155, 155), 1F);
            var minLine = new Pen(Color.FromArgb(200,200,200), 1F);

            float unitX = 1.0F / 100000000.0F;
            float multF = 5;
            // determine scale first
            while (unitX * xs < 35)
            {
                unitX *= multF;
                multF = multF == 2 ? 5 : 2;
            }

            float minX = 0, maxX = 0;
            while (minX * xs < -xog)
                minX += unitX;
            while (minX * xs > -xog)
                minX -= unitX;

            while (maxX * xs > w - xog)
                maxX -= unitX;
            while (maxX * xs < w - xog)
                maxX += unitX;

            Font f = new Font("ARIAL", 8);

            bool isMinLine = false;
            for (float i = minX; i <= maxX; i += unitX / 2)
            {
                PointF drawableMid = VtoG(new PointF(i, 0), xog / xs, xs, yog / ys, ys, h);
                drawableMid = new PointF(drawableMid.X, h);

                if (!isMinLine)
                {
                    PointF drawable1 = new PointF(drawableMid.X, drawableMid.Y - 1.5F);
                    PointF drawable2 = new PointF(drawableMid.X, drawableMid.Y + 1.5F);
                    if (grid) drawable1 = new PointF(drawable1.X, 0);
                    if (grid) drawable2 = new PointF(drawable2.X, h - 15);
                    string s = roundedFrac(i, unitX);
                    var xyo = g.MeasureString(s, f);
                    PointF drawableStrPos = new PointF(drawableMid.X - xyo.Width / 2, drawableMid.Y - xyo.Height - 2);
                    if (drawable1.X < w - 30)
                        g.DrawLine(majLine, drawable1, drawable2);
                    //g.FillRectangle(new SolidBrush(backColor), drawableStrPos.X, drawableStrPos.Y, xyo.Width, xyo.Height);
                    g.DrawString(s, f, Brushes.Gray, drawableStrPos);
                }
                else
                {
                    PointF drawable1 = new PointF(drawableMid.X, drawableMid.Y - 1);
                    PointF drawable2 = new PointF(drawableMid.X, drawableMid.Y + 1);

                    if (grid) drawable1 = new PointF(drawable1.X, 0);
                    if (grid) drawable2 = new PointF(drawable2.X, h - 15);

                    if (drawable1.X < w - 30)
                        g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }

            g.DrawLine(axisP, xog, 0, xog, h - 15);
            // Y Axis
            float unitY = 1.0F / 100000000.0F;
            multF = 5;
            // determine scale first
            while (unitY * ys < 22)
            {
                unitY *= multF;
                multF = multF == 2 ? 5 : 2;
            }

            float minY = 0, maxY = 0;
            while (minY * ys < -yog)
                minY += unitY;
            while (minY * ys > -yog)
                minY -= unitY;

            while (maxY * ys > h - yog)
                maxY -= unitX;
            while (maxY * ys < h - yog)
                maxY += unitY;
            
            isMinLine = false;
            for (float i = minY; i <= maxY; i += unitY / 2)
            {
                PointF drawableMid = VtoG(new PointF(0, i), xog / xs, xs, yog / ys, ys, h);
                drawableMid = new PointF(w, drawableMid.Y);

                if (!isMinLine)
                {
                    PointF drawable1 = new PointF(drawableMid.X - 1.5F, drawableMid.Y);
                    PointF drawable2 = new PointF(drawableMid.X + 1.5F, drawableMid.Y);
                    if (grid) drawable1 = new PointF(0, drawable1.Y);
                    if (grid) drawable2 = new PointF(w - 30, drawable2.Y);
                    string s = roundedFrac(i, unitY, false);
                    var xyo = g.MeasureString(s, f);
                    PointF drawableStrPos = new PointF(drawableMid.X - xyo.Width, drawableMid.Y - xyo.Height / 2 - 2);
                    if (drawable2.Y < h - 15)
                        g.DrawLine(majLine, drawable1, drawable2);
                    //g.FillRectangle(new SolidBrush(backColor), drawableStrPos.X, drawableStrPos.Y, xyo.Width, xyo.Height);
                    if (drawable2.Y < h - 15)
                        g.DrawString(s, f, Brushes.Gray, drawableStrPos);
                }
                else
                {
                    PointF drawable1 = new PointF(drawableMid.X - 1F, drawableMid.Y);
                    PointF drawable2 = new PointF(drawableMid.X + 1F, drawableMid.Y);
                    if (grid) drawable1 = new PointF(0, drawable1.Y);
                    if (grid) drawable2 = new PointF(w - 30, drawable2.Y);
                    if (drawable2.Y < h - 15)
                        g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }
            g.DrawLine(axisP, 0, h - yog, w - 30, h - yog);
            g.DrawRectangle(axisP, 0, 0, w - 30, h - 15);
        }


        static float XVtoXG(float xV, float xov, float xs)
        {
            return (xV + xov) * xs;
        }
        static float YVtoYG(float yV, float yov, float ys, float h)
        {
            return h - (yV + yov) * ys;
        }

        static PointF VtoG(PointF pV, float xov, float xs, float yov, float ys, float h)
        {
            return new PointF(XVtoXG(pV.X, xov, xs), YVtoYG(pV.Y, yov, ys, h));
        }

        float XGtoXV(float xG)
        {
            return (xG - xOffsetG) / XPPU;
        }
        float YGtoYV(float yG)
        {
            return ((Height - yG) - yOffsetG) / YPPU;
        }
        PointF GtoV(PointF pG)
        {
            return new PointF(XGtoXV(pG.X), YGtoYV(pG.Y));
        }
        static string roundedFrac(float frac, float leastCount = 0, bool addPrefix = true)
        {
            float fracBkp = frac;
            if (frac <= 1e-10 && frac >= -1e-10)
                return "0";
            double thisFrac = bringAbove1(frac);
            double nextFrac = bringAbove1(frac + leastCount);
            int sigFigure = 1;
            while (thisFrac == nextFrac)
            {
                sigFigure++;
                thisFrac = bringAbove1(frac, sigFigure);
                nextFrac = bringAbove1(frac + leastCount, sigFigure);
            }
            if (addPrefix)
                return NumberUtils.AddPrefix(thisFrac, "");
            else
                return thisFrac.ToString();
        }
        static double bringAbove1(float frac, int sigFigures = 1)
        {
            if (frac == 0)
                return 0;
            bool isNeg = frac < 0;
            if (isNeg) frac *= -1;
            int mp = 0;
            while (frac < 1)
            {
                frac *= 10;
                mp++;
            }
            if (Math.Floor(frac) == 9)
            {
                mp--;
                frac /= 10;
            }
            float mult = 1;
            while (mp-- > 0)
                mult *= 10;

            return (double)(Math.Round(frac * (isNeg ? -1 : 1) / mult, sigFigures) );
        }
    }
    enum MoveOp
    {
        None,
        xZoom,
        yZoom,
        xPan,
        yPan,
        xyPan

    }
}
