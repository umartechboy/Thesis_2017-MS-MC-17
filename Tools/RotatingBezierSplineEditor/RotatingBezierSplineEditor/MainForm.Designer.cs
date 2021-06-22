
namespace RotatingBezierSplineEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.centerP = new RotatingBezierSplineEditor.AnchorEditToolControl();
            this.rotationHandleP = new RotatingBezierSplineEditor.AnchorEditToolControl();
            this.curvatureHandlesP = new RotatingBezierSplineEditor.AnchorEditToolControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bothSplinesP = new RotatingBezierSplineEditor.ToolControl();
            this.linearSplineOnly = new RotatingBezierSplineEditor.ToolControl();
            this.rotatingSplineOnlyP = new RotatingBezierSplineEditor.ToolControl();
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
            this.fromCipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.addAnchorWithLeftClickToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.opacityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.documentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anchorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.centerPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.curvatureHandlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotationHandlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.addAnchorWithLeftClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bezierBoard1 = new RotatingBezierSplineEditor.BezierBoard();
            this.documentLayoutFP = new System.Windows.Forms.FlowLayoutPanel();
            this.previewRefreshTimerT = new System.Windows.Forms.Timer(this.components);
            this.autoSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSaverT = new System.Windows.Forms.Timer(this.components);
            this.secondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.minuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.secondsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.secondsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.centerP);
            this.panel1.Controls.Add(this.rotationHandleP);
            this.panel1.Controls.Add(this.curvatureHandlesP);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(60, 196);
            this.panel1.TabIndex = 1;
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
            this.centerP.Margin = new System.Windows.Forms.Padding(2);
            this.centerP.Name = "centerP";
            this.centerP.Size = new System.Drawing.Size(60, 66);
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
            this.rotationHandleP.Location = new System.Drawing.Point(0, 130);
            this.rotationHandleP.Margin = new System.Windows.Forms.Padding(2);
            this.rotationHandleP.Name = "rotationHandleP";
            this.rotationHandleP.Size = new System.Drawing.Size(60, 66);
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
            this.curvatureHandlesP.Location = new System.Drawing.Point(0, 65);
            this.curvatureHandlesP.Margin = new System.Windows.Forms.Padding(2);
            this.curvatureHandlesP.Name = "curvatureHandlesP";
            this.curvatureHandlesP.Size = new System.Drawing.Size(60, 66);
            this.curvatureHandlesP.TabIndex = 0;
            this.curvatureHandlesP.TargetPart = RotatingBezierSplineEditor.AnchorDrawMode.None;
            this.toolTip1.SetToolTip(this.curvatureHandlesP, "Show/hide spline curvature handles");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bothSplinesP);
            this.panel2.Controls.Add(this.linearSplineOnly);
            this.panel2.Controls.Add(this.rotatingSplineOnlyP);
            this.panel2.Location = new System.Drawing.Point(0, 272);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(60, 196);
            this.panel2.TabIndex = 1;
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
            this.bothSplinesP.Margin = new System.Windows.Forms.Padding(2);
            this.bothSplinesP.Name = "bothSplinesP";
            this.bothSplinesP.Size = new System.Drawing.Size(60, 66);
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
            this.linearSplineOnly.Location = new System.Drawing.Point(0, 130);
            this.linearSplineOnly.Margin = new System.Windows.Forms.Padding(2);
            this.linearSplineOnly.Name = "linearSplineOnly";
            this.linearSplineOnly.Size = new System.Drawing.Size(60, 66);
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
            this.rotatingSplineOnlyP.Location = new System.Drawing.Point(0, 65);
            this.rotatingSplineOnlyP.Margin = new System.Windows.Forms.Padding(2);
            this.rotatingSplineOnlyP.Name = "rotatingSplineOnlyP";
            this.rotatingSplineOnlyP.Size = new System.Drawing.Size(60, 66);
            this.rotatingSplineOnlyP.TabIndex = 0;
            this.toolTip1.SetToolTip(this.rotatingSplineOnlyP, "Ink only: Shows only the Ink marks. Editing handles will be disabled");
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
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
            this.autoSaveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.splinesToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addToolStripMenuItem.Text = "Import";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // splinesToolStripMenuItem
            // 
            this.splinesToolStripMenuItem.Name = "splinesToolStripMenuItem";
            this.splinesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.splinesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.splinesToolStripMenuItem.Text = "Spline";
            this.splinesToolStripMenuItem.Click += new System.EventHandler(this.splinesToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromCipboardToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // fromCipboardToolStripMenuItem
            // 
            this.fromCipboardToolStripMenuItem.Name = "fromCipboardToolStripMenuItem";
            this.fromCipboardToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.fromCipboardToolStripMenuItem.Text = "Paste Cipboard";
            this.fromCipboardToolStripMenuItem.Click += new System.EventHandler(this.fromCipboardToolStripMenuItem_Click);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
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
            this.addAnchorWithLeftClickToolStripMenuItem1,
            this.toolStripSeparator9,
            this.opacityToolStripMenuItem,
            this.documentToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // displayModeToolStripMenuItem
            // 
            this.displayModeToolStripMenuItem.Enabled = false;
            this.displayModeToolStripMenuItem.Name = "displayModeToolStripMenuItem";
            this.displayModeToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.displayModeToolStripMenuItem.Text = "Spline display";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(256, 6);
            // 
            // combinedModeToolStripMenuItem
            // 
            this.combinedModeToolStripMenuItem.Checked = true;
            this.combinedModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.combinedModeToolStripMenuItem.Name = "combinedModeToolStripMenuItem";
            this.combinedModeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.combinedModeToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.combinedModeToolStripMenuItem.Text = "Combined Mode";
            this.combinedModeToolStripMenuItem.Click += new System.EventHandler(this.combinedModeToolStripMenuItem_Click);
            // 
            // inkOnlyToolStripMenuItem
            // 
            this.inkOnlyToolStripMenuItem.Name = "inkOnlyToolStripMenuItem";
            this.inkOnlyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.inkOnlyToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.inkOnlyToolStripMenuItem.Text = "Ink Only";
            this.inkOnlyToolStripMenuItem.Click += new System.EventHandler(this.inkOnlyToolStripMenuItem_Click);
            // 
            // splineOnlyToolStripMenuItem
            // 
            this.splineOnlyToolStripMenuItem.Name = "splineOnlyToolStripMenuItem";
            this.splineOnlyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.splineOnlyToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.splineOnlyToolStripMenuItem.Text = "Spline Only";
            this.splineOnlyToolStripMenuItem.Click += new System.EventHandler(this.splineOnlyToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(256, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Checked = true;
            this.gridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.Checked = true;
            this.scaleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.scaleToolStripMenuItem.Text = "Scale";
            this.scaleToolStripMenuItem.Click += new System.EventHandler(this.scaleToolStripMenuItem_Click);
            // 
            // xYAxisToolStripMenuItem
            // 
            this.xYAxisToolStripMenuItem.Checked = true;
            this.xYAxisToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xYAxisToolStripMenuItem.Name = "xYAxisToolStripMenuItem";
            this.xYAxisToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.xYAxisToolStripMenuItem.Text = "X-Y Axis";
            this.xYAxisToolStripMenuItem.Click += new System.EventHandler(this.xYAxisToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(256, 6);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(256, 6);
            // 
            // backgroundImagesToolStripMenuItem
            // 
            this.backgroundImagesToolStripMenuItem.Checked = true;
            this.backgroundImagesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backgroundImagesToolStripMenuItem.Name = "backgroundImagesToolStripMenuItem";
            this.backgroundImagesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
            this.backgroundImagesToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.backgroundImagesToolStripMenuItem.Text = "Background Images";
            this.backgroundImagesToolStripMenuItem.Click += new System.EventHandler(this.backgroundImagesToolStripMenuItem_Click);
            // 
            // addAnchorWithLeftClickToolStripMenuItem1
            // 
            this.addAnchorWithLeftClickToolStripMenuItem1.Checked = true;
            this.addAnchorWithLeftClickToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addAnchorWithLeftClickToolStripMenuItem1.Name = "addAnchorWithLeftClickToolStripMenuItem1";
            this.addAnchorWithLeftClickToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.addAnchorWithLeftClickToolStripMenuItem1.Size = new System.Drawing.Size(259, 22);
            this.addAnchorWithLeftClickToolStripMenuItem1.Text = "Add Anchor With Left Click";
            this.addAnchorWithLeftClickToolStripMenuItem1.Click += new System.EventHandler(this.addAnchorWithLeftClickToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(256, 6);
            // 
            // opacityToolStripMenuItem
            // 
            this.opacityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11});
            this.opacityToolStripMenuItem.Name = "opacityToolStripMenuItem";
            this.opacityToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.opacityToolStripMenuItem.Text = "See through";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem2.Text = "10%";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem3.Text = "20%";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem4.Text = "30%";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem5.Text = "40%";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem6.Text = "50%";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem7.Text = "60%";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem8.Text = "70%";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem9.Text = "80%";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem10.Text = "90%";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Checked = true;
            this.toolStripMenuItem11.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem11.Text = "100%";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // documentToolStripMenuItem
            // 
            this.documentToolStripMenuItem.Name = "documentToolStripMenuItem";
            this.documentToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.documentToolStripMenuItem.Text = "Document Index";
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
            this.editingToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editingToolStripMenuItem.Text = "Edit";
            // 
            // anchorsToolStripMenuItem
            // 
            this.anchorsToolStripMenuItem.Enabled = false;
            this.anchorsToolStripMenuItem.Name = "anchorsToolStripMenuItem";
            this.anchorsToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.anchorsToolStripMenuItem.Text = "Handles";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(256, 6);
            // 
            // centerPointsToolStripMenuItem
            // 
            this.centerPointsToolStripMenuItem.Name = "centerPointsToolStripMenuItem";
            this.centerPointsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.centerPointsToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.centerPointsToolStripMenuItem.Text = "Center points";
            this.centerPointsToolStripMenuItem.Click += new System.EventHandler(this.centerPointsToolStripMenuItem_Click);
            // 
            // curvatureHandlesToolStripMenuItem
            // 
            this.curvatureHandlesToolStripMenuItem.Name = "curvatureHandlesToolStripMenuItem";
            this.curvatureHandlesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.curvatureHandlesToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.curvatureHandlesToolStripMenuItem.Text = "Curvature Handles";
            this.curvatureHandlesToolStripMenuItem.Click += new System.EventHandler(this.curvatureHandlesToolStripMenuItem_Click);
            // 
            // rotationHandlesToolStripMenuItem
            // 
            this.rotationHandlesToolStripMenuItem.Name = "rotationHandlesToolStripMenuItem";
            this.rotationHandlesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.rotationHandlesToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.rotationHandlesToolStripMenuItem.Text = "Rotation Handles";
            this.rotationHandlesToolStripMenuItem.Click += new System.EventHandler(this.rotationHandlesToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(256, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(256, 6);
            // 
            // addAnchorWithLeftClickToolStripMenuItem
            // 
            this.addAnchorWithLeftClickToolStripMenuItem.Name = "addAnchorWithLeftClickToolStripMenuItem";
            this.addAnchorWithLeftClickToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.addAnchorWithLeftClickToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.addAnchorWithLeftClickToolStripMenuItem.Text = "Add Anchor With Left Click";
            this.addAnchorWithLeftClickToolStripMenuItem.Click += new System.EventHandler(this.addAnchorWithLeftClickToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.aboutToolStripMenuItem.Text = "How to";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(64, 27);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bezierBoard1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.documentLayoutFP);
            this.splitContainer1.Size = new System.Drawing.Size(720, 543);
            this.splitContainer1.SplitterDistance = 566;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // bezierBoard1
            // 
            this.bezierBoard1.AnchorDrawMode = RotatingBezierSplineEditor.AnchorDrawMode.Centers;
            this.bezierBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bezierBoard1.GridEnabled = true;
            this.bezierBoard1.InkDrawMode = RotatingBezierSplineEditor.InkDrawMode.All;
            this.bezierBoard1.Location = new System.Drawing.Point(0, 0);
            this.bezierBoard1.Margin = new System.Windows.Forms.Padding(2);
            this.bezierBoard1.Name = "bezierBoard1";
            this.bezierBoard1.ScaleEnabled = true;
            this.bezierBoard1.Size = new System.Drawing.Size(566, 543);
            this.bezierBoard1.TabIndex = 2;
            this.bezierBoard1.XYLinesEnabled = true;
            this.bezierBoard1.Paint += new System.Windows.Forms.PaintEventHandler(this.bezierBoard1_Paint);
            // 
            // documentLayoutFP
            // 
            this.documentLayoutFP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.documentLayoutFP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentLayoutFP.Location = new System.Drawing.Point(0, 0);
            this.documentLayoutFP.Name = "documentLayoutFP";
            this.documentLayoutFP.Size = new System.Drawing.Size(151, 543);
            this.documentLayoutFP.TabIndex = 0;
            this.documentLayoutFP.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.documentLayout_ControlAdded);
            // 
            // previewRefreshTimerT
            // 
            this.previewRefreshTimerT.Enabled = true;
            this.previewRefreshTimerT.Interval = 2000;
            this.previewRefreshTimerT.Tick += new System.EventHandler(this.previewRefreshTimerT_Tick);
            // 
            // autoSaveToolStripMenuItem
            // 
            this.autoSaveToolStripMenuItem.Checked = true;
            this.autoSaveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSaveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secondsToolStripMenuItem2,
            this.secondsToolStripMenuItem3,
            this.secondsToolStripMenuItem,
            this.secondsToolStripMenuItem1,
            this.minuteToolStripMenuItem,
            this.minutesToolStripMenuItem,
            this.minutesToolStripMenuItem1,
            this.minutesToolStripMenuItem2,
            this.minutesToolStripMenuItem3});
            this.autoSaveToolStripMenuItem.Name = "autoSaveToolStripMenuItem";
            this.autoSaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.autoSaveToolStripMenuItem.Text = "Auto Save";
            this.autoSaveToolStripMenuItem.Click += new System.EventHandler(this.autoSaveToolStripMenuItem_Click);
            // 
            // autoSaverT
            // 
            this.autoSaverT.Enabled = true;
            this.autoSaverT.Interval = 15000;
            this.autoSaverT.Tick += new System.EventHandler(this.autoSaverT_Tick);
            // 
            // secondsToolStripMenuItem
            // 
            this.secondsToolStripMenuItem.Checked = true;
            this.secondsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.secondsToolStripMenuItem.Name = "secondsToolStripMenuItem";
            this.secondsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.secondsToolStripMenuItem.Text = "15 seconds";
            this.secondsToolStripMenuItem.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // secondsToolStripMenuItem1
            // 
            this.secondsToolStripMenuItem1.Name = "secondsToolStripMenuItem1";
            this.secondsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.secondsToolStripMenuItem1.Text = "30 seconds";
            this.secondsToolStripMenuItem1.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // minuteToolStripMenuItem
            // 
            this.minuteToolStripMenuItem.Name = "minuteToolStripMenuItem";
            this.minuteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.minuteToolStripMenuItem.Text = "1 minute";
            this.minuteToolStripMenuItem.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // minutesToolStripMenuItem
            // 
            this.minutesToolStripMenuItem.Name = "minutesToolStripMenuItem";
            this.minutesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.minutesToolStripMenuItem.Text = "2 minutes";
            this.minutesToolStripMenuItem.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // minutesToolStripMenuItem1
            // 
            this.minutesToolStripMenuItem1.Name = "minutesToolStripMenuItem1";
            this.minutesToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.minutesToolStripMenuItem1.Text = "3 minutes";
            this.minutesToolStripMenuItem1.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // secondsToolStripMenuItem2
            // 
            this.secondsToolStripMenuItem2.Name = "secondsToolStripMenuItem2";
            this.secondsToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.secondsToolStripMenuItem2.Text = "5 seconds";
            this.secondsToolStripMenuItem2.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // secondsToolStripMenuItem3
            // 
            this.secondsToolStripMenuItem3.Name = "secondsToolStripMenuItem3";
            this.secondsToolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.secondsToolStripMenuItem3.Text = "10 seconds";
            this.secondsToolStripMenuItem3.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // minutesToolStripMenuItem2
            // 
            this.minutesToolStripMenuItem2.Name = "minutesToolStripMenuItem2";
            this.minutesToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.minutesToolStripMenuItem2.Text = "10 minutes";
            this.minutesToolStripMenuItem2.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // minutesToolStripMenuItem3
            // 
            this.minutesToolStripMenuItem3.Name = "minutesToolStripMenuItem3";
            this.minutesToolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.minutesToolStripMenuItem3.Text = "15 minutes";
            this.minutesToolStripMenuItem3.Click += new System.EventHandler(this.autoSaveDurationChange_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 567);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Rotating Bezier Spline Editor";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem opacityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem documentToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel documentLayoutFP;
        private System.Windows.Forms.Timer previewRefreshTimerT;
        private System.Windows.Forms.ToolStripMenuItem autoSaveToolStripMenuItem;
        private System.Windows.Forms.Timer autoSaverT;
        private System.Windows.Forms.ToolStripMenuItem secondsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem secondsToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem secondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem minuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem3;
    }
}

