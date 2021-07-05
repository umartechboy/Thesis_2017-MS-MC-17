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

namespace RoboSim
{
    public class Ink
    {
        protected Model3DGroup gm = null;
        public Model3DGroup Model
        {
            get { return gm; }
        }
        public double Thickness { get; set; }
        public System.Windows.Media.Color PenColor { get; set; }
        public Model3DGroup WrittingPad = new Model3DGroup();
        public EulerAngleOrientation Orientation = new EulerAngleOrientation();
        public Ink(double thickness)
        {
            Thickness = thickness;
            PenColor = System.Windows.Media.Colors.Black;
            gm = new Model3DGroup();                                                
            gm.Children.Add(WrittingPad);   
            NewChar();
        }
        public void SetBoard(double width, double height, EulerAngleOrientation orientation)
        {                                          
            var m = (ChainTranformationMatrix)orientation;
            m.Children.Add(new TranslateTransformationMatrix(0, 0, -0.001));
            var p1 = m * new Point3D(-width / 2, height / 2, 0);
            var p2 = m * new Point3D(width / 2, height / 2, 0);
            var p3 = m * new Point3D(width / 2, -height / 2, 0);
            var p4 = m * new Point3D(-width / 2, -height / 2, 0);

            MeshGeometry3D mesh = new MeshGeometry3D();
            simCalc.addTriangleToMesh(mesh, p2, p1, p3);
            simCalc.addTriangleToMesh(mesh, p1, p4, p3);

            WrittingPad.Children.Clear();
            WrittingPad.Children.Add(simCalc.meshToGeometry(mesh, System.Windows.Media.Colors.White));
        }
        public void Clear()
        {
            gm.Children.Clear();
            gm.Children.Add(WrittingPad);
            NewChar();
        }
        EulerAngleOrientation LastOrientation = null;
        public void NewChar()
        {                            
            gm.Children.Add(new Model3DGroup());
            LastOrientation = null;
        }
                    
        public void AppendFlatTipPoint(EulerAngleOrientation o, EulerAngleOrientation boardOrientation)
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
            AddFlatTipStroke(LastOrientation, o, Thickness, boardOrientation);
            LastOrientation = o.Clone();
        }
        private void AddFlatTipStroke(EulerAngleOrientation o1, EulerAngleOrientation o2, double thicknes, EulerAngleOrientation boardOrientation)
        {           
            var m = (ChainTranformationMatrix)boardOrientation;
            var H1 = new TransformationMatrix(m * (Matrix<double>)((ChainTranformationMatrix)o1));
            var H2 = new TransformationMatrix(m * (Matrix<double>)((ChainTranformationMatrix)o2));
            var p1 = H1 * new Point3D(thicknes / 2, 0, 0);
            var p2 = H1 * new Point3D(-thicknes / 2, 0, 0);
            var p3 = H2 * new Point3D(-thicknes / 2, 0, 0);
            var p4 = H2 * new Point3D(thicknes / 2, 0, 0);
            MeshGeometry3D mesh = new MeshGeometry3D();
            simCalc.addTriangleToMesh(mesh, p1, p2, p3);
            simCalc.addTriangleToMesh(mesh, p1, p3, p4);
            simCalc.addTriangleToMesh(mesh, p2, p1, p3);
            simCalc.addTriangleToMesh(mesh, p1, p4, p3);
            ((Model3DGroup)gm.Children[gm.Children.Count-1]).Children.Add(simCalc.meshToGeometry(mesh, System.Windows.Media.Colors.Red));
        }
    }
}
