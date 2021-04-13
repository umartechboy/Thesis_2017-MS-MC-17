
namespace Drogon3
{
    partial class PIDPerformanceControl
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
            this.actuatorsList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.resetP = new System.Windows.Forms.Button();
            this.stopB = new System.Windows.Forms.Button();
            this.currentRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.omegaT = new Drogon3.SpeedChangingTextBox();
            this.posT = new Drogon3.SpeedChangingTextBox();
            this.PMax = new Drogon3.SpeedChangingTextBox();
            this.tauAppliedT = new Drogon3.SpeedChangingTextBox();
            this.PCurrentT = new Drogon3.SpeedChangingTextBox();
            this.refT = new Drogon3.SpeedChangingTextBox();
            this.DT = new Drogon3.SpeedChangingTextBox();
            this.IT = new Drogon3.SpeedChangingTextBox();
            this.PT = new Drogon3.SpeedChangingTextBox();
            this.tauMaxT = new Drogon3.SpeedChangingTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // actuatorsList
            // 
            this.actuatorsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.actuatorsList.FormattingEnabled = true;
            this.actuatorsList.Location = new System.Drawing.Point(218, 3);
            this.actuatorsList.Name = "actuatorsList";
            this.actuatorsList.Size = new System.Drawing.Size(132, 106);
            this.actuatorsList.TabIndex = 0;
            this.actuatorsList.SelectedIndexChanged += new System.EventHandler(this.actuatorsList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "P";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "I";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "D";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Ref";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Pos";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(234, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "PMax";
            // 
            // resetP
            // 
            this.resetP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetP.Location = new System.Drawing.Point(282, 396);
            this.resetP.Name = "resetP";
            this.resetP.Size = new System.Drawing.Size(68, 44);
            this.resetP.TabIndex = 7;
            this.resetP.TabStop = false;
            this.resetP.Text = "Reset Position";
            this.resetP.UseVisualStyleBackColor = true;
            this.resetP.Click += new System.EventHandler(this.resetP_Click);
            // 
            // stopB
            // 
            this.stopB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopB.Location = new System.Drawing.Point(282, 446);
            this.stopB.Name = "stopB";
            this.stopB.Size = new System.Drawing.Size(68, 29);
            this.stopB.TabIndex = 2;
            this.stopB.TabStop = false;
            this.stopB.Text = "Stop";
            this.stopB.UseVisualStyleBackColor = true;
            this.stopB.Click += new System.EventHandler(this.stopB_Click);
            // 
            // currentRefreshTimer
            // 
            this.currentRefreshTimer.Enabled = true;
            this.currentRefreshTimer.Interval = 30;
            this.currentRefreshTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "PApplied";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(258, 371);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "w";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(243, 287);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "Tau";
            // 
            // omegaT
            // 
            this.omegaT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.omegaT.Location = new System.Drawing.Point(282, 368);
            this.omegaT.Name = "omegaT";
            this.omegaT.ReadOnly = true;
            this.omegaT.Size = new System.Drawing.Size(68, 22);
            this.omegaT.TabIndex = 0;
            this.omegaT.TabStop = false;
            this.omegaT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // posT
            // 
            this.posT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.posT.Location = new System.Drawing.Point(282, 340);
            this.posT.Name = "posT";
            this.posT.Size = new System.Drawing.Size(68, 22);
            this.posT.TabIndex = 6;
            this.posT.TabStop = false;
            this.posT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // PMax
            // 
            this.PMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PMax.Location = new System.Drawing.Point(282, 200);
            this.PMax.Name = "PMax";
            this.PMax.Size = new System.Drawing.Size(68, 22);
            this.PMax.TabIndex = 4;
            this.PMax.TabStop = false;
            this.PMax.Leave += new System.EventHandler(this.T_Leave);
            // 
            // tauAppliedT
            // 
            this.tauAppliedT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tauAppliedT.Location = new System.Drawing.Point(282, 284);
            this.tauAppliedT.Name = "tauAppliedT";
            this.tauAppliedT.ReadOnly = true;
            this.tauAppliedT.Size = new System.Drawing.Size(68, 22);
            this.tauAppliedT.TabIndex = 0;
            this.tauAppliedT.TabStop = false;
            this.tauAppliedT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // PCurrentT
            // 
            this.PCurrentT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PCurrentT.Location = new System.Drawing.Point(282, 256);
            this.PCurrentT.Name = "PCurrentT";
            this.PCurrentT.ReadOnly = true;
            this.PCurrentT.Size = new System.Drawing.Size(68, 22);
            this.PCurrentT.TabIndex = 0;
            this.PCurrentT.TabStop = false;
            this.PCurrentT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // refT
            // 
            this.refT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refT.Location = new System.Drawing.Point(282, 312);
            this.refT.Name = "refT";
            this.refT.Size = new System.Drawing.Size(68, 22);
            this.refT.TabIndex = 5;
            this.refT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // DT
            // 
            this.DT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DT.Location = new System.Drawing.Point(283, 172);
            this.DT.Name = "DT";
            this.DT.Size = new System.Drawing.Size(68, 22);
            this.DT.TabIndex = 3;
            this.DT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // IT
            // 
            this.IT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IT.Location = new System.Drawing.Point(282, 144);
            this.IT.Name = "IT";
            this.IT.Size = new System.Drawing.Size(68, 22);
            this.IT.TabIndex = 2;
            this.IT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // PT
            // 
            this.PT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PT.Location = new System.Drawing.Point(282, 116);
            this.PT.Name = "PT";
            this.PT.Size = new System.Drawing.Size(68, 22);
            this.PT.TabIndex = 1;
            this.PT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // tauMaxT
            // 
            this.tauMaxT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tauMaxT.Location = new System.Drawing.Point(282, 228);
            this.tauMaxT.Name = "tauMaxT";
            this.tauMaxT.Size = new System.Drawing.Size(68, 22);
            this.tauMaxT.TabIndex = 0;
            this.tauMaxT.TabStop = false;
            this.tauMaxT.Leave += new System.EventHandler(this.T_Leave);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(234, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "TMax";
            // 
            // PIDPerformanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stopB);
            this.Controls.Add(this.resetP);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.omegaT);
            this.Controls.Add(this.posT);
            this.Controls.Add(this.PMax);
            this.Controls.Add(this.tauAppliedT);
            this.Controls.Add(this.tauMaxT);
            this.Controls.Add(this.PCurrentT);
            this.Controls.Add(this.refT);
            this.Controls.Add(this.DT);
            this.Controls.Add(this.IT);
            this.Controls.Add(this.PT);
            this.Controls.Add(this.actuatorsList);
            this.Name = "PIDPerformanceControl";
            this.Size = new System.Drawing.Size(354, 563);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox actuatorsList;
        private SpeedChangingTextBox PT;
        private System.Windows.Forms.Label label1;
        private SpeedChangingTextBox IT;
        private System.Windows.Forms.Label label2;
        private SpeedChangingTextBox DT;
        private System.Windows.Forms.Label label3;
        private SpeedChangingTextBox refT;
        private System.Windows.Forms.Label label4;
        private SpeedChangingTextBox posT;
        private System.Windows.Forms.Label label5;
        private SpeedChangingTextBox PMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button resetP;
        private System.Windows.Forms.Button stopB;
        private System.Windows.Forms.Timer currentRefreshTimer;
        private SpeedChangingTextBox PCurrentT;
        private System.Windows.Forms.Label label7;
        private SpeedChangingTextBox omegaT;
        private System.Windows.Forms.Label label8;
        private SpeedChangingTextBox tauAppliedT;
        private System.Windows.Forms.Label label9;
        private SpeedChangingTextBox tauMaxT;
        private System.Windows.Forms.Label label10;
    }
}
