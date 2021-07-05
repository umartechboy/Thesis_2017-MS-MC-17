namespace RoboSim
{
    partial class PanelPlotForm: ShowCloseForm
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
            this.ZoomTB = new System.Windows.Forms.TrackBar();
            this.dispP = new RoboSim.dbPanel();
            this.saveB = new System.Windows.Forms.Button();
            this.saveFileTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomTB)).BeginInit();
            this.SuspendLayout();
            // 
            // ZoomTB
            // 
            this.ZoomTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomTB.Location = new System.Drawing.Point(359, 2);
            this.ZoomTB.Maximum = 3000;
            this.ZoomTB.Minimum = 10;
            this.ZoomTB.Name = "ZoomTB";
            this.ZoomTB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ZoomTB.Size = new System.Drawing.Size(45, 322);
            this.ZoomTB.TabIndex = 8;
            this.ZoomTB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ZoomTB.Value = 200;
            // 
            // dispP
            // 
            this.dispP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dispP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.dispP.Location = new System.Drawing.Point(3, 2);
            this.dispP.Name = "dispP";
            this.dispP.Size = new System.Drawing.Size(350, 302);
            this.dispP.TabIndex = 7;
            // 
            // saveB
            // 
            this.saveB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveB.Location = new System.Drawing.Point(275, 305);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(75, 20);
            this.saveB.TabIndex = 9;
            this.saveB.Text = "Save";
            this.saveB.UseVisualStyleBackColor = true;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // saveFileTB
            // 
            this.saveFileTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveFileTB.Location = new System.Drawing.Point(3, 305);
            this.saveFileTB.Name = "saveFileTB";
            this.saveFileTB.Size = new System.Drawing.Size(266, 20);
            this.saveFileTB.TabIndex = 10;
            this.saveFileTB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.saveFileTB_MouseDoubleClick);
            // 
            // PanelPlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 326);
            this.Controls.Add(this.saveFileTB);
            this.Controls.Add(this.saveB);
            this.Controls.Add(this.ZoomTB);
            this.Controls.Add(this.dispP);
            this.Name = "PanelPlotForm";
            this.Text = "RobotTrackTopView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PanelPlotForm_FormClosing);
            this.Load += new System.EventHandler(this.PanelPlotForm_Load);
            this.Shown += new System.EventHandler(this.PanelPlotForm_Shown);
            this.Resize += new System.EventHandler(this.form_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomTB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar ZoomTB;
        public dbPanel dispP;
        private System.Windows.Forms.Button saveB;
        private System.Windows.Forms.TextBox saveFileTB;
    }
}