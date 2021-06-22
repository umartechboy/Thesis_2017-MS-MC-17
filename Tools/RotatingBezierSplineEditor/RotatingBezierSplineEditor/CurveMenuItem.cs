using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class CurveMenuItem : UserControl
    {
        public CurveMenuItem()
        {
            InitializeComponent();
            try
            {
                visibleTC.SetImage(Image.FromFile("Resources\\visible.png"), 30);
                activeTC.SetImage(Image.FromFile("Resources\\active.png"), 30);
            }
            catch { }
        }
    }
}
