
namespace RotatingBezierSplineEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.combinedModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inkOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xYAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.backgroundImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anchorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.centerPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.curvatureHandlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotationHandlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addAnchorWithLeftClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.bezierBoard1 = new RotatingBezierSplineEditor.BezierBoard();
            this.bothSplinesP = new RotatingBezierSplineEditor.ToolControl();
            this.linearSplineOnly = new RotatingBezierSplineEditor.ToolControl();
            this.rotatingSplineOnlyP = new RotatingBezierSplineEditor.ToolControl();
            this.centerP = new RotatingBezierSplineEditor.AnchorEditToolControl();
            this.rotationHandleP = new RotatingBezierSplineEditor.AnchorEditToolControl();
            this.curvatureHandlesP = new RotatingBezierSplineEditor.AnchorEditToolControl();
            this.addAnchorWithLeftClickToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromCipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.centerP);
            this.panel1.Controls.Add(this.rotationHandleP);
            this.panel1.Controls.Add(this.curvatureHandlesP);
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 241);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bothSplinesP);
            this.panel2.Controls.Add(this.linearSplineOnly);
            this.panel2.Controls.Add(this.rotatingSplineOnlyP);
            this.panel2.Location = new System.Drawing.Point(0, 335);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 241);
            this.panel2.TabIndex = 1;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(102, 28);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.viewToolStripMenuItem,
            this.editingToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1046, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.addToolStripMenuItem,
            this.clearAllToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.splinesToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.addToolStripMenuItem.Text = "Import";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // splinesToolStripMenuItem
            // 
            this.splinesToolStripMenuItem.Name = "splinesToolStripMenuItem";
            this.splinesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.splinesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.splinesToolStripMenuItem.Text = "Spline";
            this.splinesToolStripMenuItem.Click += new System.EventHandler(this.splinesToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromCipboardToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayModeToolStripMenuItem,
            this.toolStripSeparator2,
            this.combinedModeToolStripMenuItem,
            this.inkOnlyToolStripMenuItem,
            this.splineOnlyToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripSeparator1,
            this.gridToolStripMenuItem,
            this.scaleToolStripMenuItem,
            this.xYAxisToolStripMenuItem,
            this.toolStripSeparator6,
            this.toolStripSeparator5,
            this.backgroundImagesToolStripMenuItem,
            this.addAnchorWithLeftClickToolStripMenuItem1});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // displayModeToolStripMenuItem
            // 
            this.displayModeToolStripMenuItem.Enabled = false;
            this.displayModeToolStripMenuItem.Name = "displayModeToolStripMenuItem";
            this.displayModeToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.displayModeToolStripMenuItem.Text = "Spline display";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(318, 6);
            // 
            // combinedModeToolStripMenuItem
            // 
            this.combinedModeToolStripMenuItem.Checked = true;
            this.combinedModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.combinedModeToolStripMenuItem.Name = "combinedModeToolStripMenuItem";
            this.combinedModeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.combinedModeToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.combinedModeToolStripMenuItem.Text = "Combined Mode";
            this.combinedModeToolStripMenuItem.Click += new System.EventHandler(this.combinedModeToolStripMenuItem_Click);
            // 
            // inkOnlyToolStripMenuItem
            // 
            this.inkOnlyToolStripMenuItem.Name = "inkOnlyToolStripMenuItem";
            this.inkOnlyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.inkOnlyToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.inkOnlyToolStripMenuItem.Text = "Ink Only";
            this.inkOnlyToolStripMenuItem.Click += new System.EventHandler(this.inkOnlyToolStripMenuItem_Click);
            // 
            // splineOnlyToolStripMenuItem
            // 
            this.splineOnlyToolStripMenuItem.Name = "splineOnlyToolStripMenuItem";
            this.splineOnlyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.splineOnlyToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.splineOnlyToolStripMenuItem.Text = "Spline Only";
            this.splineOnlyToolStripMenuItem.Click += new System.EventHandler(this.splineOnlyToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(318, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(318, 6);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Checked = true;
            this.gridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.Checked = true;
            this.scaleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.scaleToolStripMenuItem.Text = "Scale";
            this.scaleToolStripMenuItem.Click += new System.EventHandler(this.scaleToolStripMenuItem_Click);
            // 
            // xYAxisToolStripMenuItem
            // 
            this.xYAxisToolStripMenuItem.Checked = true;
            this.xYAxisToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xYAxisToolStripMenuItem.Name = "xYAxisToolStripMenuItem";
            this.xYAxisToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.xYAxisToolStripMenuItem.Text = "X-Y Axis";
            this.xYAxisToolStripMenuItem.Click += new System.EventHandler(this.xYAxisToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(318, 6);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(318, 6);
            // 
            // backgroundImagesToolStripMenuItem
            // 
            this.backgroundImagesToolStripMenuItem.Checked = true;
            this.backgroundImagesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backgroundImagesToolStripMenuItem.Name = "backgroundImagesToolStripMenuItem";
            this.backgroundImagesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
            this.backgroundImagesToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.backgroundImagesToolStripMenuItem.Text = "Background Images";
            this.backgroundImagesToolStripMenuItem.Click += new System.EventHandler(this.backgroundImagesToolStripMenuItem_Click);
            // 
            // editingToolStripMenuItem
            // 
            this.editingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anchorsToolStripMenuItem,
            this.toolStripSeparator4,
            this.centerPointsToolStripMenuItem,
            this.curvatureHandlesToolStripMenuItem,
            this.rotationHandlesToolStripMenuItem,
            this.toolStripSeparator7,
            this.toolStripSeparator8,
            this.addAnchorWithLeftClickToolStripMenuItem});
            this.editingToolStripMenuItem.Name = "editingToolStripMenuItem";
            this.editingToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editingToolStripMenuItem.Text = "Edit";
            // 
            // anchorsToolStripMenuItem
            // 
            this.anchorsToolStripMenuItem.Enabled = false;
            this.anchorsToolStripMenuItem.Name = "anchorsToolStripMenuItem";
            this.anchorsToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.anchorsToolStripMenuItem.Text = "Handles";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(318, 6);
            // 
            // centerPointsToolStripMenuItem
            // 
            this.centerPointsToolStripMenuItem.Name = "centerPointsToolStripMenuItem";
            this.centerPointsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.centerPointsToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.centerPointsToolStripMenuItem.Text = "Center points";
            this.centerPointsToolStripMenuItem.Click += new System.EventHandler(this.centerPointsToolStripMenuItem_Click);
            // 
            // curvatureHandlesToolStripMenuItem
            // 
            this.curvatureHandlesToolStripMenuItem.Name = "curvatureHandlesToolStripMenuItem";
            this.curvatureHandlesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.curvatureHandlesToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.curvatureHandlesToolStripMenuItem.Text = "Curvature Handles";
            this.curvatureHandlesToolStripMenuItem.Click += new System.EventHandler(this.curvatureHandlesToolStripMenuItem_Click);
            // 
            // rotationHandlesToolStripMenuItem
            // 
            this.rotationHandlesToolStripMenuItem.Name = "rotationHandlesToolStripMenuItem";
            this.rotationHandlesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.rotationHandlesToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.rotationHandlesToolStripMenuItem.Text = "Rotation Handles";
            this.rotationHandlesToolStripMenuItem.Click += new System.EventHandler(this.rotationHandlesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "How to";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // addAnchorWithLeftClickToolStripMenuItem
            // 
            this.addAnchorWithLeftClickToolStripMenuItem.Checked = true;
            this.addAnchorWithLeftClickToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addAnchorWithLeftClickToolStripMenuItem.Name = "addAnchorWithLeftClickToolStripMenuItem";
            this.addAnchorWithLeftClickToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.addAnchorWithLeftClickToolStripMenuItem.Size = new System.Drawing.Size(321, 26);
            this.addAnchorWithLeftClickToolStripMenuItem.Text = "Add Anchor With Left Click";
            this.addAnchorWithLeftClickToolStripMenuItem.Click += new System.EventHandler(this.addAnchorWithLeftClickToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(318, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(318, 6);
            // 
            // bezierBoard1
            // 
            this.bezierBoard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bezierBoard1.AnchorDrawMode = RotatingBezierSplineEditor.AnchorDrawMode.Centers;
            this.bezierBoard1.GridEnabled = true;
            this.bezierBoard1.InkDrawMode = RotatingBezierSplineEditor.InkDrawMode.All;
            this.bezierBoard1.Location = new System.Drawing.Point(86, 33);
            this.bezierBoard1.Name = "bezierBoard1";
            this.bezierBoard1.ScaleEnabled = true;
            this.bezierBoard1.Size = new System.Drawing.Size(948, 653);
            this.bezierBoard1.TabIndex = 2;
            this.bezierBoard1.XYLinesEnabled = true;
            this.bezierBoard1.Paint += new System.Windows.Forms.PaintEventHandler(this.bezierBoard1_Paint);
            // 
            // bothSplinesP
            // 
            this.bothSplinesP.Active = true;
            this.bothSplinesP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bothSplinesP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bothSplinesP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bothSplinesP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bothSplinesP.DistinctSelection = true;
            this.bothSplinesP.Location = new System.Drawing.Point(0, 0);
            this.bothSplinesP.Name = "bothSplinesP";
            this.bothSplinesP.Size = new System.Drawing.Size(80, 81);
            this.bothSplinesP.TabIndex = 0;
            this.toolTip1.SetToolTip(this.bothSplinesP, "Combined: Show both the spline and the ink");
            // 
            // linearSplineOnly
            // 
            this.linearSplineOnly.Active = false;
            this.linearSplineOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linearSplineOnly.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.linearSplineOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linearSplineOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linearSplineOnly.DistinctSelection = true;
            this.linearSplineOnly.Location = new System.Drawing.Point(0, 160);
            this.linearSplineOnly.Name = "linearSplineOnly";
            this.linearSplineOnly.Size = new System.Drawing.Size(80, 81);
            this.linearSplineOnly.TabIndex = 0;
            this.toolTip1.SetToolTip(this.linearSplineOnly, "Spline Only: Shows the spline alongwitht th eselected handles and hide the ink ma" +
        "rk");
            // 
            // rotatingSplineOnlyP
            // 
            this.rotatingSplineOnlyP.Active = false;
            this.rotatingSplineOnlyP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rotatingSplineOnlyP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rotatingSplineOnlyP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rotatingSplineOnlyP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rotatingSplineOnlyP.DistinctSelection = true;
            this.rotatingSplineOnlyP.Location = new System.Drawing.Point(0, 80);
            this.rotatingSplineOnlyP.Name = "rotatingSplineOnlyP";
            this.rotatingSplineOnlyP.Size = new System.Drawing.Size(80, 81);
            this.rotatingSplineOnlyP.TabIndex = 0;
            this.toolTip1.SetToolTip(this.rotatingSplineOnlyP, "Ink only: Shows only the Ink marks. Editing handles will be disabled");
            // 
            // centerP
            // 
            this.centerP.Active = true;
            this.centerP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.centerP.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.Center;
            this.centerP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.centerP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.centerP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.centerP.DistinctSelection = true;
            this.centerP.Location = new System.Drawing.Point(0, 0);
            this.centerP.Name = "centerP";
            this.centerP.Size = new System.Drawing.Size(80, 81);
            this.centerP.TabIndex = 0;
            this.centerP.TargetPart = RotatingBezierSplineEditor.AnchorDrawMode.None;
            this.toolTip1.SetToolTip(this.centerP, "Show/Hide anchor center points");
            // 
            // rotationHandleP
            // 
            this.rotationHandleP.Active = false;
            this.rotationHandleP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rotationHandleP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rotationHandleP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rotationHandleP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rotationHandleP.DistinctSelection = true;
            this.rotationHandleP.Location = new System.Drawing.Point(0, 160);
            this.rotationHandleP.Name = "rotationHandleP";
            this.rotationHandleP.Size = new System.Drawing.Size(80, 81);
            this.rotationHandleP.TabIndex = 0;
            this.rotationHandleP.TargetPart = RotatingBezierSplineEditor.AnchorDrawMode.None;
            this.toolTip1.SetToolTip(this.rotationHandleP, "Show/hide rotation handles");
            // 
            // curvatureHandlesP
            // 
            this.curvatureHandlesP.Active = false;
            this.curvatureHandlesP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.curvatureHandlesP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.curvatureHandlesP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curvatureHandlesP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.curvatureHandlesP.DistinctSelection = true;
            this.curvatureHandlesP.Location = new System.Drawing.Point(0, 80);
            this.curvatureHandlesP.Name = "curvatureHandlesP";
            this.curvatureHandlesP.Size = new System.Drawing.Size(80, 81);
            this.curvatureHandlesP.TabIndex = 0;
            this.curvatureHandlesP.TargetPart = RotatingBezierSplineEditor.AnchorDrawMode.None;
            this.toolTip1.SetToolTip(this.curvatureHandlesP, "Show/hide spline curvature handles");
            // 
            // addAnchorWithLeftClickToolStripMenuItem1
            // 
            this.addAnchorWithLeftClickToolStripMenuItem1.Checked = true;
            this.addAnchorWithLeftClickToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addAnchorWithLeftClickToolStripMenuItem1.Name = "addAnchorWithLeftClickToolStripMenuItem1";
            this.addAnchorWithLeftClickToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.addAnchorWithLeftClickToolStripMenuItem1.Size = new System.Drawing.Size(321, 26);
            this.addAnchorWithLeftClickToolStripMenuItem1.Text = "Add Anchor With Left Click";
            this.addAnchorWithLeftClickToolStripMenuItem1.Click += new System.EventHandler(this.addAnchorWithLeftClickToolStripMenuItem_Click);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // fromCipboardToolStripMenuItem
            // 
            this.fromCipboardToolStripMenuItem.Name = "fromCipboardToolStripMenuItem";
            this.fromCipboardToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.fromCipboardToolStripMenuItem.Text = "Paste Cipboard";
            this.fromCipboardToolStripMenuItem.Click += new System.EventHandler(this.fromCipboardToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 698);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.bezierBoard1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Rotating Bezier Spline Editor";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AnchorEditToolControl centerP;
        private AnchorEditToolControl curvatureHandlesP;
        private AnchorEditToolControl rotationHandleP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ToolControl bothSplinesP;
        private ToolControl linearSplineOnly;
        private ToolControl rotatingSplineOnlyP;
        private BezierBoard bezierBoard1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem splineOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inkOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem combinedModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anchorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem centerPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem curvatureHandlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotationHandlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xYAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem backgroundImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAnchorWithLeftClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem addAnchorWithLeftClickToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromCipboardToolStripMenuItem;
    }
}

