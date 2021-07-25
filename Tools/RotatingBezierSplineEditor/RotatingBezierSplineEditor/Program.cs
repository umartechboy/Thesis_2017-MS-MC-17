using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] Args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var f = new MainForm();
                if (Args.Length > 0)
                    f.FileToLoad = Args[0];
                Application.Run(f);
            }
            catch (Exception ex){ MessageBox.Show(ex.ToString()); }
        }
    }
}
