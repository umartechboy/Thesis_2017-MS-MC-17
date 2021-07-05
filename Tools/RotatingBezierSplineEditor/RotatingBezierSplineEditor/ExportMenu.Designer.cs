
namespace RotatingBezierSplineEditor
{
    partial class ExportMenu
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
            this.inkCB = new System.Windows.Forms.CheckBox();
            this.splineCB = new System.Windows.Forms.CheckBox();
            this.bImageCB = new System.Windows.Forms.CheckBox();
            this.handlesCB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dpiNUD = new System.Windows.Forms.NumericUpDown();
            this.exportB = new System.Windows.Forms.Button();
            this.prevB = new System.Windows.Forms.Button();
            this.prevP = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.singleColCB = new System.Windows.Forms.CheckBox();
            this.colorP = new System.Windows.Forms.Panel();
            this.polyRB = new System.Windows.Forms.RadioButton();
            this.rectsRB = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dontRenderSplineCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dpiNUD)).BeginInit();
            this.prevP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inkCB
            // 
            this.inkCB.AutoSize = true;
            this.inkCB.Checked = true;
            this.inkCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inkCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inkCB.Location = new System.Drawing.Point(0, 2);
            this.inkCB.Name = "inkCB";
            this.inkCB.Size = new System.Drawing.Size(60, 29);
            this.inkCB.TabIndex = 2;
            this.inkCB.Text = "Ink";
            this.inkCB.UseVisualStyleBackColor = true;
            this.inkCB.CheckedChanged += new System.EventHandler(this.inkCB_CheckedChanged);
            // 
            // splineCB
            // 
            this.splineCB.AutoSize = true;
            this.splineCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splineCB.Location = new System.Drawing.Point(0, 37);
            this.splineCB.Name = "splineCB";
            this.splineCB.Size = new System.Drawing.Size(89, 29);
            this.splineCB.TabIndex = 2;
            this.splineCB.Text = "Spline";
            this.splineCB.UseVisualStyleBackColor = true;
            this.splineCB.CheckedChanged += new System.EventHandler(this.inkCB_CheckedChanged);
            // 
            // bImageCB
            // 
            this.bImageCB.AutoSize = true;
            this.bImageCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bImageCB.Location = new System.Drawing.Point(0, 107);
            this.bImageCB.Name = "bImageCB";
            this.bImageCB.Size = new System.Drawing.Size(198, 29);
            this.bImageCB.TabIndex = 2;
            this.bImageCB.Text = "Background Image";
            this.bImageCB.UseVisualStyleBackColor = true;
            this.bImageCB.CheckedChanged += new System.EventHandler(this.inkCB_CheckedChanged);
            // 
            // handlesCB
            // 
            this.handlesCB.AutoSize = true;
            this.handlesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.handlesCB.Location = new System.Drawing.Point(0, 72);
            this.handlesCB.Name = "handlesCB";
            this.handlesCB.Size = new System.Drawing.Size(106, 29);
            this.handlesCB.TabIndex = 2;
            this.handlesCB.Text = "Handles";
            this.handlesCB.UseVisualStyleBackColor = true;
            this.handlesCB.CheckedChanged += new System.EventHandler(this.inkCB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "DPI";
            // 
            // dpiNUD
            // 
            this.dpiNUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpiNUD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.dpiNUD.Location = new System.Drawing.Point(48, 373);
            this.dpiNUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.dpiNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dpiNUD.Name = "dpiNUD";
            this.dpiNUD.Size = new System.Drawing.Size(120, 30);
            this.dpiNUD.TabIndex = 4;
            this.dpiNUD.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.dpiNUD.ValueChanged += new System.EventHandler(this.dpiNUD_ValueChanged);
            // 
            // exportB
            // 
            this.exportB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportB.Location = new System.Drawing.Point(1034, 699);
            this.exportB.Name = "exportB";
            this.exportB.Size = new System.Drawing.Size(140, 35);
            this.exportB.TabIndex = 5;
            this.exportB.Text = "Export";
            this.exportB.UseVisualStyleBackColor = true;
            this.exportB.Click += new System.EventHandler(this.exportB_Click);
            // 
            // prevB
            // 
            this.prevB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prevB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevB.Location = new System.Drawing.Point(1034, 658);
            this.prevB.Name = "prevB";
            this.prevB.Size = new System.Drawing.Size(140, 35);
            this.prevB.TabIndex = 5;
            this.prevB.Text = "Preview";
            this.prevB.UseVisualStyleBackColor = true;
            this.prevB.Click += new System.EventHandler(this.prevB_Click);
            // 
            // prevP
            // 
            this.prevP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prevP.AutoScroll = true;
            this.prevP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.prevP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prevP.Controls.Add(this.pictureBox1);
            this.prevP.Location = new System.Drawing.Point(12, 12);
            this.prevP.Name = "prevP";
            this.prevP.Size = new System.Drawing.Size(871, 703);
            this.prevP.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // singleColCB
            // 
            this.singleColCB.AutoSize = true;
            this.singleColCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleColCB.Location = new System.Drawing.Point(0, 177);
            this.singleColCB.Name = "singleColCB";
            this.singleColCB.Size = new System.Drawing.Size(196, 29);
            this.singleColCB.TabIndex = 2;
            this.singleColCB.Text = "Force Single Color";
            this.singleColCB.UseVisualStyleBackColor = true;
            this.singleColCB.CheckedChanged += new System.EventHandler(this.inkCB_CheckedChanged);
            // 
            // colorP
            // 
            this.colorP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.colorP.Location = new System.Drawing.Point(28, 212);
            this.colorP.Name = "colorP";
            this.colorP.Size = new System.Drawing.Size(164, 49);
            this.colorP.TabIndex = 6;
            this.colorP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.colorP_MouseClick);
            // 
            // polyRB
            // 
            this.polyRB.AutoSize = true;
            this.polyRB.Checked = true;
            this.polyRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.polyRB.Location = new System.Drawing.Point(3, 279);
            this.polyRB.Name = "polyRB";
            this.polyRB.Size = new System.Drawing.Size(219, 29);
            this.polyRB.TabIndex = 7;
            this.polyRB.TabStop = true;
            this.polyRB.Text = "Render with polygons";
            this.polyRB.UseVisualStyleBackColor = true;
            this.polyRB.CheckedChanged += new System.EventHandler(this.polyRB_CheckedChanged);
            // 
            // rectsRB
            // 
            this.rectsRB.AutoSize = true;
            this.rectsRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectsRB.Location = new System.Drawing.Point(3, 314);
            this.rectsRB.Name = "rectsRB";
            this.rectsRB.Size = new System.Drawing.Size(237, 29);
            this.rectsRB.TabIndex = 7;
            this.rectsRB.Text = "Render with Rectangles";
            this.rectsRB.UseVisualStyleBackColor = true;
            this.rectsRB.CheckedChanged += new System.EventHandler(this.rectsRB_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.inkCB);
            this.panel1.Controls.Add(this.rectsRB);
            this.panel1.Controls.Add(this.splineCB);
            this.panel1.Controls.Add(this.polyRB);
            this.panel1.Controls.Add(this.handlesCB);
            this.panel1.Controls.Add(this.colorP);
            this.panel1.Controls.Add(this.dontRenderSplineCB);
            this.panel1.Controls.Add(this.bImageCB);
            this.panel1.Controls.Add(this.singleColCB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dpiNUD);
            this.panel1.Location = new System.Drawing.Point(922, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 431);
            this.panel1.TabIndex = 8;
            // 
            // dontRenderSplineCB
            // 
            this.dontRenderSplineCB.AutoSize = true;
            this.dontRenderSplineCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dontRenderSplineCB.Location = new System.Drawing.Point(0, 142);
            this.dontRenderSplineCB.Name = "dontRenderSplineCB";
            this.dontRenderSplineCB.Size = new System.Drawing.Size(125, 29);
            this.dontRenderSplineCB.TabIndex = 2;
            this.dontRenderSplineCB.Text = "No splines";
            this.dontRenderSplineCB.UseVisualStyleBackColor = true;
            this.dontRenderSplineCB.CheckedChanged += new System.EventHandler(this.inkCB_CheckedChanged);
            // 
            // ExportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 746);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.prevB);
            this.Controls.Add(this.exportB);
            this.Controls.Add(this.prevP);
            this.Name = "ExportMenu";
            this.Text = "ExportMenu";
            this.Load += new System.EventHandler(this.ExportMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dpiNUD)).EndInit();
            this.prevP.ResumeLayout(false);
            this.prevP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox inkCB;
        private System.Windows.Forms.CheckBox splineCB;
        private System.Windows.Forms.CheckBox bImageCB;
        private System.Windows.Forms.CheckBox handlesCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown dpiNUD;
        private System.Windows.Forms.Button exportB;
        private System.Windows.Forms.Button prevB;
        private System.Windows.Forms.Panel prevP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox singleColCB;
        private System.Windows.Forms.Panel colorP;
        private System.Windows.Forms.RadioButton polyRB;
        private System.Windows.Forms.RadioButton rectsRB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox dontRenderSplineCB;
    }
}