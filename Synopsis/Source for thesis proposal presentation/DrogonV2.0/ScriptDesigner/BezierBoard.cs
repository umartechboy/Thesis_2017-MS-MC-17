using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Physics;

namespace SplineDesigner
{
    public class BezierBoard:Panel
    {
        public BezierSpline Spline { get; set; } = new BezierSpline();
        public BezierBoard()
        {
            DoubleBuffered = true;
            MouseDown += BezierBoard_MouseDown;
            MouseUp += BezierBoard_MouseUp;
            MouseMove += BezierBoard_MouseMove;
            BackgroundImageLayout = ImageLayout.Center;
        }

        public ops Op = ops.AddNewPoint;
        Point DownAt = new Point();

        Point lastCursor = new Point();
       

        private void BezierBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (Op == ops.AddNewPoint)
            {
                Op = ops.AddingP;
            }
            else if (Op == ops.RemovePoint)
            {
                if (AnchorUnderEdit != null)
                {
                    Spline.Anchors.Remove(AnchorUnderEdit);
                    Invalidate();
                }
            }        
            else if (Op == ops.ChangePoint)
            {
                if (AnchorUnderEdit != null && PartUnderEdit != AnchorPart.None)
                    Op = ops.ChangingPoint;
            }
            else if (Op == ops.ChangeRotation)
            {
                if (AnchorUnderEdit != null && PartUnderEdit != AnchorPart.None)
                    Op = ops.ChangingRotation;
            }

            DownAt = lastCursor;
        }
        SplineAnchor AnchorUnderEdit = null;
        AnchorPart PartUnderEdit = AnchorPart.None;
        private void BezierBoard_MouseMove(object sender, MouseEventArgs e)
        {

            if (Op == ops.RemovePoint)
            {
                Cursor = Cursors.Default;
                foreach (var anchor in Spline.Anchors)
                {
                    if (anchor.P1.IsCloseBy(lastCursor))
                    {
                        AnchorUnderEdit = anchor;
                        Cursor = Cursors.Cross;
                        break;
                    }
                }
            }
            else if (Op == ops.ChangePoint)
            {
                Cursor = Cursors.Default;
                foreach (var anchor in Spline.Anchors)
                {
                    if (anchor.a1.IsCloseBy(lastCursor))
                    {
                        AnchorUnderEdit = anchor;
                        PartUnderEdit = AnchorPart.a1;
                        Cursor = Cursors.UpArrow;
                        break;
                    }
                    else if (anchor.a2.IsCloseBy(lastCursor))
                    {
                        AnchorUnderEdit = anchor;
                        PartUnderEdit = AnchorPart.a2;
                        Cursor = Cursors.UpArrow;
                        break;
                    }
                    else if (anchor.P1.IsCloseBy(lastCursor))
                    {
                        AnchorUnderEdit = anchor;
                        PartUnderEdit = AnchorPart.P1;
                        Cursor = Cursors.Cross;
                        break;
                    }
                }
            }
            else if (Op == ops.ChangingPoint)// move anchor part
            {
                if (PartUnderEdit == AnchorPart.P1)
                {
                    double changeX = e.Location.X - lastCursor.X;
                    double changeY = e.Location.Y - lastCursor.Y;
                    AnchorUnderEdit.P1.X += changeX;
                    AnchorUnderEdit.P1.Y += changeY;
                    AnchorUnderEdit.a1.X += changeX;
                    AnchorUnderEdit.a1.Y += changeY;
                    AnchorUnderEdit.a2.X += changeX;
                    AnchorUnderEdit.a2.Y += changeY;
                    AnchorUnderEdit.r1.X += changeX;
                    AnchorUnderEdit.r1.Y += changeY;
                    AnchorUnderEdit.r2.X += changeX;
                    AnchorUnderEdit.r2.Y += changeY;
                    Invalidate();
                }
                else if (PartUnderEdit == AnchorPart.a1)
                {
                    var P1 = AnchorUnderEdit.P1;
                    var a1 = AnchorUnderEdit.a1;
                    var a2 = AnchorUnderEdit.a2;                                               
                    double d2 = Math.Sqrt(Math.Pow(P1.X - a2.X, 2) + Math.Pow(P1.Y - a2.Y, 2));
                    AnchorUnderEdit.a1.X = e.Location.X;
                    AnchorUnderEdit.a1.Y = e.Location.Y;
                    double th = Math.Atan2(AnchorUnderEdit.a1.Y - P1.Y, AnchorUnderEdit.a1.X - P1.X) + Math.PI;

                    AnchorUnderEdit.a2.X = P1.X + d2 * Math.Cos(th);
                    AnchorUnderEdit.a2.Y = P1.Y + d2 * Math.Sin(th);
                    Invalidate();
                }
                else if (PartUnderEdit == AnchorPart.a2)
                {
                    var P1 = AnchorUnderEdit.P1;
                    var a1 = AnchorUnderEdit.a1;
                    var a2 = AnchorUnderEdit.a2;
                    double d1 = Math.Sqrt(Math.Pow(P1.X - a1.X, 2) + Math.Pow(P1.Y - a1.Y, 2));
                    AnchorUnderEdit.a2.X = e.Location.X;
                    AnchorUnderEdit.a2.Y = e.Location.Y;
                    double th = Math.Atan2(AnchorUnderEdit.a2.Y - P1.Y, AnchorUnderEdit.a2.X - P1.X) + Math.PI;

                    AnchorUnderEdit.a1.X = P1.X + d1 * Math.Cos(th);
                    AnchorUnderEdit.a1.Y = P1.Y + d1 * Math.Sin(th);
                    Invalidate();
                }
            }
            else if (Op == ops.ChangeRotation)
            {
                Cursor = Cursors.Default;
                foreach (var anchor in Spline.Anchors)
                {
                    if (anchor.r1.IsCloseBy(lastCursor))
                    {
                        AnchorUnderEdit = anchor;
                        PartUnderEdit = AnchorPart.r1;
                        Cursor = Cursors.UpArrow;
                        break;
                    }
                    else if (anchor.r2.IsCloseBy(lastCursor))
                    {
                        AnchorUnderEdit = anchor;
                        PartUnderEdit = AnchorPart.r2;
                        Cursor = Cursors.UpArrow;
                        break;
                    }                
                }
            }
            else if (Op == ops.ChangingRotation)// move anchor part
            {
                if (PartUnderEdit == AnchorPart.r1)
                {
                    var P1 = AnchorUnderEdit.P1;
                    var r1 = AnchorUnderEdit.r1;
                    var r2 = AnchorUnderEdit.r2;
                    double d2 = Math.Sqrt(Math.Pow(P1.X - r2.X, 2) + Math.Pow(P1.Y - r2.Y, 2));
                    double th = Math.Atan2(e.Location.Y - P1.Y, e.Location.X - P1.X) + Math.PI;

                    AnchorUnderEdit.r1.X = P1.X + d2 * Math.Cos(th + Math.PI);
                    AnchorUnderEdit.r1.Y = P1.Y + d2 * Math.Sin(th + Math.PI);

                    AnchorUnderEdit.r2.X = P1.X + d2 * Math.Cos(th);
                    AnchorUnderEdit.r2.Y = P1.Y + d2 * Math.Sin(th);
                    Invalidate();
                }
                else if (PartUnderEdit == AnchorPart.r2)
                {
                    var P1 = AnchorUnderEdit.P1;
                    var r1 = AnchorUnderEdit.r1;
                    var r2 = AnchorUnderEdit.r2;
                    double d1 = Math.Sqrt(Math.Pow(P1.X - r1.X, 2) + Math.Pow(P1.Y - r1.Y, 2));
                    AnchorUnderEdit.r2.X = e.Location.X;
                    AnchorUnderEdit.r2.Y = e.Location.Y;
                    double th = Math.Atan2(AnchorUnderEdit.r2.Y - P1.Y, AnchorUnderEdit.r2.X - P1.X) + Math.PI;

                    AnchorUnderEdit.r1.X = P1.X + d1 * Math.Cos(th);
                    AnchorUnderEdit.r1.Y = P1.Y + d1 * Math.Sin(th);
                    Invalidate();
                }
            }

            lastCursor = e.Location;
        }

        private void BezierBoard_MouseUp(object sender, MouseEventArgs e)
        {
            if (Op == ops.AddingP)
            {
                Spline.Add(DownAt, lastCursor);
            }
            else if (Op == ops.ChangingPoint)
            {
                PartUnderEdit = AnchorPart.None;
                Op = ops.ChangePoint;
                Cursor = Cursors.Default;
            }
            else if (Op == ops.ChangingRotation)
            {
                PartUnderEdit = AnchorPart.None;
                Op = ops.ChangeRotation;
                Cursor = Cursors.Default;
            }
            Invalidate();
        }
        bool _io = false;
        public bool InkOnly { get => _io; set  { _io = value; Invalidate(); } } 
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            if (InkOnly)
                g.Clear(BackColor);
            else
                base.OnPaint(e);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Spline.Draw(g, new Pen(Color.Blue, 2), !InkOnly);
        }
    }
    public enum AnchorPart
    {
        P1, a1, a2, r1, r2,
        None
    }
    public enum ops {
        AddNewPoint,
        AddingP,
        AddIntermediatePoint,
        AddingIntermediatePoint,
        None,
        RemovePoint,
        ChangePoint,
        ChangingPoint,
        ChangeRotation,
        ChangingRotation
    }
    public class PointD
    {
        public PointD YInvert()
        {
            return new PointD(X, -Y);
        }
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public bool IsCloseBy(PointF P, int tol = 3)
        {
            return Math.Sqrt(Math.Pow(P.X - X, 2) + Math.Pow(P.Y - Y, 2)) < 3;
        }
        public PointD(double x = 0, double y = 0)
        {
            X = x; Y = y;
        }
        public static PointD Intermediate(PointD a, PointD b, double frac = 0.5)
        {
            return new PointD(a.X * frac + b.X * (1 - frac), a.Y * frac + b.Y * (1 - frac));
        }
        public static implicit operator PointF(PointD P)
        {
            return new PointF((float)P.X, (float)P.Y);
        }
        public override string ToString()
        {
            return ((PointF)this).ToString();
        }
    }
    public class BezierCurveWithRotation
    {
        public PointD P1, P2, a1, a2, r1, r2;
        public BezierCurveWithRotation (PointD P1, PointD P2)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.a1 = P1;
            this.a2 = P2;
        }
        public BezierCurveWithRotation(PointD P1, PointD P2, PointD a1, PointD a2, PointD r1, PointD r2)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.a1 = a1;
            this.a2 = a2;
            this.r1 = r1;
            this.r2 = r2;
        }
        public void Draw(Graphics g, Pen p, int divisions, float thickness, bool DrawForEditing)
        {
            // draw the spline
            PointF[] ps = new PointF[divisions + 1];
            PointD pp = null;
            double A1 = Math.Atan2(P1.Y - r1.Y, P1.X - r1.X);
            double A2 = Math.Atan2(P2.Y - r2.Y, P2.X - r2.X) + Math.PI + 0.001;
            if (A2 > 2 * Math.PI)
                A2 -= 2 * Math.PI;
            if (A1 < 0)
                A1 += 2 * Math.PI;
            if (A2 < 0)
                A2 += 2 * Math.PI;
            PointF[] psInk = new PointF[divisions * 4];
            for (int i = 0; i <= divisions; i++)
            {
                double f = i / (double)divisions;
                PointD pl1_1 = PointD.Intermediate(P1, a1, f);
                PointD pl1_2 = PointD.Intermediate(a1, a2, f);
                PointD pl1_3 = PointD.Intermediate(a2, P2, f);

                PointD pl2_1 = PointD.Intermediate(pl1_1, pl1_2, f);
                PointD pl2_2 = PointD.Intermediate(pl1_2, pl1_3, f);

                PointD P = PointD.Intermediate(pl2_1, pl2_2, f);

                if (i > 0)
                {
                    double fp = (i - 1) / (double)divisions;
                    PointD pp1 = new PointD(
                        pp.X + thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
                        pp.Y + thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));

                    PointD pp2 = new PointD(
                        pp.X - thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
                        pp.Y - thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));
                    PointD pc1 = new PointD(
                        P.X + thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
                        P.Y + thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));

                    PointD pc2 = new PointD(
                        P.X - thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
                        P.Y - thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));
                    psInk[(i - 1) * 2] = pp1;
                    psInk[(i - 1) * 2 + 1] = pc1;
                    psInk[psInk.Length - ((i - 1) * 2) - 1] = pp2;
                    psInk[psInk.Length - ((i - 1) * 2 + 1) - 1] = pc2;
                }
                ps[i] = P;
                pp = P;
            }

            g.FillPolygon(Brushes.Gray, psInk);
            g.DrawLines(p, ps);

            if (DrawForEditing)
            {
                // draw the handles
                Pen p2 = new Pen(p.Brush, 1);
                g.DrawLine(p2, P1, a1);
                g.DrawLine(p2, P2, a2);
                g.DrawRectangle(p2, (float)a1.X - 2, (float)a1.Y - 2, 4.0F, 4.0F);
                g.DrawRectangle(p2, (float)a2.X - 2, (float)a2.Y - 2, 4.0F, 4.0F);

                g.DrawLine(Pens.Green, P1, r1);
                g.DrawLine(Pens.Green, P2, r2);
                g.DrawRectangle(Pens.Green, (float)r1.X - 2, (float)r1.Y - 2, 4.0F, 4.0F);
                g.DrawRectangle(Pens.Green, (float)r2.X - 2, (float)r2.Y - 2, 4.0F, 4.0F);

                g.DrawRectangle(p2, (float)P1.X - 2, (float)P1.Y - 2, 4.0F, 4.0F);
                g.DrawRectangle(p2, (float)P2.X - 2, (float)P2.Y - 2, 4.0F, 4.0F);
            }
        }

        public RectangleF GetBounds(double thickness)
        {                                        
            int divisions = 15;                     
            PointD pp = null;
            double A1 = Math.Atan2(P1.Y - r1.Y, P1.X - r1.X);
            double A2 = Math.Atan2(P2.Y - r2.Y, P2.X - r2.X) + Math.PI + 0.001;
            if (A2 > 2 * Math.PI)
                A2 -= 2 * Math.PI;
            if (A1 < 0)
                A1 += 2 * Math.PI;
            if (A2 < 0)
                A2 += 2 * Math.PI;
            PointF[] psInk = new PointF[divisions * 4];
            for (int i = 0; i <= divisions; i++)
            {
                double f = i / (double)divisions;
                PointD pl1_1 = PointD.Intermediate(P1, a1, f);
                PointD pl1_2 = PointD.Intermediate(a1, a2, f);
                PointD pl1_3 = PointD.Intermediate(a2, P2, f);

                PointD pl2_1 = PointD.Intermediate(pl1_1, pl1_2, f);
                PointD pl2_2 = PointD.Intermediate(pl1_2, pl1_3, f);

                PointD P = PointD.Intermediate(pl2_1, pl2_2, f);

                if (i > 0)
                {
                    double fp = (i - 1) / (double)divisions;
                    PointD pp1 = new PointD(
                        pp.X + thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
                        pp.Y + thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));

                    PointD pp2 = new PointD(
                        pp.X - thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
                        pp.Y - thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));
                    PointD pc1 = new PointD(
                        P.X + thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
                        P.Y + thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));

                    PointD pc2 = new PointD(
                        P.X - thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
                        P.Y - thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));
                    psInk[(i - 1) * 2] = pp1;
                    psInk[(i - 1) * 2 + 1] = pc1;
                    psInk[psInk.Length - ((i - 1) * 2) - 1] = pp2;
                    psInk[psInk.Length - ((i - 1) * 2 + 1) - 1] = pc2;
                }             
                pp = P;
            }

            float minX = psInk.Min(p => p.X);
            float maxX = psInk.Max(p => p.X);
            float minY = psInk.Min(p => p.Y);
            float maxY = psInk.Max(p => p.Y);
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }
        public bool Contains(Point p, double thickness)
        {
            return GetBounds(thickness).Contains(p);
        }
        static bool IsInPolygon(PointF[] poly, Point point)
        {
            var coef = poly.Skip(1).Select((p, i) =>
                                            (point.Y - poly[i].Y) * (p.X - poly[i].X)
                                          - (point.X - poly[i].X) * (p.Y - poly[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

        internal void MakeTargets(double inc, List<EulerAngleOrientation> targets)
        {
            // draw the spline                      
            PointD pp = null;
            double A1 = Math.Atan2(P1.Y - r1.Y, P1.X - r1.X);
            double A2 = Math.Atan2(P2.Y - r2.Y, P2.X - r2.X) + Math.PI + 0.001;
            if (A2 > 2 * Math.PI)
                A2 -= 2 * Math.PI;
            if (A1 < 0)
                A1 += 2 * Math.PI;
            if (A2 < 0)
                A2 += 2 * Math.PI;
                              
            for (double f = 1; f >= 0; f -= inc)
            {           
                PointD pl1_1 = PointD.Intermediate(P1, a1, f);
                PointD pl1_2 = PointD.Intermediate(a1, a2, f);
                PointD pl1_3 = PointD.Intermediate(a2, P2, f);

                PointD pl2_1 = PointD.Intermediate(pl1_1, pl1_2, f);
                PointD pl2_2 = PointD.Intermediate(pl1_2, pl1_3, f);

                PointD P = PointD.Intermediate(pl2_1, pl2_2, f);

                var twist = A1 * f + A2 * (1 - f);

                if (f == 1) // first                                                 
                {
                    if (targets.Count > 0)
                    {
                        if (new EulerAngleOrientation(twist, 0, 0, P.X, P.Y, 0) != targets[targets.Count - 1])
                            targets.Add(new EulerAngleOrientation(0,0, twist, P.X, P.Y, 0));
                    }
                    else
                        targets.Add(new EulerAngleOrientation(0, 0, twist, P.X, P.Y, 0));
                }
                else
                {
                    double d = Math.Sqrt(Math.Pow(targets[targets.Count - 1].X - P.X, 2) + Math.Pow(targets[targets.Count - 1].Y - P.Y, 2));
                    if (d >= inc)
                        targets.Add(new EulerAngleOrientation(0, 0, twist, P.X, P.Y, 0));
                }     
                // add
                pp = P;                           
            }                                  
        }
    }
    public class SplineAnchor
    {
        public PointD P1, a1, a2, r1, r2;
        public SplineAnchor(PointD P)
        {
            P1 = P;
            a1 = new PointD(P.X, P.Y);
            a2 = new PointD(P.X, P.Y);
            r1 = new PointD(P.X +20, P.Y);
            r2 = new PointD(P.X - 20, P.Y);
        }
        public SplineAnchor(PointD P, PointD a1, PointD a2)
        {
            P1 = new PointD(P.X, P.Y);
            this.a1 = new PointD(a1.X, a1.Y);
            this.a2 = new PointD(a2.X, a2.Y);
            r1 = new PointD(P.X + 20, P.Y);
            r2 = new PointD(P.X - 20, P.Y);
        }
        public SplineAnchor YInvertedClone()
        {
            SplineAnchor a = Clone();
            a.P1.Y *= -1;
            a.a1.Y *= -1;
            a.a2.Y *= -1;
            a.r1.Y *= -1;
            a.r2.Y *= -1;

            return a;
        }
        public SplineAnchor Clone()
        {
            SplineAnchor a = new SplineAnchor(P1, a1, a2);
            a.r1 = new PointD(r1.X, r1.Y);
            a.r2 = new PointD(r2.X, r2.Y);
            return a;
        }
    }
    [Serializable]
    public class BezierSpline
    {

        public List<SplineAnchor> Anchors = new List<SplineAnchor>();

        public float FlatTipThickness { get; internal set; }

        public void Add(PointF point)
        {
            PointD P = new PointD(point.X, point.Y);
            Anchors.Add(new SplineAnchor(P));
        }
        public void Add(PointF point, PointF a12)
        {
            PointD P = new PointD(point.X, point.Y);
            PointD a1 = new PointD(a12.X, a12.Y);
            PointD a2 = PointD.Intermediate(a1, P, -1);
            Anchors.Add(new SplineAnchor(P, a1, a2));
        }
        public static List<BezierSpline> FromFile(string fileName)
        {
            var lines = System.IO.File.ReadAllLines(fileName);
            List<BezierSpline> Splines = new List<BezierSpline>();
            BezierSpline Spline = new BezierSpline();
            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { ';', ',' });
                if (parts.Length == 1)
                {
                    if (line == "%")
                    {
                        if (Spline.Anchors.Count > 0)
                            Splines.Add(Spline);
                        Spline = new BezierSpline();
                        continue;
                    }
                    Spline.FlatTipThickness = Convert.ToSingle(line);
                    continue;
                }
                SplineAnchor a = new SplineAnchor(
                    new PointD(Convert.ToDouble(parts[0]), Convert.ToDouble(parts[1])),
                    new PointD(Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3])),
                    new PointD(Convert.ToDouble(parts[4]), Convert.ToDouble(parts[5]))
                    );
                a.r1 = new PointD(Convert.ToDouble(parts[6]), Convert.ToDouble(parts[7]));
                a.r2 = new PointD(Convert.ToDouble(parts[8]), Convert.ToDouble(parts[9]));
                Spline.Anchors.Add(a);
            }

            if (Spline.Anchors.Count > 0)
                Splines.Add(Spline);
            return Splines;
        }

        public void Draw(Graphics g, Pen p, bool DrawForEditing)
        {
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                new BezierCurveWithRotation(
                    Anchors[i].P1,
                    Anchors[i + 1].P1,
                    Anchors[i].a2,
                    Anchors[i + 1].a1,
                    Anchors[i].r2,
                    Anchors[i + 1].r1
                    ).Draw(g, p, 20, FlatTipThickness, DrawForEditing);
            }
            if (!DrawForEditing)
            {
                var b = GetBounds();
                g.DrawRectangle(Pens.Black, (int)b.X, (int)b.Y, (int)b.Width, (int)b.Height);
            }

        }
        public RectangleF GetBounds()
        {
            if (Anchors.Count < 2)
                return new RectangleF();                     

            float minX = float.PositiveInfinity, minY = float.PositiveInfinity;
            float maxX = float.NegativeInfinity, maxY = float.NegativeInfinity;
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                var b = new BezierCurveWithRotation(
                    Anchors[i].P1,
                    Anchors[i + 1].P1,
                    Anchors[i].a2,
                    Anchors[i + 1].a1,
                    Anchors[i].r2,
                    Anchors[i + 1].r1
                    ).GetBounds(FlatTipThickness);
                if (b.X < minX)
                    minX = b.X;
                if (b.Y < minY)
                    minY = b.Y;
                if (b.X + b.Width > maxX)
                    maxX = b.X + b.Width;
                if (b.Y + b.Height > maxY)
                    maxY = b.Y + b.Height;
            }
            if (Anchors.Count < 2)
                return new RectangleF();
            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }
        public bool Contains(Point p)
        {
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                if (
                new BezierCurveWithRotation(
                    Anchors[i].P1,
                    Anchors[i + 1].P1,
                    Anchors[i].a2,
                    Anchors[i + 1].a1,
                    Anchors[i].r2,
                    Anchors[i + 1].r1
                    ).Contains(p, FlatTipThickness))
                    return true;
            }
            return false;
        }

        public List<EulerAngleOrientation> MakeTargets(double inc, double scale, double xOffset, double yOffset)
        {
            List<EulerAngleOrientation> targets = new List<EulerAngleOrientation>();
            SplineAnchor[] anchors = new SplineAnchor[Anchors.Count];

            for (int i = 0; i < Anchors.Count; i++)
            {
                anchors[i] = Anchors[i].Clone();
            }
                                                                     
            foreach (var a in anchors)
            {
                a.P1.X = (a.P1.X - xOffset) * scale;
                a.P1.Y = (a.P1.Y - yOffset) * scale;
                a.a1.X = (a.a1.X - xOffset) * scale;
                a.a1.Y = (a.a1.Y - yOffset) * scale;
                a.a2.X = (a.a2.X - xOffset) * scale;
                a.a2.Y = (a.a2.Y - yOffset) * scale;
                a.r1.X = (a.r1.X - xOffset) * scale;
                a.r1.Y = (a.r1.Y - yOffset) * scale;
                a.r2.X = (a.r2.X - xOffset) * scale;
                a.r2.Y = (a.r2.Y - yOffset) * scale;
            }
            for (int i = 0; i < anchors.Length - 1; i++)
            {      
                new BezierCurveWithRotation(
                    anchors[i].P1,
                    anchors[i + 1].P1,
                    anchors[i].a2,
                    anchors[i + 1].a1,
                    anchors[i].r2,
                    anchors[i + 1].r1
                    ).MakeTargets(inc, targets);
            }
            return targets;
        }

        internal string ToSaveString()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FlatTipThickness.ToString());
            foreach (var anchor in Anchors)
            {
                sb.Append(anchor.P1.X.ToString() + "," + anchor.P1.Y.ToString() + ";");
                sb.Append(anchor.a1.X.ToString() + "," + anchor.a1.Y.ToString() + ";");
                sb.Append(anchor.a2.X.ToString() + "," + anchor.a2.Y.ToString() + ";");
                sb.Append(anchor.r1.X.ToString() + "," + anchor.r1.Y.ToString() + ";");
                sb.AppendLine(anchor.r2.X.ToString() + "," + anchor.r2.Y.ToString());
            }
            return sb.ToString();
        }
    }
}
