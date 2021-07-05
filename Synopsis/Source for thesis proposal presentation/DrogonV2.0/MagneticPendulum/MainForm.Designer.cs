using MagneticPendulum;

namespace MagneticPendulum
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
            this.angularFreqTrB = new System.Windows.Forms.TrackBar();
            this.angularFreqTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.motorHomeOffset = new System.Windows.Forms.NumericUpDown();
            this.applySpeed = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.spEventPoll = new System.Windows.Forms.Timer(this.components);
            this.controlGP = new System.Windows.Forms.GroupBox();
            this.stopB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.setAngleB = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.magPendulumVisualizer1 = new MagneticPendulum.MagPendulumVisualizer();
            this.label7 = new System.Windows.Forms.Label();
            this.dataPort = new FivePointNine.Windows.Controls.SerialChannelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.timeElapsedL = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button22 = new MagneticPendulum.Button2();
            this.button21 = new MagneticPendulum.Button2();
            this.splitContainer1 = new MagneticPendulum.SplitContainer2();
            this.dvPlot = new MagneticPendulum.xyComboControl();
            this.label6 = new System.Windows.Forms.Label();
            this.phasePortraitP = new MagneticPendulum.xyPlotControl();
            this.label5 = new System.Windows.Forms.Label();
            this.minMaxLabel2 = new MagneticPendulum.MinMaxLabel();
            this.minMaxLabel1 = new MagneticPendulum.MinMaxLabel();
            ((System.ComponentModel.ISupportInitialize)(this.angularFreqTrB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorHomeOffset)).BeginInit();
            this.controlGP.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // angularFreqTrB
            // 
            this.angularFreqTrB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.angularFreqTrB.Location = new System.Drawing.Point(222, 31);
            this.angularFreqTrB.Maximum = 200;
            this.angularFreqTrB.Name = "angularFreqTrB";
            this.angularFreqTrB.Size = new System.Drawing.Size(1098, 45);
            this.angularFreqTrB.TabIndex = 0;
            this.angularFreqTrB.Scroll += new System.EventHandler(this.angularFreqTrB_Scroll);
            // 
            // angularFreqTB
            // 
            this.angularFreqTB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.angularFreqTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.angularFreqTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.angularFreqTB.Location = new System.Drawing.Point(215, 64);
            this.angularFreqTB.Name = "angularFreqTB";
            this.angularFreqTB.Size = new System.Drawing.Size(100, 29);
            this.angularFreqTB.TabIndex = 1;
            this.angularFreqTB.TextChanged += new System.EventHandler(this.angularFreqTB_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Applied Angular Frequency \t\r\n(rad/s)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // motorHomeOffset
            // 
            this.motorHomeOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.motorHomeOffset.Location = new System.Drawing.Point(215, 111);
            this.motorHomeOffset.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.motorHomeOffset.Name = "motorHomeOffset";
            this.motorHomeOffset.Size = new System.Drawing.Size(100, 26);
            this.motorHomeOffset.TabIndex = 7;
            // 
            // applySpeed
            // 
            this.applySpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.applySpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.applySpeed.FlatAppearance.BorderSize = 0;
            this.applySpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applySpeed.Location = new System.Drawing.Point(321, 64);
            this.applySpeed.Name = "applySpeed";
            this.applySpeed.Size = new System.Drawing.Size(124, 27);
            this.applySpeed.TabIndex = 0;
            this.applySpeed.Text = "Apply";
            this.applySpeed.UseVisualStyleBackColor = false;
            this.applySpeed.Click += new System.EventHandler(this.applySpeed_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // spEventPoll
            // 
            this.spEventPoll.Enabled = true;
            this.spEventPoll.Interval = 30;
            this.spEventPoll.Tick += new System.EventHandler(this.spEventPoll_Tick);
            // 
            // controlGP
            // 
            this.controlGP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlGP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.controlGP.Controls.Add(this.angularFreqTB);
            this.controlGP.Controls.Add(this.stopB);
            this.controlGP.Controls.Add(this.applySpeed);
            this.controlGP.Controls.Add(this.label2);
            this.controlGP.Controls.Add(this.label1);
            this.controlGP.Controls.Add(this.angularFreqTrB);
            this.controlGP.Controls.Add(this.motorHomeOffset);
            this.controlGP.Controls.Add(this.setAngleB);
            this.controlGP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlGP.Location = new System.Drawing.Point(3, 504);
            this.controlGP.Name = "controlGP";
            this.controlGP.Size = new System.Drawing.Size(1328, 152);
            this.controlGP.TabIndex = 12;
            this.controlGP.TabStop = false;
            this.controlGP.Text = "Motor Control";
            this.controlGP.Enter += new System.EventHandler(this.controlGP_Enter);
            // 
            // stopB
            // 
            this.stopB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.stopB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.stopB.FlatAppearance.BorderSize = 0;
            this.stopB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopB.Location = new System.Drawing.Point(451, 64);
            this.stopB.Name = "stopB";
            this.stopB.Size = new System.Drawing.Size(153, 27);
            this.stopB.TabIndex = 1;
            this.stopB.Text = "Stop";
            this.stopB.UseVisualStyleBackColor = false;
            this.stopB.Click += new System.EventHandler(this.applySpeed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Initial Angle (°)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // setAngleB
            // 
            this.setAngleB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.setAngleB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.setAngleB.FlatAppearance.BorderSize = 0;
            this.setAngleB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setAngleB.Location = new System.Drawing.Point(321, 111);
            this.setAngleB.Name = "setAngleB";
            this.setAngleB.Size = new System.Drawing.Size(171, 27);
            this.setAngleB.TabIndex = 2;
            this.setAngleB.Text = "Calibrate and set";
            this.setAngleB.UseVisualStyleBackColor = false;
            this.setAngleB.Click += new System.EventHandler(this.homeMotorB_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.magPendulumVisualizer1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dataPort);
            this.panel1.Location = new System.Drawing.Point(1132, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 412);
            this.panel1.TabIndex = 2;
            // 
            // magPendulumVisualizer1
            // 
            this.magPendulumVisualizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.magPendulumVisualizer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.magPendulumVisualizer1.Location = new System.Drawing.Point(31, 114);
            this.magPendulumVisualizer1.MotorAngle = 0F;
            this.magPendulumVisualizer1.Name = "magPendulumVisualizer1";
            this.magPendulumVisualizer1.PendulumAngle = -1F;
            this.magPendulumVisualizer1.Size = new System.Drawing.Size(143, 129);
            this.magPendulumVisualizer1.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Apparatus Overview";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataPort
            // 
            this.dataPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPort.DefaultDTR = false;
            this.dataPort.ID = ((byte)(0));
            this.dataPort.Location = new System.Drawing.Point(6, 326);
            this.dataPort.MinimumSize = new System.Drawing.Size(170, 51);
            this.dataPort.Name = "dataPort";
            this.dataPort.PingDuration = 1000;
            this.dataPort.PingEnabled = true;
            this.dataPort.ReceiveEnabled = true;
            this.dataPort.ShowDTR = false;
            this.dataPort.Size = new System.Drawing.Size(185, 77);
            this.dataPort.TabIndex = 3;
            this.dataPort.Connected += new System.EventHandler(this.dataPort_Connected);
            this.dataPort.PacketReceived += new FivePointNine.Windows.Controls.PacketCommandRecieveHandler(this.serialChannelControl1_PacketReceived);
            this.dataPort.Load += new System.EventHandler(this.dataPort_Load);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(426, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Anglular Velocity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Angle";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1288, 22);
            this.panel2.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1288, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "PhysLab Magnetic Pendulum";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(990, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 20);
            this.label9.TabIndex = 2;
            this.label9.Text = "Time elapsed";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timeElapsedL
            // 
            this.timeElapsedL.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeElapsedL.Location = new System.Drawing.Point(1099, 38);
            this.timeElapsedL.Name = "timeElapsedL";
            this.timeElapsedL.Size = new System.Drawing.Size(103, 41);
            this.timeElapsedL.TabIndex = 2;
            this.timeElapsedL.Text = "0";
            this.timeElapsedL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1203, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "seconds";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button22
            // 
            this.button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button22.BackgroundImage = global::MagneticPendulum.Properties.Resources.MinimizeDim;
            this.button22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button22.FlatAppearance.BorderSize = 0;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.HoverBackgroundImage = global::MagneticPendulum.Properties.Resources.CloseHighlighte;
            this.button22.Location = new System.Drawing.Point(1287, 0);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(22, 22);
            this.button22.TabIndex = 13;
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button21
            // 
            this.button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button21.BackgroundImage = global::MagneticPendulum.Properties.Resources.CloseDim;
            this.button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button21.FlatAppearance.BorderSize = 0;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.HoverBackgroundImage = global::MagneticPendulum.Properties.Resources.CloseHighlighted;
            this.button21.Location = new System.Drawing.Point(1309, 0);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(22, 22);
            this.button21.TabIndex = 13;
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 92);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.splitContainer1.Panel1.Controls.Add(this.dvPlot);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.splitContainer1.Panel2.Controls.Add(this.phasePortraitP);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Size = new System.Drawing.Size(1131, 415);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 11;
            // 
            // dvPlot
            // 
            this.dvPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvPlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.dvPlot.Location = new System.Drawing.Point(3, 30);
            this.dvPlot.Name = "dvPlot";
            this.dvPlot.Size = new System.Drawing.Size(500, 376);
            this.dvPlot.TabIndex = 0;
            this.dvPlot.PhasePortraitRequest += new MagneticPendulum.GetPhasePortraitHandler(this.dvPlot_PhasePortraitRequest);
            this.dvPlot.ClearPhasePortraitRequest += new System.EventHandler(this.dvPlot_ClearPhasePortraitRequest);
            this.dvPlot.Load += new System.EventHandler(this.dvPlot_Load);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(169, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Time Trajectory";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // phasePortraitP
            // 
            this.phasePortraitP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.phasePortraitP.AutoScroll = true;
            this.phasePortraitP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.phasePortraitP.Location = new System.Drawing.Point(3, 30);
            this.phasePortraitP.Name = "phasePortraitP";
            this.phasePortraitP.Size = new System.Drawing.Size(478, 376);
            this.phasePortraitP.TabIndex = 0;
            this.phasePortraitP.UniqueXAxisStamps = true;
            this.phasePortraitP.XLabel = "x axis";
            this.phasePortraitP.YLabel = "y axis";
            this.phasePortraitP.SizeChanged += new System.EventHandler(this.phasePortraitP_SizeChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(214, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Phase Portrait";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minMaxLabel2
            // 
            this.minMaxLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxLabel2.Location = new System.Drawing.Point(565, 36);
            this.minMaxLabel2.MainFont = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxLabel2.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.minMaxLabel2.Max = 0D;
            this.minMaxLabel2.Min = 0D;
            this.minMaxLabel2.MinMaxFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxLabel2.Name = "minMaxLabel2";
            this.minMaxLabel2.Rounding = 1;
            this.minMaxLabel2.Size = new System.Drawing.Size(340, 44);
            this.minMaxLabel2.Suffix = "rad/s";
            this.minMaxLabel2.TabIndex = 1;
            this.minMaxLabel2.Value = 0D;
            // 
            // minMaxLabel1
            // 
            this.minMaxLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxLabel1.Location = new System.Drawing.Point(66, 36);
            this.minMaxLabel1.MainFont = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxLabel1.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.minMaxLabel1.Max = 0D;
            this.minMaxLabel1.Min = 0D;
            this.minMaxLabel1.MinMaxFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxLabel1.Name = "minMaxLabel1";
            this.minMaxLabel1.Rounding = 1;
            this.minMaxLabel1.Size = new System.Drawing.Size(283, 44);
            this.minMaxLabel1.Suffix = "°";
            this.minMaxLabel1.TabIndex = 0;
            this.minMaxLabel1.Value = 0D;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(1331, 659);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.button21);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.controlGP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeElapsedL);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.minMaxLabel2);
            this.Controls.Add(this.minMaxLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.angularFreqTrB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorHomeOffset)).EndInit();
            this.controlGP.ResumeLayout(false);
            this.controlGP.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar angularFreqTrB;
        private System.Windows.Forms.TextBox angularFreqTB;
        private System.Windows.Forms.Label label1;
        private FivePointNine.Windows.Controls.SerialChannelControl dataPort;
        private MagPendulumVisualizer magPendulumVisualizer1;
        private MinMaxLabel minMaxLabel1;
        private MinMaxLabel minMaxLabel2;
        private System.Windows.Forms.NumericUpDown motorHomeOffset;
        private System.Windows.Forms.Button applySpeed;
        private System.Windows.Forms.Timer timer1;
        private xyComboControl dvPlot;
        private xyPlotControl phasePortraitP;
        private SplitContainer2 splitContainer1;
        private System.Windows.Forms.Timer spEventPoll;
        private System.Windows.Forms.GroupBox controlGP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button stopB;
        private System.Windows.Forms.Button setAngleB;
        private System.Windows.Forms.Label label2;
        private Button2 button21;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private Button2 button22;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label timeElapsedL;
        private System.Windows.Forms.Label label11;
    }
}

