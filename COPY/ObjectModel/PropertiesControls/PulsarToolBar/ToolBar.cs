using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PulsarToolBar
{
    public partial class PulsarToolBar: UserControl
    {
        public event EventHandler AddComponent;

        private int _minimumWidth = 332;
        private int _minimumHeight = 32;
        private int _maximumHeight = 32;

        public int MinimumWidth 
        { 
            get
            {
                return _minimumWidth;
            }
            private set { } 
        }

        public int MinimumHeight 
        { 
            get
            {
                return _minimumHeight;
            }
            private set { } 
        }

        public int MaximumHeight 
        { 
            get
            {
                return _maximumHeight;
            }
            private set { } 
        }

        public PulsarToolBar()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Invoke(AddComponent);
        }
    }
}
