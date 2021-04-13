
namespace Drogon3
{
    partial class StepperMotorEditor
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
            this.min = new System.Windows.Forms.Label();
            this.minimum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.range = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maximum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.resolution = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maxStepTime = new System.Windows.Forms.TextBox();
            this.id = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // min
            // 
            this.min.AutoSize = true;
            this.min.Location = new System.Drawing.Point(110, 0);
            this.min.Name = "min";
            this.min.Size = new System.Drawing.Size(63, 17);
            this.min.TabIndex = 0;
            this.min.Text = "Minimum";
            // 
            // minimum
            // 
            this.minimum.Location = new System.Drawing.Point(91, 20);
            this.minimum.Name = "minimum";
            this.minimum.Size = new System.Drawing.Size(100, 22);
            this.minimum.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Range";
            // 
            // range
            // 
            this.range.Location = new System.Drawing.Point(197, 20);
            this.range.Name = "range";
            this.range.Size = new System.Drawing.Size(100, 22);
            this.range.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Maximum";
            // 
            // maximum
            // 
            this.maximum.Location = new System.Drawing.Point(302, 20);
            this.maximum.Name = "maximum";
            this.maximum.Size = new System.Drawing.Size(100, 22);
            this.maximum.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Resolution";
            // 
            // resolution
            // 
            this.resolution.Location = new System.Drawing.Point(409, 20);
            this.resolution.Name = "resolution";
            this.resolution.Size = new System.Drawing.Size(100, 22);
            this.resolution.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(528, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Step Time";
            // 
            // stepTime
            // 
            this.maxStepTime.Location = new System.Drawing.Point(515, 20);
            this.maxStepTime.Name = "stepTime";
            this.maxStepTime.Size = new System.Drawing.Size(100, 22);
            this.maxStepTime.TabIndex = 1;
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id.Location = new System.Drawing.Point(3, 5);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(109, 39);
            this.id.TabIndex = 2;
            this.id.Text = "label5";
            // 
            // StepperMotorEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.id);
            this.Controls.Add(this.maximum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxStepTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resolution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.range);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minimum);
            this.Controls.Add(this.min);
            this.Name = "StepperMotorEditor";
            this.Size = new System.Drawing.Size(628, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label min;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox minimum;
        public System.Windows.Forms.TextBox range;
        public System.Windows.Forms.TextBox maximum;
        public System.Windows.Forms.TextBox resolution;
        public System.Windows.Forms.TextBox maxStepTime;
        private System.Windows.Forms.Label id;
    }
}
