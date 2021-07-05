using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using Physics;

namespace RoboSim
{
    public class WorkSpace
    {                                        
        protected Model3DGroup gm = new Model3DGroup();
        public Model3DGroup Model
        {
            get { return gm; }
        }
        public double XStart, XRange, YStart, YRange, ZStart, ZRange;
        double res = 0;
        public void SetMap(Point3D point, double factor)
        {   
            int indX = (int)Math.Round((point.X - XStart) / res);
            int indY = (int)Math.Round((point.Y - YStart) / res);
            int indZ = (int)Math.Round((point.Z - ZStart) / res);
            if (indX < 0 || indX >= Map.GetLength(0))
                return;
            if (indY < 0 || indY >= Map.GetLength(1))
                return;
            if (indZ < 0 || indZ >= Map.GetLength(2))
                return;
            Map[indX, indY, indZ] = factor == 1;

            var cube = simCalc.meshToGeometry(simCalc.CubeMesh(res, res, res), System.Windows.Media.Color.FromArgb(
                100, 
                (byte)(Math.Round((1 - factor) * 255)), 
                (byte)(Math.Round(factor * 255)),
                0
                ));
            cube.Transform = new TranslateTransform3D(point.X, point.Y, point.Z);

            gm.Children.Add(cube);
        }
        public void GenerateMap(double resoltion, double xStart, double xRange, double yStart, double yRange, double zStart, double zRange)
        {
            res = resoltion;

            XStart = xStart;
            YStart = yStart;
            ZStart = zStart;

            XRange = xRange;
            YRange = yRange;
            ZRange = zRange;


            Map = new bool[
                (int)Math.Round(XRange / resoltion),
                (int)Math.Round(YRange / resoltion),
                (int)Math.Round(ZRange / resoltion)];
            gm.Children.Clear();    
        }
        public bool[,,] Map;
    }
}
