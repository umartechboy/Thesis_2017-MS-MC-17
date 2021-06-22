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
            this.activeTC = new RotatingBezierSplineEditor.ToolControl();
            this.visibleTC = new RotatingBezierSplineEditor.ToolControl();
            this.appearnceP = new System.Windows.Forms.Panel();
            this.delP = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // prevP
            // 
            this.prevP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prevP.Location = new System.Drawing.Point(0, 0);
            this.prevP.Margin = new System.Windows.Forms.Padding(2);
            this.prevP.Name = "prevP";
            this.prevP.Size = new System.Drawing.Size(75, 81);
            this.prevP.TabIndex = 0;
            // 
            // activeTC
            // 
            this.activeTC.Active = true;
            this.activeTC.DistinctSelection = false;
            this.activeTC.Location = new System.Drawing.Point(79, 42);
            this.activeTC.Margin = new System.Windows.Forms.Padding(2);
            this.activeTC.Name = "activeTC";
            this.activeTC.Size = new System.Drawing.Size(33, 36);
            this.activeTC.TabIndex = 1;
            this.activeTC.OnActivated += new System.EventHandler(this.activeTC_OnActivated);
            // 
            // visibleTC
            // 
            this.visibleTC.Active = true;
            this.visibleTC.DistinctSelection = false;
            this.visibleTC.Location = new System.Drawing.Point(79, 2);
            this.visibleTC.Margin = new System.Windows.Forms.Padding(2);
            this.visibleTC.Name = "visibleTC";
            this.visibleTC.Size = new System.Drawing.Size(33, 36);
            this.visibleTC.TabIndex = 1;
            this.visibleTC.OnActivated += new System.EventHandler(this.visibleTC_OnActivated);
            // 
            // appearnceP
            // 
            this.appearnceP.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.pen;
            this.appearnceP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.appearnceP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appearnceP.Location = new System.Drawing.Point(116, 3);
            this.appearnceP.Name = "appearnceP";
            this.appearnceP.Size = new System.Drawing.Size(33, 36);
            this.appearnceP.TabIndex = 2;
            this.appearnceP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.appearnceP_MouseClick);
            // 
            // delP
            // 
            this.delP.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.delete;
            this.delP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.delP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delP.Location = new System.Drawing.Point(116, 42);
            this.delP.Name = "delP";
            this.delP.Size = new System.Drawing.Size(33, 36);
            this.delP.TabIndex = 2;
            this.delP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.delP_MouseClick);
            // 
            // CurveMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.appearnceP);
            this.Controls.Add(this.delP);
            this.Controls.Add(this.activeTC);
            this.Controls.Add(this.visibleTC);
            this.Controls.Add(this.prevP);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CurveMenuItem";
            this.Size = new System.Drawing.Size(152, 81);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel prevP;
        private ToolControl visibleTC;
        private ToolControl activeTC;
        private System.Windows.Forms.Panel delP;
        private System.Windows.Forms.Panel appearnceP;
    }
}
