using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.Windows.Media.Media3D;

namespace Physics
{

    public enum Axis
    { X, Y, Z }

    public class TransformationMatrixSeries    :TransformationMatrix
    {
        public override event EventHandler ArrayChanged;
        public int Width { get { return collection.Length; } }
        TransformationMatrix[] collection;
        public TransformationMatrixSeries(params TransformationMatrix[] singles)
        {
            collection = singles;
            foreach (var s in singles)
            { s.ArrayChanged += S_ArrayChanged; }
        }

        private void S_ArrayChanged(object sender, EventArgs e)
        {
            ArrayChanged?.Invoke(this, new EventArgs());
        }

        private TransformationMatrixSeries(int width)
        {
            collection = new TransformationMatrix[width];
        }
        public static TransformationMatrixSeries operator * (TransformationMatrixSeries series1, TransformationMatrixSeries series2)
        {
            if (series1.Width == series2.Width)
            {
                var ans = new TransformationMatrixSeries(series1.Width);
                for (int i = 0; i < ans.Width; i++)
                    ans.collection[i] = new TransformationMatrix(series1.collection[i] * (Matrix<double>)series2.collection[i]);
                return ans;
            }
            else
                throw new Exception("Series dimensions don't match.");
        }
    }
    public class TransformationMatrix
    {                                   
        bool isInv = false;
        public bool IsInverse { get { return isInv; } set { isInv = value; ArrayChanged?.Invoke(this, new EventArgs()); } }
        public TransformationMatrix Inverse()
        {
            return new TransformationMatrix(castAsMatrixShadow.Clone().Inverse());
        }

        public TransformationMatrix(Matrix<double> matrix)
        {
            castAsMatrixShadow = matrix;
        }
        public static TransformationMatrix operator ^(TransformationMatrix H, int exponent)
        {
            var ans = new TransformationMatrix();

            if (exponent == 0)
                ans.castAsMatrixShadow = Matrix<double>.Build.DenseIdentity(4);
            else if (exponent < 0)
                ans.castAsMatrixShadow = ((Matrix<double>)H).Power(-exponent).Inverse();
            else
                ans.castAsMatrixShadow = ((Matrix<double>)H).Power(exponent);
            
            return ans;
        }
        public static Point3D operator * (TransformationMatrix H, Point3D p)
        {
            var P = Matrix<double>.Build.Dense(4, 1);
            P[0, 0] = p.X;
            P[1, 0] = p.Y;
            P[2, 0] = p.Z;
            P[3, 0] = 1;
            var ans = H * P;
            return new Point3D(ans[0, 0], ans[1, 0], ans[2, 0]);
        }                             
        protected Matrix<double> castAsMatrixShadow;
        protected TransformationMatrix()
        {
            reset();       
        }

        public static implicit operator Matrix<double>(TransformationMatrix tm)
        {
            if (!tm.IsInverse)
                return tm.castAsMatrixShadow;
            else
                return tm.castAsMatrixShadow.Inverse();
        }
        protected void reset()
        {
            var matrix = new double[16] {
             1,0,0,0,
             0,1,0,0,
             0,0,1,0,
             0,0,0,1 };

            castAsMatrixShadow = Matrix<double>.Build.Dense(4, 4, matrix);
            ArrayChanged?.Invoke(this, new EventArgs());
        }
        public virtual event EventHandler ArrayChanged;
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int ri = 0; ri < 4; ri++)
            {
                sb.Append("|");
                for (int ci = 0; ci < 4; ci++)
                {
                    sb.Append(Math.Round((((Matrix<double>)this)[ri, ci]), 3).ToString());
                    if (ci < 3)
                        sb.Append(",\t");
                    else
                        sb.Append("\t");
                }
                sb.Append("|; ");
            }
            return sb.ToString();
        }
    }
    public class RotateTransformationMatrix : TransformationMatrix
    {
        public override event EventHandler ArrayChanged;
        public RotateTransformationMatrix(double th, Axis axis)
        {
            this.axis = axis;
            this.Th = th;
        }
        double th = 0;
        Axis axis = Axis.X;
        public Axis Axis { get { return axis; } set { axis = value; reset(); Th = th; } }
        public double Th
        {
            get { return th; }
            set
            {
                th = value;
                if (axis == Axis.X)
                {
                    ((Matrix<double>)this)[1, 1] = Math.Cos(th);
                    ((Matrix<double>)this)[1, 2] = -Math.Sin(th);
                    ((Matrix<double>)this)[2, 1] = Math.Sin(th);
                    ((Matrix<double>)this)[2, 2] = Math.Cos(th);
                }
                else if (axis == Axis.Y)
                {
                    ((Matrix<double>)this)[2, 2] = Math.Cos(th);
                    ((Matrix<double>)this)[2, 0] = -Math.Sin(th);
                    ((Matrix<double>)this)[0, 2] = Math.Sin(th);
                    ((Matrix<double>)this)[0, 0] = Math.Cos(th);
                }
                else
                {
                    ((Matrix<double>)this)[0, 0] = Math.Cos(th);
                    ((Matrix<double>)this)[0, 1] = -Math.Sin(th);
                    ((Matrix<double>)this)[1, 0] = Math.Sin(th);
                    ((Matrix<double>)this)[1, 1] = Math.Cos(th);
                }
                ArrayChanged?.Invoke(this, new EventArgs());
            }
        }
    }
    public class ChainTranformationMatrix : TransformationMatrix
    {
        public ChainTranformationMatrix()
        {
            Children.CollectionChanged += Children_CollectionChanged;
        }

        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var i in e.NewItems)
                ((TransformationMatrix)i).ArrayChanged += ChainTranformationMatrix_ArrayChanged;
            Compute();
        }
        
        private void ChainTranformationMatrix_ArrayChanged(object sender, EventArgs e)
        {
            Compute();
        }

        public List<RotateTransformationMatrix> Rotations
        {
            get
            {
                var ans = new List<RotateTransformationMatrix>();
                foreach (var c in Children)
                {
                    if (c is RotateTransformationMatrix)
                        ans.Add((RotateTransformationMatrix)c);
                }
                return ans;
            }
        }
        public List<TranslateTransformationMatrix> Translations
        {
            get
            {
                var ans = new List<TranslateTransformationMatrix>();
                foreach (var c in Children)
                {
                    if (c is TranslateTransformationMatrix)
                        ans.Add((TranslateTransformationMatrix)c);
                }
                return ans;
            }
        }
        public ObservableCollection<TransformationMatrix> Children { get; protected set; } = new ObservableCollection<TransformationMatrix>();
        public TransformationMatrix Compute()
        {
            castAsMatrixShadow = Matrix<double>.Build.DenseIdentity(4);
            foreach (var H in Children)
                castAsMatrixShadow *= H;
            return this;
        }
    }
    public class TranslateTransformationMatrix : TransformationMatrix
    {

        public override event EventHandler ArrayChanged;
        public Point3D Translate
        {
            get { return new Point3D(((Matrix<double>)this)[0, 3], ((Matrix<double>)this)[1, 3], ((Matrix<double>)this)[2, 3]); }
            set { ((Matrix<double>)this)[0, 3] = value.X; ((Matrix<double>)this)[1, 3] = value.Y; ((Matrix<double>)this)[2, 3] = value.Z; ArrayChanged?.Invoke(this, new EventArgs()); }
        }
        public double X { get { return ((Matrix<double>)this)[0, 3]; } set { ((Matrix<double>)this)[0, 3] = value; ArrayChanged?.Invoke(this, new EventArgs()); } }
        public double Y { get { return ((Matrix<double>)this)[1, 3]; } set { ((Matrix<double>)this)[1, 3] = value; ArrayChanged?.Invoke(this, new EventArgs()); } }
        public double Z { get { return ((Matrix<double>)this)[2, 3]; } set { ((Matrix<double>)this)[2, 3] = value; ArrayChanged?.Invoke(this, new EventArgs()); } }
        public TranslateTransformationMatrix(double x, double y, double z)
        { X = x; Y = y; Z = z; }
    }

    public class EulerAngleOrientation
    {
        public EulerAngleOrientation(double a = 0, double b = 0, double g = 0, double x = 0, double y = 0, double z = 0)
        {
            A = a;
            B = b;
            G = g;
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return
                "P = [" + Math.Round(X, 3) + ", " + Math.Round(Y, 3) + ", " + Math.Round(Z, 3) + "], " +
                "O = [" + Math.Round(A, 3) + ", " + Math.Round(B, 3) + ", " + Math.Round(G, 3) + "]";
        }
        public EulerAngleOrientation Clone()
        {
            return new EulerAngleOrientation(A, B, G, X, Y, Z);
        }
        public override bool Equals(object obj)
        {
            if (obj is EulerAngleOrientation)
            return ((EulerAngleOrientation)obj) == this;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static EulerAngleOrientation FromTransformationMatrix(TransformationMatrix H)
        {
            var EulerAngles = new EulerAngleOrientation();
            EulerAngles.X = ((Matrix<double>)H)[0, 3];
            EulerAngles.Y = ((Matrix<double>)H)[1, 3];
            EulerAngles.Z = ((Matrix<double>)H)[2, 3];
            EulerAngles.B = Math.Acos(((Matrix<double>)H)[2, 2]);
            if (EulerAngles.B == 0)
            {
                EulerAngles.G = 0;
                EulerAngles.A = Math.Atan2(((Matrix<double>)H)[1, 0], ((Matrix<double>)H)[0, 0]);
            }
            else
            {
                EulerAngles.G = Math.Acos(((Matrix<double>)H)[2, 1] / Math.Sin(Math.Acos(((Matrix<double>)H)[2, 2])));
                EulerAngles.A = Math.Acos(-((Matrix<double>)H)[1, 2] / Math.Sin(Math.Acos(((Matrix<double>)H)[2, 2])));
            }
            return EulerAngles;

            //var T = new EulerAngleOrientation();
            //T.Offset = H * new Point3D();
            //var Pt = T.Offset;
            //ChainTranformationMatrix H1 = new ChainTranformationMatrix();
            //H1.Children.Add(H);
            //H1.Children.Add(new TranslateTransformationMatrix(0, 0, -1));

            //var P2 = H1 * new Point3D();
            //var Vl2 = P2 - Pt;
            //T.A = Math.Atan2(Vl2.Y, Vl2.X) + Math.PI / 2;
            //if (Vl2.X == 0 && Vl2.Y == 0)
            //    T.A = 0;

            //var a = Math.Sqrt(Vl2.X*Vl2.X + Vl2.Y * Vl2.Y);
            //T.B = -Math.Atan2(Vl2.Z, a) + Math.PI/ 2;
            //var H2 = new ChainTranformationMatrix();
            //H2.Children.Add(new RotateTransformationMatrix(-T.B, Axis.X));
            //H2.Children.Add(new RotateTransformationMatrix(-T.A, Axis.Z));
            //H2.Children.Add(new TranslateTransformationMatrix(-T.X, -T.Y, -T.Z));
            //H2.Children.Add(H);
            //var Pt_ = H2 * new Point3D(1, 0, 0);
            //T.G = Math.Atan2(Pt_.Y, Pt_.X) - Math.PI/ 2;
            //return T;
        }
        public EulerAngleOrientation(Point3D p, double a = 0, double b = 0, double g = 0)
        {
            A = a;
            B = b;
            G = g;
            X = p.X;
            Y = p.Y;
            Z = p.Z ;
        }

        public double A { get { return ((AxisAngleRotation3D)(a.Rotation)).Angle; } set { ((AxisAngleRotation3D)(a.Rotation)).Angle = value; } }
        public double B { get { return ((AxisAngleRotation3D)(b.Rotation)).Angle; } set { ((AxisAngleRotation3D)(b.Rotation)).Angle = value; } }
        public double G { get { return ((AxisAngleRotation3D)(g.Rotation)).Angle; } set { ((AxisAngleRotation3D)(g.Rotation)).Angle = value; } }
        public double X { get { return trans.OffsetX; } set { trans.OffsetX = value; } }
        public double Y { get { return trans.OffsetY; } set { trans.OffsetY = value; } }
        public double Z { get { return trans.OffsetZ; } set { trans.OffsetZ = value; } }
        protected RotateTransform3D a = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0));
        protected RotateTransform3D b = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0));
        protected RotateTransform3D g = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0));
        protected TranslateTransform3D trans = new TranslateTransform3D();
        public Point3D Offset { get { return new Point3D(X, Y, Z); } set { X = value.X; Y = value.Y; Z = value.Z; } }
        public Vector3D Orientation { get { return new Vector3D(A, B, G); } set { A = value.X; B = value.Y; G = value.Z; } }
        public Transform3DCollection Children { get { return ((Transform3DGroup)this).Children; } }    
                                    

        public static implicit operator Transform3DGroup(EulerAngleOrientation orient)
        {
            var gp = new Transform3DGroup();
            var a = new RotateTransform3D(new AxisAngleRotation3D(((AxisAngleRotation3D)orient.a.Rotation).Axis, ((AxisAngleRotation3D)orient.a.Rotation).Angle * 180 / Math.PI));
            var b = new RotateTransform3D(new AxisAngleRotation3D(((AxisAngleRotation3D)orient.b.Rotation).Axis, ((AxisAngleRotation3D)orient.b.Rotation).Angle * 180 / Math.PI));
            var g = new RotateTransform3D(new AxisAngleRotation3D(((AxisAngleRotation3D)orient.g.Rotation).Axis, ((AxisAngleRotation3D)orient.g.Rotation).Angle * 180 / Math.PI));

            gp.Children.Add(g); 
            gp.Children.Add(b); 
            gp.Children.Add(a);
            gp.Children.Add(orient.trans);
            return gp;
        }
        public static implicit operator ChainTranformationMatrix(EulerAngleOrientation vo)
        {
            var H = new ChainTranformationMatrix();
            H.Children.Add(new TranslateTransformationMatrix(vo.X, vo.Y, vo.Z));
            H.Children.Add(new RotateTransformationMatrix(vo.A, Axis.Z));
            H.Children.Add(new RotateTransformationMatrix(vo.B, Axis.Y));
            H.Children.Add(new RotateTransformationMatrix(vo.G, Axis.Z));
            return H;
        }
        public static implicit operator EulerAngleOrientation(Transform3DGroup gp)
        {
            var orient = new EulerAngleOrientation();
            orient.a = (RotateTransform3D)gp.Children[0];
            orient.b = (RotateTransform3D)gp.Children[1];
            orient.g = (RotateTransform3D)gp.Children[2];
            orient.trans = (TranslateTransform3D)gp.Children[3];
            return orient;
        }
        public static bool operator ==(EulerAngleOrientation o1, EulerAngleOrientation o2)
        {
            bool aIsNull = false;
            bool bIsNull = false;
            if (object.ReferenceEquals(o1, null) && object.ReferenceEquals(o1, null))
                return true;
            if (object.ReferenceEquals(o1, null))
                return false;
            if (object.ReferenceEquals(o2, null))
                return false;
            try { double a = o1.A; }
            catch { aIsNull = true; }
            try { double a = o2.A; }
            catch { bIsNull = true; }
            if (aIsNull && bIsNull)
                return true;
            if (aIsNull || bIsNull)
                return false;
            return o1.A == o2.A && o1.b == o2.b && o1.g == o2.g && o1.X == o2.X && o1.Y == o2.Y && o1.Z == o2.Z;
        }
        public static bool operator !=(EulerAngleOrientation o1, EulerAngleOrientation o2)
        {
            return !(o1 == o2);
        }
    }
}
