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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            centerP.SetImage(Image.FromFile("Center.png"), 65);
            lineHandleP.SetImage(Image.FromFile("CurvatureHandles.png"), 65);
            rotationHandleP.SetImage(Image.FromFile("RotationHandles.png"), 56);
            bothSplinesP.SetImage(Image.FromFile("bothSplines.png"), 45);
            linearSplineOnly.SetImage(Image.FromFile("rotatingSplineOnly.png"), 45);
            rotatingSplineOnlyP.SetImage(Image.FromFile("splineOnly.png"), 45);
        }
    }
}
