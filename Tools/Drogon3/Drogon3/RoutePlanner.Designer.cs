
namespace Drogon3
{
    partial class RoutePlanner
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
            this.speedTB = new Drogon3.SpeedChangingTextBox();
            this.pErrorTB = new Drogon3.SpeedChangingTextBox();
            this.g = new System.Windows.Forms.TextBox();
            this.z = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.b = new System.Windows.Forms.TextBox();
            this.y = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.a = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.x = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.solveB = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tEnabledRB = new System.Windows.Forms.RadioButton();
            this.tDisabledRB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // speedTB
            // 
            this.speedTB.BackColor = System.Drawing.Color.White;
            this.speedTB.Location = new System.Drawing.Point(144, 41);
            this.speedTB.Name = "speedTB";
            this.speedTB.ReadOnly = true;
            this.speedTB.Size = new System.Drawing.Size(100, 22);
            this.speedTB.TabIndex = 18;
            this.speedTB.Text = "0";
            // 
            // pErrorTB
            // 
            this.pErrorTB.BackColor = System.Drawing.Color.White;
            this.pErrorTB.Location = new System.Drawing.Point(18, 41);
            this.pErrorTB.Name = "pErrorTB";
            this.pErrorTB.ReadOnly = true;
            this.pErrorTB.Size = new System.Drawing.Size(100, 22);
            this.pErrorTB.TabIndex = 19;
            this.pErrorTB.Text = "0";
            // 
            // g
            // 
            this.g.Location = new System.Drawing.Point(548, 130);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(100, 22);
            this.g.TabIndex = 12;
            this.g.Text = "0";
            // 
            // z
            // 
            this.z.Location = new System.Drawing.Point(230, 130);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(100, 22);
            this.z.TabIndex = 13;
            this.z.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(591, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "g";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Z";
            // 
            // b
            // 
            this.b.Location = new System.Drawing.Point(442, 130);
            this.b.Name = "b";
            this.b.Size = new System.Drawing.Size(100, 22);
            this.b.TabIndex = 14;
            this.b.Text = "0";
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(124, 130);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(100, 22);
            this.y.TabIndex = 15;
            this.y.Text = "0.5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(485, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "b";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(149, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Linear Speed";
            // 
            // a
            // 
            this.a.Location = new System.Drawing.Point(336, 130);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(100, 22);
            this.a.TabIndex = 16;
            this.a.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Positional Error";
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(18, 130);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(100, 22);
            this.x.TabIndex = 17;
            this.x.Text = "0.5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "X";
            // 
            // solveB
            // 
            this.solveB.Location = new System.Drawing.Point(19, 227);
            this.solveB.Name = "solveB";
            this.solveB.Size = new System.Drawing.Size(148, 36);
            this.solveB.TabIndex = 4;
            this.solveB.Text = "Solve";
            this.solveB.UseVisualStyleBackColor = true;
            this.solveB.Click += new System.EventHandler(this.solveB_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "X";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(339, 183);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 16;
            this.textBox2.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(170, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(488, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "b";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(127, 183);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 15;
            this.textBox3.Text = "-.5";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(445, 183);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 22);
            this.textBox4.TabIndex = 14;
            this.textBox4.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(276, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 17);
            this.label11.TabIndex = 6;
            this.label11.Text = "Z";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(594, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "g";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(233, 183);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 22);
            this.textBox5.TabIndex = 13;
            this.textBox5.Text = ".5";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(551, 183);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 22);
            this.textBox6.TabIndex = 12;
            this.textBox6.Text = "0";
            // 
            // tEnabledRB
            // 
            this.tEnabledRB.AutoSize = true;
            this.tEnabledRB.Location = new System.Drawing.Point(362, 235);
            this.tEnabledRB.Name = "tEnabledRB";
            this.tEnabledRB.Size = new System.Drawing.Size(139, 21);
            this.tEnabledRB.TabIndex = 20;
            this.tEnabledRB.Text = "Tracking enabled";
            this.tEnabledRB.UseVisualStyleBackColor = true;
            // 
            // tDisabledRB
            // 
            this.tDisabledRB.AutoSize = true;
            this.tDisabledRB.Checked = true;
            this.tDisabledRB.Location = new System.Drawing.Point(520, 235);
            this.tDisabledRB.Name = "tDisabledRB";
            this.tDisabledRB.Size = new System.Drawing.Size(141, 21);
            this.tDisabledRB.TabIndex = 20;
            this.tDisabledRB.TabStop = true;
            this.tDisabledRB.Text = "Tracking disabled";
            this.tDisabledRB.UseVisualStyleBackColor = true;
            // 
            // RoutePlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tDisabledRB);
            this.Controls.Add(this.tEnabledRB);
            this.Controls.Add(this.speedTB);
            this.Controls.Add(this.pErrorTB);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.g);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.z);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.b);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.y);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.a);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.solveB);
            this.Name = "RoutePlanner";
            this.Size = new System.Drawing.Size(710, 534);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpeedChangingTextBox speedTB;
        private SpeedChangingTextBox pErrorTB;
        private System.Windows.Forms.TextBox g;
        private System.Windows.Forms.TextBox z;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox b;
        private System.Windows.Forms.TextBox y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox a;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button solveB;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.RadioButton tEnabledRB;
        private System.Windows.Forms.RadioButton tDisabledRB;
    }
}
