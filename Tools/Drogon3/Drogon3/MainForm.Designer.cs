
namespace Drogon3
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robotEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inverseKinematicsSolverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pIDPerformanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splinePainterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routePlannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSplineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tools = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editRobotT = new RotatingBezierSplineEditor.ToolControl();
            this.splinePainterT = new RotatingBezierSplineEditor.ToolControl();
            this.endEffectorControlT = new RotatingBezierSplineEditor.ToolControl();
            this.pidPerformanceT = new RotatingBezierSplineEditor.ToolControl();
            this.inverseKinematicsT = new RotatingBezierSplineEditor.ToolControl();
            this.realTimeTickT = new RotatingBezierSplineEditor.ToolControl();
            this._3dPrevT = new RotatingBezierSplineEditor.ToolControl();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analysisToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(783, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dSimulationToolStripMenuItem,
            this.robotEditorToolStripMenuItem,
            this.inverseKinematicsSolverToolStripMenuItem,
            this.pIDPerformanceToolStripMenuItem,
            this.splinePainterToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.analysisToolStripMenuItem.Text = "Analysis";
            // 
            // dSimulationToolStripMenuItem
            // 
            this.dSimulationToolStripMenuItem.Name = "dSimulationToolStripMenuItem";
            this.dSimulationToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.dSimulationToolStripMenuItem.Text = "3D Simulation";
            // 
            // robotEditorToolStripMenuItem
            // 
            this.robotEditorToolStripMenuItem.Name = "robotEditorToolStripMenuItem";
            this.robotEditorToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.robotEditorToolStripMenuItem.Text = "Robot Editor";
            // 
            // inverseKinematicsSolverToolStripMenuItem
            // 
            this.inverseKinematicsSolverToolStripMenuItem.Name = "inverseKinematicsSolverToolStripMenuItem";
            this.inverseKinematicsSolverToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.inverseKinematicsSolverToolStripMenuItem.Text = "Inverse Kinematics Solver";
            // 
            // pIDPerformanceToolStripMenuItem
            // 
            this.pIDPerformanceToolStripMenuItem.Name = "pIDPerformanceToolStripMenuItem";
            this.pIDPerformanceToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.pIDPerformanceToolStripMenuItem.Text = "PID performance";
            // 
            // splinePainterToolStripMenuItem
            // 
            this.splinePainterToolStripMenuItem.Name = "splinePainterToolStripMenuItem";
            this.splinePainterToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            this.splinePainterToolStripMenuItem.Text = "Spline Painter";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.routePlannerToolStripMenuItem,
            this.importSplineToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // routePlannerToolStripMenuItem
            // 
            this.routePlannerToolStripMenuItem.Name = "routePlannerToolStripMenuItem";
            this.routePlannerToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.routePlannerToolStripMenuItem.Text = "Rotating Spline Editor";
            this.routePlannerToolStripMenuItem.Click += new System.EventHandler(this.routePlannerToolStripMenuItem_Click);
            // 
            // importSplineToolStripMenuItem
            // 
            this.importSplineToolStripMenuItem.Name = "importSplineToolStripMenuItem";
            this.importSplineToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.importSplineToolStripMenuItem.Text = "Spline Analyzer";
            this.importSplineToolStripMenuItem.Click += new System.EventHandler(this.importSplineToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tools
            // 
            this.tools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tools.Location = new System.Drawing.Point(86, 31);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(685, 605);
            this.tools.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.editRobotT);
            this.panel1.Controls.Add(this.splinePainterT);
            this.panel1.Controls.Add(this.endEffectorControlT);
            this.panel1.Controls.Add(this.pidPerformanceT);
            this.panel1.Controls.Add(this.inverseKinematicsT);
            this.panel1.Location = new System.Drawing.Point(0, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 421);
            this.panel1.TabIndex = 3;
            // 
            // editRobotT
            // 
            this.editRobotT.Active = false;
            this.editRobotT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editRobotT.DistinctSelection = true;
            this.editRobotT.Location = new System.Drawing.Point(0, 1);
            this.editRobotT.Name = "editRobotT";
            this.editRobotT.Size = new System.Drawing.Size(80, 80);
            this.editRobotT.TabIndex = 0;
            this.editRobotT.OnActivated += new System.EventHandler(this._3dPrev_OnActivated);
            // 
            // splinePainterT
            // 
            this.splinePainterT.Active = false;
            this.splinePainterT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.splinePainterT.DistinctSelection = true;
            this.splinePainterT.Location = new System.Drawing.Point(0, 333);
            this.splinePainterT.Name = "splinePainterT";
            this.splinePainterT.Size = new System.Drawing.Size(80, 80);
            this.splinePainterT.TabIndex = 0;
            this.splinePainterT.OnActivated += new System.EventHandler(this._3dPrev_OnActivated);
            // 
            // endEffectorControlT
            // 
            this.endEffectorControlT.Active = false;
            this.endEffectorControlT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.endEffectorControlT.DistinctSelection = true;
            this.endEffectorControlT.Location = new System.Drawing.Point(0, 250);
            this.endEffectorControlT.Name = "endEffectorControlT";
            this.endEffectorControlT.Size = new System.Drawing.Size(80, 80);
            this.endEffectorControlT.TabIndex = 0;
            this.endEffectorControlT.OnActivated += new System.EventHandler(this._3dPrev_OnActivated);
            // 
            // pidPerformanceT
            // 
            this.pidPerformanceT.Active = false;
            this.pidPerformanceT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pidPerformanceT.DistinctSelection = true;
            this.pidPerformanceT.Location = new System.Drawing.Point(0, 167);
            this.pidPerformanceT.Name = "pidPerformanceT";
            this.pidPerformanceT.Size = new System.Drawing.Size(80, 80);
            this.pidPerformanceT.TabIndex = 0;
            this.pidPerformanceT.OnActivated += new System.EventHandler(this._3dPrev_OnActivated);
            // 
            // inverseKinematicsT
            // 
            this.inverseKinematicsT.Active = false;
            this.inverseKinematicsT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.inverseKinematicsT.DistinctSelection = true;
            this.inverseKinematicsT.Location = new System.Drawing.Point(0, 84);
            this.inverseKinematicsT.Name = "inverseKinematicsT";
            this.inverseKinematicsT.Size = new System.Drawing.Size(80, 80);
            this.inverseKinematicsT.TabIndex = 0;
            this.inverseKinematicsT.OnActivated += new System.EventHandler(this._3dPrev_OnActivated);
            // 
            // realTimeTickT
            // 
            this.realTimeTickT.Active = false;
            this.realTimeTickT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.realTimeTickT.DistinctSelection = false;
            this.realTimeTickT.Location = new System.Drawing.Point(0, 31);
            this.realTimeTickT.Name = "realTimeTickT";
            this.realTimeTickT.Size = new System.Drawing.Size(80, 80);
            this.realTimeTickT.TabIndex = 0;
            this.realTimeTickT.OnActivated += new System.EventHandler(this.realTimeTick_OnActivated);
            // 
            // _3dPrevT
            // 
            this._3dPrevT.Active = false;
            this._3dPrevT.Cursor = System.Windows.Forms.Cursors.Hand;
            this._3dPrevT.DistinctSelection = false;
            this._3dPrevT.Location = new System.Drawing.Point(0, 114);
            this._3dPrevT.Name = "_3dPrevT";
            this._3dPrevT.Size = new System.Drawing.Size(80, 80);
            this._3dPrevT.TabIndex = 0;
            this._3dPrevT.OnActivated += new System.EventHandler(this._3dPrev_OnActivated);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.realTimeTickT);
            this.Controls.Add(this._3dPrevT);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Drogon 3";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public RotatingBezierSplineEditor.ToolControl _3dPrevT;
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robotEditorToolStripMenuItem;
        public RotatingBezierSplineEditor.ToolControl editRobotT;
        public RotatingBezierSplineEditor.ToolControl realTimeTickT;
        private System.Windows.Forms.Timer timer1;
        public RotatingBezierSplineEditor.ToolControl inverseKinematicsT;
        private System.Windows.Forms.Panel tools;
        private System.Windows.Forms.ToolStripMenuItem inverseKinematicsSolverToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        public RotatingBezierSplineEditor.ToolControl pidPerformanceT;
        private System.Windows.Forms.ToolStripMenuItem pIDPerformanceToolStripMenuItem;
        //public RotatingBezierSplineEditor.ToolControl routePlannerT;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routePlannerToolStripMenuItem;
        public RotatingBezierSplineEditor.ToolControl endEffectorControlT;
        private System.Windows.Forms.ToolStripMenuItem importSplineToolStripMenuItem;
        public RotatingBezierSplineEditor.ToolControl splinePainterT;
        private System.Windows.Forms.ToolStripMenuItem splinePainterToolStripMenuItem;
    }
}

