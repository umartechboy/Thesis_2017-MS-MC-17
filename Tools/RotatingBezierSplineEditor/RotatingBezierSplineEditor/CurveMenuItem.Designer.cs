namespace RotatingBezierSplineEditor
{
    partial class CurveMenuItem
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
            this.prevP = new System.Windows.Forms.Panel();
            this.visibleTC = new RotatingBezierSplineEditor.ToolControl();
            this.activeTC = new RotatingBezierSplineEditor.ToolControl();
            this.SuspendLayout();
            // 
            // prevP
            // 
            this.prevP.Location = new System.Drawing.Point(0, 0);
            this.prevP.Name = "prevP";
            this.prevP.Size = new System.Drawing.Size(100, 100);
            this.prevP.TabIndex = 0;
            // 
            // visibleTC
            // 
            this.visibleTC.Active = false;
            this.visibleTC.DistinctSelection = true;
            this.visibleTC.Location = new System.Drawing.Point(103, 2);
            this.visibleTC.Name = "visibleTC";
            this.visibleTC.Size = new System.Drawing.Size(44, 44);
            this.visibleTC.TabIndex = 1;
            // 
            // activeTC
            // 
            this.activeTC.Active = false;
            this.activeTC.DistinctSelection = true;
            this.activeTC.Location = new System.Drawing.Point(153, 2);
            this.activeTC.Name = "activeTC";
            this.activeTC.Size = new System.Drawing.Size(44, 44);
            this.activeTC.TabIndex = 1;
            // 
            // CurveMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.activeTC);
            this.Controls.Add(this.visibleTC);
            this.Controls.Add(this.prevP);
            this.Name = "CurveMenuItem";
            this.Size = new System.Drawing.Size(202, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel prevP;
        private ToolControl visibleTC;
        private ToolControl activeTC;
    }
}
