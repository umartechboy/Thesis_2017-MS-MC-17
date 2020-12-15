using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            // set images
            centerP.SetImage(Image.FromFile("Center.png"), 65); 
            curvatureHandlesP.SetImage(Image.FromFile("CurvatureHandles.png"), 65);
            rotationHandleP.SetImage(Image.FromFile("RotationHandles.png"), 56);
            centerP.DistinctSelection = false;
            curvatureHandlesP.DistinctSelection = false;
            rotationHandleP.DistinctSelection = false;

            // set targets
            centerP.TargetPart = AnchorDrawMode.Centers;
            curvatureHandlesP.TargetPart = AnchorDrawMode.CurvatureHandles;
            rotationHandleP.TargetPart = AnchorDrawMode.RotaionHandles;

            void setEditingModeMenuItems()
            {
                rotationHandlesToolStripMenuItem.Checked = rotationHandleP.Active;
                curvatureHandlesToolStripMenuItem.Checked = curvatureHandlesP.Active;
                centerPointsToolStripMenuItem.Checked = centerP.Active;
            };
            centerP.OnActivated += (s, e) =>
            {
                if (centerP.Active)
                    bezierBoard1.AnchorDrawMode |= AnchorDrawMode.Centers;
                else
                    bezierBoard1.AnchorDrawMode &= ~AnchorDrawMode.Centers;
                setEditingModeMenuItems();
            };
            curvatureHandlesP.OnActivated += (s, e) =>
            {
                if (curvatureHandlesP.Active)
                    bezierBoard1.AnchorDrawMode |= AnchorDrawMode.CurvatureHandles;
                else
                    bezierBoard1.AnchorDrawMode &= ~AnchorDrawMode.CurvatureHandles;
                setEditingModeMenuItems();
            };
            rotationHandleP.OnActivated += (s, e) =>
            {
                if (rotationHandleP.Active)
                    bezierBoard1.AnchorDrawMode |= AnchorDrawMode.RotaionHandles;
                else
                    bezierBoard1.AnchorDrawMode &= ~AnchorDrawMode.RotaionHandles;
                setEditingModeMenuItems();
            };
            curvatureHandlesP.Active = true;

            void setDisplayStyleMenuItems()
            {
                splineOnlyToolStripMenuItem.Checked = linearSplineOnly.Active;
                combinedModeToolStripMenuItem.Checked = bothSplinesP.Active;
                inkOnlyToolStripMenuItem.Checked = rotatingSplineOnlyP.Active;
            }
            bothSplinesP.OnActivated += (s, e) => setDisplayStyleMenuItems();
            rotatingSplineOnlyP.OnActivated += (s, e) => setDisplayStyleMenuItems();
            linearSplineOnly.OnActivated += (s, e) => setDisplayStyleMenuItems();

            bothSplinesP.SetImage(Image.FromFile("bothSplines.png"), 45);
            rotatingSplineOnlyP.SetImage(Image.FromFile("rotatingSplineOnly.png"), 45);
            linearSplineOnly.SetImage(Image.FromFile("splineOnly.png"), 45);

            bothSplinesP.OnActivated += (s, e) => { bezierBoard1.InkDrawMode = InkDrawMode.Ink | InkDrawMode.Spline;};
            rotatingSplineOnlyP.OnActivated += (s, e) => { bezierBoard1.InkDrawMode = InkDrawMode.Ink;};
            linearSplineOnly.OnActivated += (s, e) => { bezierBoard1.InkDrawMode = InkDrawMode.Spline; };



            var sp = new RotatingBezierSpline();
            var a0 = new RotatingBezierSplineAnchor(new PointF(100,100), new PointF(150, 150), 50.0D, 0.0F);
            var a1 = new RotatingBezierSplineAnchor(new PointF(300, 100), new PointF(350, 50), 50.0D, 0.0F);
            var a2 = new RotatingBezierSplineAnchor(new PointF(300, 300), new PointF(300, 350), 50.0D, 0.0F);
            sp.Thickness = 40;
            sp.AddAnchor(a0); 
            sp.AddAnchor(a1); 
            sp.AddAnchor(a2);
            bezierBoard1.AddItem(sp);

            sp = new RotatingBezierSpline();
            a0 = new RotatingBezierSplineAnchor(new PointF(400, 300), new PointF(350, 350), 50.0D, 0.0F);
            a1 = new RotatingBezierSplineAnchor(new PointF(600, 100), new PointF(450, 50), 50.0D, 0.0F);
            a2 = new RotatingBezierSplineAnchor(new PointF(600, 300), new PointF(400, 350), 50.0D, 0.0F);
            sp.Thickness = 40;
            sp.AddAnchor(a0);
            sp.AddAnchor(a1);
            sp.AddAnchor(a2);
            bezierBoard1.AddItem(sp);
        }

        private void bezierBoard1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splineOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linearSplineOnly.Active = true;
        }

        private void inkOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotatingSplineOnlyP.Active = true;
        }

        private void combinedModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bothSplinesP.Active = true;
        }

        private void centerPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerP.Active = !centerP.Active;
            centerPointsToolStripMenuItem.Checked = centerP.Active;
        }

        private void curvatureHandlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curvatureHandlesP.Active = !curvatureHandlesP.Active;
            curvatureHandlesToolStripMenuItem.Checked = curvatureHandlesP.Active;
        }

        private void rotationHandlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotationHandleP.Active = !rotationHandleP.Active;
            rotationHandlesToolStripMenuItem.Checked = rotationHandleP.Active;
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bezierBoard1.GridEnabled = !bezierBoard1.GridEnabled;
            gridToolStripMenuItem.Checked = bezierBoard1.GridEnabled;
            // also disable the axis selection. There is no point having it if the whole grid is disabled.
            xYAxisToolStripMenuItem.Enabled = gridToolStripMenuItem.Checked;
            xYAxisToolStripMenuItem.Checked = bezierBoard1.XYLinesEnabled;
        }

        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bezierBoard1.ScaleEnabled = !bezierBoard1.ScaleEnabled;
            scaleToolStripMenuItem.Checked = bezierBoard1.ScaleEnabled;
        }

        private void xYAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bezierBoard1.XYLinesEnabled = !bezierBoard1.XYLinesEnabled;
            xYAxisToolStripMenuItem.Checked = bezierBoard1.XYLinesEnabled;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://github.com/umartechboy/Thesis_2017-MS-MC-17");
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v2.0\r\nA minimalist editor and machine data generator for rotating beizier splines.");
        }
    }
}
