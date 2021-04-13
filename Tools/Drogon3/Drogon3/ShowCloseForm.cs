using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoboSim
{
    public class ShowCloseForm : Form
    {
        public event EventHandler OnHidden;
        public new event EventHandler OnShown;
        public void NotifyHidden() { OnHidden?.Invoke(this, new EventArgs()); }
        public void NotifyShown() { OnShown?.Invoke(this, new EventArgs()); }
        public void Show2(bool value)
        {
            if (value)
                Show2();
            else
                Hide2();
        }
        public virtual void Show2()
        {
            Show();
            NotifyShown();
        }
        public virtual void Hide2()
        {
            Hide();
            NotifyHidden();
        }
    }
}
