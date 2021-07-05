using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace RoboSim
{
    public partial class WindowToForm : ShowCloseForm
    {
        public Window Face;
        public WindowToForm(Window face, SphericalRobot robot, Ink ink, WorkSpace workspace)
        {
            InitializeComponent();
            Face = face;
            ((MainWpfPrevWindow)Face).Intialize(robot, ink, workspace);
            Face.LocationChanged += Face_LocationChanged;
            Face.SizeChanged += Face_SizeChanged;
            LocationChanged += WindowToForm_LocationChanged;
        }
        private void WindowToForm_LocationChanged(object sender, EventArgs e)
        {
            Face.Left = Left;
            Face.Top = Top;
        }

        private void Face_SizeChanged(object sender, SizeChangedEventArgs e)
        {                             
            Width = (int)Math.Round(Face.Width);
            Height = (int)Math.Round(Face.Height);
        }

        private void Face_LocationChanged(object sender, EventArgs e)
        {
            LocationChanged -= WindowToForm_LocationChanged;
            Left = (int)Math.Round(Face.Left);
            Top = (int)Math.Round(Face.Top);
            LocationChanged += WindowToForm_LocationChanged;
        }

        
        private void WindowToForm_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Visible && Face.Visibility == Visibility.Visible)
            {
            }
        }
        public override void Show2()
        {       
            Face.Left = Left;
            Face.Top = Top;
            Face.SizeChanged -= Face_SizeChanged;
            Face.Width = Width;
            Face.Height = Height;
            Face.SizeChanged += Face_SizeChanged;

            System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Face);
            Show();
            Face.Show();
        }
        public override void Hide2()
        {
            Hide();
            Face.Hide();
        }
    }   
}
