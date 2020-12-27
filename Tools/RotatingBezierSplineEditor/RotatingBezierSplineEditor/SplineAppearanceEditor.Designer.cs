
namespace RotatingBezierSplineEditor
{
    partial class SplineAppearanceEditor
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
            this.colorP = new System.Windows.Forms.Panel();
            this.widthTB = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.widthTB)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorP
            // 
            this.colorP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.colorP.Location = new System.Drawing.Point(3, 3);
            this.colorP.Name = "colorP";
            this.colorP.Size = new System.Drawing.Size(105, 48);
            this.colorP.TabIndex = 0;
            this.colorP.Click += new System.EventHandler(this.colorP_Click);
            // 
            // widthTB
            // 
            this.widthTB.Location = new System.Drawing.Point(114, 11);
            this.widthTB.Maximum = 500;
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(205, 56);
            this.widthTB.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.widthTB);
            this.panel1.Controls.Add(this.colorP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 56);
            this.panel1.TabIndex = 0;
            // 
            // SplineAppearanceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 56);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplineAppearanceEditor";
            this.Text = "Edit spline appearance";
            ((System.ComponentModel.ISupportInitialize)(this.widthTB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel colorP;
        public System.Windows.Forms.TrackBar widthTB;
        private System.Windows.Forms.Panel panel1;
    }
}