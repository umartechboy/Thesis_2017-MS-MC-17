using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class TraceAnalyzer : Form
    {
        public TraceAnalyzer()
        {
            InitializeComponent();
        }

        public event ExportRequestHandler OnExportRequest;
        Image RenderInk()
        {
            var er = new ExportRequest();
            er.DrawMode = InkDrawMode.Ink;
            er.RenderImages = false;
            er.DontRenderSplines = false;
            er.RenderAlgorithm = FlatTipRenderAlgorithm.Rectangle;
            er.ForceSingleColor = true;
            er.ForceColor = Color.Black;
            er.DPI = (int)dpiNUD.Value;
            OnExportRequest?.Invoke(this, new ExportRequestEventArgs() { Request = er });
            return er.RenderOutput;
        }
        Image RenderSource()
        {
            var er = new ExportRequest();
            er.DrawMode = InkDrawMode.Images;
            er.RenderImages = true;
            er.DontRenderSplines = true;
            er.RenderAlgorithm = FlatTipRenderAlgorithm.Rectangle;
            er.DPI = (int)dpiNUD.Value;
            OnExportRequest?.Invoke(this, new ExportRequestEventArgs() { Request = er });
            return er.RenderOutput;
        }
        BinaryBitmap Source, Ink;
        Bitmap Extra, Missing, Highlight;

        private void button1_Click(object sender, EventArgs e)
        {
            Process();
            Display();
        }

        private void dpiNUD_ValueChanged(object sender, EventArgs e)
        {
            Process();
            Display();
        }

        Image alternateInk, alternateSource;

        private void loadSourceB_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.png, *.bmp, *.jpg)|*.png;*.bmp;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    alternateSource = Image.FromFile(ofd.FileName);
                    Process();
                    Display();
                }
                catch { }
            }
        }

        long SourcePixels = 0, ExtraPixels = 0, MissingPixels = 0;
        private void bWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var inkImg = alternateInk;
            var sourceImg = alternateSource;
            if (inkImg == null)
                inkImg = RenderInk();
            worker.ReportProgress(10);
            if (worker.CancellationPending) return;
            if (sourceImg == null)
                sourceImg = RenderSource();
            worker.ReportProgress(20);
            if (worker.CancellationPending) return;
            if (inkImg == null || sourceImg == null)
                return;
            if (inkImg.Size != sourceImg.Size)
                return;
            Ink = new BinaryBitmap(new Bitmap(inkImg));
            Source = new BinaryBitmap(new Bitmap(sourceImg));
            Extra = BinaryBitmap.Subtract(Ink, Source);
            worker.ReportProgress(35);
            if (worker.CancellationPending) return;
            Missing = BinaryBitmap.Subtract(Source, Ink);
            worker.ReportProgress(50);
            if (worker.CancellationPending) return;
            Highlight = BinaryBitmap.Difference(Source, Ink);
            if (worker.CancellationPending) return;
            worker.ReportProgress(65);
            SourcePixels = BinaryBitmap.CountPixels(new Bitmap(sourceImg));
            if (worker.CancellationPending) return;
            worker.ReportProgress(75);
            ExtraPixels = BinaryBitmap.CountPixels(Extra);
            if (worker.CancellationPending) return;
            worker.ReportProgress(90);
            MissingPixels = BinaryBitmap.CountPixels(Missing);
            worker.ReportProgress(100);
        }

        private void bWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progLabel.Text = "Processing...";
            progP.Value = e.ProgressPercentage;
        }

        private void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progLabel.Text = "";
            progP.Value = 0;
            Display();
            processB.Text = "Process";
            if (Source == null || Ink == null)
                return;
            summaryL.Text = string.Format("Total Pixels: {0}\r\nPixels in Ink: {1}\r\nMissing: -{2}% ({3})\r\nExtra: {4}% ({5})",
                Source.Width * Source.Height,
                SourcePixels,
                (MissingPixels / (double)SourcePixels * 100).ToString("F2"),
                MissingPixels,
                (ExtraPixels / (double)SourcePixels * 100).ToString("F2"),
                ExtraPixels);
        }

        private void loadInkB_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image file (*.png, *.bmp, *.jpg)|*.png;*.bmp;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    alternateInk = Image.FromFile(ofd.FileName);
                    Process();
                    Display();
                }
                catch { }
            }
        }

        void Process()
        {
            if (bWorker.IsBusy)
            {
                bWorker.CancelAsync();
                processB.Text = "Process";
                return;
            }
            processB.Text = "Stop";
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.RunWorkerAsync();
        }
        void Display()
        {
            if (extraCB.Checked)
                pictureBox2.Image = Extra;
            else if (missingCB.Checked)
                pictureBox2.Image = Missing;
            else if (highLightCB.Checked)
                pictureBox2.Image = Highlight;
            else if (sourceCB.Checked)
                pictureBox2.Image = Source.Bitmap;
            else if (traceCB.Checked)
            {
                if (Ink != null)
                    pictureBox2.Image = Ink.Bitmap;
            }
        }
        private void previewModeCBs_CheckedChanged(object sender, EventArgs e)
        {
            Display();
        }
    }
    public class BinaryBitmap
    {
        public bool[,] Pixels;
        public bool[,] Cached;
        public int Width { get { return Bitmap.Width; } }
        public int Height { get { return Bitmap.Height; } }
        public Bitmap Bitmap { get; private set; }
        public BinaryBitmap(Bitmap bmp)
        {
            Pixels = new bool[bmp.Width, bmp.Height];
            Cached = new bool[bmp.Width, bmp.Height];
            this.Bitmap = bmp;
        }
        public static Bitmap Subtract(BinaryBitmap A, BinaryBitmap B)
        {
            var newBmp = new Bitmap(A.Width, A.Height);
            for (int y = 0; y < newBmp.Height; y++)
                for (int x = 0; x < newBmp.Width; x++)
                {
                    if (A[x, y] && !B[x, y])
                        newBmp.SetPixel(x, y, Color.Black);
                }
            return newBmp;
        }
        public static long CountPixels(Bitmap bmp)
        {
            long ps = 0;
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (IsPixel(bmp.GetPixel(x, y)))
                        ps++;
                }
            return ps;
        }
        public static Bitmap Common(BinaryBitmap A, BinaryBitmap B)
        {
            var newBmp = new Bitmap(A.Width, A.Height);
            for (int y = 0; y < newBmp.Height; y++)
                for (int x = 0; x < newBmp.Width; x++)
                {
                    if (A[x, y] && B[x, y])
                        newBmp.SetPixel(x, y, Color.Black);
                }
            return newBmp;
        }
        public static Bitmap Difference(BinaryBitmap A, BinaryBitmap B)
        {
            var newBmp = new Bitmap(A.Width, A.Height);
            for (int y = 0; y < newBmp.Height; y++)
                for (int x = 0; x < newBmp.Width; x++)
                {
                    if (A[x, y] && !B[x, y])
                        newBmp.SetPixel(x, y, Color.Red);
                    else if (!A[x, y] && B[x, y])
                        newBmp.SetPixel(x, y, Color.Blue);
                    else if (A[x, y] || B[x, y])
                        newBmp.SetPixel(x, y, Color.Black);
                }
            return newBmp;
        }
        public static bool IsPixel(Color c)
        {
            return c.A > 128;
        }
        public bool CalculatePixel(int x, int y)
        {
            try
            {
                var c = Bitmap.GetPixel(x, y);
                return IsPixel(c);
            }
            catch { return false; }
        }
        public bool this[int x, int y]
        {
            get
            {
                if (!Cached[x, y])
                {
                    Pixels[x, y] = CalculatePixel(x, y);
                    Cached[x, y] = true;
                }
                return Pixels[x, y];
            }
            set
            {
                Pixels[x, y] = value;
                Cached[x, y] = true;
            }
        }
    }
}
