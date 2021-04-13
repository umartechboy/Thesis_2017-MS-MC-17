
namespace Drogon3
{
    partial class SplineRasterizationProgress
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
            this.components = new System.ComponentModel.Container();
            this.pm = new System.Windows.Forms.ProgressBar();
            this.pu = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pcm = new System.Windows.Forms.Label();
            this.pcu = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pm
            // 
            this.pm.Location = new System.Drawing.Point(12, 42);
            this.pm.Name = "pm";
            this.pm.Size = new System.Drawing.Size(561, 23);
            this.pm.TabIndex = 0;
            // 
            // pu
            // 
            this.pu.Location = new System.Drawing.Point(12, 102);
            this.pu.Name = "pu";
            this.pu.Size = new System.Drawing.Size(561, 23);
            this.pu.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Overall Progress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Spline Progress";
            // 
            // pcm
            // 
            this.pcm.Location = new System.Drawing.Point(459, 19);
            this.pcm.Name = "pcm";
            this.pcm.Size = new System.Drawing.Size(96, 17);
            this.pcm.TabIndex = 1;
            this.pcm.Text = "0";
            this.pcm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pcu
            // 
            this.pcu.Location = new System.Drawing.Point(459, 82);
            this.pcu.Name = "pcu";
            this.pcu.Size = new System.Drawing.Size(96, 17);
            this.pcu.TabIndex = 1;
            this.pcu.Text = "0";
            this.pcu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(552, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(552, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "%";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplineRasterizationProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 141);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pcu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pcm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pu);
            this.Controls.Add(this.pm);
            this.Name = "SplineRasterizationProgress";
            this.Text = "Rasterizing Splines";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SplineRasterizationProgress_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pm;
        private System.Windows.Forms.ProgressBar pu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pcm;
        private System.Windows.Forms.Label pcu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
    }
}