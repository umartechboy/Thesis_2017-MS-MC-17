
namespace Drogon3
{
    partial class InverseKinematicsSolver
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
            this.label1 = new System.Windows.Forms.Label();
            this.x = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.z = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.a = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.b = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.g = new System.Windows.Forms.TextBox();
            this.pErrorTB = new Drogon3.SpeedChangingTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.speedTB = new Drogon3.SpeedChangingTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.solveB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(32, 119);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(100, 22);
            this.x.TabIndex = 2;
            this.x.Text = "0.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(138, 119);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(100, 22);
            this.y.TabIndex = 2;
            this.y.Text = "0.5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Z";
            // 
            // z
            // 
            this.z.Location = new System.Drawing.Point(244, 119);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(100, 22);
            this.z.TabIndex = 2;
            this.z.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Positional Error";
            // 
            // a
            // 
            this.a.Location = new System.Drawing.Point(350, 119);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(100, 22);
            this.a.TabIndex = 2;
            this.a.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(499, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "b";
            // 
            // b
            // 
            this.b.Location = new System.Drawing.Point(456, 119);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(100, 22);
            this.b.TabIndex = 2;
            this.b.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(605, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "g";
            // 
            // g
            // 
            this.g.Location = new System.Drawing.Point(562, 119);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(100, 22);
            this.g.TabIndex = 2;
            this.g.Text = "0";
            // 
            // pErrorTB
            // 
            this.pErrorTB.BackColor = System.Drawing.Color.White;
            this.pErrorTB.Location = new System.Drawing.Point(32, 30);
            this.pErrorTB.Name = "pErrorTB";
            this.pErrorTB.ReadOnly = true;
            this.pErrorTB.Size = new System.Drawing.Size(100, 22);
            this.pErrorTB.TabIndex = 3;
            this.pErrorTB.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(163, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Linear Speed";
            // 
            // speedTB
            // 
            this.speedTB.BackColor = System.Drawing.Color.White;
            this.speedTB.Location = new System.Drawing.Point(158, 30);
            this.speedTB.Name = "speedTB";
            this.speedTB.ReadOnly = true;
            this.speedTB.Size = new System.Drawing.Size(100, 22);
            this.speedTB.TabIndex = 3;
            this.speedTB.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // solveB
            // 
            this.solveB.Location = new System.Drawing.Point(32, 147);
            this.solveB.Name = "solveB";
            this.solveB.Size = new System.Drawing.Size(148, 36);
            this.solveB.TabIndex = 0;
            this.solveB.Text = "Solve";
            this.solveB.UseVisualStyleBackColor = true;
            this.solveB.Click += new System.EventHandler(this.solveB_Click);
            // 
            // InverseKinematicsSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.speedTB);
            this.Controls.Add(this.pErrorTB);
            this.Controls.Add(this.g);
            this.Controls.Add(this.z);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.b);
            this.Controls.Add(this.y);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.a);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.solveB);
            this.Name = "InverseKinematicsSolver";
            this.Size = new System.Drawing.Size(696, 518);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox a;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox b;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox g;
        private SpeedChangingTextBox pErrorTB;
        private System.Windows.Forms.Label label7;
        private SpeedChangingTextBox speedTB;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button solveB;
    }
}
