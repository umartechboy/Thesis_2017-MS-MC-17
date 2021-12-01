
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
            this.scaleTB = new System.Windows.Forms.TextBox();
            this.commandsLB = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.totalCodesL = new System.Windows.Forms.Label();
            this.currentCodeIndexL = new System.Windows.Forms.Label();
            this.simulateSplineB = new System.Windows.Forms.Button();
            this.genMachineCodeB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.targetImageP = new System.Windows.Forms.Panel();
            this.achievedImageP = new System.Windows.Forms.Panel();
            this.xyWPJS = new Drogon3.xyJoyStick();
            this.zJPJS = new Drogon3.yJoyStick();
            this.gGPJS = new Drogon3.yJoyStick();
            this.aGPJS = new Drogon3.yJoyStick();
            this.bGPJS = new Drogon3.yJoyStick();
            this.analyzeB = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.achievedImageP.SuspendLayout();
            this.SuspendLayout();
            // 
            // importB
            // 
            this.importB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importB.Location = new System.Drawing.Point(413, 166);
            this.importB.Name = "importB";
            this.importB.Size = new System.Drawing.Size(102, 94);
            this.importB.TabIndex = 1;
            this.importB.Text = "Import";
            this.importB.UseVisualStyleBackColor = true;
            this.importB.Click += new System.EventHandler(this.importB_Click);
            // 
            // gWPL
            // 
            this.gWPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gWPL.AutoSize = true;
            this.gWPL.Location = new System.Drawing.Point(401, 29);
            this.gWPL.Name = "gWPL";
            this.gWPL.Size = new System.Drawing.Size(54, 17);
            this.gWPL.TabIndex = 16;
            this.gWPL.Text = "label13";
            // 
            // bWPL
            // 
            this.bWPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bWPL.AutoSize = true;
            this.bWPL.Location = new System.Drawing.Point(340, 29);
            this.bWPL.Name = "bWPL";
            this.bWPL.Size = new System.Drawing.Size(54, 17);
            this.bWPL.TabIndex = 17;
            this.bWPL.Text = "label13";
            // 
            // aWPL
            // 
            this.aWPL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aWPL.AutoSize = true;
            this.aWPL.Location = new System.Drawing.Point(280, 29);
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
            this.label11.Location = new System.Drawing.Point(418, 281);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 17);
            this.label11.TabIndex = 6;
            this.label11.Text = "G";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(361, 281);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "B";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(302, 281);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "A";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 281);
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
            this.label1.Location = new System.Drawing.Point(410, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Scale";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.groupBox1.Size = new System.Drawing.Size(470, 312);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Artboard Position";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.documentLayoutFP);
            this.groupBox2.Controls.Add(this.scaleTB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.importB);
            this.groupBox2.Location = new System.Drawing.Point(6, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(521, 267);
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
            this.documentLayoutFP.Size = new System.Drawing.Size(390, 238);
            this.documentLayoutFP.TabIndex = 24;
            // 
            // scaleTB
            // 
            this.scaleTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleTB.Location = new System.Drawing.Point(413, 138);
            this.scaleTB.Name = "scaleTB";
            this.scaleTB.Size = new System.Drawing.Size(102, 22);
            this.scaleTB.TabIndex = 23;
            this.scaleTB.Text = "0.0002";
            // 
            // commandsLB
            // 
            this.commandsLB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandsLB.FormattingEnabled = true;
            this.commandsLB.ItemHeight = 16;
            this.commandsLB.Location = new System.Drawing.Point(6, 24);
            this.commandsLB.Name = "commandsLB";
            this.commandsLB.Size = new System.Drawing.Size(452, 260);
            this.commandsLB.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.commandsLB);
            this.groupBox3.Controls.Add(this.totalCodesL);
            this.groupBox3.Controls.Add(this.currentCodeIndexL);
            this.groupBox3.Controls.Add(this.simulateSplineB);
            this.groupBox3.Controls.Add(this.genMachineCodeB);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(479, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(583, 303);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Machine Code";
            // 
            // totalCodesL
            // 
            this.totalCodesL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCodesL.AutoSize = true;
            this.totalCodesL.Location = new System.Drawing.Point(524, 126);
            this.totalCodesL.Name = "totalCodesL";
            this.totalCodesL.Size = new System.Drawing.Size(18, 17);
            this.totalCodesL.TabIndex = 22;
            this.totalCodesL.Text = "--";
            // 
            // currentCodeIndexL
            // 
            this.currentCodeIndexL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.currentCodeIndexL.AutoSize = true;
            this.currentCodeIndexL.Location = new System.Drawing.Point(524, 93);
            this.currentCodeIndexL.Name = "currentCodeIndexL";
            this.currentCodeIndexL.Size = new System.Drawing.Size(18, 17);
            this.currentCodeIndexL.TabIndex = 22;
            this.currentCodeIndexL.Text = "--";
            // 
            // simulateSplineB
            // 
            this.simulateSplineB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simulateSplineB.Location = new System.Drawing.Point(464, 225);
            this.simulateSplineB.Name = "simulateSplineB";
            this.simulateSplineB.Size = new System.Drawing.Size(113, 64);
            this.simulateSplineB.TabIndex = 1;
            this.simulateSplineB.Text = "Show Simulated Path";
            this.simulateSplineB.UseVisualStyleBackColor = true;
            this.simulateSplineB.Click += new System.EventHandler(this.simulateSplineB_Click);
            // 
            // genMachineCodeB
            // 
            this.genMachineCodeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.genMachineCodeB.Location = new System.Drawing.Point(464, 146);
            this.genMachineCodeB.Name = "genMachineCodeB";
            this.genMachineCodeB.Size = new System.Drawing.Size(113, 73);
            this.genMachineCodeB.TabIndex = 1;
            this.genMachineCodeB.Text = "Generate Machine Code";
            this.genMachineCodeB.UseVisualStyleBackColor = true;
            this.genMachineCodeB.Click += new System.EventHandler(this.genMachineCodeB_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Total:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Current:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.achievedImageP);
            this.groupBox4.Controls.Add(this.targetImageP);
            this.groupBox4.Location = new System.Drawing.Point(533, 321);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(523, 260);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rendered Outputs";
            // 
            // targetImageP
            // 
            this.targetImageP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetImageP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.targetImageP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.targetImageP.Location = new System.Drawing.Point(6, 22);
            this.targetImageP.Name = "targetImageP";
            this.targetImageP.Size = new System.Drawing.Size(507, 118);
            this.targetImageP.TabIndex = 0;
            this.targetImageP.Click += new System.EventHandler(this.panel2_Click);
            this.targetImageP.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // achievedImageP
            // 
            this.achievedImageP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.achievedImageP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.achievedImageP.Controls.Add(this.analyzeB);
            this.achievedImageP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.achievedImageP.Location = new System.Drawing.Point(6, 146);
            this.achievedImageP.Name = "achievedImageP";
            this.achievedImageP.Size = new System.Drawing.Size(507, 108);
            this.achievedImageP.TabIndex = 0;
            this.achievedImageP.Click += new System.EventHandler(this.panel2_Click);
            this.achievedImageP.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
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
            this.zJPJS.Size = new System.Drawing.Size(53, 229);
            this.zJPJS.TabIndex = 15;
            // 
            // gGPJS
            // 
            this.gGPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gGPJS.Location = new System.Drawing.Point(402, 49);
            this.gGPJS.Name = "gGPJS";
            this.gGPJS.Size = new System.Drawing.Size(53, 229);
            this.gGPJS.TabIndex = 14;
            // 
            // aGPJS
            // 
            this.aGPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aGPJS.Location = new System.Drawing.Point(285, 49);
            this.aGPJS.Name = "aGPJS";
            this.aGPJS.Size = new System.Drawing.Size(53, 229);
            this.aGPJS.TabIndex = 12;
            // 
            // bGPJS
            // 
            this.bGPJS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bGPJS.Location = new System.Drawing.Point(343, 49);
            this.bGPJS.Name = "bGPJS";
            this.bGPJS.Size = new System.Drawing.Size(53, 229);
            this.bGPJS.TabIndex = 13;
            // 
            // analyzeB
            // 
            this.analyzeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeB.Location = new System.Drawing.Point(405, 81);
            this.analyzeB.Name = "analyzeB";
            this.analyzeB.Size = new System.Drawing.Size(99, 24);
            this.analyzeB.TabIndex = 1;
            this.analyzeB.Text = "Analyze";
            this.analyzeB.UseVisualStyleBackColor = true;
            this.analyzeB.Click += new System.EventHandler(this.analyzeB_Click);
            // 
            // SplinePainter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SplinePainter";
            this.Size = new System.Drawing.Size(1065, 591);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.achievedImageP.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox scaleTB;
        private System.Windows.Forms.FlowLayoutPanel documentLayoutFP;
        private System.Windows.Forms.ListBox commandsLB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label totalCodesL;
        private System.Windows.Forms.Label currentCodeIndexL;
        private System.Windows.Forms.Button genMachineCodeB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button simulateSplineB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel targetImageP;
        private System.Windows.Forms.Panel achievedImageP;
        private System.Windows.Forms.Button analyzeB;
    }
}
