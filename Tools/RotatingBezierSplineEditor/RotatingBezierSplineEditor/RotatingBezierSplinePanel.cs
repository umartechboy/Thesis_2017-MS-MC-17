using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public class RotatingBezierSplinePanel:Panel
    {
        bool _drawSpline = true, _drawInkMark = true;
        public bool DrawBezierSpline { get => _drawSpline; set { _drawSpline = value;Invalidate(); } }
        public bool DrawInkMark { get => DrawInkMark; set { _drawInkMark = value; Invalidate(); } }

        public EditMode EditMode { get; set; } = EditMode.None;
        public RotatingBezierSplinePanel()
        {
            DoubleBuffered = true;
        }
    }

    public enum EditMode
    {
        Centers,
        RotationHandles,
        CurvatureHandles,
        Objects,
        None
    }
}
