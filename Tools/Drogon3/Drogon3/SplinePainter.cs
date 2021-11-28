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
                var rasterizedSpline = ink.ImportSplines(splineSet, 0.001, (f) => UpdateProgress(f));
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
    }
}
