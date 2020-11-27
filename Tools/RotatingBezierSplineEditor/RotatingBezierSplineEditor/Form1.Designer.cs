
namespace RotatingBezierSplineEditor
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bothSplinesP = new RotatingBezierSplineEditor.ToolControl();
            this.linearSplineOnly = new RotatingBezierSplineEditor.ToolControl();
            this.rotatingSplineOnlyP = new RotatingBezierSplineEditor.ToolControl();
            this.centerP = new RotatingBezierSplineEditor.ToolControl();
            this.rotationHandleP = new RotatingBezierSplineEditor.ToolControl();
            this.lineHandleP = new RotatingBezierSplineEditor.ToolControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.centerP);
            this.panel1.Controls.Add(this.rotationHandleP);
            this.panel1.Controls.Add(this.lineHandleP);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 241);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bothSplinesP);
            this.panel2.Controls.Add(this.linearSplineOnly);
            this.panel2.Controls.Add(this.rotatingSplineOnlyP);
            this.panel2.Location = new System.Drawing.Point(0, 335);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 241);
            this.panel2.TabIndex = 1;
            // 
            // bothSplinesP
            // 
            this.bothSplinesP.Active = true;
            this.bothSplinesP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bothSplinesP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bothSplinesP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bothSplinesP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bothSplinesP.Location = new System.Drawing.Point(0, 0);
            this.bothSplinesP.Name = "bothSplinesP";
            this.bothSplinesP.Size = new System.Drawing.Size(80, 81);
            this.bothSplinesP.TabIndex = 0;
            // 
            // linearSplineOnly
            // 
            this.linearSplineOnly.Active = false;
            this.linearSplineOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linearSplineOnly.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.linearSplineOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linearSplineOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linearSplineOnly.Location = new System.Drawing.Point(0, 160);
            this.linearSplineOnly.Name = "linearSplineOnly";
            this.linearSplineOnly.Size = new System.Drawing.Size(80, 81);
            this.linearSplineOnly.TabIndex = 0;
            // 
            // rotatingSplineOnlyP
            // 
            this.rotatingSplineOnlyP.Active = false;
            this.rotatingSplineOnlyP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rotatingSplineOnlyP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rotatingSplineOnlyP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rotatingSplineOnlyP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rotatingSplineOnlyP.Location = new System.Drawing.Point(0, 80);
            this.rotatingSplineOnlyP.Name = "rotatingSplineOnlyP";
            this.rotatingSplineOnlyP.Size = new System.Drawing.Size(80, 81);
            this.rotatingSplineOnlyP.TabIndex = 0;
            // 
            // centerP
            // 
            this.centerP.Active = true;
            this.centerP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.centerP.BackgroundImage = global::RotatingBezierSplineEditor.Properties.Resources.Center;
            this.centerP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.centerP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.centerP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.centerP.Location = new System.Drawing.Point(0, 0);
            this.centerP.Name = "centerP";
            this.centerP.Size = new System.Drawing.Size(80, 81);
            this.centerP.TabIndex = 0;
            // 
            // rotationHandleP
            // 
            this.rotationHandleP.Active = false;
            this.rotationHandleP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rotationHandleP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rotationHandleP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rotationHandleP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rotationHandleP.Location = new System.Drawing.Point(0, 160);
            this.rotationHandleP.Name = "rotationHandleP";
            this.rotationHandleP.Size = new System.Drawing.Size(80, 81);
            this.rotationHandleP.TabIndex = 0;
            // 
            // lineHandleP
            // 
            this.lineHandleP.Active = false;
            this.lineHandleP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineHandleP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.lineHandleP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineHandleP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lineHandleP.Location = new System.Drawing.Point(0, 80);
            this.lineHandleP.Name = "lineHandleP";
            this.lineHandleP.Size = new System.Drawing.Size(80, 81);
            this.lineHandleP.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 698);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolControl centerP;
        private ToolControl lineHandleP;
        private ToolControl rotationHandleP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ToolControl bothSplinesP;
        private ToolControl linearSplineOnly;
        private ToolControl rotatingSplineOnlyP;
    }
}

