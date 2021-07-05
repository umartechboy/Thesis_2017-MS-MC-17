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
    public partial class SplinesCollectionUC : UserControl
    {
        public SplinesCollectionUC()
        {
            InitializeComponent();
        }

        public List<BezierSpline> GetSplines()
        {
            return splineCollectionBoard1.Splines;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SplineEditorForm f = new SplineEditorForm();
            f.ShowDialog();
            var s = f.splineEditor1.bezierBoard1.Spline;
            
            if (s.Anchors.Count > 0)
            {
                splineCollectionBoard1.Splines.Add(s);
                splineCollectionBoard1.Invalidate();
            }
        }
                                                                                              
        SaveFileDialog sfd = new SaveFileDialog();
        OpenFileDialog ofd = new OpenFileDialog();
        private void saveSplinesB_Click(object sender, EventArgs e)
        {                                                                                     
            sfd.Filter = "Text Files(*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var spline in splineCollectionBoard1.Splines)
                {
                    sb.AppendLine(spline.ToSaveString() + "%");
                }

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString());
            }
        }

        private void openB_Click(object sender, EventArgs e)
        {                      
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                splineCollectionBoard1.Splines = BezierSpline.FromFile(ofd.FileName);
                splineCollectionBoard1.Invalidate();
            }
        }
    }
}
