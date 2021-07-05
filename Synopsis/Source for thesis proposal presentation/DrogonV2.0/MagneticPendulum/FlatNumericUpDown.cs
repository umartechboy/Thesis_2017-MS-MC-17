using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MagneticPendulum
{
    public class FlatNumericUpDown:UserControl
    {
        public event ValueChangeHandler ValueChanged;
        private Label label1;

        public FlatNumericUpDown()
        {
            InitializeComponent();
            MouseMove += FlatNumericUpDown_MouseMove;
            Click += FlatNumericUpDown_Click;
            MouseLeave += FlatNumericUpDown_MouseLeave;
            SizeChanged += FlatNumericUpDown_SizeChanged;
            MouseDown += FlatNumericUpDown_MouseDown;
            DoubleBuffered = true;
            MouseUp += FlatNumericUpDown_MouseUp;
            label1.MouseDown += Label1_MouseDown;
            label1.MouseUp += Label1_MouseUp;
            label1.MouseMove += Label1_MouseMove;
            label1.Cursor = Cursors.SizeNS;
            MouseEnter += FlatNumericUpDown_MouseEnter;
        }
        bool active = false;

        private void FlatNumericUpDown_MouseEnter(object sender, EventArgs e)
        {
            active = true;
            Invalidate();
        }


        Point lastLabelMouse = new Point();
        private void Label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickState)
            {
                Value -= e.Y - lastLabelMouse.Y;
                ValueChanged?.Invoke(Value);
            }
            lastLabelMouse = e.Location;
        }

        private void Label1_MouseUp(object sender, MouseEventArgs e)
        {
            label1.BackColor = BackColor;
            clickState = false;
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.BackColor = Color.LightGray;
            clickState = true;
        }

        private Timer timer1;
        private System.ComponentModel.IContainer components;
        int dir = 0;
        private void FlatNumericUpDown_MouseMove(object sender, MouseEventArgs e)
        {
            int bkp = dir;
            if (e.Y < 2 * arrowM + arrowH)
                dir = 1;
            else if (e.Y > Height - (2 * arrowM + arrowH))
                dir = -1;
            else
                dir = 0;
            if (dir == 0)
                Cursor = Cursors.SizeNS;
            if (bkp != dir)
                Invalidate();
        }
        private void FlatNumericUpDown_MouseUp(object sender, MouseEventArgs e)
        {
            clickState = false;
            active = false;
            Invalidate();
        }

        private void FlatNumericUpDown_MouseDown(object sender, MouseEventArgs e)
        {
            clickState = true;
            Invalidate();
        }

        private void FlatNumericUpDown_MouseLeave(object sender, EventArgs e)
        {
            dir = 0;
            Invalidate();
        }

        private void FlatNumericUpDown_SizeChanged(object sender, EventArgs e)
        {
            label1.Height = (int)Math.Floor(Height - arrowH * 2 - arrowM * 4 - 5 );
            label1.Width = Width;
            label1.Left = 2;
        }

        int v = 0;
        public int Maximum { get; set; } = 100;
        public int Minimum { get; set; } = 0;
        public int Value
        {
            get { return v; }
            set
            {
                if (value > Maximum) value = Maximum;
                if (value < Minimum) value = Minimum;
                v = value;
                label1.Text = v.ToString();
            }
        }
        private void FlatNumericUpDown_Click(object sender, EventArgs e)
        {
            if (dir == 1)
            {
                Value++;
                ValueChanged?.Invoke(Value);
            }
            else if (dir == -1)
            {
                Value--;
                ValueChanged?.Invoke(Value);
            }
        }


        float arrowW = 40;
        float arrowH = 5;
        float arrowM = 4;
        bool clickState = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(BackColor);
            if (dir == 1)
            {
                g.FillRectangle(clickState? Brushes.DarkGray: Brushes.LightGray, 0, 0, Width, arrowH + 2 * arrowM);
            }
            else if (dir == -1)
            {
                g.FillRectangle(clickState ? Brushes.DarkGray : Brushes.LightGray, 0, Height - arrowH - 2 * arrowM, Width, arrowH + 2 * arrowM);
            }
            
            PointF[] topPoints = new PointF[] { new PointF(Width/2 - arrowW/2, arrowM + arrowH), new PointF(Width / 2 + arrowW/2, arrowM + arrowH) , new PointF(Width/2 + 1, arrowM) };
            PointF[] bottomPoints = new PointF[] { new PointF(Width / 2 - arrowW / 2, Height - (arrowM + arrowH)), new PointF(Width / 2 + arrowW / 2, Height - (arrowM + arrowH)), new PointF(Width/2 + 1, Height - arrowM) };
            g.FillPolygon(dir == 1 ? Brushes.Black : new SolidBrush(active ? Color.LightGray : BackColor), topPoints);
            g.FillPolygon(dir == -1 ? Brushes.Black : new SolidBrush(active ? Color.LightGray : BackColor), bottomPoints);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FlatNumericUpDown
            // 
            this.Controls.Add(this.label1);
            this.Name = "FlatNumericUpDown";
            this.Size = new System.Drawing.Size(150, 49);
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (clickState)
            {
                Value += dir;
                if (timer1.Interval > 100)
                    timer1.Interval -= 100;
                else if (timer1.Interval > 30)
                    timer1.Interval -= 10;
            }
            else
                timer1.Interval = 500;
        }
    }
}
