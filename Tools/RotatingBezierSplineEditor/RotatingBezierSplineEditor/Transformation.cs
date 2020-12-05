//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RotatingBezierSplineEditor
//{
//    public class EulerAngleOrientation
//    {
//        public EulerAngleOrientation(double a = 0, double b = 0, double g = 0, double x = 0, double y = 0, double z = 0)
//        {
//            A = a;
//            B = b;
//            G = g;
//            X = x;
//            Y = y;
//            Z = z;
//        }
//        public override string ToString()
//        {
//            return
//                "P = [" + Math.Round(X, 3) + ", " + Math.Round(Y, 3) + ", " + Math.Round(Z, 3) + "], " +
//                "O = [" + Math.Round(A, 3) + ", " + Math.Round(B, 3) + ", " + Math.Round(G, 3) + "]";
//        }
//        public EulerAngleOrientation Clone()
//        {
//            return new EulerAngleOrientation(A, B, G, X, Y, Z);
//        }
//        public override bool Equals(object obj)
//        {
//            if (obj is EulerAngleOrientation)
//                return ((EulerAngleOrientation)obj) == this;
//            return false;
//        }
//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }
//        public static EulerAngleOrientation FromTransformationMatrix(TransformationMatrix H)
//        {
//            var EulerAngles = new EulerAngleOrientation();
//            EulerAngles.X = ((Matrix<double>)H)[0, 3];
//            EulerAngles.Y = ((Matrix<double>)H)[1, 3];
//            EulerAngles.Z = ((Matrix<double>)H)[2, 3];
//            EulerAngles.B = Math.Acos(((Matrix<double>)H)[2, 2]);
//            if (EulerAngles.B == 0)
//            {
//                EulerAngles.G = 0;
//                EulerAngles.A = Math.Atan2(((Matrix<double>)H)[1, 0], ((Matrix<double>)H)[0, 0]);
//            }
//            else
//            {
//                EulerAngles.G = Math.Acos(((Matrix<double>)H)[2, 1] / Math.Sin(Math.Acos(((Matrix<double>)H)[2, 2])));
//                EulerAngles.A = Math.Acos(-((Matrix<double>)H)[1, 2] / Math.Sin(Math.Acos(((Matrix<double>)H)[2, 2])));
//            }
//            return EulerAngles;

//            //var T = new EulerAngleOrientation();
//            //T.Offset = H * new Point3D();
//            //var Pt = T.Offset;
//            //ChainTranformationMatrix H1 = new ChainTranformationMatrix();
//            //H1.Children.Add(H);
//            //H1.Children.Add(new TranslateTransformationMatrix(0, 0, -1));

//            //var P2 = H1 * new Point3D();
//            //var Vl2 = P2 - Pt;
//            //T.A = Math.Atan2(Vl2.Y, Vl2.X) + Math.PI / 2;
//            //if (Vl2.X == 0 && Vl2.Y == 0)
//            //    T.A = 0;

//            //var a = Math.Sqrt(Vl2.X*Vl2.X + Vl2.Y * Vl2.Y);
//            //T.B = -Math.Atan2(Vl2.Z, a) + Math.PI/ 2;
//            //var H2 = new ChainTranformationMatrix();
//            //H2.Children.Add(new RotateTransformationMatrix(-T.B, Axis.X));
//            //H2.Children.Add(new RotateTransformationMatrix(-T.A, Axis.Z));
//            //H2.Children.Add(new TranslateTransformationMatrix(-T.X, -T.Y, -T.Z));
//            //H2.Children.Add(H);
//            //var Pt_ = H2 * new Point3D(1, 0, 0);
//            //T.G = Math.Atan2(Pt_.Y, Pt_.X) - Math.PI/ 2;
//            //return T;
//        }
//        public EulerAngleOrientation(Point3D p, double a = 0, double b = 0, double g = 0)
//        {
//            A = a;
//            B = b;
//            G = g;
//            X = p.X;
//            Y = p.Y;
//            Z = p.Z;
//        }

//        public double A { get { return ((AxisAngleRotation3D)(a.Rotation)).Angle; } set { ((AxisAngleRotation3D)(a.Rotation)).Angle = value; } }
//        public double B { get { return ((AxisAngleRotation3D)(b.Rotation)).Angle; } set { ((AxisAngleRotation3D)(b.Rotation)).Angle = value; } }
//        public double G { get { return ((AxisAngleRotation3D)(g.Rotation)).Angle; } set { ((AxisAngleRotation3D)(g.Rotation)).Angle = value; } }
//        public double X { get { return trans.OffsetX; } set { trans.OffsetX = value; } }
//        public double Y { get { return trans.OffsetY; } set { trans.OffsetY = value; } }
//        public double Z { get { return trans.OffsetZ; } set { trans.OffsetZ = value; } }
//        protected RotateTransform3D a = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0));
//        protected RotateTransform3D b = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0));
//        protected RotateTransform3D g = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0));
//        protected TranslateTransform3D trans = new TranslateTransform3D();
//        public Point3D Offset { get { return new Point3D(X, Y, Z); } set { X = value.X; Y = value.Y; Z = value.Z; } }
//        public Vector3D Orientation { get { return new Vector3D(A, B, G); } set { A = value.X; B = value.Y; G = value.Z; } }
//        public Transform3DCollection Children { get { return ((Transform3DGroup)this).Children; } }


//        public static implicit operator Transform3DGroup(EulerAngleOrientation orient)
//        {
//            var gp = new Transform3DGroup();
//            gp.Children.Add(orient.g);
//            gp.Children.Add(orient.b);
//            gp.Children.Add(orient.a);
//            gp.Children.Add(orient.trans);
//            return gp;
//        }
//        public static implicit operator ChainTranformationMatrix(EulerAngleOrientation vo)
//        {
//            var H = new ChainTranformationMatrix();
//            H.Children.Add(new TranslateTransformationMatrix(vo.X, vo.Y, vo.Z));
//            H.Children.Add(new RotateTransformationMatrix(vo.A, Axis.Z));
//            H.Children.Add(new RotateTransformationMatrix(vo.B, Axis.Y));
//            H.Children.Add(new RotateTransformationMatrix(vo.G, Axis.Z));
//            return H;
//        }
//        public static implicit operator EulerAngleOrientation(Transform3DGroup gp)
//        {
//            var orient = new EulerAngleOrientation();
//            orient.a = (RotateTransform3D)gp.Children[0];
//            orient.b = (RotateTransform3D)gp.Children[1];
//            orient.g = (RotateTransform3D)gp.Children[2];
//            orient.trans = (TranslateTransform3D)gp.Children[3];
//            return orient;
//        }
//        public static bool operator ==(EulerAngleOrientation o1, EulerAngleOrientation o2)
//        {
//            bool aIsNull = false;
//            bool bIsNull = false;
//            try { double a = o1.A; }
//            catch { aIsNull = true; }
//            try { double a = o2.A; }
//            catch { bIsNull = true; }
//            if (aIsNull && bIsNull)
//                return true;
//            if (aIsNull || bIsNull)
//                return false;
//            return o1.A == o2.A && o1.b == o2.b && o1.g == o2.g && o1.X == o2.X && o1.Y == o2.Y && o1.Z == o2.Z;
//        }
//        public static bool operator !=(EulerAngleOrientation o1, EulerAngleOrientation o2)
//        {
//            return !(o1 == o2);
//        }
//    }
//}
