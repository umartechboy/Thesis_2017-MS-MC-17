﻿using System;
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
        Image visibleIconDull, activeIconDull;
        Image visibleIcon, activeIcon;
        public MainForm()
        {
            InitializeComponent();
            var tc = new ToolControl();
            visibleIcon = Image.FromFile("Resources\\visible.png");
            activeIcon = Image.FromFile("Resources\\active.png");
            visibleIconDull = tc.SetImage(visibleIcon, 23);
            activeIconDull = tc.SetImage(activeIcon, 23); ;
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


            bezierBoard1.OnSplineAdded += BezierBoard1_OnSplineAdded;
            bezierBoard1.OnSplineRemoved += BezierBoard1_OnSplineRemoved;
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

        private void BezierBoard1_OnSplineRemoved(object sender, BezierBoard.SplineAddedEventArgs e)
        {
            documentLayoutFP.Controls.Remove(documentLayoutFP.Controls.OfType<CurveMenuItem>().ToList().Find(ci => ci.Spline == e.Spline));
        }

        private void BezierBoard1_OnSplineAdded(object sender, BezierBoard.SplineAddedEventArgs e)
        {
            var st = DateTime.Now;
            var con = new CurveMenuItem(e.Spline, visibleIcon, activeIcon, visibleIconDull, activeIconDull, 23);
            var s1 = DateTime.Now - st;
            documentLayoutFP.Controls.Add(con);
            var s2 = DateTime.Now - st;
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
                sfd.FileName = ofd.FileName;
                documentLayoutFP.Controls.Clear();
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
                //documentLayoutFP.Controls.Clear();
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
            documentLayoutFP.Controls.Clear();
            bezierBoard1.ClearObjects();
        }

        public static RotatingBezierSpline[] GetSpline()
        {
            var form = new MainForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
            return form.bezierBoard1.GetSplineObjects();
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

        private void curveMenuItem1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void documentLayout_ControlAdded(object sender, ControlEventArgs e)
        {
        }

        private void previewRefreshTimerT_Tick(object sender, EventArgs e)
        {
            foreach (var ci in documentLayoutFP.Controls.OfType<CurveMenuItem>())
            {
                if (ci.NeedsToRedrawPreview)
                {
                    ci.RecomputePreview();
                    ci.NeedsToRedrawPreview = false;
                }
            }
        }

        private void autoSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoSaverT.Enabled = autoSaveToolStripMenuItem.Checked;
        }

        void ProcessRenderRequest(ExportRequest Request)
        {
            var splines = bezierBoard1.GetSplineObjects().ToList();
            var images = bezierBoard1.GetImageObjects().ToList();
            if (splines.Count == 0)
                return;
            var minX = splines.Min(spline => spline.BoundingRectangle().Left);
            var maxX = splines.Max(spline => spline.BoundingRectangle().Right);
            var minY = splines.Min(spline => spline.BoundingRectangle().Top);
            var maxY = splines.Max(spline => spline.BoundingRectangle().Bottom);
            float scale = Request.DPI / 1000.0F;
            var rect = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            var bmp = new Bitmap((int)(rect.Width * scale), (int)(rect.Height * scale));
            var g = Graphics.FromImage(bmp);
            g.ScaleTransform(1, -1);
            g.TranslateTransform(0, -bmp.Height);
            g.TranslateTransform(-rect.X * scale, -rect.Y * scale);
            g.ScaleTransform(scale, scale);
            var scBkp = BezierBoard.ForceSingleColorSplines;
            var colBkp = BezierBoard.ForcedInkColor;
            BezierBoard.ForcedInkColor = Request.ForceColor;
            BezierBoard.ForceSingleColorSplines = Request.ForceSingleColor;
            if (Request.RenderImages)
            {
                foreach (var image in images)
                    image.Draw(g, InkDrawMode.Images, Request.AnchorMode, null, null);
            }
            if (!Request.DontRenderSplines)
            {
                foreach (var Spline in splines)
                {
                    var bkp = BezierBoard.FlatTipRenderAlgorithm;
                    BezierBoard.FlatTipRenderAlgorithm = Request.RenderAlgorithm;
                    float widBkp = Spline.FlatTipWidth;
                    if (Spline.FlatTipWidth < 2)
                        Spline.FlatTipWidth = 2;
                    Spline.Draw(g, Request.DrawMode, Request.AnchorMode, null, null);
                    Spline.FlatTipWidth = widBkp;
                    BezierBoard.FlatTipRenderAlgorithm = bkp;
                }
            }
            Request.RenderOutput = bmp;
            BezierBoard.ForcedInkColor = colBkp;
            BezierBoard.ForceSingleColorSplines = scBkp;
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var em = new ExportMenu();
            em.OnExportRequest += (s, e2) =>
            {
                ProcessRenderRequest(e2.Request);
            };
            em.ShowDialog();
        }

        private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BezierBoard.FlatTipRenderAlgorithm = FlatTipRenderAlgorithm.Polygon;
            polygonToolStripMenuItem.Checked = true;
            rectanglesToolStripMenuItem.Checked = false;
            bezierBoard1.Invalidate();
            Application.DoEvents();
        }

        private void rectanglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BezierBoard.FlatTipRenderAlgorithm = FlatTipRenderAlgorithm.Rectangle;
            polygonToolStripMenuItem.Checked = false;
            rectanglesToolStripMenuItem.Checked = true;
            bezierBoard1.Invalidate();
            Application.DoEvents();
        }

        private void analyzeTraceAccuracyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var taf = new TraceAnalyzer();
            taf.OnExportRequest += (s, e2) => { ProcessRenderRequest(e2.Request); };
            taf.ShowDialog();
        }

        private void autoSaverT_Tick(object sender, EventArgs e)
        {
            if (!bezierBoard1.MayHaveUnsavedChanges)
                return;
            bezierBoard1.MayHaveUnsavedChanges = false;
            var fNameSeed = "Untitled.xml";
            if (File.Exists(sfd.FileName))
                fNameSeed = sfd.FileName;
            var fname = Path.GetFileNameWithoutExtension(fNameSeed) + " " + DateTime.Now.ToString("MM-dd hh.mm.ss");
            var dir = Path.Combine(Path.GetDirectoryName(fNameSeed), Path.GetFileNameWithoutExtension(fNameSeed) + "_Autosave");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            fname = Path.Combine(dir, fname + Path.GetExtension(fNameSeed));
            bezierBoard1.SaveObjects(fname);
        }

        private void autoSaveDurationChange_Click(object sender, EventArgs e)
        {
            var dur = int.Parse(((ToolStripMenuItem)sender).Text.Split(new char[] { ' ' })[0]);
            if (((ToolStripMenuItem)sender).Text.Split(new char[] { ' ' })[1].ToLower().StartsWith("m"))
                dur *= 60;
            autoSaverT.Interval = dur * 1000;
        }
    }
}
