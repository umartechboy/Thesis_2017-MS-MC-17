
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
            this.label4 = new System.Windows.Forms.Label();
            this.dpiNUD = new System.Windows.Forms.NumericUpDown();
            this.processB = new System.Windows.Forms.Button();
            this.SaveResults = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progP = new System.Windows.Forms.ToolStripProgressBar();
            this.progLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bWorker = new System.ComponentModel.BackgroundWorker();
            this.inkFromScene = new System.Windows.Forms.RadioButton();
            this.loadInkFromFile = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.summaryL = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.traceCB = new System.Windows.Forms.RadioButton();
            this.sourceCB = new System.Windows.Forms.RadioButton();
            this.extraCB = new System.Windows.Forms.RadioButton();
            this.missingCB = new System.Windows.Forms.RadioButton();
            this.highLightCB = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.imagesFL = new System.Windows.Forms.FlowLayoutPanel();
            this.loadImageB = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.previewPB = new System.Windows.Forms.PictureBox();
            this.saveCurrentImageB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dpiNUD)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPB)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(5, 658);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Rasterization DPI";
            // 
            // dpiNUD
            // 
            this.dpiNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dpiNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpiNUD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.dpiNUD.Location = new System.Drawing.Point(113, 686);
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
            this.dpiNUD.Size = new System.Drawing.Size(98, 30);
            this.dpiNUD.TabIndex = 4;
            this.dpiNUD.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.dpiNUD.ValueChanged += new System.EventHandler(this.dpiNUD_ValueChanged);
            // 
            // processB
            // 
            this.processB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processB.Location = new System.Drawing.Point(3, 725);
            this.processB.Name = "processB";
            this.processB.Size = new System.Drawing.Size(208, 34);
            this.processB.TabIndex = 10;
            this.processB.Text = "Process";
            this.processB.UseVisualStyleBackColor = true;
            this.processB.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveResults
            // 
            this.SaveResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveResults.Location = new System.Drawing.Point(3, 765);
            this.SaveResults.Name = "SaveResults";
            this.SaveResults.Size = new System.Drawing.Size(208, 34);
            this.SaveResults.TabIndex = 10;
            this.SaveResults.Text = "Save Results";
            this.SaveResults.UseVisualStyleBackColor = true;
            this.SaveResults.Click += new System.EventHandler(this.SaveResults_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progP,
            this.progLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 817);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1113, 26);
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
            // inkFromScene
            // 
            this.inkFromScene.AutoSize = true;
            this.inkFromScene.Checked = true;
            this.inkFromScene.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.inkFromScene.Location = new System.Drawing.Point(17, 21);
            this.inkFromScene.Name = "inkFromScene";
            this.inkFromScene.Size = new System.Drawing.Size(160, 29);
            this.inkFromScene.TabIndex = 14;
            this.inkFromScene.TabStop = true;
            this.inkFromScene.Text = "Current Scene";
            this.inkFromScene.UseVisualStyleBackColor = true;
            // 
            // loadInkFromFile
            // 
            this.loadInkFromFile.AutoSize = true;
            this.loadInkFromFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.loadInkFromFile.Location = new System.Drawing.Point(17, 56);
            this.loadInkFromFile.Name = "loadInkFromFile";
            this.loadInkFromFile.Size = new System.Drawing.Size(149, 29);
            this.loadInkFromFile.TabIndex = 14;
            this.loadInkFromFile.Text = "Load from file";
            this.loadInkFromFile.UseVisualStyleBackColor = true;
            this.loadInkFromFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.loadInkFromFile_MouseClick);
            this.loadInkFromFile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loadInkFromFile_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.loadInkFromFile);
            this.groupBox2.Controls.Add(this.inkFromScene);
            this.groupBox2.Location = new System.Drawing.Point(3, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ink";
            // 
            // summaryL
            // 
            this.summaryL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.summaryL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.summaryL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summaryL.Location = new System.Drawing.Point(6, 18);
            this.summaryL.Name = "summaryL";
            this.summaryL.Size = new System.Drawing.Size(198, 151);
            this.summaryL.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.traceCB);
            this.groupBox1.Controls.Add(this.sourceCB);
            this.groupBox1.Controls.Add(this.extraCB);
            this.groupBox1.Controls.Add(this.missingCB);
            this.groupBox1.Controls.Add(this.highLightCB);
            this.groupBox1.Location = new System.Drawing.Point(3, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 201);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View";
            // 
            // traceCB
            // 
            this.traceCB.AutoSize = true;
            this.traceCB.Checked = true;
            this.traceCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.traceCB.Location = new System.Drawing.Point(6, 21);
            this.traceCB.Name = "traceCB";
            this.traceCB.Size = new System.Drawing.Size(84, 29);
            this.traceCB.TabIndex = 7;
            this.traceCB.TabStop = true;
            this.traceCB.Text = "Trace";
            this.traceCB.UseVisualStyleBackColor = true;
            this.traceCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // sourceCB
            // 
            this.sourceCB.AutoSize = true;
            this.sourceCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceCB.Location = new System.Drawing.Point(6, 56);
            this.sourceCB.Name = "sourceCB";
            this.sourceCB.Size = new System.Drawing.Size(96, 29);
            this.sourceCB.TabIndex = 7;
            this.sourceCB.Text = "Source";
            this.sourceCB.UseVisualStyleBackColor = true;
            this.sourceCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // extraCB
            // 
            this.extraCB.AutoSize = true;
            this.extraCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extraCB.Location = new System.Drawing.Point(6, 91);
            this.extraCB.Name = "extraCB";
            this.extraCB.Size = new System.Drawing.Size(78, 29);
            this.extraCB.TabIndex = 7;
            this.extraCB.Text = "Extra";
            this.extraCB.UseVisualStyleBackColor = true;
            this.extraCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // missingCB
            // 
            this.missingCB.AutoSize = true;
            this.missingCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missingCB.Location = new System.Drawing.Point(6, 126);
            this.missingCB.Name = "missingCB";
            this.missingCB.Size = new System.Drawing.Size(100, 29);
            this.missingCB.TabIndex = 7;
            this.missingCB.Text = "Missing";
            this.missingCB.UseVisualStyleBackColor = true;
            this.missingCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // highLightCB
            // 
            this.highLightCB.AutoSize = true;
            this.highLightCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highLightCB.Location = new System.Drawing.Point(6, 161);
            this.highLightCB.Name = "highLightCB";
            this.highLightCB.Size = new System.Drawing.Size(108, 29);
            this.highLightCB.TabIndex = 7;
            this.highLightCB.Text = "Highlight";
            this.highLightCB.UseVisualStyleBackColor = true;
            this.highLightCB.CheckedChanged += new System.EventHandler(this.previewModeCBs_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.imagesFL);
            this.groupBox3.Controls.Add(this.loadImageB);
            this.groupBox3.Location = new System.Drawing.Point(3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 166);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reference";
            // 
            // imagesFL
            // 
            this.imagesFL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagesFL.AutoScroll = true;
            this.imagesFL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagesFL.Location = new System.Drawing.Point(7, 22);
            this.imagesFL.Margin = new System.Windows.Forms.Padding(4);
            this.imagesFL.Name = "imagesFL";
            this.imagesFL.Size = new System.Drawing.Size(200, 91);
            this.imagesFL.TabIndex = 1;
            // 
            // loadImageB
            // 
            this.loadImageB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadImageB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadImageB.Location = new System.Drawing.Point(37, 120);
            this.loadImageB.Name = "loadImageB";
            this.loadImageB.Size = new System.Drawing.Size(171, 34);
            this.loadImageB.TabIndex = 10;
            this.loadImageB.Text = "Add from file";
            this.loadImageB.UseVisualStyleBackColor = true;
            this.loadImageB.Click += new System.EventHandler(this.loadImageB_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.SaveResults);
            this.panel1.Controls.Add(this.processB);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dpiNUD);
            this.panel1.Location = new System.Drawing.Point(884, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 802);
            this.panel1.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.summaryL);
            this.groupBox4.Location = new System.Drawing.Point(3, 483);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 172);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "View";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.transparentBack;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.previewPB);
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(877, 752);
            this.panel2.TabIndex = 2;
            // 
            // previewPB
            // 
            this.previewPB.BackColor = System.Drawing.Color.Transparent;
            this.previewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPB.Location = new System.Drawing.Point(3, 3);
            this.previewPB.Name = "previewPB";
            this.previewPB.Size = new System.Drawing.Size(100, 50);
            this.previewPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.previewPB.TabIndex = 0;
            this.previewPB.TabStop = false;
            // 
            // saveCurrentImageB
            // 
            this.saveCurrentImageB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveCurrentImageB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveCurrentImageB.Location = new System.Drawing.Point(670, 763);
            this.saveCurrentImageB.Name = "saveCurrentImageB";
            this.saveCurrentImageB.Size = new System.Drawing.Size(208, 34);
            this.saveCurrentImageB.TabIndex = 10;
            this.saveCurrentImageB.Text = "Save Current Image";
            this.saveCurrentImageB.UseVisualStyleBackColor = true;
            this.saveCurrentImageB.Click += new System.EventHandler(this.saveCurrentB_Click);
            // 
            // TraceAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 843);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.saveCurrentImageB);
            this.Controls.Add(this.panel2);
            this.Name = "TraceAnalyzer";
            this.Text = "TraceAnalyzer";
            ((System.ComponentModel.ISupportInitialize)(this.dpiNUD)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox previewPB;
        private System.Windows.Forms.NumericUpDown dpiNUD;
        private System.Windows.Forms.Button processB;
        private System.Windows.Forms.Button SaveResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progP;
        private System.Windows.Forms.ToolStripStatusLabel progLabel;
        private System.ComponentModel.BackgroundWorker bWorker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton inkFromScene;
        private System.Windows.Forms.RadioButton loadInkFromFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label summaryL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton traceCB;
        private System.Windows.Forms.RadioButton sourceCB;
        private System.Windows.Forms.RadioButton extraCB;
        private System.Windows.Forms.RadioButton missingCB;
        private System.Windows.Forms.RadioButton highLightCB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel imagesFL;
        private System.Windows.Forms.Button loadImageB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button saveCurrentImageB;
    }
}