namespace RoboSim
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realTimeSimulation_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePlotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToProteusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.periodicallySaveOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceAScreenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animated3DRefresh_t = new System.Windows.Forms.Timer(this.components);
            this.simulationStepT = new System.Windows.Forms.Timer(this.components);
            this.plotsRefreshT = new System.Windows.Forms.Timer(this.components);
            this.periodicSaveT = new System.Windows.Forms.Timer(this.components);
            this.manualControlP = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.solutionsCB = new System.Windows.Forms.ComboBox();
            this.showDotPosTB = new System.Windows.Forms.TextBox();
            this.showDotOrientationTB = new System.Windows.Forms.TextBox();
            this.applySolutionB = new System.Windows.Forms.Button();
            this.solveTargetB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.currentPosL = new System.Windows.Forms.Label();
            this.currentOrientL = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.animDivsTB = new System.Windows.Forms.TextBox();
            this.toPosAnimTB = new System.Windows.Forms.TextBox();
            this.toOrientAnimTB = new System.Windows.Forms.TextBox();
            this.fromPosAnimTB = new System.Windows.Forms.TextBox();
            this.fromOrientAnimTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.scriptMakePB = new System.Windows.Forms.ProgressBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sizeOfWrittingPlaneTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.orientationOfWrittingPlaneTB = new System.Windows.Forms.TextBox();
            this.centerOfWrittingPlaneTB = new System.Windows.Forms.TextBox();
            this.resolutionForScriptTB = new System.Windows.Forms.TextBox();
            this.planTrajectoryButton = new System.Windows.Forms.Button();
            this.sendTooWorkspaceB = new System.Windows.Forms.Button();
            this.splineEditor = new SplineDesigner.SplinesCollectionUC();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.wsProgZ = new System.Windows.Forms.ProgressBar();
            this.wsProgY = new System.Windows.Forms.ProgressBar();
            this.wsProgX = new System.Windows.Forms.ProgressBar();
            this.workSpaceRangeTB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.workSpaceStartTB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.computeWorkspace = new System.Windows.Forms.Button();
            this.trajectoryStepT = new System.Windows.Forms.Timer(this.components);
            this.linearResWsTB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.aResWsTB = new System.Windows.Forms.TextBox();
            this.exportWsReportB = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.totalWsAreaL = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.totalWorkableAreaWsL = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.successRatioWsl = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulationToolStripMenuItem,
            this.simStepToolStripMenuItem,
            this.analysisToolStripMenuItem,
            this.presentationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.realTimeSimulation_MI,
            this.updatePlotsToolStripMenuItem,
            this.connectToProteusToolStripMenuItem});
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.simulationToolStripMenuItem.Text = "Simulation";
            // 
            // realTimeSimulation_MI
            // 
            this.realTimeSimulation_MI.Name = "realTimeSimulation_MI";
            this.realTimeSimulation_MI.Size = new System.Drawing.Size(176, 22);
            this.realTimeSimulation_MI.Text = "&Real Time Tick";
            this.realTimeSimulation_MI.Click += new System.EventHandler(this.realTimeSimulationT_MI_Click);
            // 
            // updatePlotsToolStripMenuItem
            // 
            this.updatePlotsToolStripMenuItem.Checked = true;
            this.updatePlotsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.updatePlotsToolStripMenuItem.Name = "updatePlotsToolStripMenuItem";
            this.updatePlotsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.updatePlotsToolStripMenuItem.Text = "Update Plots";
            this.updatePlotsToolStripMenuItem.Click += new System.EventHandler(this.updatePlotsToolStripMenuItem_Click);
            // 
            // connectToProteusToolStripMenuItem
            // 
            this.connectToProteusToolStripMenuItem.Name = "connectToProteusToolStripMenuItem";
            this.connectToProteusToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.connectToProteusToolStripMenuItem.Text = "Connect to Proteus";
            this.connectToProteusToolStripMenuItem.Click += new System.EventHandler(this.connectToProteusToolStripMenuItem_Click);
            // 
            // simStepToolStripMenuItem
            // 
            this.simStepToolStripMenuItem.Name = "simStepToolStripMenuItem";
            this.simStepToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.simStepToolStripMenuItem.Text = "SimStep";
            this.simStepToolStripMenuItem.Click += new System.EventHandler(this.simStepToolStripMenuItem_Click);
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.analysisToolStripMenuItem.Text = "Analysis Tools";
            // 
            // presentationToolStripMenuItem
            // 
            this.presentationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.periodicallySaveOutputToolStripMenuItem,
            this.forceAScreenshotToolStripMenuItem});
            this.presentationToolStripMenuItem.Name = "presentationToolStripMenuItem";
            this.presentationToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.presentationToolStripMenuItem.Text = "Presentation";
            // 
            // periodicallySaveOutputToolStripMenuItem
            // 
            this.periodicallySaveOutputToolStripMenuItem.Name = "periodicallySaveOutputToolStripMenuItem";
            this.periodicallySaveOutputToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.periodicallySaveOutputToolStripMenuItem.Text = "Periodically Save Output";
            this.periodicallySaveOutputToolStripMenuItem.Click += new System.EventHandler(this.periodicallySaveOutputToolStripMenuItem_Click);
            // 
            // forceAScreenshotToolStripMenuItem
            // 
            this.forceAScreenshotToolStripMenuItem.Name = "forceAScreenshotToolStripMenuItem";
            this.forceAScreenshotToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.forceAScreenshotToolStripMenuItem.Text = "Force a Screenshot";
            this.forceAScreenshotToolStripMenuItem.Click += new System.EventHandler(this.forceAScreenshotToolStripMenuItem_Click);
            // 
            // animated3DRefresh_t
            // 
            this.animated3DRefresh_t.Enabled = true;
            this.animated3DRefresh_t.Interval = 30;
            this.animated3DRefresh_t.Tick += new System.EventHandler(this.animated3DRefresh_t_Tick);
            // 
            // simulationStepT
            // 
            this.simulationStepT.Enabled = true;
            this.simulationStepT.Interval = 30;
            this.simulationStepT.Tick += new System.EventHandler(this.simulationStepT_Tick);
            // 
            // plotsRefreshT
            // 
            this.plotsRefreshT.Enabled = true;
            this.plotsRefreshT.Interval = 30;
            this.plotsRefreshT.Tick += new System.EventHandler(this.plotsRefreshT_Tick);
            // 
            // periodicSaveT
            // 
            this.periodicSaveT.Interval = 1000;
            this.periodicSaveT.Tick += new System.EventHandler(this.periodicSaveT_Tick);
            // 
            // manualControlP
            // 
            this.manualControlP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.manualControlP.Location = new System.Drawing.Point(3, 3);
            this.manualControlP.Name = "manualControlP";
            this.manualControlP.Size = new System.Drawing.Size(595, 165);
            this.manualControlP.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.manualControlP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(620, 380);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(620, 186);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.solutionsCB);
            this.tabPage1.Controls.Add(this.showDotPosTB);
            this.tabPage1.Controls.Add(this.showDotOrientationTB);
            this.tabPage1.Controls.Add(this.applySolutionB);
            this.tabPage1.Controls.Add(this.solveTargetB);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.currentPosL);
            this.tabPage1.Controls.Add(this.currentOrientL);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 160);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Single Point Solution ";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Show Dot";
            // 
            // solutionsCB
            // 
            this.solutionsCB.FormattingEnabled = true;
            this.solutionsCB.Location = new System.Drawing.Point(68, 42);
            this.solutionsCB.Name = "solutionsCB";
            this.solutionsCB.Size = new System.Drawing.Size(284, 21);
            this.solutionsCB.TabIndex = 10;
            // 
            // showDotPosTB
            // 
            this.showDotPosTB.Location = new System.Drawing.Point(68, 12);
            this.showDotPosTB.Name = "showDotPosTB";
            this.showDotPosTB.Size = new System.Drawing.Size(98, 20);
            this.showDotPosTB.TabIndex = 6;
            this.showDotPosTB.Text = "1,1,1";
            this.showDotPosTB.TextChanged += new System.EventHandler(this.showDotPosTB_TextChanged);
            this.showDotPosTB.Enter += new System.EventHandler(this.showDotPosTB_TextChanged);
            // 
            // showDotOrientationTB
            // 
            this.showDotOrientationTB.Location = new System.Drawing.Point(172, 12);
            this.showDotOrientationTB.Name = "showDotOrientationTB";
            this.showDotOrientationTB.Size = new System.Drawing.Size(73, 20);
            this.showDotOrientationTB.TabIndex = 6;
            this.showDotOrientationTB.Text = "0,0,0";
            this.showDotOrientationTB.TextChanged += new System.EventHandler(this.showDotPosTB_TextChanged);
            this.showDotOrientationTB.Enter += new System.EventHandler(this.showDotPosTB_TextChanged);
            // 
            // applySolutionB
            // 
            this.applySolutionB.Location = new System.Drawing.Point(295, 69);
            this.applySolutionB.Name = "applySolutionB";
            this.applySolutionB.Size = new System.Drawing.Size(57, 27);
            this.applySolutionB.TabIndex = 9;
            this.applySolutionB.Text = "Apply";
            this.applySolutionB.UseVisualStyleBackColor = true;
            this.applySolutionB.Click += new System.EventHandler(this.applySolutionB_Click);
            // 
            // solveTargetB
            // 
            this.solveTargetB.Location = new System.Drawing.Point(251, 9);
            this.solveTargetB.Name = "solveTargetB";
            this.solveTargetB.Size = new System.Drawing.Size(101, 27);
            this.solveTargetB.TabIndex = 9;
            this.solveTargetB.Text = "Solve for this";
            this.solveTargetB.UseVisualStyleBackColor = true;
            this.solveTargetB.Click += new System.EventHandler(this.solveTargetB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Current Position:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Current Orientation:";
            // 
            // currentPosL
            // 
            this.currentPosL.AutoSize = true;
            this.currentPosL.Location = new System.Drawing.Point(497, 15);
            this.currentPosL.Name = "currentPosL";
            this.currentPosL.Size = new System.Drawing.Size(13, 13);
            this.currentPosL.TabIndex = 8;
            this.currentPosL.Text = "--";
            // 
            // currentOrientL
            // 
            this.currentOrientL.AutoSize = true;
            this.currentOrientL.Location = new System.Drawing.Point(497, 41);
            this.currentOrientL.Name = "currentOrientL";
            this.currentOrientL.Size = new System.Drawing.Size(13, 13);
            this.currentOrientL.TabIndex = 8;
            this.currentOrientL.Text = "--";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.animDivsTB);
            this.tabPage2.Controls.Add(this.toPosAnimTB);
            this.tabPage2.Controls.Add(this.toOrientAnimTB);
            this.tabPage2.Controls.Add(this.fromPosAnimTB);
            this.tabPage2.Controls.Add(this.fromOrientAnimTB);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 160);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Animation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "To";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(178, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "α, ß, δ (Radians)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "X, Y, Z (m)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "From";
            // 
            // animDivsTB
            // 
            this.animDivsTB.Location = new System.Drawing.Point(77, 78);
            this.animDivsTB.Name = "animDivsTB";
            this.animDivsTB.Size = new System.Drawing.Size(70, 20);
            this.animDivsTB.TabIndex = 10;
            this.animDivsTB.Text = "100";
            // 
            // toPosAnimTB
            // 
            this.toPosAnimTB.Location = new System.Drawing.Point(77, 47);
            this.toPosAnimTB.Name = "toPosAnimTB";
            this.toPosAnimTB.Size = new System.Drawing.Size(98, 20);
            this.toPosAnimTB.TabIndex = 10;
            this.toPosAnimTB.Text = "1,1,1";
            this.toPosAnimTB.TextChanged += new System.EventHandler(this.setAnimLimitsPosControls_TextChanged);
            this.toPosAnimTB.Enter += new System.EventHandler(this.setAnimLimitsPosControls_TextChanged);
            // 
            // toOrientAnimTB
            // 
            this.toOrientAnimTB.Location = new System.Drawing.Point(181, 47);
            this.toOrientAnimTB.Name = "toOrientAnimTB";
            this.toOrientAnimTB.Size = new System.Drawing.Size(73, 20);
            this.toOrientAnimTB.TabIndex = 11;
            this.toOrientAnimTB.Text = "0,0,0";
            this.toOrientAnimTB.TextChanged += new System.EventHandler(this.setAnimLimitsOrientationControls_TextChanged);
            this.toOrientAnimTB.Enter += new System.EventHandler(this.setAnimLimitsOrientationControls_TextChanged);
            // 
            // fromPosAnimTB
            // 
            this.fromPosAnimTB.Location = new System.Drawing.Point(77, 21);
            this.fromPosAnimTB.Name = "fromPosAnimTB";
            this.fromPosAnimTB.Size = new System.Drawing.Size(98, 20);
            this.fromPosAnimTB.TabIndex = 10;
            this.fromPosAnimTB.Text = "1,1,1";
            this.fromPosAnimTB.TextChanged += new System.EventHandler(this.setAnimLimitsPosControls_TextChanged);
            this.fromPosAnimTB.Enter += new System.EventHandler(this.setAnimLimitsPosControls_TextChanged);
            // 
            // fromOrientAnimTB
            // 
            this.fromOrientAnimTB.Location = new System.Drawing.Point(181, 21);
            this.fromOrientAnimTB.Name = "fromOrientAnimTB";
            this.fromOrientAnimTB.Size = new System.Drawing.Size(73, 20);
            this.fromOrientAnimTB.TabIndex = 11;
            this.fromOrientAnimTB.Text = "0,0,0";
            this.fromOrientAnimTB.TextChanged += new System.EventHandler(this.setAnimLimitsOrientationControls_TextChanged);
            this.fromOrientAnimTB.Enter += new System.EventHandler(this.setAnimLimitsOrientationControls_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Animate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.animateB_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 24);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(634, 412);
            this.tabControl2.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(626, 386);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Testing and Manual Control";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.scriptMakePB);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.sizeOfWrittingPlaneTB);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.orientationOfWrittingPlaneTB);
            this.tabPage4.Controls.Add(this.centerOfWrittingPlaneTB);
            this.tabPage4.Controls.Add(this.resolutionForScriptTB);
            this.tabPage4.Controls.Add(this.planTrajectoryButton);
            this.tabPage4.Controls.Add(this.sendTooWorkspaceB);
            this.tabPage4.Controls.Add(this.splineEditor);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(626, 386);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Ink Painter";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // scriptMakePB
            // 
            this.scriptMakePB.Location = new System.Drawing.Point(490, 226);
            this.scriptMakePB.Name = "scriptMakePB";
            this.scriptMakePB.Size = new System.Drawing.Size(125, 23);
            this.scriptMakePB.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(487, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Size of Pad";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(487, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Orientation of Pad";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(487, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Center of Writting Pad";
            // 
            // sizeOfWrittingPlaneTB
            // 
            this.sizeOfWrittingPlaneTB.Location = new System.Drawing.Point(490, 164);
            this.sizeOfWrittingPlaneTB.Name = "sizeOfWrittingPlaneTB";
            this.sizeOfWrittingPlaneTB.Size = new System.Drawing.Size(125, 20);
            this.sizeOfWrittingPlaneTB.TabIndex = 4;
            this.sizeOfWrittingPlaneTB.Text = "1,0.5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(484, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Resolution";
            // 
            // orientationOfWrittingPlaneTB
            // 
            this.orientationOfWrittingPlaneTB.Location = new System.Drawing.Point(490, 115);
            this.orientationOfWrittingPlaneTB.Name = "orientationOfWrittingPlaneTB";
            this.orientationOfWrittingPlaneTB.Size = new System.Drawing.Size(125, 20);
            this.orientationOfWrittingPlaneTB.TabIndex = 4;
            this.orientationOfWrittingPlaneTB.Text = "0,0,0";
            // 
            // centerOfWrittingPlaneTB
            // 
            this.centerOfWrittingPlaneTB.Location = new System.Drawing.Point(490, 70);
            this.centerOfWrittingPlaneTB.Name = "centerOfWrittingPlaneTB";
            this.centerOfWrittingPlaneTB.Size = new System.Drawing.Size(125, 20);
            this.centerOfWrittingPlaneTB.TabIndex = 4;
            this.centerOfWrittingPlaneTB.Text = "1,1,0.2";
            // 
            // resolutionForScriptTB
            // 
            this.resolutionForScriptTB.Location = new System.Drawing.Point(487, 19);
            this.resolutionForScriptTB.Name = "resolutionForScriptTB";
            this.resolutionForScriptTB.Size = new System.Drawing.Size(125, 20);
            this.resolutionForScriptTB.TabIndex = 4;
            this.resolutionForScriptTB.Text = "0.001";
            // 
            // planTrajectoryButton
            // 
            this.planTrajectoryButton.Enabled = false;
            this.planTrajectoryButton.Location = new System.Drawing.Point(488, 255);
            this.planTrajectoryButton.Name = "planTrajectoryButton";
            this.planTrajectoryButton.Size = new System.Drawing.Size(127, 23);
            this.planTrajectoryButton.TabIndex = 1;
            this.planTrajectoryButton.Text = "Plan Trajectory";
            this.planTrajectoryButton.UseVisualStyleBackColor = true;
            this.planTrajectoryButton.Click += new System.EventHandler(this.planTrajectoryButton_Click);
            // 
            // sendTooWorkspaceB
            // 
            this.sendTooWorkspaceB.Location = new System.Drawing.Point(488, 197);
            this.sendTooWorkspaceB.Name = "sendTooWorkspaceB";
            this.sendTooWorkspaceB.Size = new System.Drawing.Size(127, 23);
            this.sendTooWorkspaceB.TabIndex = 1;
            this.sendTooWorkspaceB.Text = "Send to Workspace";
            this.sendTooWorkspaceB.UseVisualStyleBackColor = true;
            this.sendTooWorkspaceB.Click += new System.EventHandler(this.sendTooWorkspaceB_Click);
            // 
            // splineEditor
            // 
            this.splineEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splineEditor.Location = new System.Drawing.Point(0, 0);
            this.splineEditor.Name = "splineEditor";
            this.splineEditor.Size = new System.Drawing.Size(462, 380);
            this.splineEditor.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.wsProgZ);
            this.tabPage5.Controls.Add(this.wsProgY);
            this.tabPage5.Controls.Add(this.wsProgX);
            this.tabPage5.Controls.Add(this.aResWsTB);
            this.tabPage5.Controls.Add(this.linearResWsTB);
            this.tabPage5.Controls.Add(this.workSpaceRangeTB);
            this.tabPage5.Controls.Add(this.successRatioWsl);
            this.tabPage5.Controls.Add(this.label21);
            this.tabPage5.Controls.Add(this.totalWorkableAreaWsL);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.totalWsAreaL);
            this.tabPage5.Controls.Add(this.label17);
            this.tabPage5.Controls.Add(this.label16);
            this.tabPage5.Controls.Add(this.label14);
            this.tabPage5.Controls.Add(this.label15);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.workSpaceStartTB);
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Controls.Add(this.exportWsReportB);
            this.tabPage5.Controls.Add(this.computeWorkspace);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(626, 386);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Work Space Optimizer";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.Click += new System.EventHandler(this.tabPage5_Click);
            // 
            // wsProgZ
            // 
            this.wsProgZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wsProgZ.Location = new System.Drawing.Point(40, 356);
            this.wsProgZ.Name = "wsProgZ";
            this.wsProgZ.Size = new System.Drawing.Size(533, 23);
            this.wsProgZ.TabIndex = 5;
            // 
            // wsProgY
            // 
            this.wsProgY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wsProgY.Location = new System.Drawing.Point(40, 327);
            this.wsProgY.Name = "wsProgY";
            this.wsProgY.Size = new System.Drawing.Size(533, 23);
            this.wsProgY.TabIndex = 5;
            // 
            // wsProgX
            // 
            this.wsProgX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wsProgX.Location = new System.Drawing.Point(40, 298);
            this.wsProgX.Name = "wsProgX";
            this.wsProgX.Size = new System.Drawing.Size(533, 23);
            this.wsProgX.TabIndex = 5;
            // 
            // workSpaceRangeTB
            // 
            this.workSpaceRangeTB.Location = new System.Drawing.Point(108, 36);
            this.workSpaceRangeTB.Name = "workSpaceRangeTB";
            this.workSpaceRangeTB.Size = new System.Drawing.Size(100, 20);
            this.workSpaceRangeTB.TabIndex = 4;
            this.workSpaceRangeTB.Text = "4,4,2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 282);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Progress";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(40, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Test Range";
            // 
            // workSpaceStartTB
            // 
            this.workSpaceStartTB.Location = new System.Drawing.Point(108, 10);
            this.workSpaceStartTB.Name = "workSpaceStartTB";
            this.workSpaceStartTB.Size = new System.Drawing.Size(100, 20);
            this.workSpaceStartTB.TabIndex = 2;
            this.workSpaceStartTB.Text = "-2,-2,0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Test Start";
            // 
            // computeWorkspace
            // 
            this.computeWorkspace.Location = new System.Drawing.Point(74, 114);
            this.computeWorkspace.Name = "computeWorkspace";
            this.computeWorkspace.Size = new System.Drawing.Size(134, 23);
            this.computeWorkspace.TabIndex = 0;
            this.computeWorkspace.Text = "Compute and visuallize workspace";
            this.computeWorkspace.UseVisualStyleBackColor = true;
            this.computeWorkspace.Click += new System.EventHandler(this.computeWorkspace_Click);
            // 
            // trajectoryStepT
            // 
            this.trajectoryStepT.Interval = 30;
            this.trajectoryStepT.Tick += new System.EventHandler(this.trajectoryStepT_Tick);
            // 
            // linearResWsTB
            // 
            this.linearResWsTB.Location = new System.Drawing.Point(108, 62);
            this.linearResWsTB.Name = "linearResWsTB";
            this.linearResWsTB.Size = new System.Drawing.Size(100, 20);
            this.linearResWsTB.TabIndex = 4;
            this.linearResWsTB.Text = "0.2";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Linear Increment";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 91);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Angular Increment";
            // 
            // aResWsTB
            // 
            this.aResWsTB.Location = new System.Drawing.Point(108, 88);
            this.aResWsTB.Name = "aResWsTB";
            this.aResWsTB.Size = new System.Drawing.Size(100, 20);
            this.aResWsTB.TabIndex = 4;
            this.aResWsTB.Text = "1.575796";
            // 
            // exportWsReportB
            // 
            this.exportWsReportB.Location = new System.Drawing.Point(74, 147);
            this.exportWsReportB.Name = "exportWsReportB";
            this.exportWsReportB.Size = new System.Drawing.Size(134, 23);
            this.exportWsReportB.TabIndex = 0;
            this.exportWsReportB.Text = "Export";
            this.exportWsReportB.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(364, 17);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Total Area:";
            // 
            // totalWsAreaL
            // 
            this.totalWsAreaL.AutoSize = true;
            this.totalWsAreaL.Location = new System.Drawing.Point(426, 17);
            this.totalWsAreaL.Name = "totalWsAreaL";
            this.totalWsAreaL.Size = new System.Drawing.Size(13, 13);
            this.totalWsAreaL.TabIndex = 3;
            this.totalWsAreaL.Text = "--";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(308, 41);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(115, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Total Achievable Area:";
            // 
            // totalWorkableAreaWsL
            // 
            this.totalWorkableAreaWsL.AutoSize = true;
            this.totalWorkableAreaWsL.Location = new System.Drawing.Point(426, 41);
            this.totalWorkableAreaWsL.Name = "totalWorkableAreaWsL";
            this.totalWorkableAreaWsL.Size = new System.Drawing.Size(13, 13);
            this.totalWorkableAreaWsL.TabIndex = 3;
            this.totalWorkableAreaWsL.Text = "--";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(304, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(119, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Percent Success Ratio:";
            // 
            // successRatioWsl
            // 
            this.successRatioWsl.AutoSize = true;
            this.successRatioWsl.Location = new System.Drawing.Point(426, 65);
            this.successRatioWsl.Name = "successRatioWsl";
            this.successRatioWsl.Size = new System.Drawing.Size(13, 13);
            this.successRatioWsl.TabIndex = 3;
            this.successRatioWsl.Text = "--";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(634, 436);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Drogon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer animated3DRefresh_t;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realTimeSimulation_MI;
        private System.Windows.Forms.Timer simulationStepT;
        private System.Windows.Forms.ToolStripMenuItem updatePlotsToolStripMenuItem;
        private System.Windows.Forms.Timer plotsRefreshT;
        private System.Windows.Forms.ToolStripMenuItem simStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToProteusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem periodicallySaveOutputToolStripMenuItem;
        private System.Windows.Forms.Timer periodicSaveT;
        private System.Windows.Forms.ToolStripMenuItem forceAScreenshotToolStripMenuItem;
        private System.Windows.Forms.Panel manualControlP;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox solutionsCB;
        private System.Windows.Forms.TextBox showDotPosTB;
        private System.Windows.Forms.TextBox showDotOrientationTB;
        private System.Windows.Forms.Button applySolutionB;
        private System.Windows.Forms.Button solveTargetB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentPosL;
        private System.Windows.Forms.Label currentOrientL;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox animDivsTB;
        private System.Windows.Forms.TextBox toPosAnimTB;
        private System.Windows.Forms.TextBox toOrientAnimTB;
        private System.Windows.Forms.TextBox fromPosAnimTB;
        private System.Windows.Forms.TextBox fromOrientAnimTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private SplineDesigner.SplinesCollectionUC splineEditor;
        private System.Windows.Forms.Button sendTooWorkspaceB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox resolutionForScriptTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox orientationOfWrittingPlaneTB;
        private System.Windows.Forms.TextBox centerOfWrittingPlaneTB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox sizeOfWrittingPlaneTB;
        private System.Windows.Forms.Button planTrajectoryButton;
        private System.Windows.Forms.Timer trajectoryStepT;
        private System.Windows.Forms.ProgressBar scriptMakePB;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button computeWorkspace;
        private System.Windows.Forms.TextBox workSpaceRangeTB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox workSpaceStartTB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar wsProgZ;
        private System.Windows.Forms.ProgressBar wsProgY;
        private System.Windows.Forms.ProgressBar wsProgX;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox aResWsTB;
        private System.Windows.Forms.TextBox linearResWsTB;
        private System.Windows.Forms.Label successRatioWsl;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label totalWorkableAreaWsL;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label totalWsAreaL;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button exportWsReportB;
    }
}

