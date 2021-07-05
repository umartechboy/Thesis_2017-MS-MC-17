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
    public partial class ExportMenu : Form
    {
        public ExportMenu()
        {
            InitializeComponent();
            polyRB.Checked = BezierBoard.FlatTipRenderAlgorithm == FlatTipRenderAlgorithm.Polygon;
            rectsRB.Checked = BezierBoard.FlatTipRenderAlgorithm == FlatTipRenderAlgorithm.Rectangle;
        }

        ExportRequest MakeRequest()
        {
            var er = new ExportRequest();
            if (inkCB.Checked)
                er.DrawMode |= InkDrawMode.Ink;
            if (splineCB.Checked)
                er.DrawMode |= InkDrawMode.Spline;
            if (handlesCB.Checked)
                er.AnchorMode |= AnchorDrawMode.All;
            er.RenderImages = bImageCB.Checked;
            er.DontRenderSplines = dontRenderSplineCB.Checked;
            er.RenderAlgorithm = polyRB.Checked ? FlatTipRenderAlgorithm.Polygon : FlatTipRenderAlgorithm.Rectangle;
            er.ForceSingleColor = singleColCB.Checked;
            er.ForceColor = colorP.BackColor;
            er.DPI = (int)dpiNUD.Value;
            return er;
        }
        private void exportB_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|PNG (*.png)|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //er.FileName = sfd.FileName;
                var request = MakeRequest();
                OnExportRequest?.Invoke(this, new ExportRequestEventArgs() { Request = request });
                if (request.RenderOutput == null)
                    return;
                request.RenderOutput.Save(sfd.FileName);
                DialogResult = DialogResult.OK;
            }
        }
        public event ExportRequestHandler OnExportRequest;

        private void inkCB_CheckedChanged(object sender, EventArgs e)
        {
            var request = MakeRequest();
            OnExportRequest?.Invoke(this, new ExportRequestEventArgs() { Request = request });
            pictureBox1.BackgroundImage = request.RenderOutput;
            if (request.RenderOutput == null)
                return;
            pictureBox1.Width = request.RenderOutput.Width;
            pictureBox1.Height = request.RenderOutput.Height;
        }

        private void prevB_Click(object sender, EventArgs e)
        {
            inkCB_CheckedChanged(this, new EventArgs());
        }

        private void ExportMenu_Load(object sender, EventArgs e)
        {
            inkCB_CheckedChanged(this, new EventArgs());
        }

        private void dpiNUD_ValueChanged(object sender, EventArgs e)
        {
            inkCB_CheckedChanged(this, new EventArgs());
        }

        private void colorP_MouseClick(object sender, MouseEventArgs e)
        {
            var cd = new ColorDialog();
            cd.Color = colorP.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                colorP.BackColor = cd.Color;
                inkCB_CheckedChanged(this, new EventArgs());
            }
        }

        private void polyRB_CheckedChanged(object sender, EventArgs e)
        {
            inkCB_CheckedChanged(this, new EventArgs());
        }

        private void rectsRB_CheckedChanged(object sender, EventArgs e)
        {
            inkCB_CheckedChanged(this, new EventArgs());
        }
    }
    public delegate void ExportRequestHandler(object sender, ExportRequestEventArgs e);
    public class ExportRequestEventArgs : EventArgs
    {
        public ExportRequest Request { get; set; }
    }
    public class ExportRequest
    {
        public InkDrawMode DrawMode { get; set; } = InkDrawMode.None;
        public AnchorDrawMode AnchorMode { get; set; } = AnchorDrawMode.None;
        public int DPI { get; set; }
        public bool RenderImages { get; set; }
        public bool DontRenderSplines { get; set; }
        //public string FileName { get; set; }
        public Image RenderOutput { get; set; }
        public bool ForceSingleColor { get; internal set; }
        public Color ForceColor { get; internal set; }
        public FlatTipRenderAlgorithm RenderAlgorithm { get; internal set; }
    }
}
