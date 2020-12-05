using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotatingBezierSplineEditor
{
    public enum MouseState
    {
        None,
        Hover,
        Held,
        Selected,
    } 
    public enum AnchorParts:byte
    {
        Center = 1,
        RotationHandle1 = 2,
        RotationHandle2 = 4,
        CurvatureHandle1 = 8,
        CurvatureHandle2 = 16,
    }
    public class PointD
    {
        internal void ProcessMouse(Point location, float v1, float v2)
        {
            throw new NotImplementedException();
        }

        public MouseState MouseState { get; set; } = MouseState.None;
        internal bool ContainsMouse(Point location)
        {
            return false;
            //throw new NotImplementedException();
        }
        internal void NitifyMouseDown(Point location)
        {
            //throw new NotImplementedException();
        }

        public static implicit operator PointF(PointD p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }
        public static implicit operator Point(PointD p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
        public static implicit operator PointD(Point p)
        {
            return new PointD(p.X, p.Y);
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
        public override string ToString()
        {
            return ((PointF)this).ToString();
        }

        internal void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.Black, (float)X - 3, (float)Y - 3, 6, 6);
        }
    }

    public class RotatingBezierSplineAnchor
    {
        public PointD P { get; set; }
        public PointD C1 { get; set; }
        public PointD C2 { get;set; }
        public PointD R1 { get;set; }

        internal bool ContainsMouse(Point location)
        {
            foreach (var p in Points)
                if (p.ContainsMouse(location))
                    return true;
            return false;
        }

        public PointD R2 { get; private set; }

        public PointD[] Points;
        int completeRotation = 0;
        public double R { get{ return completeRotation * 2 * Math.PI + Math.Atan2(R1.Y - P.Y, R1.X - P.X); } }

        double lastSetR = 0;
        public RotatingBezierSplineAnchor(float x, float y) : this(new PointD(x, y)) { }
        public RotatingBezierSplineAnchor(PointD P) : this(P, new PointD(P.X, P.Y), new PointD(P.X, P.Y)) { }
        public RotatingBezierSplineAnchor(PointD P, PointD a1, PointD a2)
        {
            this.P = new PointD(P.X, P.Y);
            this.C1 = new PointD(a1.X, a1.Y);
            this.C2 = new PointD(a2.X, a2.Y);
            R1 = new PointD(P.X + 20, P.Y);
            R2 = new PointD(P.X - 20, P.Y);
            Points = new PointD[] { P, C1, C2, R1, R2 };
        }


        public void Draw(Graphics g, AnchorParts partsToDraw, AnchorParts partsHighlighted)
        {
            var colors = new Dictionary<AnchorParts, Color>();
            var cs = new KeyValuePair<AnchorParts, Color>[] {
                new KeyValuePair<AnchorParts, Color>( AnchorParts.Center, Color.Black),
                new KeyValuePair<AnchorParts, Color>( AnchorParts.CurvatureHandle1, Color.DarkBlue),
                new KeyValuePair<AnchorParts, Color>( AnchorParts.CurvatureHandle2, Color.DarkBlue),
                new KeyValuePair<AnchorParts, Color>( AnchorParts.Center, Color.DarkRed),
                new KeyValuePair<AnchorParts, Color>( AnchorParts.Center, Color.DarkRed),
            };
            foreach (var c in cs)
                colors.Add(c.Key, c.Value);
            DrawPoint(g, P, AnchorParts.Center, colors, partsToDraw, partsHighlighted);
            DrawPoint(g, C1, AnchorParts.CurvatureHandle1, colors, partsToDraw, partsHighlighted);
            DrawPoint(g, C2, AnchorParts.CurvatureHandle2, colors, partsToDraw, partsHighlighted);
            DrawPoint(g, R1, AnchorParts.RotationHandle1, colors, partsToDraw, partsHighlighted);
            DrawPoint(g, R2, AnchorParts.RotationHandle2, colors, partsToDraw, partsHighlighted);
        }
        static void DrawPoint(Graphics g, PointF P, AnchorParts part, Dictionary<AnchorParts, Color> cs, AnchorParts partsToDraw, AnchorParts partsHighlighted)
        {
            if (partsToDraw.HasFlag(part))
            {
                float size = 4;
                Pen p = new Pen(new SolidBrush(Color.FromArgb(120, cs[part])), 1);
                if (partsHighlighted.HasFlag(part))
                {
                    size = 6;
                    p = new Pen(new SolidBrush(cs[part]), 2);
                }
                g.DrawEllipse(p, (float)P.X - size / 2, (float)P.Y - size / 2, size, size);
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
        public RotatingBezierSplineAnchor Clone()
        {
            RotatingBezierSplineAnchor a = new RotatingBezierSplineAnchor(P, C1, C2);
            a.R1 = new PointD(R1.X, R1.Y);
            a.R2 = new PointD(R2.X, R2.Y);
            return a;
        }
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
        public void Draw(Graphics g, float thickness, MouseState mouseState, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            // draw the spline
            int divisions = 20;
            PointF[] ps = new PointF[divisions + 1];
            PointD pPrevious = null;
            double r1 = this.A1.R;
            double r2 = this.A2.R;
            PointF[] psInk = new PointF[divisions * 4];
            for (int i = 0; i <= divisions; i++)
            {
                double f = i / (double)divisions;
                PointD pl1_1 = PointD.Intermediate(A1.P, A1.C2, f);
                PointD pl1_2 = PointD.Intermediate(A1.C2, A2.C1, f);
                PointD pl1_3 = PointD.Intermediate(A2.C1, A2.P, f);

                PointD pl2_1 = PointD.Intermediate(pl1_1, pl1_2, f);
                PointD pl2_2 = PointD.Intermediate(pl1_2, pl1_3, f);

                PointD P = PointD.Intermediate(pl2_1, pl2_2, f);

                if (i > 0 && thickness > 0)
                {
                    double fp = (i - 1) / (double)divisions;
                    PointD pp1 = new PointD(
                        pPrevious.X + thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y + thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)));

                    PointD pp2 = new PointD(
                        pPrevious.X - thickness / 2 * Math.Cos(r1 * fp + r2 * (1 - fp)),
                        pPrevious.Y - thickness / 2 * Math.Sin(r1 * fp + r2 * (1 - fp)));
                    PointD pc1 = new PointD(
                        P.X + thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y + thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)));

                    PointD pc2 = new PointD(
                        P.X - thickness / 2 * Math.Cos(r1 * f + r2 * (1 - f)),
                        P.Y - thickness / 2 * Math.Sin(r1 * f + r2 * (1 - f)));
                    psInk[(i - 1) * 2] = pp1;
                    psInk[(i - 1) * 2 + 1] = pc1;
                    psInk[psInk.Length - ((i - 1) * 2) - 1] = pp2;
                    psInk[psInk.Length - ((i - 1) * 2 + 1) - 1] = pc2;
                }
                ps[i] = P;
                pPrevious = P;
            }


            if (mouseState == MouseState.None)
            {
                if (thickness > 0 && inkDrawMode.HasFlag(InkDrawMode.Ink))
                    g.FillPolygon(Brushes.DarkGray, psInk);
                if (inkDrawMode.HasFlag(InkDrawMode.Spline))
                    g.DrawLines(inkDrawMode.HasFlag(InkDrawMode.Ink) ? Pens.White : Pens.Gray, ps);
            }
            else if (mouseState == MouseState.Hover)
            {
                g.DrawLines(Pens.Black, ps);
                g.DrawLines(new Pen(Color.FromArgb(100, 0, 0, 0), 3), ps);
            }
            else if (mouseState == MouseState.Held)
            {
                g.DrawLines(new Pen(Color.Black, 3), ps);
                g.DrawLines(Pens.White, ps);
            }

            if (inkDrawMode.HasFlag(InkDrawMode.Spline)) // draw the handles as well
            {
                if (anchorDrawMode.HasFlag(AnchorDrawMode.Centers))
                    A1.P.Draw(g);
                if (anchorDrawMode.HasFlag(AnchorDrawMode.CurvatureHandles))
                { A1.C1.Draw(g); A1.C2.Draw(g); }
                if (anchorDrawMode.HasFlag(AnchorDrawMode.RotaionHandles))
                { A1.R1.Draw(g); A1.R2.Draw(g); }
                // draw the handles

                //Pen p2 = new Pen(p.Brush, 1);
                //g.DrawLine(p2, P1, a1);
                //g.DrawLine(p2, P2, a2);
                //g.DrawRectangle(p2, (float)a1.X - 2, (float)a1.Y - 2, 4.0F, 4.0F);
                //g.DrawRectangle(p2, (float)a2.X - 2, (float)a2.Y - 2, 4.0F, 4.0F);

                //g.DrawLine(Pens.Green, P1, r1);
                //g.DrawLine(Pens.Green, P2, r2);
                //g.DrawRectangle(Pens.Green, (float)r1.X - 2, (float)r1.Y - 2, 4.0F, 4.0F);
                //g.DrawRectangle(Pens.Green, (float)r2.X - 2, (float)r2.Y - 2, 4.0F, 4.0F);

                //g.DrawRectangle(p2, (float)P1.X - 2, (float)P1.Y - 2, 4.0F, 4.0F);
                //g.DrawRectangle(p2, (float)P2.X - 2, (float)P2.Y - 2, 4.0F, 4.0F);
            }
        }

        //public RectangleF GetBounds(double thickness)
        //{
        //    int divisions = 15;
        //    PointD pp = null;
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
        //        PointD pl1_1 = PointD.Intermediate(P1, a1, f);
        //        PointD pl1_2 = PointD.Intermediate(a1, a2, f);
        //        PointD pl1_3 = PointD.Intermediate(a2, P2, f);

        //        PointD pl2_1 = PointD.Intermediate(pl1_1, pl1_2, f);
        //        PointD pl2_2 = PointD.Intermediate(pl1_2, pl1_3, f);

        //        PointD P = PointD.Intermediate(pl2_1, pl2_2, f);

        //        if (i > 0)
        //        {
        //            double fp = (i - 1) / (double)divisions;
        //            PointD pp1 = new PointD(
        //                pp.X + thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
        //                pp.Y + thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));

        //            PointD pp2 = new PointD(
        //                pp.X - thickness / 2 * Math.Cos(A1 * fp + A2 * (1 - fp)),
        //                pp.Y - thickness / 2 * Math.Sin(A1 * fp + A2 * (1 - fp)));
        //            PointD pc1 = new PointD(
        //                P.X + thickness / 2 * Math.Cos(A1 * f + A2 * (1 - f)),
        //                P.Y + thickness / 2 * Math.Sin(A1 * f + A2 * (1 - f)));

        //            PointD pc2 = new PointD(
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
        //    PointD pp = null;
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
        //        PointD pl1_1 = PointD.Intermediate(P1, a1, f);
        //        PointD pl1_2 = PointD.Intermediate(a1, a2, f);
        //        PointD pl1_3 = PointD.Intermediate(a2, P2, f);

        //        PointD pl2_1 = PointD.Intermediate(pl1_1, pl1_2, f);
        //        PointD pl2_2 = PointD.Intermediate(pl1_2, pl1_3, f);

        //        PointD P = PointD.Intermediate(pl2_1, pl2_2, f);

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
    public class RotatingBezierSpline
    {

        public List<RotatingBezierSplineAnchor> Anchors { get; private set; } = new List<RotatingBezierSplineAnchor>();

        public float FlatTipWidth { get; internal set; }

        public void AddAnchor(PointF point)
        {
            Add(point, point);
        }
        public void Add(PointF point, PointF a12)
        {
            PointD P = new PointD(point.X, point.Y);
            PointD a1 = new PointD(a12.X, a12.Y);
            PointD a2 = PointD.Intermediate(a1, P, -1);
            Anchors.Add(new RotatingBezierSplineAnchor(P, a1, a2));
        }
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
        //            new PointD(Convert.ToDouble(parts[0]), Convert.ToDouble(parts[1])),
        //            new PointD(Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3])),
        //            new PointD(Convert.ToDouble(parts[4]), Convert.ToDouble(parts[5]))
        //            );
        //        a.rh1 = new PointD(Convert.ToDouble(parts[6]), Convert.ToDouble(parts[7]));
        //        a.rh2 = new PointD(Convert.ToDouble(parts[8]), Convert.ToDouble(parts[9]));
        //        Spline.Anchors.Add(a);
        //    }

        //    if (Spline.Anchors.Count > 0)
        //        Splines.Add(Spline);
        //    return Splines;
        //}
        public MouseState MouseState { get; set; }
        public float Thickness { get; set; } = 0;
        public void Draw(Graphics g, InkDrawMode inkDrawMode, AnchorDrawMode anchorDrawMode)
        {
            for (int i = 0; i < Anchors.Count - 1; i++)
            {
                new BezierCurveCellWithRotation(Anchors[i], Anchors[i + 1]).Draw(g, Thickness, MouseState, inkDrawMode, anchorDrawMode);
            }
        }

        internal void ProcessMouse(Point location, float v1, float v2)
        {
            //throw new NotImplementedException();
        }

        internal void NitifyMouseDown(Point location)
        {
            //throw new NotImplementedException();
        }

        internal bool ContainsMouse(Point location)
        {
            return false;
            //throw new NotImplementedException();
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