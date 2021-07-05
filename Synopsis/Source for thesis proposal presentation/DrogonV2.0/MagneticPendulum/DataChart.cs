using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FivePointNine.Windows.Drawing
{
    public class DataChart:Panel
    {
        int xmin = 0;
        int xDivs = 1;
        double ymin = -100, ymax = 200;
        int majorGridLines = 3;
        int subDivs = 10;
        int bottomPad_ = 20;
        int leftPad = 40;
        int topPad = 4;
        Color frameCol = Color.DarkGray;
        Color gridCol = Color.Red;
        Color plot1Col = Color.White;
        Color plot2Col = Color.Green;
        Color plot3Col = Color.Red;
        Color plot4Col = Color.Yellow;
        bool gridEnabled = true;
        bool[] plotEnabled = new bool[] { true, true, true, true };
        bool legendEnabled = false;
        TextBox XMaxHistTB = new TextBox();
        TextBox YMaxTB = new TextBox();
        TextBox YMinTB = new TextBox();
        Label[] legendsLB = new Label[] { new Label(), new Label(), new Label(), new Label() };
        public DataChart()
        {
            DoubleBuffered = true;
            Timer refresher = new Timer();
            refresher.Interval = 30;
            refresher.Tick += Refresher_Tick;
            refresher.Enabled = true;

            YMaxTB.Location = new Point(0, 0);
            YMaxTB.BackColor = frameCol;
            YMaxTB.BorderStyle = BorderStyle.None;
            YMaxTB.Width = leftPad;
            YMaxTB.TextAlign = HorizontalAlignment.Center;
            YMaxTB.TextChanged += YMaxTB_TextChanged;
            YMaxTB.Text = YMaximum.ToString();
            YMaxTB.Font = Font;


            YMinTB.Location = new Point(0, Height - Font.Height - bottomPad);
            YMinTB.BackColor = frameCol;
            YMinTB.BorderStyle = BorderStyle.None;
            YMinTB.Width = leftPad;
            YMinTB.TextAlign = HorizontalAlignment.Center;
            YMinTB.TextChanged += YMinTB_TextChanged;
            YMinTB.Text = YMinimum.ToString();
            YMinTB.Font = Font;


            XMaxHistTB.Location = new Point(leftPad + (Width - leftPad) / 2 - MeasureString(XMaxHistTB.Text, Font).Width / 2, Height - bottomPad + 1);
            XMaxHistTB.BackColor = frameCol;
            XMaxHistTB.BorderStyle = BorderStyle.None;
            XMaxHistTB.TextAlign = HorizontalAlignment.Center;
            XMaxHistTB.TextChanged += XMaxHistTB_TextChanged;
            XMaxHistTB.Text = XMaxHistory.ToString();
            XMaxHistTB.Font = Font;
            
            
            Controls.Add(YMaxTB);
            Controls.Add(YMinTB);
            Controls.Add(XMaxHistTB);
            for (int i = 0; i < 4; i++)
            {
                int lastWid = 0;
                if (i > 0)
                    lastWid = legendsLB[0].Height;
                legendsLB[i].Location = new Point(i * 50 + lastWid + 10, Height - legendsLB[i].Height);
                legendsLB[i].Visible = false;
                legendsLB[i].BackColor = frameCol;
                legendsLB[i].ForeColor = ForeColor;
                legendsLB[i].AutoSize = true;

                Controls.Add(legendsLB[i]);
            }
            resetLegendLoc();
        }
        Size MeasureString(string str, Font font)
        {
            Image fakeImage = new Bitmap(1, 1); //As we cannot use CreateGraphics() in a class library, so the fake image is used to load the Graphics.
            Graphics graphics = Graphics.FromImage(fakeImage);
            var sz = graphics.MeasureString(str, font);
            return new Size((int)Math.Round(sz.Width), (int)Math.Round(sz.Height));
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            YMinTB.Location = new Point(0, Height - Font.Height - bottomPad);
            XMaxHistTB.Location = new Point(leftPad + (Width - leftPad) / 2 - MeasureString(XMaxHistTB.Text, Font).Width / 2, Height - bottomPad + 1);
            resetLegendLoc();
        }
        void resetLegendLoc()
        {
            for (int i = 0; i < 4; i++)
            {
                int lastWid = 0;
                if (i > 0)
                    lastWid = legendsLB[i - 1].Width;
                legendsLB[i].Location = new Point(i * 50 + lastWid + 10, Height - legendsLB[0].Height);
            }
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            YMaxTB.ForeColor = ForeColor;
            YMinTB.ForeColor = ForeColor;
            XMaxHistTB.ForeColor = ForeColor;
            for (int i = 0; i < 4; i++)
            {
                legendsLB[i].ForeColor = ForeColor;
            }
        }
        protected override void OnFontChanged(EventArgs e)
        {
            YMaxTB.Font = Font;
            YMinTB.Font = Font;
            XMaxHistTB.Font = Font;
            topPad = Font.Height / 2;
            leftPad = Math.Max(MeasureString(YMinTB.Text, Font).Width, MeasureString(YMaxTB.Text, Font).Width);
            bottomPad_ = MeasureString(XMaxHistTB.Text, Font).Height + 2;
            YMaxTB.Width = leftPad;
            YMinTB.Width = leftPad;
            hasToInvalidate = true;
            for (int i = 0; i < 4; i++)
                legendsLB[i].Font = Font;
            resetLegendLoc();
        }
        private void YMaxTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                YMaximum = Convert.ToDouble(YMaxTB.Text);
                YMaxTB.Text = YMaximum.ToString();
                leftPad = Math.Max(MeasureString(YMinTB.Text, Font).Width, MeasureString(YMaxTB.Text, Font).Width);
                YMinTB.Width = leftPad;
                YMaxTB.Width = leftPad;
                hasToInvalidate = true;
            }
            catch
            { }
        }
        private void YMinTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                YMinimum = Convert.ToDouble(YMinTB.Text);
                YMinTB.Text = YMinimum.ToString();
                
                leftPad = Math.Max(MeasureString(YMinTB.Text, Font).Width, MeasureString(YMaxTB.Text, Font).Width);
                YMinTB.Width = leftPad;
                YMaxTB.Width = leftPad;
                hasToInvalidate = true;
            }
            catch
            { }
        }
        private void XMaxHistTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                XMaxHistory = Convert.ToInt32(XMaxHistTB.Text);
                XMaxHistTB.Text = XMaxHistory.ToString();
                XMaxHistTB.Location = new Point(leftPad + (Width - leftPad) / 2 - MeasureString(XMaxHistTB.Text, Font).Width / 2, XMaxHistTB.Location.Y);
                XMaxHistTB.Width = MeasureString(XMaxHistTB.Text, Font).Width;
                hasToInvalidate = true;
            }
            catch
            { }
        }

        bool hasToInvalidate = true;
        private void Refresher_Tick(object sender, EventArgs e)
        {
            if (hasToInvalidate)
            {
                hasToInvalidate = false;
                Invalidate();
            }
        }
        public bool GridEnabled
        {
            get { return gridEnabled; }
            set
            {
                gridEnabled = value;
                hasToInvalidate = true;
            }
        }
        int bottomPad
        {
            //set { bottomPad_ = value; }
            get { return bottomPad_ + (legendEnabled ? legendsLB[0].Height : 0); }
        }
        public bool LegendEnabled
        {
            get { return legendEnabled; }
            set
            {
                for (int i = 0; i < 4; i++)
                    legendsLB[i].Visible = plotEnabled[i];
                legendEnabled = value;
                hasToInvalidate = true;
                OnSizeChanged(null);
            }
        }
        public bool Plot1Enabled
        {
            get { return plotEnabled[0]; }
            set
            {
                plotEnabled[0] = value;
                hasToInvalidate = true;
            }
        }
        public bool Plot2Enabled
        {
            get { return plotEnabled[1]; }
            set
            {
                plotEnabled[1] = value;
                hasToInvalidate = true;
            }
        }
        public bool Plot3Enabled
        {
            get { return plotEnabled[2]; }
            set
            {
                plotEnabled[2] = value;
                hasToInvalidate = true;
            }
        }
        public bool Plot4Enabled
        {
            get { return plotEnabled[3]; }
            set
            {
                plotEnabled[3] = value;
                hasToInvalidate = true;
            }
        }
        public string Plot1Legend
        {
            get { return legendsLB[0].Text; }
            set
            {
                legendsLB[0].Text = value;
                resetLegendLoc();
                hasToInvalidate = true;
            }
        }
        public string Plot2Legend
        {
            get { return legendsLB[1].Text; }
            set
            {
                legendsLB[1].Text = value;
                resetLegendLoc();
                hasToInvalidate = true;
            }
        }
        public string Plot3Legend
        {
            get { return legendsLB[2].Text; }
            set
            {
                legendsLB[2].Text = value;
                resetLegendLoc();
                hasToInvalidate = true;
            }
        }
        public string Plot4Legend
        {
            get { return legendsLB[3].Text; }
            set
            {
                legendsLB[3].Text = value;
                resetLegendLoc();
                hasToInvalidate = true;
            }
        }
        public int GridMajorLines
        {
            get { return majorGridLines; }
            set
            {
                if (value > 2)
                    majorGridLines = value;
                else
                    majorGridLines = 2;
                hasToInvalidate = true;
            }
        }
        public int GridSubDivisions
        {
            get { return subDivs; }
            set
            {
                if (value >= 1)
                    subDivs = value;
                else
                    subDivs = 1;
                hasToInvalidate = true;
            }
        }
        public Color FrameColor
        {
            get { return frameCol; }
            set
            {
                YMaxTB.BackColor = value;
                YMinTB.BackColor = value;
                XMaxHistTB.BackColor = value;
                frameCol = value; hasToInvalidate = true;
                for (int i = 0; i < 4; i++)
                {
                    legendsLB[i].BackColor= frameCol;
                }
            }
        }
        public Color GridColor
        {
            get { return gridCol; }
            set
            {
                gridCol= value; hasToInvalidate = true;
            }
        }
        public Color Plot1Color
        {
            get { return plot1Col; }
            set
            {
                plot1Col = value; hasToInvalidate = true;
            }
        }
        public Color Plot2Color
        {
            get { return plot2Col; }
            set
            {
                plot2Col = value; hasToInvalidate = true;
            }
        }
        public Color Plot3Color
        {
            get { return plot3Col; }
            set
            {
                plot3Col = value; hasToInvalidate = true;
            }
        }

        public Color Plot4Color
        {
            get { return plot4Col; }
            set
            {
                plot4Col = value; hasToInvalidate = true;
            }
        }
        public void Add(float data1, float data2 = 0, float data3 = 0, float data4 = 0)
        {
            dataPoints1[dataCrsr] = data1;
            dataPoints2[dataCrsr] = data2;
            dataPoints3[dataCrsr] = data3;
            dataPoints4[dataCrsr] = data4;
            dataCrsr++;
            dataCount++;
            if (dataCrsr >= dataPoints1.Length)
                dataCrsr = 0;
            Invalidate();
        }
        public int XMaxHistory
        {
            get
            {
                return dataPoints1.Length - xmin;
            }
            set
            {
                if (value < 2) value = 2; if (value > dataPoints1.Length) value = dataPoints1.Length; xmin = dataPoints1.Length - value; hasToInvalidate = true;
                XMaxHistTB.Text = XMaxHistory.ToString();
                int fac = 1;
                int mult = 2;
                while (XMaxHistory / fac > 50)
                {
                    fac *= mult;
                    if (mult == 2)
                        mult = 5;
                    else mult = 2;
                }
                xDivs = fac;
            }
        }
        public double YMinimum
        {
            get { return ymin; }
            set
            {
                ymin = value; if (ymin >= ymax) ymin = ymax - 0.0001; hasToInvalidate = true;
                YMinTB.Text = YMinimum.ToString();
            }
        }
        public double YMaximum
        {
            get { return ymax; }
            set
            {
                ymax = value; if (ymax <= ymin) ymax = ymin + 0.0001; hasToInvalidate = true;
                YMaxTB.Text = YMaximum.ToString();
            }
        }

        //This is a FIFO Cyclic buffer. Overflow protection is not implemented.
        float[] dataPoints1 = new float[1000000];
        float[] dataPoints2 = new float[1000000];
        float[] dataPoints3 = new float[1000000];
        float[] dataPoints4 = new float[1000000];
        int dataCrsr = 0;
        int dataCount = 0;
        protected override void OnPaint(PaintEventArgs e)
        {
            //var g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.Clear(BackColor);
            //g.FillRectangle(new SolidBrush(frameCol), -1, 0, leftPad + 1, Height);
            //g.FillRectangle(new SolidBrush(frameCol), leftPad - 1, Height - bottomPad, Width - leftPad + 2, bottomPad);
            //g.Clip = new Region(new RectangleF(leftPad, 0, Width - leftPad, Height - bottomPad));
            //g.ScaleTransform(1, -1F);
            //g.TranslateTransform(leftPad, -Height + bottomPad);
            //float
            //    dh = Height - bottomPad - topPad,
            //    dw = Width - leftPad;
            //float scale = dh / (float)(ymax - ymin);
            //float offsetG = -(float)ymin * scale;

            ////PointF[] nodes = new PointF[dataPoints1.Length - xmin];
            ////for (int i = 0; i < dataPoints1.Length - xmin; i++)
            ////{
            ////    float x = dw - i / (float)(dataPoints1.Length - xmin) * dw;
            ////    int crsr = dataCrsr - i - 1;
            ////    if (crsr < 0) crsr += dataPoints1.Length;
            ////    nodes[i] = new PointF(x, (dataPoints1[crsr] * scale) + offsetG);
            ////}

            //// draw the plot
            //if (gridEnabled)
            //{
            //    for (int i = 0; i < dataPoints1.Length; i+=xDivs)
            //    {
            //        float x = dw - (i + dataCount % xDivs) / (float)(dataPoints1.Length - xmin) * dw;
            //        if (x > 0 && x <= dw)
            //        {
            //            var p1 = new PointF(x, 0);
            //            var p2 = new PointF(x, 5);
            //            g.DrawLine(new Pen(gridCol), p1, p2);
            //        }
            //    }
            //    for (int i = 0; i < majorGridLines; i++)
            //    {
            //        float scale2 = dh / (float)(ymax);
            //        float fac = (float)((float)i / (majorGridLines - 1) * YMaximum);
            //        int gInc = 0;
            //        if (fac == 0)
            //            gInc = 1;
            //        g.DrawLine(new Pen(gridCol),
            //                new PointF(0, (float)fac * scale2 + gInc),
            //                new PointF(Width - leftPad, (float)fac * scale2 + gInc));
            //        for (int j = 0; j < subDivs - 1 && (i + 1) < majorGridLines; j++)
            //        {
            //            Color c = gridCol;
            //            c = Color.FromArgb((int)Math.Round((float)c.A / (float) 3), c.R, c.G, c.B);
            //            float lmx = (float)((float)(i + 1) / (majorGridLines - 1) * YMaximum);
            //            float lmn = (float)((float)i / (majorGridLines - 1) * YMaximum);
                        
            //            float facMinor = ((lmx - lmn) / (float)(subDivs)) * (float)(j + 1) + lmn;
            //            g.DrawLine(new Pen(c),
            //                    new PointF(0, (float)facMinor * scale2 + gInc),
            //                    new PointF(Width - leftPad, (float)facMinor * scale2 + gInc));
            //        }
            //    }
            //}
            ////g.DrawLines(new Pen(plotCol), nodes);
            //if (Plot1Enabled)
            //    plotPoints(dataPoints1, g, plot1Col);
            //if (Plot2Enabled)
            //    plotPoints(dataPoints2, g, plot2Col);
            //if (Plot3Enabled)
            //    plotPoints(dataPoints3, g, plot3Col);
            //if (Plot4Enabled)
            //    plotPoints(dataPoints4, g, plot4Col);
        }
        void plotPoints(float[] dataPoints, Graphics g, Color c)
        {
            PointF[] nodes = new PointF[dataPoints.Length - xmin];
            float
                dh = Height - bottomPad - topPad,
                dw = Width - leftPad;
            float scale = dh / (float)(ymax - ymin);
            float offsetG = -(float)ymin * scale;
            for (int i = 0; i < dataPoints.Length - xmin; i++)
            {
                float x = dw - i / (float)(dataPoints.Length - xmin) * dw;
                int crsr = dataCrsr - i - 1;
                if (crsr < 0) crsr += dataPoints.Length;
                nodes[i] = new PointF(x, (dataPoints[crsr] * scale) + offsetG);
            }

            g.DrawLines(new Pen(c), nodes);
        }
    }
}
