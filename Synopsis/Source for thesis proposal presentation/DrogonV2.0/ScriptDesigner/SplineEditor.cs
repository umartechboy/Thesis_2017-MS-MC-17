using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplineDesigner
{
    public partial class SplineEditor : UserControl
    {
        public SplineEditor()
        {
            InitializeComponent();
        }

        private void addP_Click(object sender, EventArgs e)
        {
            bezierBoard1.Op = ops.AddNewPoint;
        }

        private void removeP_Click(object sender, EventArgs e)
        {
            bezierBoard1.Op = ops.RemovePoint;
        }

        private void changeP_Click(object sender, EventArgs e)
        {
            bezierBoard1.Op = ops.ChangePoint;
        }

        private void changeRotationB_Click(object sender, EventArgs e)
        {
            bezierBoard1.Op = ops.ChangeRotation;
        }

        SaveFileDialog sfd = new SaveFileDialog();
        OpenFileDialog ofd = new OpenFileDialog();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfd.Filter = "Text Files(*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)                                
                System.IO.File.WriteAllText(sfd.FileName, bezierBoard1.Spline.ToSaveString());
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bezierBoard1.Spline = BezierSpline.FromFile(ofd.FileName)[0];
                bezierBoard1.Invalidate();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            bezierBoard1.Spline.FlatTipThickness = (float)numericUpDown1.Value;
            bezierBoard1.Invalidate();
        }

        private void openBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Images|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bezierBoard1.BackgroundImage = Image.FromFile(ofd.FileName);
                Width += bezierBoard1.BackgroundImage.Width - bezierBoard1.Width;
                Height += bezierBoard1.BackgroundImage.Height - bezierBoard1.Height;
                bezierBoard1.Invalidate();
            }
        }

        private void inkOnlyCB_CheckedChanged(object sender, EventArgs e)
        {
            bezierBoard1.InkOnly = inkOnlyCB.Checked;
        }
    }
}
