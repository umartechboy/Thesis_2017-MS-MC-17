using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using Physics;
using RotatingBezierSplineEditor;

namespace RoboSim
{
    public class Ink
    {
        protected Model3DGroup gm = null;
        public Model3DGroup Model
        {
            get { return gm; }
        }
        //public double Thickness { get; set; }
        public System.Windows.Media.Color PenColor { get; set; }
        public System.Windows.Media.Color TraceColor { get; set; }
        public Model3DGroup WrittingPad = new Model3DGroup();
        EulerAngleOrientation _o;
        public EulerAngleOrientation InkOrientation
        {
            get { return _o; }
            set
            {
                _o = value;
                WrittingPad.Children[0].Transform = _o;
                WrittingPad.Children[1].Transform = _o;
            }
        }
        public Ink()
        {
            //Thickness = thickness;
            PenColor = System.Windows.Media.Colors.Black;
            TraceColor = System.Windows.Media.Colors.LightGray;
            gm = new Model3DGroup();                                                
            gm.Children.Add(WrittingPad);
            WrittingPad.Children.Add(new Model3DGroup()); // board
            WrittingPad.Children.Add(new Model3DGroup()); // ink
            WrittingPad.Children.Add(new Model3DGroup()); // trace
            InkOrientation = new EulerAngleOrientation();
        }
        public void SetBoard(double width, double height)
        {
            var m = new ChainTranformationMatrix(); ;
            m.Children.Add(new TranslateTransformationMatrix(0, 0, -0.0002));
            var p1 = m * new Point3D(-width / 2, height / 2, 0);
            var p2 = m * new Point3D(width / 2, height / 2, 0);
            var p3 = m * new Point3D(width / 2, -height / 2, 0);
            var p4 = m * new Point3D(-width / 2, -height / 2, 0);

            MeshGeometry3D mesh = new MeshGeometry3D();
            simCalc.addTriangleToMesh(mesh, p2, p1, p3);
            simCalc.addTriangleToMesh(mesh, p1, p4, p3);
            simCalc.addTriangleToMesh(mesh, p1, p2, p3);
            simCalc.addTriangleToMesh(mesh, p4, p1, p3);

            WrittingPad.Children[0] = simCalc.meshToGeometry(mesh, System.Windows.Media.Colors.White);
            ClearInk();
            ClearTrace();
        }


        public void ClearInk()
        {
            ((Model3DGroup)WrittingPad.Children[1]).Children.Clear();
            NewCharCell();
        }
        public void ClearTrace()
        {
            ((Model3DGroup)WrittingPad.Children[2]).Children.Clear();

            NewTraceChar();
        }
        EulerAngleOrientation LastOrientation = null;
        TransformationMatrix LastTraceOrientation = null;
        public void NewCharCell()
        {
            ((Model3DGroup)WrittingPad.Children[1]).Children.Add(new Model3DGroup());
            LastOrientation = null;
        }
        public void NewTraceChar()
        {
            ((Model3DGroup)WrittingPad.Children[2]).Children.Add(new Model3DGroup());
            LastTraceOrientation = null;
        }

        public void AppendFlatTipPoint(double x, double y, double twist, double Thickness)
        {
            var o = new EulerAngleOrientation(twist, 0, 0, x, y, 0);
            AppendFlatTipPoint(o, Thickness);
        }
        public void AppendTracePoint(double x, double y, double twist, double Thickness)
        {
            var o = new EulerAngleOrientation(twist, 0, 0, x, y, 0);
            AppendTracePoint(o, Thickness);
        }
        private void AppendFlatTipPoint(EulerAngleOrientation o, double Thickness)
        {             
            if (o == LastOrientation)
            {
                if (o != null)
                    LastOrientation = o.Clone();
                return;
            }
            if (o == null || LastOrientation == null)
            {
                if (o != null)
                    LastOrientation = o.Clone();
                return;
            }
            AddFlatTipStroke(LastOrientation, o, Thickness, true);
            LastOrientation = o.Clone();
        }
        private void AppendTracePoint(EulerAngleOrientation o, double Thickness)
        {
            AppendTracePoint((ChainTranformationMatrix)o, Thickness);
        }
        public void AppendTracePoint(TransformationMatrix o, double Thickness)
        {
            if (o == LastTraceOrientation)
            {
                if (o != null)
                    LastTraceOrientation = o;
                return;
            }
            if (o == null || LastTraceOrientation == null)
            {
                if (o != null)
                    LastTraceOrientation = o;
                return;
            }
            AddFlatTipStroke(LastTraceOrientation, o, Thickness, false);
            LastTraceOrientation = o;
        }
        private void AddFlatTipStroke(EulerAngleOrientation o1, EulerAngleOrientation o2, double thicknes, bool isInk)
        {
            AddFlatTipStroke((ChainTranformationMatrix)o1, (ChainTranformationMatrix)o2, thicknes, isInk);
        }

        private void AddFlatTipStroke(TransformationMatrix o1, TransformationMatrix o2, double thicknes, bool isInk)
        {
            //if (InkParts.Last().Targets.Count == 0)
            //    InkParts.Last().Targets.Add(o1);
            //InkParts.Last().Targets.Add(o2);
            System.Windows.Media.Color color = PenColor;
            if (!isInk)
                color = TraceColor;
            var m = new ChainTranformationMatrix();
            var H1 = new TransformationMatrix(m * (Matrix<double>)(o1));
            var H2 = new TransformationMatrix(m * (Matrix<double>)(o2));
            var p1 = H1 * new Point3D(thicknes / 2, 0, -.0001);
            var p2 = H1 * new Point3D(-thicknes / 2, 0, -.0001);
            var p3 = H2 * new Point3D(-thicknes / 2, 0, -.0001);
            var p4 = H2 * new Point3D(thicknes / 2, 0, -.0001);
            MeshGeometry3D mesh = new MeshGeometry3D();
            simCalc.addTriangleToMesh(mesh, p1, p2, p3);
            simCalc.addTriangleToMesh(mesh, p1, p3, p4);
            simCalc.addTriangleToMesh(mesh, p2, p1, p3);
            simCalc.addTriangleToMesh(mesh, p1, p4, p3);
            var chr = ((Model3DGroup)((Model3DGroup)WrittingPad.Children[isInk ? 1 : 2]).Children.Last());
            chr.Children.Add(simCalc.meshToGeometry(mesh, color));
            //p1 = H1 * new Point3D(thicknes / 2, 0, 0);
            //p2 = H1 * new Point3D(-thicknes / 2, 0, 0);
            //p3 = H2 * new Point3D(-thicknes / 2, 0, 0);
            //p4 = H2 * new Point3D(thicknes / 2, 0, 0);
            //mesh = new MeshGeometry3D();
            //simCalc.addTriangleToMesh(mesh, p1, p2, p3);
            //simCalc.addTriangleToMesh(mesh, p1, p3, p4);
            //simCalc.addTriangleToMesh(mesh, p2, p1, p3);
            //simCalc.addTriangleToMesh(mesh, p1, p4, p3);
            //((Model3DGroup)WrittingPad.Children.Last()).Children.Add(simCalc.meshToGeometry(mesh, color));
        }

        public void RemoveSpline(RasterizedRotatingBezierSpline rasterizedSpline)
        {
            foreach (var model in rasterizedSpline.RasterizedCells.Select(rc => rc.Model))
            {
                ((Model3DGroup)WrittingPad.Children[1]).Children.Remove(model);
            }
        }
        public RasterizedRotatingBezierSpline ImportSplines(RotatingBezierSpline spline, double scale, Func<float, bool> progressUpdate)
        {
            if (spline.Anchors.Count <= 1)
                return null;
            var rSplines = RasterizedRotatingBezierSpline.Rasterize(spline, 1, scale, progressUpdate);
            if (rSplines.RasterizedCells.Count == 0)
                return null;
            rSplines.CurveMenuItem = new CurveMenuItem(spline, true);

            foreach (var cell in rSplines.RasterizedCells)
            {
                NewCharCell();
                for (int i = 0; i < cell.RasterizedData.Count; i++)
                    AppendFlatTipPoint(cell.RasterizedData[i].Orientation, spline.FlatTipWidth * scale);
                cell.Model = ((Model3DGroup)WrittingPad.Children[1]).Children.Last();
            }
            return rSplines;
        }
    }
}
