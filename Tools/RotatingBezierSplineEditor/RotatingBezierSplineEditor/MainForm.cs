using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // set images
            centerP.SetImage(Image.FromFile("Resources\\Center.png"), 65); 
            curvatureHandlesP.SetImage(Image.FromFile("Resources\\CurvatureHandles.png"), 65);
            rotationHandleP.SetImage(Image.FromFile("Resources\\RotationHandles.png"), 56);
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

            bothSplinesP.SetImage(Image.FromFile("Resources\\bothSplines.png"), 45);
            rotatingSplineOnlyP.SetImage(Image.FromFile("Resources\\rotatingSplineOnly.png"), 45);
            linearSplineOnly.SetImage(Image.FromFile("Resources\\splineOnly.png"), 45);

            bothSplinesP.OnActivated += (s, e) => bezierBoard1.InkDrawMode = InkDrawMode.Ink | InkDrawMode.Spline | (bezierBoard1.InkDrawMode & InkDrawMode.Images);
            rotatingSplineOnlyP.OnActivated += (s, e) => bezierBoard1.InkDrawMode = InkDrawMode.Ink | (bezierBoard1.InkDrawMode & InkDrawMode.Images);
            linearSplineOnly.OnActivated += (s, e) => bezierBoard1.InkDrawMode = InkDrawMode.Spline | (bezierBoard1.InkDrawMode & InkDrawMode.Images);


            //// add dummy data
            //var sp = new RotatingBezierSpline();
            //sp.AddAnchor(new RotatingBezierSplineAnchor(new PointF(34.5F, -2.75F)));
            //sp.AddAnchor(new RotatingBezierSplineAnchor(new PointF(3.6F, -101)));
            //sp.AddAnchor(new RotatingBezierSplineAnchor(new PointF(110, -28)));
            //sp.AddAnchor(new RotatingBezierSplineAnchor(new PointF(24.5F, -209)));
            //sp.AddAnchor(new RotatingBezierSplineAnchor(new PointF(203, -311)));
            //sp.AddAnchor(new RotatingBezierSplineAnchor(new PointF(242, -194)));
            //sp.FlatTipWidth = 40;
            //bezierBoard1.AddItem(sp);
            //bezierBoard1.AddItem(new ImageItem(Properties.Resources.Ain_for_sample_trace, 0, 0));

            //sp = new RotatingBezierSpline();
            //var a0 = new RotatingBezierSplineAnchor(new PointF(400, 300), new PointF(350, 350), 50.0D, 0.0F);
            //var a1 = new RotatingBezierSplineAnchor(new PointF(600, 100), new PointF(450, 50), 50.0D, 0.0F);
            //var a2 = new RotatingBezierSplineAnchor(new PointF(600, 300), new PointF(400, 350), 50.0D, 0.0F);
            //sp.FlatTipWidth = 40;
            //sp.AddAnchor(a0);
            //sp.AddAnchor(a1);
            //sp.AddAnchor(a2);
            ////bezierBoard1.AddItem(sp);
        }

        private void bezierBoard1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splineOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linearSplineOnly.Active = true;
        }

        private void backgroundImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundImagesToolStripMenuItem.Checked = !backgroundImagesToolStripMenuItem.Checked;
            if (backgroundImagesToolStripMenuItem.Checked)
                bezierBoard1.InkDrawMode|= InkDrawMode.Images;
            else
                bezierBoard1.InkDrawMode  &= ~InkDrawMode.Images;
            bezierBoard1.Invalidate();
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
            MessageBox.Show(
@"Gregor v2.0
Gregor is a minimalist editor and machine data generator for rotating beizier splines.
To read more about rotating bezier splines, press F1 or view the program help page.

Editing:
Click and drag left mouse button on empty space to add spline anchors
Right click right mouse button to start a new spline
Drag with left mouse the anchors to modify a spline
Click a spline to change Pen Width
Drag and move objects with left mouse button

Viewing:
Use Ctrl + V to switch to pan mode. 
Drag with Left mouse button to pan
Drag with Right mouse button to zoom

File Handling:
Uou can save, open and import rotating bezier splines using the File menu
", "Gregor");
        }

        SaveFileDialog sfd = new SaveFileDialog();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfd.Filter = "XML files (*.xml)|*.xml";
            if (File.Exists(sfd.FileName))
                bezierBoard1.SaveObjects(sfd.FileName);
            else if (sfd.ShowDialog() == DialogResult.OK)
            {
                bezierBoard1.SaveObjects(sfd.FileName);
            }
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bezierBoard1.ClearObjects();
                bezierBoard1.ImportObjects(ofd.FileName);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void splinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //bezierBoard1.ClearObjects();
                bezierBoard1.ImportObjects(ofd.FileName);
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Image files (*.jpg, *.bmp, *.png)|*.png;*.jpg;*.bmp|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var item = bezierBoard1.AddItem(ImageItem.FromFile(ofd.FileName));
                bezierBoard1.ForceBeginDragItem(item);
            }
        }
        private void fromCipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = bezierBoard1.AddItem(ImageItem.FromClipBoard());
            bezierBoard1.ForceBeginDragItem(item);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void addAnchorWithLeftClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bezierBoard1.clickIsForAdding = addAnchorWithLeftClickToolStripMenuItem.Checked = !addAnchorWithLeftClickToolStripMenuItem.Checked;
            addAnchorWithLeftClickToolStripMenuItem1.Checked = addAnchorWithLeftClickToolStripMenuItem.Checked;
            bezierBoard1.DefaultCursor = addAnchorWithLeftClickToolStripMenuItem.Checked ? Cursors.Default: Cursors.SizeAll;
            bezierBoard1.Cursor = bezierBoard1.DefaultCursor;
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bezierBoard1.ClearObjects();
        }

        public static RotatingBezierSpline[] GetSpline()
        {
            var form = new MainForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
            return form.bezierBoard1.GetObjects();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in opacityToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            var clickedOn = (ToolStripMenuItem)sender;
            clickedOn.Checked = true;
            BezierBoard.Fill = float.Parse(clickedOn.Text.Replace("%", "")) / 100;
        }
    }
}
