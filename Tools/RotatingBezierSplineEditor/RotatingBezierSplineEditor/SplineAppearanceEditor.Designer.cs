
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
            this.label1 = new System.Windows.Forms.Label();
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
            this.widthTB.Scroll += new System.EventHandler(this.widthTB_Scroll);
            this.widthTB.ValueChanged += new System.EventHandler(this.widthTB_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.widthTB);
            this.panel1.Controls.Add(this.colorP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 56);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "--";
            // 
            // SplineAppearanceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 56);
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
        private System.Windows.Forms.Label label1;
    }
}