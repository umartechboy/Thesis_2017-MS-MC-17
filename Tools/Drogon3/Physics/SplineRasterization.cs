using Physics;
using RotatingBezierSplineEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace RoboSim
{
    public class RasterizedRotatingBezierSpline
    {

        public RotatingBezierSpline Spline { get; private set; }
        public List<RasterizedSplineCell> RasterizedCells { get; set; }
        public CurveMenuItem CurveMenuItem { get; set; }
        //public List<RasterizedSplinePart> SplineParts { get; set; }
        //public RotatingBezierSpline Spline { get; set; }

        public static RasterizedRotatingBezierSpline Rasterize(RotatingBezierSpline spline, double resolution, double scale, Func<float, bool> progressUpdate)
        {
            int ind = 0;
            bool Progress(float f)
            {
                f /= (spline.Anchors.Count - 1);
                f += ind / (float)(spline.Anchors.Count - 1);
                progressUpdate(f);
                if (f > 1)
                    f = 1;
                return true;
            }
            RasterizedRotatingBezierSpline rCurve = new RasterizedRotatingBezierSpline();
            rCurve.RasterizedCells = new List<RasterizedSplineCell>();
            rCurve.Spline = spline;
            for (int i = 0; i < spline.Anchors.Count - 1; i++)
            {
                List<RasterizedPoint> rps = new List<RasterizedPoint>();
                var cell = new BezierCurveCellWithRotation(spline.Anchors[i], spline.Anchors[i + 1]);
                RasterizeCell(cell, resolution, scale, rps, spline.FlatTipWidth, Progress);
                var rasterizedCurvePart = new RasterizedSplineCell()
                {
                    RasterizedData = rps
                };
                rCurve.RasterizedCells.Add(rasterizedCurvePart);
                ind++;
            }
            return rCurve;
        }
        static void RasterizeCell(BezierCurveCellWithRotation cell, double resolution, double scale, List<RasterizedPoint> rps, float thickness, Func<float, bool> progressUpdate)
        {
            double r1 = cell.A1.R;
            double r2 = cell.A2.R;
            double f = 0;
            while (f < 1)
            {
                double bkpf = f;
                var XY = moveOn(ref f, resolution, scale, resolution * 0.05, cell.A1, cell.A2);
                if (f > 1)
                    break;
                var T = r1 * f + r2 * (1 - f);
                rps.Add(new RasterizedPoint()
                {
                    X = XY.X * scale,
                    Y = XY.Y * scale,
                    T = T,
                    Orientaation = new EulerAngleOrientation(T, 0, 0, XY.X * scale, XY.Y * scale, 0),
            });
                progressUpdate((float)f);
            }
            //x.Reverse();
            //y.Reverse();
            return;// new RasterizedRotatingBezierSpline() { X = x.ToArray(), Y = y.ToArray(), T = t.ToArray() };
        }
        static RBSPoint moveOn(ref double f, double distance, double scale, double tol, RotatingBezierSplineAnchor A1, RotatingBezierSplineAnchor A2)
        {
            var p = BezierCurveCellWithRotation.BezierInterpolate(f, A1, A2);

            double f0 = f;
            double f1 = f + .001;
            var p0 = BezierCurveCellWithRotation.BezierInterpolate(f0, A1, A2);
            var p1 = BezierCurveCellWithRotation.BezierInterpolate(f1, A1, A2);
            double d0 = p0.DistanceFrom(p0);
            double d1 = p1.DistanceFrom(p0);
            double m = (f1 - f0) / (d1 - d0);
            double c = f0;
            double testF = distance * m + c;
            var pTest = BezierCurveCellWithRotation.BezierInterpolate(testF, A1, A2); ;
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
    }
    public class RasterizedSplineCell
    {
        public List<RasterizedPoint> RasterizedData { get; set; }
        public Model3D Model { get; internal set; }
    }
    public class RasterizedPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double T { get; set; }
        public EulerAngleOrientation Orientaation { get; set; }
    }
}
