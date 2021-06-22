using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class SplineAppearanceEditor : Form
    {
        public SplineAppearanceEditor()
        {
            InitializeComponent();
        }

        private void colorP_Click(object sender, EventArgs e)
        {
        }

        private void widthTB_Scroll(object sender, EventArgs e)
        {
        }

        private void widthTB_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = widthTB.Value.ToString();
        }
    }
}
