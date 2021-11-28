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
            this.components = new System.ComponentModel.Container();
            this.prevP = new System.Windows.Forms.Panel();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlockAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appearnceP = new System.Windows.Forms.Panel();
            this.delP = new System.Windows.Forms.Panel();
            this.SplineEnabled = new RotatingBezierSplineEditor.ToolControl();
            this.SplineVisible = new RotatingBezierSplineEditor.ToolControl();
            this.SplineLabel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // prevP
            // 
            this.prevP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prevP.ContextMenuStrip = this.cms;
            this.prevP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevP.Location = new System.Drawing.Point(0, 0);
            this.prevP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.prevP.Name = "prevP";
            this.prevP.Size = new System.Drawing.Size(99, 99);
            this.prevP.TabIndex = 0;
            this.prevP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.prevP_MouseClick);
            this.prevP.MouseEnter += new System.EventHandler(this.prevP_MouseEnter);
            this.prevP.MouseLeave += new System.EventHandler(this.prevP_MouseLeave);
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAllToolStripMenuItem,
            this.unlockAllToolStripMenuItem,
            this.showOnlyToolStripMenuItem,
            this.lockAllButThisToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(182, 100);
            // 
            // showAllToolStripMenuItem
            // 
            this.showAllToolStripMenuItem.Name = "showAllToolStripMenuItem";
            this.showAllToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.showAllToolStripMenuItem.Text = "Show All";
            this.showAllToolStripMenuItem.Click += new System.EventHandler(this.showAllToolStripMenuItem_Click);
            // 
            // unlockAllToolStripMenuItem
            // 
            this.unlockAllToolStripMenuItem.Name = "unlockAllToolStripMenuItem";
            this.unlockAllToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.unlockAllToolStripMenuItem.Text = "Unlock All";
            this.unlockAllToolStripMenuItem.Click += new System.EventHandler(this.unlockAllToolStripMenuItem_Click);
            // 
            // showOnlyToolStripMenuItem
            // 
            this.showOnlyToolStripMenuItem.Name = "showOnlyToolStripMenuItem";
            this.showOnlyToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.showOnlyToolStripMenuItem.Text = "Show Only";
            this.showOnlyToolStripMenuItem.Click += new System.EventHandler(this.showOnlyToolStripMenuItem_Click);
            // 
            // lockAllButThisToolStripMenuItem
            // 
            this.lockAllButThisToolStripMenuItem.Name = "lockAllButThisToolStripMenuItem";
            this.lockAllButThisToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.lockAllButThisToolStripMenuItem.Text = "Lock all but this";
            this.lockAllButThisToolStripMenuItem.Click += new System.EventHandler(this.lockAllButThisToolStripMenuItem_Click);
            // 
            // appearnceP
            // 
            this.appearnceP.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.pen;
            this.appearnceP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.appearnceP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appearnceP.Location = new System.Drawing.Point(189, 52);
            this.appearnceP.Margin = new System.Windows.Forms.Padding(4);
            this.appearnceP.Name = "appearnceP";
            this.appearnceP.Size = new System.Drawing.Size(44, 44);
            this.appearnceP.TabIndex = 2;
            this.appearnceP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.appearnceP_MouseClick);
            // 
            // delP
            // 
            this.delP.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.delete;
            this.delP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.delP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delP.Location = new System.Drawing.Point(233, 52);
            this.delP.Margin = new System.Windows.Forms.Padding(4);
            this.delP.Name = "delP";
            this.delP.Size = new System.Drawing.Size(44, 44);
            this.delP.TabIndex = 2;
            this.delP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.delP_MouseClick);
            // 
            // SplineEnabled
            // 
            this.SplineEnabled.Active = true;
            this.SplineEnabled.DistinctSelection = false;
            this.SplineEnabled.Location = new System.Drawing.Point(101, 52);
            this.SplineEnabled.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SplineEnabled.Name = "SplineEnabled";
            this.SplineEnabled.Size = new System.Drawing.Size(44, 44);
            this.SplineEnabled.TabIndex = 1;
            this.SplineEnabled.OnActivated += new System.EventHandler(this.activeTC_OnActivated);
            // 
            // SplineVisible
            // 
            this.SplineVisible.Active = true;
            this.SplineVisible.DistinctSelection = false;
            this.SplineVisible.Location = new System.Drawing.Point(145, 52);
            this.SplineVisible.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SplineVisible.Name = "SplineVisible";
            this.SplineVisible.Size = new System.Drawing.Size(44, 44);
            this.SplineVisible.TabIndex = 1;
            this.SplineVisible.OnActivated += new System.EventHandler(this.visibleTC_OnActivated);
            // 
            // textBox1
            // 
            this.SplineLabel.BackColor = System.Drawing.SystemColors.Control;
            this.SplineLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SplineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SplineLabel.Location = new System.Drawing.Point(105, 3);
            this.SplineLabel.Name = "textBox1";
            this.SplineLabel.Size = new System.Drawing.Size(140, 27);
            this.SplineLabel.TabIndex = 0;
            this.SplineLabel.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(12, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(258, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(12, 21);
            this.button2.TabIndex = 3;
            this.button2.Text = ".";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CurveMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SplineLabel);
            this.Controls.Add(this.appearnceP);
            this.Controls.Add(this.delP);
            this.Controls.Add(this.SplineEnabled);
            this.Controls.Add(this.SplineVisible);
            this.Controls.Add(this.prevP);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CurveMenuItem";
            this.Size = new System.Drawing.Size(270, 100);
            this.MouseEnter += new System.EventHandler(this.CurveMenuItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CurveMenuItem_MouseLeave);
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel prevP;
        private System.Windows.Forms.Panel delP;
        private System.Windows.Forms.Panel appearnceP;
        public System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem showAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlockAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockAllButThisToolStripMenuItem;
        public ToolControl SplineVisible;
        public ToolControl SplineEnabled;
        private System.Windows.Forms.TextBox SplineLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
