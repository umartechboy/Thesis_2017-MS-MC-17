using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboSim
{
    public partial class PanelPlotForm : ShowCloseForm
    {
        public ExplorablePanelExt explorer = new ExplorablePanelExt();
        public Graphics G;
        public PanelPlotForm(string name_, float xPos, float yPos)
        {
            InitializeComponent();
            dispP.BackgroundImage = new Bitmap(dispP.Width, dispP.Height);
            G = Graphics.FromImage(dispP.BackgroundImage);
            explorer.SubscribePanel(dispP, new PointF(dispP.BackgroundImage.Width * xPos, dispP.BackgroundImage.Height * yPos));
            Text = name_;
        }                              

        private void form_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                dispP.BackgroundImage = new Bitmap(dispP.Width, dispP.Height);
                G = Graphics.FromImage(dispP.BackgroundImage);
            }
        }
        public bool IsActive = false;

        private void PanelPlotForm_Load(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void PanelPlotForm_FormClosing(object sender, FormClosingEventArgs e)
        {                        
            //e.Cancel = true;
        }

        private void PanelPlotForm_Shown(object sender, EventArgs e)
        {

        }
        public override void Show2()
        {
            Show();
        }
        public override void Hide2()
        {
             Hide();
        }

        private void saveB_Click(object sender, EventArgs e)
        {
            try
            {
                dispP.BackgroundImage.Save(saveFileTB.Text, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch
            {
                MessageBox.Show("The image could not be saved.");
            }
        }

        private void saveFileTB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap Images (*.bmp)|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                saveFileTB.Text = sfd.FileName;
            }
        }
    }
}
