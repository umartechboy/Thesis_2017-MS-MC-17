using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RotatingBezierSplineEditor
{
    public partial class BezierBoardItemMenuItem : UserControl
    {
        public event EventHandler OnRequestToShowAll;
        public event EventHandler OnRequestToUnlockAll;
        public event EventHandler OnRequestShowOnly;
        public event EventHandler OnRequestToUnlockOnly;
        public event EventHandler OnMoveUpRequest;
        public event EventHandler OnMoveDownRequest;
        public void NotifyRequestToShowAll()
        {
            OnRequestToShowAll?.Invoke(this, new EventArgs());
        }
        public void NotifyRequestToUnlockAll()
        {
            OnRequestToUnlockAll?.Invoke(this, new EventArgs());
        }
        public void NotifyRequestShowOnly()
        {
            OnRequestShowOnly?.Invoke(this, new EventArgs());
        }
        public void NotifyRequestToUnlockOnly()
        {
            OnRequestToUnlockOnly?.Invoke(this, new EventArgs());
        }
        public void NotifyMoveUpRequest()
        {
            OnMoveUpRequest?.Invoke(this, new EventArgs());
        }
        public void NotifyMoveDownRequest()
        {
            OnMoveDownRequest?.Invoke(this, new EventArgs());
        }
        public BezierBoardItem Item { get; protected set; } 
        public BezierBoardItemMenuItem()
        {
            InitializeComponent();
        }
        public BezierBoardItemMenuItem(BezierBoardItem item )
        {
            this.Item = item;
        }
    }
}
