using Physics;
using RoboSim;
using RotatingBezierSplineEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace Drogon3
{
    public partial class SplinePainter : UserControl
    {
        Ink ink;
        RoboSim.Robot Robot;
        public event ObjectShareHandler OnWritingPadOrientationChangeRequested;
        public EulerAngleOrientation WritingPadOrientation = new EulerAngleOrientation(0, 0, 0, 1, 1, 0);
        public SplinePainter(RoboSim.Robot robot = null, Ink ink = null)
        {
            this.Robot = robot;
            this.ink = ink;
            InitializeComponent();
        }

        List<RasterizedRotatingBezierSpline> ImportedSplines = new List<RasterizedRotatingBezierSpline>();
        private void importB_Click(object sender, EventArgs e)
        {
            var splineSets = BezierBoard.LoadFile();
            int ind = 0;
            var progress = new SplineRasterizationProgress();
            if (splineSets == null)
                return;
            progress.UnitsCount = splineSets.Count;
            var t = new Thread(() => { progress.ShowDialog(); });
            t.Start();
            bool UpdateProgress(float f)
            {
                progress.Update(ind, f);
                return true;
            }
            foreach (var splineSet in splineSets.OfType<RotatingBezierSpline>())
            {
                var rasterizedSpline = ink.ImportSplines(splineSet, 0.0002, (f) => UpdateProgress(f));
                if (rasterizedSpline == null)
                        continue;
                ImportedSplines.Add(rasterizedSpline);
                var thisCmi = rasterizedSpline.CurveMenuItem;
                void ShowAll()
                {
                    foreach (var item in ImportedSplines)
                    {
                        var color = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                        foreach (var model in item.RasterizedCells.Select(rc => rc.Model))
                            foreach (var strokePart in ((Model3DGroup)model).Children)
                                ((GeometryModel3D)strokePart).Material = new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(color));
                    }
                }
                void ShowThis()
                {
                    foreach (var item in ImportedSplines)
                    {
                        var color = System.Windows.Media.Color.FromArgb(255, 0, 0, 0);
                        if (item.Spline != splineSet)
                            color = System.Windows.Media.Color.FromArgb(60, 0, 0, 0);
                        foreach (var model in item.RasterizedCells.Select(rc => rc.Model))
                            foreach (var strokePart in ((Model3DGroup)model).Children)
                                ((GeometryModel3D)strokePart).Material = new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(color));
                    }
                }
                thisCmi.OnRequestToHiglight += (s_, e_) => ShowThis();
                thisCmi.OnRequestToUndoHighlight += (s_, e_) => ShowAll();
                thisCmi.RecomputePreview();
                thisCmi.Item.OnSelfRemoveRequest += (s_, e_) =>
                {
                    documentLayoutFP.Controls.Remove(thisCmi);
                    ink.RemoveSpline(rasterizedSpline);
                    ImportedSplines.Remove(rasterizedSpline);
                };
                thisCmi.OnRequestShowOnly += (s_, e_) => ShowThis();
                thisCmi.OnRequestToShowAll += (s_, e_) => ShowAll();
                ind++;
                documentLayoutFP.Controls.Add(thisCmi);
            }
            try
            {
                progress.Invoke(new MethodInvoker(() => { progress.Close(); }));
            }
            catch { }
        }

        private void joyStickReaderT_Tick(object sender, EventArgs e)
        {
                WritingPadOrientation.X += xyWPJS.DX / 100;
                WritingPadOrientation.Y += xyWPJS.DY / 100;
                WritingPadOrientation.Z += zJPJS.DY / 100;
                WritingPadOrientation.A += aGPJS.DY / 100;
                WritingPadOrientation.B += bGPJS.DY / 100;
                WritingPadOrientation.G += gGPJS.DY / 20;
                OnWritingPadOrientationChangeRequested(this, WritingPadOrientation);
                xyWPL.Text = WritingPadOrientation.X.ToString() + ", " + WritingPadOrientation.Y;
                zWPL.Text = WritingPadOrientation.Z.ToString();
                aWPL.Text = WritingPadOrientation.A.ToString();
                gWPL.Text = WritingPadOrientation.G.ToString();
                bWPL.Text = WritingPadOrientation.B.ToString();
        }

        private void genMachineCodeB_Click(object sender, EventArgs e)
        {
            commandsLB.Items.Clear();
            var origin = ink.InkOrientation.Clone();
            SphericalRobotSolution lastSol = null;
            Robot.Program = new MachineProgram();
            Robot.Program.Codes.Add(new ToolChangeCommand(0));
            foreach (var rSpline in ImportedSplines)
            {
                var startPos = new ChainTranformationMatrix();
                startPos.Children.Add(origin);
                startPos.Children.Add(rSpline.RasterizedCells.First().RasterizedData.First().Orientation);
                startPos.Children.Add(new RotateTransformationMatrix(Math.PI / 2, Axis.Z));
                startPos.Children.Add(new TranslateTransformationMatrix(0, 0, .1));

                Robot.Program.Codes.Add(new G90()
                {
                    Target = EulerAngleOrientation.FromTransformationMatrix(startPos)
                });

                bool firstInSpline = true;

                foreach (var cell in rSpline.RasterizedCells)
                {
                    if (cell.RasterizedData.Count <= 0)
                        continue;
                    foreach (var target in cell.RasterizedData)
                    {
                        var nextPos = new ChainTranformationMatrix();
                        nextPos.Children.Add(origin);
                        nextPos.Children.Add(target.Orientation);
                        nextPos.Children.Add(new RotateTransformationMatrix(Math.PI / 2, Axis.Z));
                        Robot.Program.Codes.Add(new G90()
                        {
                            Target = EulerAngleOrientation.FromTransformationMatrix(nextPos)
                        });
                        if (firstInSpline)
                            Robot.Program.Codes.Add(new ToolChangeCommand(rSpline.ToolWidth));
                        firstInSpline = false;
                    }
                    //cell.MachineCode = codes;
                }

                Robot.Program.Codes.Add(new ToolChangeCommand(0));
                var endPos = new ChainTranformationMatrix();
                endPos.Children.Add(origin);
                endPos.Children.Add(rSpline.RasterizedCells.Last().RasterizedData.Last().Orientation);
                endPos.Children.Add(new RotateTransformationMatrix(Math.PI / 2, Axis.Z));
                endPos.Children.Add(new TranslateTransformationMatrix(0, 0, .1));
                var t = EulerAngleOrientation.FromTransformationMatrix(endPos);
                Robot.Program.Codes.Add(new G90()
                {
                    Target = t
                });
            }
            commandsLB.Items.Clear();
            commandsLB.Items.AddRange(Robot.Program.Codes.ToArray());
            foreach (var code in Robot.Program.Codes.OfType<GCode>())
            {
                var sol = (SphericalRobotSolution)SphericalRobotSolution.Solution(lastSol, 0, Robot, ((G90)code).Target);
                lastSol = sol;
                if (!sol.IsValid())
                {
                    MessageBox.Show("Some of the points in the path are not achievable");
                    return;
                }
            }
            Robot.Program.Index = 0;
        }

        private void simulateSplineB_Click(object sender, EventArgs e)
        {
            List<List<TransformationMatrix>> SimulatedEndEffectorPostions = new List<List<TransformationMatrix>>();
            var SimulatedRasterizedDataTopProjection = new List<RasterizedRotatingBezierSpline>();

            ink.ClearTrace();
            var progress = new SplineRasterizationProgress();
            progress.UnitsCount = Robot.Program.Codes.Count;
            var t = new Thread(() => { progress.ShowDialog(); });
            t.Start();
            Robot.ControlSource = RobotControlSource.BezierSpline;
            double lastSetToolSize = 0;
            ink.PenColor = System.Windows.Media.Color.FromRgb(255, 0, 0);
            while (Robot.Program.UnderWorking != null)
            {
                progress.Update(Robot.Program.Index, 0);
                if (Robot.Program.UnderWorking is G90)
                {
                    if (lastSetToolSize != 0)
                    {
                        var rasterized = SimulatedRasterizedDataTopProjection.Last();
                        rasterized.RasterizedCells = new List<RasterizedSplineCell>();
                        Point3D p1 = new Point3D(-lastSetToolSize / 2, 0, 0);
                        Point3D p2 = new Point3D(+lastSetToolSize / 2, 0, 0);
                        p1 = Robot.CurrentEndEffectorTransformationMatrix * p1;
                        p2 = Robot.CurrentEndEffectorTransformationMatrix * p2;
                        var rp = new RasterizedPoint()
                        {
                            X = (p1.X + p2.X) / 2,
                            Y = (p1.Y + p2.Y) / 2,
                            T = Math.Atan2(p2.Y - p1.Y, p2.Y - p1.Y)
                        };
                        rasterized.RasterizedCells.Last().RasterizedData.Add(rp);
                        var ee = Robot.CurrentEndEffectorTransformationMatrix;
                        SimulatedEndEffectorPostions.Last().Add(ee);
                        ink.AppendTracePoint(ee, lastSetToolSize);
                    }
                }
                else
                {
                    if (Robot.Program.UnderWorking is ToolChangeCommand)
                        lastSetToolSize = ((ToolChangeCommand)Robot.Program.UnderWorking).ToolSize;
                    if (SimulatedEndEffectorPostions.Count == 0)
                    {
                        SimulatedRasterizedDataTopProjection.Add(new RasterizedRotatingBezierSpline());
                        SimulatedEndEffectorPostions.Add(new List<TransformationMatrix>());
                        ink.NewTraceChar();
                    }
                    else if (SimulatedEndEffectorPostions.Last().Count > 0)
                    {
                        SimulatedRasterizedDataTopProjection.Add(new RasterizedRotatingBezierSpline());
                        SimulatedEndEffectorPostions.Add(new List<TransformationMatrix>());
                        ink.NewTraceChar();
                    }
                }
                Robot.ThreadStep(.00001);
            }
            // we can now generate comparison images
            var target = RenderImages(ImportedSplines);
            var achievedd = RenderImages(SimulatedRasterizedDataTopProjection);
            progress.Invoke(new MethodInvoker(() => { progress.Close(); }));
        }

        private object RenderImages(List<RasterizedRotatingBezierSpline> splines, double dpi = 300)
        {
            // get the image bounds
            var minX = splines.Min(s => s.RasterizedCells.Min(rc => rc.RasterizedData.Min(rd => rd.X)));
            var maxX = splines.Max(s => s.RasterizedCells.Max(rc => rc.RasterizedData.Max(rd => rd.X)));
            var minY = splines.Min(s => s.RasterizedCells.Min(rc => rc.RasterizedData.Min(rd => rd.Y)));
            var maxY = splines.Max(s => s.RasterizedCells.Max(rc => rc.RasterizedData.Max(rd => rd.Y)));

            var bitmap = new Bitmap((int)((maxX - minX) * dpi), (int)((maxX - minX) * dpi));
            var g = Graphics.FromImage(bitmap);
            foreach (var spline in splines)
            {
                
            }

        }
    }
}
