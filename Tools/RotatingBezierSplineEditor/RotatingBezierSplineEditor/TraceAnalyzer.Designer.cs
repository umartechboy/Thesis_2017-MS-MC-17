
namespace RotatingBezierSplineEditor
{
    partial class TraceAnalyzer
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadInkB = new System.Windows.Forms.Button();
            this.summaryL = new System.Windows.Forms.Label();
            this.highLightCB = new System.Windows.Forms.RadioButton();
            this.loadSourceB = new System.Windows.Forms.Button();
            this.missingCB = new System.Windows.Forms.RadioButton();
            this.extraCB = new System.Windows.Forms.RadioButton();
            this.sourceCB = new System.Windows.Forms.RadioButton();
            this.traceCB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dpiNUD = new System.Windows.Forms.NumericUpDown();
            this.processB = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progP = new System.Windows.Forms.ToolStripProgressBar();
            this.progLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bWorker = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpiNUD)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(895, 802);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.loadInkB);
            this.panel1.Controls.Add(this.summaryL);
            this.panel1.Controls.Add(this.highLightCB);
            this.panel1.Controls.Add(this.loadSourceB);
            this.panel1.Controls.Add(this.missingCB);
            this.panel1.Controls.Add(this.extraCB);
            this.panel1.Controls.Add(this.sourceCB);
            this.panel1.Controls.Add(this.traceCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dpiNUD);
            this.panel1.Location = new System.Drawing.Point(902, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 506);
            this.panel1.TabIndex = 9;
            // 
            // loadInkB
            // 
            this.loadInkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadInkB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadInkB.Location = new System.Drawing.Point(3, 49);
            this.loadInkB.Name = "loadInkB";
            this.loadInkB.Size = new System.Drawing.Size(171, 34);
            this.loadInkB.TabIndex = 10;
            this.loadInkB.Text = "Load Ink";
            this.loadInkB.UseVisualStyleBackColor = true;
            this.loadInkB.Click += new System.EventHandler(this.loadInkB_Click);
            // 
            // summaryL
            // 
            this.summaryL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summaryL.Location = new System.Drawing.Point(0, 332);
            this.summaryL.Name = "summaryL";
            this.summaryL.Size = new System.Drawing.Size(177, 174);
            this.summaryL.TabIndex = 11;
            // 
            // highLightCB
            // 
            this.highLightCB.AutoSize = true;
            this.highLightCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highLightCB.Location = new System.Drawing.Point(0, 239);
            this.highLightCB.Name = "highLightCB";
            this.highLightCB.Size = new System.Drawing.Size(108, 29);
            this.highLightCB.TabIndex = 7;
            this.highLightCB.Text = "Highlight";
            this.highLightCB.UseVisualStyleBackColor = true;
            this.highLightCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // loadSourceB
            // 
            this.loadSourceB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadSourceB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadSourceB.Location = new System.Drawing.Point(3, 9);
            this.loadSourceB.Name = "loadSourceB";
            this.loadSourceB.Size = new System.Drawing.Size(171, 34);
            this.loadSourceB.TabIndex = 10;
            this.loadSourceB.Text = "Load Source";
            this.loadSourceB.UseVisualStyleBackColor = true;
            this.loadSourceB.Click += new System.EventHandler(this.loadSourceB_Click);
            // 
            // missingCB
            // 
            this.missingCB.AutoSize = true;
            this.missingCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missingCB.Location = new System.Drawing.Point(0, 204);
            this.missingCB.Name = "missingCB";
            this.missingCB.Size = new System.Drawing.Size(100, 29);
            this.missingCB.TabIndex = 7;
            this.missingCB.Text = "Missing";
            this.missingCB.UseVisualStyleBackColor = true;
            this.missingCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // extraCB
            // 
            this.extraCB.AutoSize = true;
            this.extraCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extraCB.Location = new System.Drawing.Point(0, 169);
            this.extraCB.Name = "extraCB";
            this.extraCB.Size = new System.Drawing.Size(78, 29);
            this.extraCB.TabIndex = 7;
            this.extraCB.Text = "Extra";
            this.extraCB.UseVisualStyleBackColor = true;
            this.extraCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // sourceCB
            // 
            this.sourceCB.AutoSize = true;
            this.sourceCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceCB.Location = new System.Drawing.Point(0, 134);
            this.sourceCB.Name = "sourceCB";
            this.sourceCB.Size = new System.Drawing.Size(96, 29);
            this.sourceCB.TabIndex = 7;
            this.sourceCB.Text = "Source";
            this.sourceCB.UseVisualStyleBackColor = true;
            this.sourceCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // traceCB
            // 
            this.traceCB.AutoSize = true;
            this.traceCB.Checked = true;
            this.traceCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traceCB.Location = new System.Drawing.Point(0, 99);
            this.traceCB.Name = "traceCB";
            this.traceCB.Size = new System.Drawing.Size(84, 29);
            this.traceCB.TabIndex = 7;
            this.traceCB.TabStop = true;
            this.traceCB.Text = "Trace";
            this.traceCB.UseVisualStyleBackColor = true;
            this.traceCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "DPI";
            // 
            // dpiNUD
            // 
            this.dpiNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpiNUD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.dpiNUD.Location = new System.Drawing.Point(54, 291);
            this.dpiNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.dpiNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dpiNUD.Name = "dpiNUD";
            this.dpiNUD.Size = new System.Drawing.Size(120, 30);
            this.dpiNUD.TabIndex = 4;
            this.dpiNUD.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.dpiNUD.ValueChanged += new System.EventHandler(this.dpiNUD_ValueChanged);
            // 
            // processB
            // 
            this.processB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.processB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processB.Location = new System.Drawing.Point(905, 769);
            this.processB.Name = "processB";
            this.processB.Size = new System.Drawing.Size(171, 34);
            this.processB.TabIndex = 10;
            this.processB.Text = "Process";
            this.processB.UseVisualStyleBackColor = true;
            this.processB.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(905, 729);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 34);
            this.button2.TabIndex = 10;
            this.button2.Text = "Save Current";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progP,
            this.progLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 867);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1091, 26);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progP
            // 
            this.progP.Name = "progP";
            this.progP.Size = new System.Drawing.Size(100, 18);
            // 
            // progLabel
            // 
            this.progLabel.Name = "progLabel";
            this.progLabel.Size = new System.Drawing.Size(151, 20);
            this.progLabel.Text = "toolStripStatusLabel1";
            // 
            // bWorker
            // 
            this.bWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWorker_DoWork);
            this.bWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWorker_ProgressChanged);
            this.bWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bWorker_RunWorkerCompleted);
            // 
            // TraceAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 893);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.processB);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "TraceAnalyzer";
            this.Text = "TraceAnalyzer";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpiNUD)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton highLightCB;
        private System.Windows.Forms.RadioButton missingCB;
        private System.Windows.Forms.RadioButton extraCB;
        private System.Windows.Forms.RadioButton sourceCB;
        private System.Windows.Forms.RadioButton traceCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown dpiNUD;
        private System.Windows.Forms.Button processB;
        private System.Windows.Forms.Button loadSourceB;
        private System.Windows.Forms.Button loadInkB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label summaryL;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progP;
        private System.Windows.Forms.ToolStripStatusLabel progLabel;
        private System.ComponentModel.BackgroundWorker bWorker;
    }
}