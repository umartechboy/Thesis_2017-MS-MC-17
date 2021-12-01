using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class TraceAnalyzer : Form
    {
        BezierBoard Board;
        public TraceAnalyzer()
        {
            InitializeComponent();
        }

        public TraceAnalyzer(BezierBoard board) : this()
        {
            this.Board = board;
        }
        public TraceAnalyzer(Image refImg, Image inkImg):this()
        {
            alternateSource = refImg;
            alternateInk = inkImg;
        }


        public event ExportRequestHandler OnExportRequest;
        Image RenderInk()
        {
            var er = new ExportRequest();
            er.DrawMode = InkDrawMode.Ink;
            er.RenderAllImages = false;
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
            if (alternateSource != null)
                return alternateSource;
            var er = new ExportRequest();
            er.DrawMode = InkDrawMode.Images;
            er.RenderAllImages = false;
            er.AlternateImageToRender = ReferenceImageItem;
            er.DontRenderSplines = true;
            er.RenderAlgorithm = FlatTipRenderAlgorithm.Rectangle;
            er.DPI = (int)dpiNUD.Value;
            OnExportRequest?.Invoke(this, new ExportRequestEventArgs() { Request = er });
            if (er.RenderOutput == null)
            {
                var bmp = new Bitmap(ReferenceImageItem.SourceImage);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
                return bmp;
            }
            return er.RenderOutput;
        }

        ImageItem ReferenceImageItem;
        BinaryBitmap ReferenceBitmap, InkBitmap;
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
            if (inkFromScene.Checked)
                inkImg = RenderInk();
            worker.ReportProgress(10);
            if (worker.CancellationPending) return;
            var sourceImg = RenderSource();
            worker.ReportProgress(20);
            if (worker.CancellationPending) return;
            if (inkImg == null || sourceImg == null)
                return;
            if (inkImg.Size != sourceImg.Size)
                return;
            InkBitmap = new BinaryBitmap(new Bitmap(inkImg));
            ReferenceBitmap = new BinaryBitmap(new Bitmap(sourceImg));
            Extra = BinaryBitmap.Subtract(InkBitmap, ReferenceBitmap);
            worker.ReportProgress(35);
            if (worker.CancellationPending) return;
            Missing = BinaryBitmap.Subtract(ReferenceBitmap, InkBitmap);
            worker.ReportProgress(50);
            if (worker.CancellationPending) return;
            Highlight = BinaryBitmap.Difference(ReferenceBitmap, InkBitmap);
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

        private void loadImageB_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.bmp, *.png)|*.png;*.jpg;*.bmp|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    AddReferenceImage(ImageItem.FromFile(Board, ofd.FileName));
                }
                catch { }
            }
        }

        private void loadInkFromFile_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void saveCurrentB_Click(object sender, EventArgs e)
        {
            if (previewPB.Image == null)
                return;
            var sfd = new SaveFileDialog();
            sfd.Filter = "Transperent image (*.png)|*.png;|Bitmap (*.bmp)|*.bmp|Light weight compressed image (*.jpg)|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                previewPB.Image.Save(sfd.FileName);
            }

        }

        private void SaveResults_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Title = "Choose a seed name";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var seed = Path.Combine(
                    Path.GetDirectoryName(sfd.FileName),
                    Path.GetFileNameWithoutExtension(sfd.FileName));
                Extra?.Save(seed + " - Extra - " + dpiNUD.Value + "dpi.png", System.Drawing.Imaging.ImageFormat.Png);
                Missing?.Save(seed + " - Missing - " + dpiNUD.Value + "dpi.png", System.Drawing.Imaging.ImageFormat.Png);
                Highlight?.Save(seed + " - Highlights - " + dpiNUD.Value + "dpi.png", System.Drawing.Imaging.ImageFormat.Png);
                ReferenceBitmap.Bitmap?.Save(seed + " - Source - " + dpiNUD.Value + "dpi.png", System.Drawing.Imaging.ImageFormat.Png);
                InkBitmap.Bitmap?.Save(seed + " - Ink - " + dpiNUD.Value + "dpi.png", System.Drawing.Imaging.ImageFormat.Png);
                File.WriteAllText(seed + " - Summary - " + dpiNUD.Value + "dpi.txt", summaryL.Text);
            }
        }

        private void inkFromScene_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void loadInkFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if (loadInkFromFile.Checked)
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Image file (*.png, *.bmp, *.jpg)|*.png;*.bmp;*.jpg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        loadInkFromFile.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
                        alternateInk = Image.FromFile(ofd.FileName);
                        Process();
                        Display();
                    }
                    catch { }
                }
            }
        }

        private void loadInkFromFile_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void bWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progLabel.Text = "";
            progP.Value = 0;
            Display();
            processB.Text = "Process";
            if (ReferenceBitmap == null || InkBitmap == null)
                return;
            summaryL.Text = string.Format("Total Pixels: {0}\r\nPixels in Ink: {1}\r\nMissing: -{2}% ({3})\r\nExtra: {4}% ({5})",
                ReferenceBitmap.Width * ReferenceBitmap.Height,
                SourcePixels,
                (MissingPixels / (double)SourcePixels * 100).ToString("F2"),
                MissingPixels,
                (ExtraPixels / (double)SourcePixels * 100).ToString("F2"),
                ExtraPixels);
            Clipboard.SetText(summaryL.Text);
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
                previewPB.Image = Extra;
            else if (missingCB.Checked)
                previewPB.Image = Missing;
            else if (highLightCB.Checked)
                previewPB.Image = Highlight;
            else if (sourceCB.Checked)
                previewPB.Image = ReferenceBitmap.Bitmap;
            else if (traceCB.Checked)
            {
                if (InkBitmap != null)
                    try
                    {
                        previewPB.Image = InkBitmap.Bitmap;
                    }
                    catch
                    { }
            }
        }
        private void previewModeCBs_CheckedChanged(object sender, EventArgs e)
        {
            Display();
        }

        internal void AddReferenceImage(ImageItem sourceImage)
        {
            var p = new Panel();
            p.Cursor = Cursors.Hand;
            void p_Click()
            {
                foreach (var po in imagesFL.Controls.OfType<Panel>())
                    po.BorderStyle = BorderStyle.None;
                p.BorderStyle = BorderStyle.Fixed3D;
                ReferenceImageItem = sourceImage;
            }
            p.Click += (s, e) => p_Click();
            p.BackgroundImageLayout = ImageLayout.Zoom;
            var img = new Bitmap(sourceImage.SourceImage);
            img.RotateFlip(RotateFlipType.Rotate180FlipX);
            p.BackgroundImage = img;
            p.Width = imagesFL.Height - 15;
            p.Height = imagesFL.Height - 15;
            imagesFL.Controls.Add(p);
            if (imagesFL.Controls.Count == 1)
                p_Click();
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
