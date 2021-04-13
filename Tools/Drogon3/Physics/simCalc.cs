using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Media3D;
using MathNet.Numerics.LinearAlgebra;

namespace RoboSim
{
    public class simCalc
    {
        static string roundedFrac(float frac)
        {
            if (frac <= 1e-5 && frac >= -1e-5)
                return "0";
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
            return (Math.Round(frac * (isNeg ? -1 : 1), 1) / mult).ToString();
        }           
        public static void drawXAxis(Graphics g, float w, float h, float scale, float tx, float ty, bool grid, Color backColor)
        {
            var axisP = new Pen(Color.LightGray, 1.5F);
            var majLine = new Pen(Color.Gray, 1.5F);
            var minLine = new Pen(Color.LightGray, 1F);
            g.DrawLine(axisP, tx, 0, tx, h);

            float unitX = 1.0F / 100000000.0F;
            float multF = 5;
            // determine scale first
            while (unitX * scale < 25)
            {
                unitX *= multF;
                multF = multF == 2 ? 5 : 2;
            }

            float minX = 0, maxX = 0;
            while (minX * scale < -tx)
                minX += unitX;
            while (minX * scale > -tx)
                minX -= unitX;

            while (maxX * scale > w - tx)
                maxX -= unitX;
            while (maxX * scale < w - tx)
                maxX += unitX;

            Font f = new Font("ARIAL", 8);

            bool isMinLine = false;
            for (float i = minX; i <= maxX; i += unitX / 2)
            {
                PointF drawableMid = pointToDrawable(new PointF(i, 0), scale, new PointF(tx, ty), h);
                if (!isMinLine)
                {
                    PointF drawable1 = new PointF(drawableMid.X, drawableMid.Y - 1.5F);
                    PointF drawable2 = new PointF(drawableMid.X, drawableMid.Y + 1.5F);
                    if (grid) drawable1 = new PointF(drawable1.X, 0);
                    if (grid) drawable2 = new PointF(drawable2.X, h);
                    string s = roundedFrac(i);
                    var xyo = g.MeasureString(s, f);
                    PointF drawableStr = new PointF(drawableMid.X - xyo.Width / 2, drawableMid.Y + 2);
                    g.DrawLine(majLine, drawable1, drawable2);
                    g.FillRectangle(new SolidBrush(backColor), drawableStr.X, drawableStr.Y, xyo.Width, xyo.Height);
                    g.DrawString(s, f, Brushes.Gray, drawableStr);
                }
                else
                {
                    PointF drawable1 = new PointF(drawableMid.X, drawableMid.Y - 1);
                    PointF drawable2 = new PointF(drawableMid.X, drawableMid.Y + 1);

                    if (grid) drawable1 = new PointF(drawable1.X, 0);
                    if (grid) drawable2 = new PointF(drawable2.X, h);

                    g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }
        }
        public static void drawXAxis(Graphics g, float w, float h, float scale, float tx, float ty)
        {
            drawXAxis(g, w, h, scale, tx, ty, false, Color.White);
        }
        public static void drawYAxis(Graphics g, float w, float h, float scale, float tx, float ty)
        {
            var axisP = new Pen(Color.LightGray, 1.5F);
            var majLine = new Pen(Color.Gray, 1.5F);
            var minLine = new Pen(Color.LightGray, 1F);
            g.DrawLine(axisP, 0, h - ty, w, h - ty);

            float unitX = 1.0F / 100000000.0F;
            float multF = 5;
            // determine scale first
            while (unitX * scale < 25)
            {
                unitX *= multF;
                multF = multF == 2 ? 5 : 2;
            }
            float unitY = 1.0F / 100000000.0F;
            multF = 5;
            // determine scale first
            while (unitY * scale < 15)
            {
                unitY *= multF;
                multF = multF == 2 ? 5 : 2;
            }
                                      
            float minY = 0, maxY = 0;
            while (minY * scale < -ty)
                minY += unitY;
            while (minY * scale > -ty)
                minY -= unitY;

            while (maxY * scale > h - ty)
                maxY -= unitY;
            while (maxY * scale < h - ty)
                maxY += unitY;

            Font f = new Font("ARIAL", 8);

            bool isMinLine = false;     
            for (float i = minY; i <= maxY; i += unitY / 2)
            {
                PointF drawableMid = pointToDrawable(new PointF(0, i), scale, new PointF(tx, ty), h);
                if (!isMinLine)
                {
                    PointF drawable1 = new PointF(drawableMid.X - 1.5F, drawableMid.Y);
                    PointF drawable2 = new PointF(drawableMid.X + 1.5F, drawableMid.Y);
                    string s = roundedFrac(i);
                    float yo = -g.MeasureString(s, f).Height / 2;
                    PointF drawableStr = new PointF(drawableMid.X + 5, drawableMid.Y + yo);
                    g.DrawString(s, f, Brushes.Gray, drawableStr);
                    g.DrawLine(majLine, drawable1, drawable2);
                }
                else
                {
                    PointF drawable1 = new PointF(drawableMid.X - 1, drawableMid.Y);
                    PointF drawable2 = new PointF(drawableMid.X + 1, drawableMid.Y);
                    g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }
        }
        public static void drawXYAxis(Graphics g, float w, float h, float scale, float tx, float ty)
        {
            drawXAxis(g, w, h, scale, tx, ty);
            drawYAxis(g, w, h, scale, tx, ty);
        }

        public static void drawDirectionVector(Graphics g, PointF Position, double Yaw, float vecLen, PointF trans, float h, double scale)
        {
            var dirArrowP = new Pen(Color.Red, 1);
            // draw the current direction
            float dirX = vecLen * (float)Math.Cos(-Yaw);
            float dirY = vecLen * (float)Math.Sin(-Yaw);

            PointF dirLinePivot = Position;
            PointF dirLineEnd = new PointF(Position.X + dirX, Position.Y + dirY);
            PointF PositionD = pointToDrawable(dirLinePivot, scale, trans, h);
            g.DrawLine(dirArrowP,
                PositionD,
                pointToDrawable(dirLineEnd, scale, trans, h));
            // Draw the little Dot on the starting Point
            g.FillEllipse(Brushes.Red, PositionD.X - 2, PositionD.Y - 2, 4, 4);
        }

        public static MeshGeometry3D SphereMesh(double R, double res = 10)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            for (double inc = Math.PI / 2; inc > -Math.PI / 2; inc -= Math.PI / res)
            {
                for (double az = 0; az < Math.PI * 2; az += 2 * Math.PI / res)
                {
                    double inc2 = inc - Math.PI / res;
                    double az2 = az + 2 * Math.PI / res;
                    Point3D pu1 = new Point3D(
                        R * Math.Cos(inc) * Math.Cos(az),
                        R * Math.Cos(inc) * Math.Sin(az),
                        R * Math.Sin(inc));
                    Point3D pd1 = new Point3D(
                        R * Math.Cos(inc2) * Math.Cos(az),
                        R * Math.Cos(inc2) * Math.Sin(az),
                        R * Math.Sin(inc2));
                    Point3D pu2 = new Point3D(
                        R * Math.Cos(inc) * Math.Cos(az2),
                        R * Math.Cos(inc) * Math.Sin(az2),
                        R * Math.Sin(inc));
                    Point3D pd2 = new Point3D(
                        R * Math.Cos(inc2) * Math.Cos(az2),
                        R * Math.Cos(inc2) * Math.Sin(az2),
                        R * Math.Sin(inc2));
                    addRectangleToMesh(mesh, pu1, pu2, pd2, pd1, true);
                }        
            }
            return mesh;
        }

        public static PointF pointToDrawable(PointF point, double scale, PointF offset, float invertHeight)
        {
            return new PointF(
                (float)(point.X * scale + offset.X),
                invertHeight - (float)(point.Y * scale + offset.Y));
        }

        public static PointF xyz2xy(Point3D p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }


        public static void addTriangleToMesh(MeshGeometry3D mesh, Point3D p1, Point3D p2, Point3D p3)
        {
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 3);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 2);
            mesh.TriangleIndices.Add(mesh.Positions.Count - 1);
        }
        static void addRectangleToMesh(MeshGeometry3D mesh, Point3D p1, Point3D p2, Point3D p3, Point3D p4, bool invert = false)
        {
            if (!invert)
            {
                addTriangleToMesh(mesh, p1, p2, p3);
                addTriangleToMesh(mesh, p1, p3, p4);
            }
            else
            {
                addTriangleToMesh(mesh, p2, p1, p3);
                addTriangleToMesh(mesh, p3, p1, p4);
            }   
        }
        public static MeshGeometry3D OneSidedPlateMesh(double width, double height)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            addRectangleToMesh(mesh,
                new Point3D(-width / 2, height / 2, 0),
                new Point3D(-width / 2, -height / 2, 0),
                new Point3D(width / 2, -height / 2, 0),
                new Point3D(width / 2, height / 2, 0)
                );
            return mesh;
        }
        public static Point3D ChangePointFoR(Point3D p, Point3D currentFoRPosition, Vector3D currentFoRRotation)
        {
            double x = p.X, y = p.Y, z = p.Z;
            // Apply Roll. It will only affect y and z.      
            double thRight = Math.Atan2(z, y) + currentFoRRotation.X;
            double rRight = Math.Sqrt(y * y + z * z);
            y = rRight * Math.Cos(thRight);
            z = rRight * Math.Sin(thRight);
            // Apply Pitch. It will only affect x and z.      
            double thBack = Math.Atan2(x, z) + currentFoRRotation.Y;
            double rBack = Math.Sqrt(x * x + z * z);
            x = rBack * Math.Sin(thBack);
            z = rBack * Math.Cos(thBack);
            // Apply Yaw. It will only affect x and y.      
            double thTop = Math.Atan2(y, x) + currentFoRRotation.Z;
            double rTop = Math.Sqrt(x * x + y * y);
            x = rTop * Math.Cos(thTop);
            y = rTop * Math.Sin(thTop);
            // Apply Translation
            return new Point3D((float)x + currentFoRPosition.X, (float)y + currentFoRPosition.Y, (float)z + currentFoRPosition.Z);
        }

        public static bool IsInPolygon(PointF[] poly, PointF pnt)
        {
            int i, j;
            int nvert = poly.Length;
            bool c = false;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((poly[i].Y > pnt.Y) != (poly[j].Y > pnt.Y)) &&
                 (pnt.X < (poly[j].X - poly[i].X) * (pnt.Y - poly[i].Y) / (poly[j].Y - poly[i].Y) + poly[i].X))
                    c = !c;
            }
            return c;
        }
        static public MeshGeometry3D CylenderMesh(double l, double r, bool center = true, double sweep = 2 * Math.PI)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            double res = 20;
            double zOffset = 0;
            if (!center)
                zOffset = l / 2;
            for (double i = 0; i < sweep; i += sweep / res)
            {
                double j = i + 2 * Math.PI / res;
                Point3D p1 = new Point3D(r * Math.Cos(i), r * Math.Sin(i), -l / 2 + zOffset);
                Point3D p2 = new Point3D(r * Math.Cos(j), r * Math.Sin(j), -l / 2 + zOffset);
                Point3D p3 = new Point3D(r * Math.Cos(j), r * Math.Sin(j), l / 2 + zOffset);
                Point3D p4 = new Point3D(r * Math.Cos(i), r * Math.Sin(i), l / 2 + zOffset);
                Point3D pc1 = new Point3D(0, 0, -l / 2 + zOffset);
                Point3D pc2 = new Point3D(0, 0, l / 2 + zOffset);
                addTriangleToMesh(mesh, p2, p1, pc1);
                addTriangleToMesh(mesh, p4, p3, pc2);
                addRectangleToMesh(mesh, p1, p2, p3, p4);
            }
            return mesh;
        }

        static public MeshGeometry3D ConeMesh(double l, double r, bool cover = true, bool inverted = false)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            double res = 20;
            double h1 = 0, h2 = l;
            if (inverted)
            { h2 = 0; h1 = l; }
            for (double i = 0; i < 2*Math.PI; i += 2 * Math.PI / res)
            {
                double j = i + 2 * Math.PI / res;

                Point3D p1 = new Point3D(r * Math.Cos(i), r * Math.Sin(i), h1);
                Point3D p2 = new Point3D(r * Math.Cos(j), r * Math.Sin(j), h1);
                Point3D pc1 = new Point3D(0, 0, h2);
                Point3D pc2 = new Point3D(0, 0, h1);
                if (inverted)
                    addTriangleToMesh(mesh, p2, p1, pc1);
                else
                    addTriangleToMesh(mesh, p1, p2, pc1);
                if (cover)
                {
                    if (inverted)
                        addTriangleToMesh(mesh, p1, p2, pc2);
                    else
                        addTriangleToMesh(mesh, p2, p1, pc2);
                }
            }
            return mesh;
        }
        static public MeshGeometry3D MeshUnion(MeshGeometry3D mesh1, MeshGeometry3D mesh2)
        {
            MeshGeometry3D union = new MeshGeometry3D();
            for (int i = 0; i < mesh1.Positions.Count; i++)
                union.Positions.Add(mesh1.Positions[i]);
            for (int i = 0; i < mesh2.Positions.Count; i++)
                union.Positions.Add(mesh2.Positions[i]);
            for (int i = 0; i < mesh1.TriangleIndices.Count; i++)
                union.TriangleIndices.Add(mesh1.TriangleIndices[i]);

            for (int i = 0; i < mesh2.TriangleIndices.Count; i++)
                union.TriangleIndices.Add(mesh1.Positions.Count + mesh2.TriangleIndices[i]);
            return union;
        }
        static public MeshGeometry3D RectangleToCircle(double w, double h, double r, double length)
        {
            if (w >= r * 2 && h >= r * 2)
            {
                return MeshUnion(
                  CubeMesh(w, h, length, false, 0, 0),
                  CylenderMesh(length, r, false));
            }
            else
            {
                return MeshUnion(
                  CubeMesh(w, h, length, false),
                  ConeMesh(length, r, true, true));
            }

        }
        static public GeometryModel3D CircleToRectangle(double r, double w, double h, double length, System.Windows.Media.Color c)
        {
            return meshToGeometry(MeshUnion(ConeMesh(length / 2, r, false), CubeMesh(w, h, length, false)), c);
        }
        static public Model3DGroup ArrowModel(Vector3D vector, System.Windows.Media.Color color)
        {
            double length = vector.Length;
            var model = new Model3DGroup();
            model.Children.Add(meshToGeometry(CylenderMesh(length * 0.8, length / 30, false), new System.Windows.Media.SolidColorBrush(color)));
            model.Children.Add(meshToGeometry(ConeMesh(length * 0.2, length / 30*1.8), new System.Windows.Media.SolidColorBrush(color)));
            model.Children[1].Transform = new TranslateTransform3D(0, 0, length * 0.8);
            double inc = Math.Asin(vector.Z / vector.Length);
            double l2 = Math.Sqrt(vector.X * vector.X + vector.Y + vector.Y);
            double azm = Math.Acos(vector.X / l2) + Math.PI;
            var tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), inc / Math.PI * 180 - 90)));
            if (!double.IsNaN(azm))
                tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), azm / Math.PI * 180)));
            model.Transform = tg;
            return model;
        }
        static public Model3DGroup AxisRepMesh(double size)
        {
            var model = new Model3DGroup();
            model.Children.Add(ArrowModel(new Vector3D(size, 0, 0), System.Windows.Media.Colors.Red));
            model.Children.Add(ArrowModel(new Vector3D(0, size, 0), System.Windows.Media.Colors.Green));
            model.Children.Add(ArrowModel(new Vector3D(0, 0, size), System.Windows.Media.Colors.Blue));
            model.Children.Add(meshToGeometry(SphereMesh(size / 10), new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black)));
            return model;
        }
        public static MeshGeometry3D CubeMesh(double w, double h, double z, bool center = true, double w2 = double.NaN, double h2 = double.NaN)
        {
            double zOffset = 0;
            if (!center)
                zOffset = z / 2;
            if (double.IsNaN(w2))
                w2 = w;
            if (double.IsNaN(h2))
                h2 = h;
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3D
                p1 = new Point3D(-w / 2, -h / 2, -z / 2 + zOffset),
                p2 = new Point3D(w / 2, -h / 2, -z / 2 + zOffset),
                p3 = new Point3D(w / 2, h / 2, -z / 2 + zOffset),
                p4 = new Point3D(-w / 2, h / 2, -z / 2 + zOffset),
                p5 = new Point3D(-w2 / 2, -h2 / 2, z / 2 + zOffset),
                p6 = new Point3D(w2 / 2, -h2 / 2, z / 2 + zOffset),
                p7 = new Point3D(w2 / 2, h2 / 2, z / 2 + zOffset),
                p8 = new Point3D(-w2 / 2, h2 / 2, z / 2 + zOffset);
            ;
            addRectangleToMesh(mesh, p1, p2, p3, p4, true);
            addRectangleToMesh(mesh, p1, p2, p6, p5);
            addRectangleToMesh(mesh, p5, p6, p7, p8);
            addRectangleToMesh(mesh, p3, p4, p8, p7);
            addRectangleToMesh(mesh, p2, p3, p7, p6);
            addRectangleToMesh(mesh, p1, p4, p8, p5, true);
            return mesh;
        }
        public static Model3DGroup UjointToUJoint(double linkLength, double lX, double lY, double R, double innerClearance1, double outerCut1, double innerClearance2, double outerCut2, System.Windows.Media.Color color, bool hingeFirst = false)
        {
            Model3DGroup model = new Model3DGroup();
            model.Children.Add(RectangulToUJoint(linkLength / 2, lX, lY, innerClearance1, outerCut1, color, true));
            model.Children.Add(RectangulToUJoint(linkLength / 2, lX, lY, innerClearance2, outerCut2, color, false));
            model.Children[1].Transform = new TranslateTransform3D(0, 0, linkLength / 2);
            return model;
        }
        public static Model3DGroup CylinderToEndEffector(double linkLength, double R, double efRepSize, System.Windows.Media.Color color)
        {
            Model3DGroup m0 = new Model3DGroup();
            m0.Children.Add(meshToGeometry(CylenderMesh(linkLength, R, false), new System.Windows.Media.SolidColorBrush(color)));
            m0.Children.Add(AxisRepMesh(efRepSize));
            m0.Children[1].Transform = new TranslateTransform3D(0, 0, linkLength);
            return m0;

        }                                       
        public static Model3DGroup CylinderToUJoint(double linkLength, double lX, double lY, double R, double innerClearance, double outerCut, System.Windows.Media.Color color, bool hingeFirst = false)
        {
            if (hingeFirst)
            {
                Model3DGroup m2 = new Model3DGroup();
                var m = CylinderToUJoint(linkLength, lX, lY, R, innerClearance, outerCut, color, false);
                m.Transform = new Transform3DGroup();
                ((Transform3DGroup)m.Transform).Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180)));
                ((Transform3DGroup)m.Transform).Children.Add(new TranslateTransform3D(0, 0, linkLength));
                m2.Children.Add(m);
                return m2;
            }
            double halfRatio = 0.5;
            innerClearance += outerCut;
            var m0 = new Model3DGroup();
            var tg = new Transform3DGroup();
            var p1 = simCalc.meshToGeometry(simCalc.RectangleToCircle(lX, lY, R, linkLength * halfRatio), color);
            var p2 = simCalc.meshToGeometry(simCalc.CylenderMesh(lX / 2 * (1 - innerClearance), lY / 2, true, Math.PI), color);
            var p3 = simCalc.meshToGeometry(simCalc.CylenderMesh(lX / 2 * (1 - innerClearance), lY / 2, true, Math.PI), color);
            tg = new Transform3DGroup();

            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180)));
            tg.Children.Add(new TranslateTransform3D(0, 0, linkLength * halfRatio));
            p1.Transform = tg;
            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)));
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
            tg.Children.Add(new TranslateTransform3D((lX / 2 - lX / 2 * outerCut) - lX / 4 * (1 - innerClearance), 0, linkLength * halfRatio));
            p2.Transform = tg;
            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)));
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
            tg.Children.Add(new TranslateTransform3D(-((lX / 2 - lX / 2 * outerCut) - lX / 4 * (1 - innerClearance)), 0, linkLength * halfRatio));
            p3.Transform = tg;

            m0.Children.Add(p1);
            m0.Children.Add(p2);
            m0.Children.Add(p3);
            m0.Transform = new TranslateTransform3D(0, 0, linkLength * halfRatio);
            Model3DGroup m3 = new Model3DGroup();
            m3.Children.Add(m0);
            m3.Children.Add(meshToGeometry(CylenderMesh(linkLength * (1 - halfRatio), R, false), new System.Windows.Media.SolidColorBrush(color)));

            
            return m3;
        }
        public static Model3DGroup RectangulToUJoint(double linkLength, double lX, double lY, double innerClearance, double outerCut, System.Windows.Media.Color color, bool hingeFirst = false)
        {
            if (hingeFirst)
            {
                Model3DGroup m2 = new Model3DGroup();
                var m = RectangulToUJoint(linkLength, lX, lY, innerClearance, outerCut, color, false);
                m.Transform = new Transform3DGroup();
                ((Transform3DGroup)m.Transform).Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180)));
                ((Transform3DGroup)m.Transform).Children.Add(new TranslateTransform3D(0, 0, linkLength));
                m2.Children.Add(m);
                return m2;
            }
            innerClearance += outerCut;
            var m0 = new Model3DGroup();
            var tg = new Transform3DGroup();
            var p1 = simCalc.meshToGeometry(simCalc.CubeMesh(lX, lY, linkLength, false), color);
            var p2 = simCalc.meshToGeometry(simCalc.CylenderMesh(lX / 2 * (1 - innerClearance), lY / 2, true, Math.PI), color);
            var p3 = simCalc.meshToGeometry(simCalc.CylenderMesh(lX / 2 * (1 - innerClearance), lY / 2, true, Math.PI), color);
            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)));
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
            tg.Children.Add(new TranslateTransform3D((lX / 2 - lX / 2 * outerCut) - lX / 4 * (1 - innerClearance), 0, linkLength));
            p2.Transform = tg;
            tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)));
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
            tg.Children.Add(new TranslateTransform3D(-((lX / 2 - lX / 2 * outerCut) - lX / 4 * (1 - innerClearance)), 0, linkLength));
            p3.Transform = tg;

            m0.Children.Add(p1);
            m0.Children.Add(p2);
            m0.Children.Add(p3);
            
            return m0;
        }
        public static GeometryModel3D meshToGeometry(MeshGeometry3D mesh, System.Windows.Media.Brush brush = null)
        {
            if (brush == null)
                brush = System.Windows.Media.Brushes.Gray;
            return new GeometryModel3D(mesh, new DiffuseMaterial(brush));
        }          
        public static GeometryModel3D meshToGeometry(MeshGeometry3D mesh, System.Windows.Media.Color color)
        {
            return new GeometryModel3D(mesh, new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(color)));
        }

        public static Vector3D PRYtoXYZ(double pitch, double roll, double yaw)
        {
            return new Vector3D(pitch, roll, yaw);
        }
        public static bool LineSegementsIntersect(PointF p1, PointF p2, PointF q1, PointF q2,
            out PointF intersection, bool considerCollinearOverlapAsIntersect = false)
        {
            Vector2D intersectionV = new Vector2D();
            bool ans = LineSegementsIntersect(
                new Vector2D(p1.X, p1.Y),
                new Vector2D(p2.X, p2.Y),
                new Vector2D(q1.X, q1.Y),
                new Vector2D(q2.X, q2.Y),
                out intersectionV,
                considerCollinearOverlapAsIntersect);
            intersection = new PointF((float)intersectionV.X, (float)intersectionV.Y);
            return ans;
        }
        /// <summary>
        /// Test whether two line segments intersect. If so, calculate the intersection point.
        /// <see cref="http://stackoverflow.com/a/14143738/292237"/>
        /// </summary>
        /// <param name="p">Vector2D to the start point of p.</param>
        /// <param name="p2">Vector2D to the end point of p.</param>
        /// <param name="q">Vector2D to the start point of q.</param>
        /// <param name="q2">Vector2D to the end point of q.</param>
        /// <param name="intersection">The point of intersection, if any.</param>
        /// <param name="considerOverlapAsIntersect">Do we consider overlapping lines as intersecting?
        /// </param>
        /// <returns>True if an intersection point was found.</returns>
        public static bool LineSegementsIntersect(Vector2D p, Vector2D p2, Vector2D q, Vector2D q2,
            out Vector2D intersection, bool considerCollinearOverlapAsIntersect = false)
        {
            intersection = new Vector2D();

            var r = p2 - p;
            var s = q2 - q;
            var rxs = r.Cross(s);
            var qpxr = (q - p).Cross(r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (rxs.IsZero() && qpxr.IsZero())
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s
                // then the two lines are overlapping,
                if (considerCollinearOverlapAsIntersect)
                    if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s))
                        return true;

                // 2. If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s
                // then the two lines are collinear but disjoint.
                // No need to implement this expression, as it follows from the expression above.
                return false;
            }

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (rxs.IsZero() && !qpxr.IsZero())
                return false;

            // t = (q - p) x s / (r x s)
            var t = (q - p).Cross(s) / rxs;

            // u = (q - p) x r / (r x s)

            var u = (q - p).Cross(r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if (!rxs.IsZero() && (0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                // We can calculate the intersection point using either t or u.
                intersection = p + t * r;

                // An intersection was found.
                return true;
            }

            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }
    }
    public class Vector2D
    {
        public double X;
        public double Y;
        public double R
        {
        get { return Math.Sqrt(X * X + Y * Y); } }
        // Constructors.
        public Vector2D(double x, double y) { X = x; Y = y; }
        public Vector2D() : this(double.NaN, double.NaN) { }

        public static Vector2D operator -(Vector2D v, Vector2D w)
        {
            return new Vector2D(v.X - w.X, v.Y - w.Y);
        }

        public static Vector2D operator +(Vector2D v, Vector2D w)
        {
            return new Vector2D(v.X + w.X, v.Y + w.Y);
        }

        public static double operator *(Vector2D v, Vector2D w)
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static Vector2D operator *(Vector2D v, double mult)
        {
            return new Vector2D(v.X * mult, v.Y * mult);
        }

        public static Vector2D operator *(double mult, Vector2D v)
        {
            return new Vector2D(v.X * mult, v.Y * mult);
        }

        public double Cross(Vector2D v)
        {
            return X * v.Y - Y * v.X;
        }

        public override bool Equals(object obj)
        {
            var v = (Vector2D)obj;
            return (X - v.X).IsZero() && (Y - v.Y).IsZero();
        }
    }
    public static class Extensions
    {
        private const double Epsilon = 1e-10;

        public static bool IsZero(this double d)
        {
            return Math.Abs(d) < Epsilon;
        }
    }
    public class RobotLinkElement
    {
        public double Length { get; set; }
        public Model3DGroup   Model { get; protected set; }
        public static implicit operator Model3DGroup(RobotLinkElement element)
        {
            return element.Model;
        }
    }
    public class RectangleToCircleExtrudeLink : RobotLinkElement
    {
        public RectangleToCircleExtrudeLink(double Width, double Height, double Radius, double extrudeLength, System.Windows.Media.Color color)
        {
            Length = extrudeLength;
            Model.Children.Add(simCalc.meshToGeometry(simCalc.RectangleToCircle(Width, Height, Radius, extrudeLength), new System.Windows.Media.SolidColorBrush(color)));
            
        }
    }
    public class CircleToRectangleExtrudeLink : RobotLinkElement
    {
        public CircleToRectangleExtrudeLink(double Width, double Height, double Radius, double extrudeLength, System.Windows.Media.Color color)
        {
            Length = extrudeLength;
            var m = simCalc.meshToGeometry(simCalc.RectangleToCircle(Width, Height, Radius, extrudeLength), new System.Windows.Media.SolidColorBrush(color));
            Transform3DGroup tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180)));
            tg.Children.Add(new TranslateTransform3D(0, 0, -Length));
            m.Transform = tg;
            Model.Children.Add(m);
            

        }
    }

}
