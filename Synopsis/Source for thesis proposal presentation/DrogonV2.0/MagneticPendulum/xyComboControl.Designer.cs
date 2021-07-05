namespace MagneticPendulum
{
    partial class xyComboControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xyComboControl));
            this.autoScrollCB = new System.Windows.Forms.CheckBox();
            this.clearAllB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.saveB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer21 = new MagneticPendulum.SplitContainer2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dPlot = new MagneticPendulum.xyPlot();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.vPlot = new MagneticPendulum.xyPlot();
            this.saveDurationNUD = new MagneticPendulum.FlatNumericUpDown();
            this.recordDurationNUD = new MagneticPendulum.FlatNumericUpDown();
            this.recordButton1 = new MagneticPendulum.RecordButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer21)).BeginInit();
            this.splitContainer21.Panel1.SuspendLayout();
            this.splitContainer21.Panel2.SuspendLayout();
            this.splitContainer21.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoScrollCB
            // 
            this.autoScrollCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.autoScrollCB.AutoSize = true;
            this.autoScrollCB.Checked = true;
            this.autoScrollCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoScrollCB.Location = new System.Drawing.Point(493, 316);
            this.autoScrollCB.Name = "autoScrollCB";
            this.autoScrollCB.Size = new System.Drawing.Size(105, 24);
            this.autoScrollCB.TabIndex = 2;
            this.autoScrollCB.Text = "Auto Scroll";
            this.autoScrollCB.UseVisualStyleBackColor = true;
            this.autoScrollCB.CheckedChanged += new System.EventHandler(this.autoScrollCB_CheckedChanged);
            // 
            // clearAllB
            // 
            this.clearAllB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearAllB.BackColor = System.Drawing.Color.LightGray;
            this.clearAllB.FlatAppearance.BorderSize = 0;
            this.clearAllB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearAllB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearAllB.Location = new System.Drawing.Point(5, 350);
            this.clearAllB.Name = "clearAllB";
            this.clearAllB.Size = new System.Drawing.Size(111, 30);
            this.clearAllB.TabIndex = 11;
            this.clearAllB.Text = "Clear All";
            this.clearAllB.UseVisualStyleBackColor = false;
            this.clearAllB.Click += new System.EventHandler(this.clearAllB_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(491, 29);
            this.label3.TabIndex = 12;
            this.label3.Text = "Time (s)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveB
            // 
            this.saveB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveB.BackColor = System.Drawing.Color.LightGray;
            this.saveB.FlatAppearance.BorderSize = 0;
            this.saveB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveB.Location = new System.Drawing.Point(280, 384);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(111, 30);
            this.saveB.TabIndex = 11;
            this.saveB.Text = "Save Last: ";
            this.saveB.UseVisualStyleBackColor = false;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(158, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "seconds";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(442, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "seconds";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer21
            // 
            this.splitContainer21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer21.Location = new System.Drawing.Point(2, 2);
            this.splitContainer21.Name = "splitContainer21";
            this.splitContainer21.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer21.Panel1
            // 
            this.splitContainer21.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer21.Panel2
            // 
            this.splitContainer21.Panel2.Controls.Add(this.panel2);
            this.splitContainer21.Size = new System.Drawing.Size(596, 311);
            this.splitContainer21.SplitterDistance = 154;
            this.splitContainer21.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.dPlot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 154);
            this.panel1.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(537, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 43);
            this.label8.TabIndex = 15;
            this.label8.Text = "ϴ\r\n(°)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dPlot
            // 
            this.dPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dPlot.AutoScroll = true;
            this.dPlot.DataPoints = ((System.Collections.Generic.List<System.Drawing.PointF>)(resources.GetObject("dPlot.DataPoints")));
            this.dPlot.LastPointHighlight = false;
            this.dPlot.LineColor = System.Drawing.Color.Maroon;
            this.dPlot.LineOpacity = 1F;
            this.dPlot.LineThickness = 3F;
            this.dPlot.Location = new System.Drawing.Point(3, 3);
            this.dPlot.MaxX = ((System.Drawing.PointF)(resources.GetObject("dPlot.MaxX")));
            this.dPlot.MaxY = ((System.Drawing.PointF)(resources.GetObject("dPlot.MaxY")));
            this.dPlot.MinX = ((System.Drawing.PointF)(resources.GetObject("dPlot.MinX")));
            this.dPlot.MinY = ((System.Drawing.PointF)(resources.GetObject("dPlot.MinY")));
            this.dPlot.Name = "dPlot";
            this.dPlot.Size = new System.Drawing.Size(531, 151);
            this.dPlot.TabIndex = 3;
            this.dPlot.UniqueXAxisStamps = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.vPlot);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(596, 153);
            this.panel2.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(556, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 35);
            this.label6.TabIndex = 14;
            this.label6.Text = ".";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(537, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 43);
            this.label7.TabIndex = 13;
            this.label7.Text = "ϴ\r\n(rad/s)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vPlot
            // 
            this.vPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vPlot.AutoScroll = true;
            this.vPlot.DataPoints = ((System.Collections.Generic.List<System.Drawing.PointF>)(resources.GetObject("vPlot.DataPoints")));
            this.vPlot.LastPointHighlight = false;
            this.vPlot.LineColor = System.Drawing.Color.Navy;
            this.vPlot.LineOpacity = 1F;
            this.vPlot.LineThickness = 3F;
            this.vPlot.Location = new System.Drawing.Point(3, 3);
            this.vPlot.MaxX = ((System.Drawing.PointF)(resources.GetObject("vPlot.MaxX")));
            this.vPlot.MaxY = ((System.Drawing.PointF)(resources.GetObject("vPlot.MaxY")));
            this.vPlot.MinX = ((System.Drawing.PointF)(resources.GetObject("vPlot.MinX")));
            this.vPlot.MinY = ((System.Drawing.PointF)(resources.GetObject("vPlot.MinY")));
            this.vPlot.Name = "vPlot";
            this.vPlot.Size = new System.Drawing.Size(531, 150);
            this.vPlot.TabIndex = 3;
            this.vPlot.UniqueXAxisStamps = true;
            // 
            // saveDurationNUD
            // 
            this.saveDurationNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveDurationNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveDurationNUD.Location = new System.Drawing.Point(397, 377);
            this.saveDurationNUD.Maximum = 180;
            this.saveDurationNUD.Minimum = 0;
            this.saveDurationNUD.Name = "saveDurationNUD";
            this.saveDurationNUD.Size = new System.Drawing.Size(41, 48);
            this.saveDurationNUD.TabIndex = 15;
            this.saveDurationNUD.Value = 3;
            // 
            // recordDurationNUD
            // 
            this.recordDurationNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recordDurationNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordDurationNUD.Location = new System.Drawing.Point(119, 376);
            this.recordDurationNUD.Maximum = 180;
            this.recordDurationNUD.Minimum = 1;
            this.recordDurationNUD.Name = "recordDurationNUD";
            this.recordDurationNUD.Size = new System.Drawing.Size(41, 48);
            this.recordDurationNUD.TabIndex = 15;
            this.recordDurationNUD.Value = 60;
            this.recordDurationNUD.ValueChanged += new MagneticPendulum.ValueChangeHandler(this.recordDurationNUD_ValueChanged);
            // 
            // recordButton1
            // 
            this.recordButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recordButton1.BackColor = System.Drawing.Color.LightGray;
            this.recordButton1.FlatAppearance.BorderSize = 0;
            this.recordButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recordButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordButton1.Location = new System.Drawing.Point(5, 386);
            this.recordButton1.Name = "recordButton1";
            this.recordButton1.RecordingState = false;
            this.recordButton1.Size = new System.Drawing.Size(111, 29);
            this.recordButton1.TabIndex = 14;
            this.recordButton1.Text = "Record";
            this.recordButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.recordButton1.UseVisualStyleBackColor = false;
            this.recordButton1.RecordingStateChanged += new MagneticPendulum.RecordingStateChangeHandler(this.recordButton1_RecordingStateChanged);
            this.recordButton1.Click += new System.EventHandler(this.recordButton1_Click);
            // 
            // xyComboControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer21);
            this.Controls.Add(this.saveDurationNUD);
            this.Controls.Add(this.recordDurationNUD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.recordButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveB);
            this.Controls.Add(this.clearAllB);
            this.Controls.Add(this.autoScrollCB);
            this.Name = "xyComboControl";
            this.Size = new System.Drawing.Size(601, 426);
            this.Load += new System.EventHandler(this.xyComboControl_Load);
            this.SizeChanged += new System.EventHandler(this.ComboTimePlot_SizeChanged);
            this.splitContainer21.Panel1.ResumeLayout(false);
            this.splitContainer21.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer21)).EndInit();
            this.splitContainer21.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox autoScrollCB;
        public xyPlot dPlot;
        private System.Windows.Forms.Button clearAllB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        public xyPlot vPlot;
        private System.Windows.Forms.Button saveB;
        private MagneticPendulum.RecordButton recordButton1;
        private FlatNumericUpDown recordDurationNUD;
        private System.Windows.Forms.Label label4;
        private FlatNumericUpDown saveDurationNUD;
        private System.Windows.Forms.Label label5;
        private SplitContainer2 splitContainer21;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
