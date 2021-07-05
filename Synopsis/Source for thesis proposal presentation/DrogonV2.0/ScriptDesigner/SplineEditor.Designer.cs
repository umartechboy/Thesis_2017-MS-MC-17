namespace SplineDesigner
{
    partial class SplineEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplineEditor));
            this.addP = new System.Windows.Forms.Button();
            this.removeP = new System.Windows.Forms.Button();
            this.changeP = new System.Windows.Forms.Button();
            this.changeRotationB = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.bezierBoard1 = new SplineDesigner.BezierBoard();
            this.inkOnlyCB = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // addP
            // 
            this.addP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addP.Location = new System.Drawing.Point(461, 27);
            this.addP.Name = "addP";
            this.addP.Size = new System.Drawing.Size(74, 23);
            this.addP.TabIndex = 0;
            this.addP.Text = "Append";
            this.addP.UseVisualStyleBackColor = true;
            this.addP.Click += new System.EventHandler(this.addP_Click);
            // 
            // removeP
            // 
            this.removeP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeP.Location = new System.Drawing.Point(461, 56);
            this.removeP.Name = "removeP";
            this.removeP.Size = new System.Drawing.Size(74, 23);
            this.removeP.TabIndex = 0;
            this.removeP.Text = "Remove";
            this.removeP.UseVisualStyleBackColor = true;
            this.removeP.Click += new System.EventHandler(this.removeP_Click);
            // 
            // changeP
            // 
            this.changeP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeP.Location = new System.Drawing.Point(461, 85);
            this.changeP.Name = "changeP";
            this.changeP.Size = new System.Drawing.Size(74, 23);
            this.changeP.TabIndex = 0;
            this.changeP.Text = "Move";
            this.changeP.UseVisualStyleBackColor = true;
            this.changeP.Click += new System.EventHandler(this.changeP_Click);
            // 
            // changeRotationB
            // 
            this.changeRotationB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeRotationB.Location = new System.Drawing.Point(461, 114);
            this.changeRotationB.Name = "changeRotationB";
            this.changeRotationB.Size = new System.Drawing.Size(74, 23);
            this.changeRotationB.TabIndex = 0;
            this.changeRotationB.Text = "Rotate";
            this.changeRotationB.UseVisualStyleBackColor = true;
            this.changeRotationB.Click += new System.EventHandler(this.changeRotationB_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(538, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.openBackgroundToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // openBackgroundToolStripMenuItem
            // 
            this.openBackgroundToolStripMenuItem.Name = "openBackgroundToolStripMenuItem";
            this.openBackgroundToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openBackgroundToolStripMenuItem.Text = "Open Background";
            this.openBackgroundToolStripMenuItem.Click += new System.EventHandler(this.openBackgroundToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(463, 143);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // bezierBoard1
            // 
            this.bezierBoard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bezierBoard1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bezierBoard1.Location = new System.Drawing.Point(3, 29);
            this.bezierBoard1.Name = "bezierBoard1";
            this.bezierBoard1.Size = new System.Drawing.Size(458, 396);
            this.bezierBoard1.TabIndex = 0;
            // 
            // inkOnlyCB
            // 
            this.inkOnlyCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inkOnlyCB.AutoSize = true;
            this.inkOnlyCB.Location = new System.Drawing.Point(463, 170);
            this.inkOnlyCB.Name = "inkOnlyCB";
            this.inkOnlyCB.Size = new System.Drawing.Size(65, 17);
            this.inkOnlyCB.TabIndex = 3;
            this.inkOnlyCB.Text = "Ink Only";
            this.inkOnlyCB.UseVisualStyleBackColor = true;
            this.inkOnlyCB.CheckedChanged += new System.EventHandler(this.inkOnlyCB_CheckedChanged);
            // 
            // SplineEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inkOnlyCB);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.changeRotationB);
            this.Controls.Add(this.changeP);
            this.Controls.Add(this.removeP);
            this.Controls.Add(this.addP);
            this.Controls.Add(this.bezierBoard1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "SplineEditor";
            this.Size = new System.Drawing.Size(538, 428);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addP;
        private System.Windows.Forms.Button removeP;
        private System.Windows.Forms.Button changeP;
        private System.Windows.Forms.Button changeRotationB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        public BezierBoard bezierBoard1;
        private System.Windows.Forms.ToolStripMenuItem openBackgroundToolStripMenuItem;
        private System.Windows.Forms.CheckBox inkOnlyCB;
    }
}
