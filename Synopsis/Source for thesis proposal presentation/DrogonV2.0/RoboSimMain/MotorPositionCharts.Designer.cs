namespace RoboSim
{
    partial class MotorPositionCharts
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
            this.dataChart1 = new FivePointNine.Windows.Drawing.DataChart();
            this.SuspendLayout();
            // 
            // dataChart1
            // 
            this.dataChart1.FrameColor = System.Drawing.Color.DarkGray;
            this.dataChart1.GridColor = System.Drawing.Color.Red;
            this.dataChart1.GridEnabled = true;
            this.dataChart1.GridMajorLines = 3;
            this.dataChart1.GridSubDivisions = 10;
            this.dataChart1.LegendEnabled = false;
            this.dataChart1.Location = new System.Drawing.Point(12, 12);
            this.dataChart1.Name = "dataChart1";
            this.dataChart1.Plot1Color = System.Drawing.Color.White;
            this.dataChart1.Plot1Enabled = true;
            this.dataChart1.Plot1Legend = "";
            this.dataChart1.Plot2Color = System.Drawing.Color.Green;
            this.dataChart1.Plot2Enabled = true;
            this.dataChart1.Plot2Legend = "";
            this.dataChart1.Plot3Color = System.Drawing.Color.Red;
            this.dataChart1.Plot3Enabled = true;
            this.dataChart1.Plot3Legend = "";
            this.dataChart1.Plot4Color = System.Drawing.Color.Yellow;
            this.dataChart1.Plot4Enabled = true;
            this.dataChart1.Plot4Legend = "";
            this.dataChart1.Size = new System.Drawing.Size(260, 85);
            this.dataChart1.TabIndex = 0;
            this.dataChart1.XMaxHistory = 1000000;
            this.dataChart1.YMaximum = 200D;
            this.dataChart1.YMinimum = -100D;
            // 
            // MotorPositionCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dataChart1);
            this.Name = "MotorPositionCharts";
            this.Text = "MotorPositionCharts";
            this.ResumeLayout(false);

        }

        #endregion

        private FivePointNine.Windows.Drawing.DataChart dataChart1;
    }
}