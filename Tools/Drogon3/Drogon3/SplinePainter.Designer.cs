
namespace Drogon3
{
    partial class SplinePainter
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
            this.importB = new System.Windows.Forms.Button();
            this.gWPL = new System.Windows.Forms.Label();
            this.bWPL = new System.Windows.Forms.Label();
            this.aWPL = new System.Windows.Forms.Label();
            this.zWPL = new System.Windows.Forms.Label();
            this.xyWPL = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.joyStickReaderT = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.documentLayoutFP = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.commandsLB = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.genMachineCodeB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xyWPJS = new Drogon3.xyJoyStick();
            this.zJPJS = new Drogon3.yJoyStick();
            this.gGPJS = new Drogon3.yJoyStick();
            this.aGPJS = new Drogon3.yJoyStick();
            this.bGPJS = new Drogon3.yJoyStick();
            this.simulateSplineB = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // importB
            // 
            this.importB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importB.Location = new System.Drawing.Point(663, 167);
            this.importB.Name = "importB";
            this.importB.Size = new System.Drawing.Size(190, 94);
            this.importB.TabIndex = 1;
            this.importB.Text = "Import";
            this.importB.UseVisualStyleBackColor = true;
            this.importB.Click += new System.EventHandler(this.importB_Click);
            // 
            // gWPL
            // 
            this.gWPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gWPL.AutoSize = true;
            this.gWPL.Location = new System.Drawing.Point(415, 29);
            this.gWPL.Name = "gWPL";
            this.gWPL.Size = new System.Drawing.Size(54, 17);
            this.gWPL.TabIndex = 16;
            this.gWPL.Text = "label13";
            // 
            // bWPL
            // 
            this.bWPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bWPL.AutoSize = true;
            this.bWPL.Location = new System.Drawing.Point(354, 29);
            this.bWPL.Name = "bWPL";
            this.bWPL.Size = new System.Drawing.Size(54, 17);
            this.bWPL.TabIndex = 17;
            this.bWPL.Text = "label13";
            // 
            // aWPL
            // 
            this.aWPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aWPL.AutoSize = true;
            this.aWPL.Location = new System.Drawing.Point(294, 29);
            this.aWPL.Name = "aWPL";
            this.aWPL.Size = new System.Drawing.Size(54, 17);
            this.aWPL.TabIndex = 18;
            this.aWPL.Text = "label13";
            // 
            // zWPL
            // 
            this.zWPL.AutoSize = true;
            this.zWPL.Location = new System.Drawing.Point(218, 29);
            this.zWPL.Name = "zWPL";
            this.zWPL.Size = new System.Drawing.Size(54, 17);
            this.zWPL.TabIndex = 19;
            this.zWPL.Text = "label13";
            // 
            // xyWPL
            // 
            this.xyWPL.AutoSize = true;
            this.xyWPL.Location = new System.Drawing.Point(12, 29);
            this.xyWPL.Name = "xyWPL";
            this.xyWPL.Size = new System.Drawing.Size(54, 17);
            this.xyWPL.TabIndex = 20;
            this.xyWPL.Text = "label13";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(432, 265);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 17);
            this.label11.TabIndex = 6;
            this.label11.Text = "G";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(375, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "B";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(316, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "A";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 265);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Z";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "X,Y";
            // 
            // joyStickReaderT
            // 
            this.joyStickReaderT.Enabled = true;
            this.joyStickReaderT.Interval = 30;
            this.joyStickReaderT.Tick += new System.EventHandler(this.joyStickReaderT_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(681, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Scale";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.xyWPL);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.gWPL);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.bWPL);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.aWPL);
            this.groupBox1.Controls.Add(this.xyWPJS);
            this.groupBox1.Controls.Add(this.zWPL);
            this.groupBox1.Controls.Add(this.zJPJS);
            this.groupBox1.Controls.Add(this.gGPJS);
            this.groupBox1.Controls.Add(this.aGPJS);
            this.groupBox1.Controls.Add(this.bGPJS);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 296);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Artboard Position";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.documentLayoutFP);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.importB);
            this.groupBox2.Location = new System.Drawing.Point(6, 305);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(859, 267);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import Splines";
            // 
            // documentLayoutFP
            // 
            this.documentLayoutFP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentLayoutFP.AutoScroll = true;
            this.documentLayoutFP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.documentLayoutFP.Location = new System.Drawing.Point(12, 22);
            this.documentLayoutFP.Margin = new System.Windows.Forms.Padding(4);
            this.documentLayoutFP.Name = "documentLayoutFP";
            this.documentLayoutFP.Size = new System.Drawing.Size(644, 238);
            this.documentLayoutFP.TabIndex = 24;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(730, 139);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 22);
            this.textBox1.TabIndex = 23;
            // 
            // commandsLB
            // 
            this.commandsLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandsLB.FormattingEnabled = true;
            this.commandsLB.ItemHeight = 16;
            this.commandsLB.Location = new System.Drawing.Point(24, 60);
            this.commandsLB.Name = "commandsLB";
            this.commandsLB.Size = new System.Drawing.Size(179, 212);
            this.commandsLB.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.commandsLB);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.simulateSplineB);
            this.groupBox3.Controls.Add(this.genMachineCodeB);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(493, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(372, 287);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Machine Code";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(282, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "--";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(282, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "--";
            // 
            // genMachineCodeB
            // 
            this.genMachineCodeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.genMachineCodeB.Location = new System.Drawing.Point(209, 146);
            this.genMachineCodeB.Name = "genMachineCodeB";
            this.genMachineCodeB.Size = new System.Drawing.Size(157, 57);
            this.genMachineCodeB.TabIndex = 1;
            this.genMachineCodeB.Text = "Generate Machine Code";
            this.genMachineCodeB.UseVisualStyleBackColor = true;
            this.genMachineCodeB.Click += new System.EventHandler(this.genMachineCodeB_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Total:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Current:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Machine Code";
            // 
            // xyWPJS
            // 
            this.xyWPJS.Location = new System.Drawing.Point(15, 49);
            this.xyWPJS.Name = "xyWPJS";
            this.xyWPJS.Size = new System.Drawing.Size(200, 200);
            this.xyWPJS.TabIndex = 11;
            // 
            // zJPJS
            // 
            this.zJPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.zJPJS.Location = new System.Drawing.Point(221, 49);
            this.zJPJS.Name = "zJPJS";
            this.zJPJS.Size = new System.Drawing.Size(53, 213);
            this.zJPJS.TabIndex = 15;
            // 
            // gGPJS
            // 
            this.gGPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gGPJS.Location = new System.Drawing.Point(416, 49);
            this.gGPJS.Name = "gGPJS";
            this.gGPJS.Size = new System.Drawing.Size(53, 213);
            this.gGPJS.TabIndex = 14;
            // 
            // aGPJS
            // 
            this.aGPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aGPJS.Location = new System.Drawing.Point(299, 49);
            this.aGPJS.Name = "aGPJS";
            this.aGPJS.Size = new System.Drawing.Size(53, 213);
            this.aGPJS.TabIndex = 12;
            // 
            // bGPJS
            // 
            this.bGPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bGPJS.Location = new System.Drawing.Point(357, 49);
            this.bGPJS.Name = "bGPJS";
            this.bGPJS.Size = new System.Drawing.Size(53, 213);
            this.bGPJS.TabIndex = 13;
            // 
            // simulateSplineB
            // 
            this.simulateSplineB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simulateSplineB.Location = new System.Drawing.Point(209, 209);
            this.simulateSplineB.Name = "simulateSplineB";
            this.simulateSplineB.Size = new System.Drawing.Size(157, 64);
            this.simulateSplineB.TabIndex = 1;
            this.simulateSplineB.Text = "Show Simulated Path";
            this.simulateSplineB.UseVisualStyleBackColor = true;
            this.simulateSplineB.Click += new System.EventHandler(this.simulateSplineB_Click);
            // 
            // SplinePainter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SplinePainter";
            this.Size = new System.Drawing.Size(868, 575);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button importB;
        private System.Windows.Forms.Label gWPL;
        private System.Windows.Forms.Label bWPL;
        private System.Windows.Forms.Label aWPL;
        private System.Windows.Forms.Label zWPL;
        private System.Windows.Forms.Label xyWPL;
        private yJoyStick aGPJS;
        private yJoyStick bGPJS;
        private yJoyStick gGPJS;
        private yJoyStick zJPJS;
        private xyJoyStick xyWPJS;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer joyStickReaderT;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel documentLayoutFP;
        private System.Windows.Forms.ListBox commandsLB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button genMachineCodeB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button simulateSplineB;
    }
}
