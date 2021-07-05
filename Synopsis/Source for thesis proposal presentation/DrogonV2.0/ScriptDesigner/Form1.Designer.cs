namespace SplineDesigner
{
    partial class SplineEditorForm
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
            this.splineEditor1 = new SplineDesigner.SplineEditor();
            this.SuspendLayout();
            // 
            // splineEditor1
            // 
            this.splineEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splineEditor1.Location = new System.Drawing.Point(0, 0);
            this.splineEditor1.Name = "splineEditor1";
            this.splineEditor1.Size = new System.Drawing.Size(484, 368);
            this.splineEditor1.TabIndex = 0;
            // 
            // SplineEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 368);
            this.Controls.Add(this.splineEditor1);
            this.Name = "SplineEditorForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public SplineEditor splineEditor1;
    }
}

