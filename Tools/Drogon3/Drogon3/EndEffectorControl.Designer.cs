
namespace Drogon3
{
    partial class EndEffectorControl
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
            this.manualControlRB = new System.Windows.Forms.RadioButton();
            this.calligraphyRB = new System.Windows.Forms.RadioButton();
            this.joustickReader = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.aGPJS = new Drogon3.yJoyStick();
            this.aEEJS = new Drogon3.yJoyStick();
            this.bGPJS = new Drogon3.yJoyStick();
            this.bEEJS = new Drogon3.yJoyStick();
            this.gGPJS = new Drogon3.yJoyStick();
            this.gEEJS = new Drogon3.yJoyStick();
            this.zJPJS = new Drogon3.yJoyStick();
            this.zEEJS = new Drogon3.yJoyStick();
            this.xyWPJS = new Drogon3.xyJoyStick();
            this.xyEEJS = new Drogon3.xyJoyStick();
            this.label12 = new System.Windows.Forms.Label();
            this.xyWPL = new System.Windows.Forms.Label();
            this.zWPL = new System.Windows.Forms.Label();
            this.aWPL = new System.Windows.Forms.Label();
            this.bWPL = new System.Windows.Forms.Label();
            this.gWPL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target:";
            // 
            // manualControlRB
            // 
            this.manualControlRB.AutoSize = true;
            this.manualControlRB.Checked = true;
            this.manualControlRB.Location = new System.Drawing.Point(93, 25);
            this.manualControlRB.Name = "manualControlRB";
            this.manualControlRB.Size = new System.Drawing.Size(124, 21);
            this.manualControlRB.TabIndex = 1;
            this.manualControlRB.TabStop = true;
            this.manualControlRB.Text = "Manual Control";
            this.manualControlRB.UseVisualStyleBackColor = true;
            // 
            // calligraphyRB
            // 
            this.calligraphyRB.AutoSize = true;
            this.calligraphyRB.Location = new System.Drawing.Point(244, 25);
            this.calligraphyRB.Name = "calligraphyRB";
            this.calligraphyRB.Size = new System.Drawing.Size(194, 21);
            this.calligraphyRB.TabIndex = 1;
            this.calligraphyRB.Text = "Calligraphy Route Plannar";
            this.calligraphyRB.UseVisualStyleBackColor = true;
            // 
            // joustickReader
            // 
            this.joustickReader.Enabled = true;
            this.joustickReader.Interval = 30;
            this.joustickReader.Tick += new System.EventHandler(this.joyStickReader_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "X,Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(461, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(577, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "G";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(520, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "B";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 554);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "X,Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(244, 554);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Z";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(461, 554);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(520, 554);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "B";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(577, 554);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "G";
            // 
            // aGPJS
            // 
            this.aGPJS.Location = new System.Drawing.Point(444, 351);
            this.aGPJS.Name = "aGPJS";
            this.aGPJS.Size = new System.Drawing.Size(53, 200);
            this.aGPJS.TabIndex = 3;
            // 
            // aEEJS
            // 
            this.aEEJS.Location = new System.Drawing.Point(444, 86);
            this.aEEJS.Name = "aEEJS";
            this.aEEJS.Size = new System.Drawing.Size(53, 200);
            this.aEEJS.TabIndex = 3;
            // 
            // bGPJS
            // 
            this.bGPJS.Location = new System.Drawing.Point(502, 351);
            this.bGPJS.Name = "bGPJS";
            this.bGPJS.Size = new System.Drawing.Size(53, 200);
            this.bGPJS.TabIndex = 3;
            // 
            // bEEJS
            // 
            this.bEEJS.Location = new System.Drawing.Point(502, 86);
            this.bEEJS.Name = "bEEJS";
            this.bEEJS.Size = new System.Drawing.Size(53, 200);
            this.bEEJS.TabIndex = 3;
            // 
            // gGPJS
            // 
            this.gGPJS.Location = new System.Drawing.Point(561, 351);
            this.gGPJS.Name = "gGPJS";
            this.gGPJS.Size = new System.Drawing.Size(53, 200);
            this.gGPJS.TabIndex = 3;
            // 
            // gEEJS
            // 
            this.gEEJS.Location = new System.Drawing.Point(561, 86);
            this.gEEJS.Name = "gEEJS";
            this.gEEJS.Size = new System.Drawing.Size(53, 200);
            this.gEEJS.TabIndex = 3;
            // 
            // zJPJS
            // 
            this.zJPJS.Location = new System.Drawing.Point(228, 351);
            this.zJPJS.Name = "zJPJS";
            this.zJPJS.Size = new System.Drawing.Size(53, 200);
            this.zJPJS.TabIndex = 3;
            // 
            // zEEJS
            // 
            this.zEEJS.Location = new System.Drawing.Point(228, 86);
            this.zEEJS.Name = "zEEJS";
            this.zEEJS.Size = new System.Drawing.Size(53, 200);
            this.zEEJS.TabIndex = 3;
            // 
            // xyWPJS
            // 
            this.xyWPJS.Location = new System.Drawing.Point(22, 351);
            this.xyWPJS.Name = "xyWPJS";
            this.xyWPJS.Size = new System.Drawing.Size(200, 200);
            this.xyWPJS.TabIndex = 2;
            // 
            // xyEEJS
            // 
            this.xyEEJS.Location = new System.Drawing.Point(22, 86);
            this.xyEEJS.Name = "xyEEJS";
            this.xyEEJS.Size = new System.Drawing.Size(200, 200);
            this.xyEEJS.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "label12";
            // 
            // xyWPL
            // 
            this.xyWPL.AutoSize = true;
            this.xyWPL.Location = new System.Drawing.Point(19, 331);
            this.xyWPL.Name = "xyWPL";
            this.xyWPL.Size = new System.Drawing.Size(54, 17);
            this.xyWPL.TabIndex = 5;
            this.xyWPL.Text = "label13";
            // 
            // zWPL
            // 
            this.zWPL.AutoSize = true;
            this.zWPL.Location = new System.Drawing.Point(225, 331);
            this.zWPL.Name = "zWPL";
            this.zWPL.Size = new System.Drawing.Size(54, 17);
            this.zWPL.TabIndex = 5;
            this.zWPL.Text = "label13";
            // 
            // aWPL
            // 
            this.aWPL.AutoSize = true;
            this.aWPL.Location = new System.Drawing.Point(439, 331);
            this.aWPL.Name = "aWPL";
            this.aWPL.Size = new System.Drawing.Size(54, 17);
            this.aWPL.TabIndex = 5;
            this.aWPL.Text = "label13";
            // 
            // bWPL
            // 
            this.bWPL.AutoSize = true;
            this.bWPL.Location = new System.Drawing.Point(499, 331);
            this.bWPL.Name = "bWPL";
            this.bWPL.Size = new System.Drawing.Size(54, 17);
            this.bWPL.TabIndex = 5;
            this.bWPL.Text = "label13";
            // 
            // gWPL
            // 
            this.gWPL.AutoSize = true;
            this.gWPL.Location = new System.Drawing.Point(560, 331);
            this.gWPL.Name = "gWPL";
            this.gWPL.Size = new System.Drawing.Size(54, 17);
            this.gWPL.TabIndex = 5;
            this.gWPL.Text = "label13";
            // 
            // EndEffectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gWPL);
            this.Controls.Add(this.bWPL);
            this.Controls.Add(this.aWPL);
            this.Controls.Add(this.zWPL);
            this.Controls.Add(this.xyWPL);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.aGPJS);
            this.Controls.Add(this.aEEJS);
            this.Controls.Add(this.bGPJS);
            this.Controls.Add(this.bEEJS);
            this.Controls.Add(this.gGPJS);
            this.Controls.Add(this.gEEJS);
            this.Controls.Add(this.zJPJS);
            this.Controls.Add(this.zEEJS);
            this.Controls.Add(this.xyWPJS);
            this.Controls.Add(this.xyEEJS);
            this.Controls.Add(this.calligraphyRB);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.manualControlRB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EndEffectorControl";
            this.Size = new System.Drawing.Size(702, 592);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton manualControlRB;
        private System.Windows.Forms.RadioButton calligraphyRB;
        private xyJoyStick xyEEJS;
        private System.Windows.Forms.Timer joustickReader;
        private yJoyStick zEEJS;
        private yJoyStick gEEJS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private yJoyStick bEEJS;
        private yJoyStick aEEJS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private xyJoyStick xyWPJS;
        private yJoyStick zJPJS;
        private yJoyStick gGPJS;
        private yJoyStick bGPJS;
        private yJoyStick aGPJS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label xyWPL;
        private System.Windows.Forms.Label zWPL;
        private System.Windows.Forms.Label aWPL;
        private System.Windows.Forms.Label bWPL;
        private System.Windows.Forms.Label gWPL;
    }
    public class DBPanel : System.Windows.Forms.Panel
    {
        public DBPanel()
        {
            DoubleBuffered = true;
        }
    }
}
