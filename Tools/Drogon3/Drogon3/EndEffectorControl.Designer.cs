
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
            this.joystickControlRB = new System.Windows.Forms.RadioButton();
            this.calligraphyRB = new System.Windows.Forms.RadioButton();
            this.joustickReader = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.solutionTesterControlRB = new System.Windows.Forms.RadioButton();
            this.aEEJS = new Drogon3.yJoyStick();
            this.bEEJS = new Drogon3.yJoyStick();
            this.gEEJS = new Drogon3.yJoyStick();
            this.zEEJS = new Drogon3.yJoyStick();
            this.xyEEJS = new Drogon3.xyJoyStick();
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
            // joystickControlRB
            // 
            this.joystickControlRB.AutoSize = true;
            this.joystickControlRB.Location = new System.Drawing.Point(259, 27);
            this.joystickControlRB.Name = "joystickControlRB";
            this.joystickControlRB.Size = new System.Drawing.Size(83, 21);
            this.joystickControlRB.TabIndex = 1;
            this.joystickControlRB.Text = "Joy stick";
            this.joystickControlRB.UseVisualStyleBackColor = true;
            this.joystickControlRB.CheckedChanged += new System.EventHandler(this.controlTypeRB_CheckedChanged);
            // 
            // calligraphyRB
            // 
            this.calligraphyRB.AutoSize = true;
            this.calligraphyRB.Location = new System.Drawing.Point(359, 27);
            this.calligraphyRB.Name = "calligraphyRB";
            this.calligraphyRB.Size = new System.Drawing.Size(194, 21);
            this.calligraphyRB.TabIndex = 1;
            this.calligraphyRB.Text = "Calligraphy Route Plannar";
            this.calligraphyRB.UseVisualStyleBackColor = true;
            this.calligraphyRB.CheckedChanged += new System.EventHandler(this.controlTypeRB_CheckedChanged);
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "label12";
            // 
            // solutionTesterControlRB
            // 
            this.solutionTesterControlRB.AutoSize = true;
            this.solutionTesterControlRB.Checked = true;
            this.solutionTesterControlRB.Location = new System.Drawing.Point(109, 27);
            this.solutionTesterControlRB.Name = "solutionTesterControlRB";
            this.solutionTesterControlRB.Size = new System.Drawing.Size(125, 21);
            this.solutionTesterControlRB.TabIndex = 1;
            this.solutionTesterControlRB.TabStop = true;
            this.solutionTesterControlRB.Text = "Solution Tester";
            this.solutionTesterControlRB.UseVisualStyleBackColor = true;
            this.solutionTesterControlRB.CheckedChanged += new System.EventHandler(this.controlTypeRB_CheckedChanged);
            // 
            // aEEJS
            // 
            this.aEEJS.Location = new System.Drawing.Point(444, 86);
            this.aEEJS.Name = "aEEJS";
            this.aEEJS.Size = new System.Drawing.Size(53, 200);
            this.aEEJS.TabIndex = 3;
            // 
            // bEEJS
            // 
            this.bEEJS.Location = new System.Drawing.Point(502, 86);
            this.bEEJS.Name = "bEEJS";
            this.bEEJS.Size = new System.Drawing.Size(53, 200);
            this.bEEJS.TabIndex = 3;
            // 
            // gEEJS
            // 
            this.gEEJS.Location = new System.Drawing.Point(561, 86);
            this.gEEJS.Name = "gEEJS";
            this.gEEJS.Size = new System.Drawing.Size(53, 200);
            this.gEEJS.TabIndex = 3;
            // 
            // zEEJS
            // 
            this.zEEJS.Location = new System.Drawing.Point(228, 86);
            this.zEEJS.Name = "zEEJS";
            this.zEEJS.Size = new System.Drawing.Size(53, 200);
            this.zEEJS.TabIndex = 3;
            // 
            // xyEEJS
            // 
            this.xyEEJS.Location = new System.Drawing.Point(22, 86);
            this.xyEEJS.Name = "xyEEJS";
            this.xyEEJS.Size = new System.Drawing.Size(200, 200);
            this.xyEEJS.TabIndex = 2;
            // 
            // EndEffectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.aEEJS);
            this.Controls.Add(this.bEEJS);
            this.Controls.Add(this.gEEJS);
            this.Controls.Add(this.zEEJS);
            this.Controls.Add(this.xyEEJS);
            this.Controls.Add(this.calligraphyRB);
            this.Controls.Add(this.solutionTesterControlRB);
            this.Controls.Add(this.joystickControlRB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
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
        private System.Windows.Forms.RadioButton joystickControlRB;
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
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton solutionTesterControlRB;
    }
    public class DBPanel : System.Windows.Forms.Panel
    {
        public DBPanel()
        {
            DoubleBuffered = true;
        }
    }
}
