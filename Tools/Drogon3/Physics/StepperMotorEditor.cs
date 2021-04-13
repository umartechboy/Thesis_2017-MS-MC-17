using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drogon3
{
    public partial class StepperMotorEditor : UserControl
    {
        public StepperMotorEditor()
        {
            InitializeComponent();
        }
        public StepperMotorEditor(int id) : this() 
        {
            this.id.Text = "A" + id.ToString();
        }
    }
}
