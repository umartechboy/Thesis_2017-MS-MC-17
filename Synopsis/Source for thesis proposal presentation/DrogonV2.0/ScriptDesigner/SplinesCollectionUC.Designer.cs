namespace SplineDesigner
{
    partial class SplinesCollectionUC
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
            this.button1 = new System.Windows.Forms.Button();
            this.splineCollectionBoard1 = new SplineDesigner.SplineCollectionBoard();
            this.saveSplinesB = new System.Windows.Forms.Button();
            this.openB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(385, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splineCollectionBoard1
            // 
            this.splineCollectionBoard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splineCollectionBoard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splineCollectionBoard1.Location = new System.Drawing.Point(3, 4);
            this.splineCollectionBoard1.Name = "splineCollectionBoard1";
            this.splineCollectionBoard1.Size = new System.Drawing.Size(376, 358);
            this.splineCollectionBoard1.TabIndex = 2;
            // 
            // saveSplinesB
            // 
            this.saveSplinesB.Location = new System.Drawing.Point(385, 89);
            this.saveSplinesB.Name = "saveSplinesB";
            this.saveSplinesB.Size = new System.Drawing.Size(75, 23);
            this.saveSplinesB.TabIndex = 1;
            this.saveSplinesB.Text = "Save";
            this.saveSplinesB.UseVisualStyleBackColor = true;
            this.saveSplinesB.Click += new System.EventHandler(this.saveSplinesB_Click);
            // 
            // openB
            // 
            this.openB.Location = new System.Drawing.Point(385, 118);
            this.openB.Name = "openB";
            this.openB.Size = new System.Drawing.Size(75, 23);
            this.openB.TabIndex = 1;
            this.openB.Text = "Open";
            this.openB.UseVisualStyleBackColor = true;
            this.openB.Click += new System.EventHandler(this.openB_Click);
            // 
            // SplinesCollectionUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splineCollectionBoard1);
            this.Controls.Add(this.openB);
            this.Controls.Add(this.saveSplinesB);
            this.Controls.Add(this.button1);
            this.Name = "SplinesCollectionUC";
            this.Size = new System.Drawing.Size(467, 365);
            this.ResumeLayout(false);

        }

        #endregion
                                                               
        private System.Windows.Forms.Button button1;
        private SplineCollectionBoard splineCollectionBoard1;
        private System.Windows.Forms.Button saveSplinesB;
        private System.Windows.Forms.Button openB;
    }
}
