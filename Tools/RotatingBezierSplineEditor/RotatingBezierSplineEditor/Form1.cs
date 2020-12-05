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
            // set images
            centerP.SetImage(Image.FromFile("Center.png"), 65); 
            curvatureHandlesP.SetImage(Image.FromFile("CurvatureHandles.png"), 65);
            rotationHandleP.SetImage(Image.FromFile("RotationHandles.png"), 56);

            // set targets
            centerP.TargetPart = AnchorParts.Center;
            curvatureHandlesP.TargetPart = AnchorParts.CurvatureHandle1 | AnchorParts.CurvatureHandle2;
            rotationHandleP.TargetPart = AnchorParts.RotationHandle1 | AnchorParts.RotationHandle2;

            centerP.OnActivated += (s, e) => { bezierBoard1.AnchorDrawMode = AnchorDrawMode.Centers; };
            curvatureHandlesP.OnActivated += (s, e) => { bezierBoard1.AnchorDrawMode = AnchorDrawMode.CurvatureHandles; };
            rotationHandleP.OnActivated += (s, e) => { bezierBoard1.AnchorDrawMode = AnchorDrawMode.RotaionHandles; };

            bothSplinesP.SetImage(Image.FromFile("bothSplines.png"), 45);
            rotatingSplineOnlyP.SetImage(Image.FromFile("rotatingSplineOnly.png"), 45);
            linearSplineOnly.SetImage(Image.FromFile("splineOnly.png"), 45);

            bothSplinesP.OnActivated += (s, e) => { bezierBoard1.InkDrawMode = InkDrawMode.Ink | InkDrawMode.Spline;};
            rotatingSplineOnlyP.OnActivated += (s, e) => { bezierBoard1.InkDrawMode = InkDrawMode.Ink;};
            linearSplineOnly.OnActivated += (s, e) => { bezierBoard1.InkDrawMode = InkDrawMode.Spline; };



            var sp = new RotatingBezierSpline();
            var a0 = new RotatingBezierSplineAnchor(100, 100);
            var a1 = new RotatingBezierSplineAnchor(300, 100);
            var a2 = new RotatingBezierSplineAnchor(300, 300);
            a0.C1 = new PointD(150, 150);
            a0.C2 = new PointD(50, 50);

            a1.C1 = new PointD(350, 50);
            a1.C2 = new PointD(250, 150);

            a2.C1 = new PointD(300, 300);
            a2.C2 = new PointD(250, 300);
            sp.Thickness = 40;
            sp.Anchors.Add(a0); sp.Anchors.Add(a1); sp.Anchors.Add(a2);
            bezierBoard1.Paths.Add(sp);
        }
    }
}
