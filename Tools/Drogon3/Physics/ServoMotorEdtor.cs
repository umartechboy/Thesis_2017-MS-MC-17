using RoboSim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Physics
{
    public partial class ServoMotorEditor : UserControl
    {
        private int id1;

        public ServoMotorEditor()
        {
            InitializeComponent();
        }

        public ServoMotorEditor(int id):this()
        {
            this.id1 = id;
        }
    }
}
