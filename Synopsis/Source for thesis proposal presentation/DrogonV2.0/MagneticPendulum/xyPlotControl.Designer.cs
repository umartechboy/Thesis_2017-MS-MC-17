using MagneticPendulum;
namespace MagneticPendulum
{
    partial class xyPlotControl
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
        protected virtual void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xyPlotControl));
            this.autoScaleCB = new System.Windows.Forms.CheckBox();
            this.titleL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dataPlot = new MagneticPendulum.xyPlot();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // autoScaleCB
            // 
            this.autoScaleCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoScaleCB.AutoSize = true;
            this.autoScaleCB.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoScaleCB.Checked = true;
            this.autoScaleCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScaleCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoScaleCB.Location = new System.Drawing.Point(7, 422);
            this.autoScaleCB.Name = "autoScaleCB";
            this.autoScaleCB.Size = new System.Drawing.Size(106, 24);
            this.autoScaleCB.TabIndex = 23;
            this.autoScaleCB.Text = "Auto Scale";
            this.autoScaleCB.UseVisualStyleBackColor = true;
            this.autoScaleCB.CheckedChanged += new System.EventHandler(this.autoScaleCB_CheckedChanged);
            // 
            // titleL
            // 
            this.titleL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleL.Location = new System.Drawing.Point(3, 398);
            this.titleL.Name = "titleL";
            this.titleL.Size = new System.Drawing.Size(280, 21);
            this.titleL.TabIndex = 4;
            this.titleL.Text = "ϴ (rad)";
            this.titleL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-4, 191);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 43);
            this.label1.TabIndex = 4;
            this.label1.Text = "ϴ\r\n(rad/s)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(287, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(54, 394);
            this.panel1.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 161);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = ".";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataPlot
            // 
            this.dataPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPlot.AutoScroll = true;
            this.dataPlot.DataPoints = null;
            this.dataPlot.LastPointHighlight = true;
            this.dataPlot.LineColor = System.Drawing.Color.DarkGreen;
            this.dataPlot.LineOpacity = 1F;
            this.dataPlot.LineThickness = 3F;
            this.dataPlot.Location = new System.Drawing.Point(3, 3);
            this.dataPlot.MaxX = ((System.Drawing.PointF)(resources.GetObject("dataPlot.MaxX")));
            this.dataPlot.MaxY = ((System.Drawing.PointF)(resources.GetObject("dataPlot.MaxY")));
            this.dataPlot.MinX = ((System.Drawing.PointF)(resources.GetObject("dataPlot.MinX")));
            this.dataPlot.MinY = ((System.Drawing.PointF)(resources.GetObject("dataPlot.MinY")));
            this.dataPlot.Name = "dataPlot";
            this.dataPlot.Size = new System.Drawing.Size(280, 392);
            this.dataPlot.TabIndex = 3;
            this.dataPlot.UniqueXAxisStamps = true;
            this.dataPlot.XOffsetChanged += new MagneticPendulum.ValueChangeHandler(this.dataPlot_XOffsetChanged);
            this.dataPlot.XScaleChanged += new MagneticPendulum.ValueChangeHandler(this.dataPlot_XScaleChanged);
            // 
            // xyPlotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.autoScaleCB);
            this.Controls.Add(this.titleL);
            this.Controls.Add(this.dataPlot);
            this.Name = "xyPlotControl";
            this.Size = new System.Drawing.Size(344, 449);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public xyPlot dataPlot;
        private System.Windows.Forms.CheckBox autoScaleCB;
        private System.Windows.Forms.Label titleL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}
