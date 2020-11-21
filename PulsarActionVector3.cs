using Pulsar.EventArguments;
using System;
using System.Windows.Forms;
using Urho;

namespace Pulsar
{
    public partial class PulsarActionVector3 : UserControl
    {
        public event EventHandler ActionVector3Changed;

        private float _x;
        private float _y;
        private float _z;
        private string _heading;

        public Vector3 Vector3 
        { 
            get
            {
                return new Vector3(_x, _y, _z);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
                _z = value.Z;

                txtVectorX.Text = _x.ToString();
                txtVectorY.Text = _y.ToString();
                txtVectorZ.Text = _z.ToString();
            }
        }

        public string Heading 
        { 
            get
            {
                return _heading;
            }
            set
            {
                _heading = value;
                groupVector3.Text = value;
            }
        }

        public string PropertyName { get; set; }

        public PulsarActionVector3()
        {
            InitializeComponent();
        }

        private void VectorX_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVectorX.Text, out _))
            {
                txtVectorX.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVectorX.BackColor = System.Drawing.Color.White;
            }
        }

        private void VectorY_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVectorY.Text, out _))
            {
                txtVectorY.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVectorY.BackColor = System.Drawing.Color.White;
            }
        }

        private void VectorZ_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVectorZ.Text, out _))
            {
                txtVectorZ.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVectorZ.BackColor = System.Drawing.Color.White;
            }
        }

        private void VectorX_Changed(object sender, EventArgs e)
        {
            if(float.TryParse(txtVectorX.Text, out float vectorX))
            {
                _x = vectorX;
                Vector3 newVector = new Vector3(_x, _y, _z);
                Vector3 = newVector;

                RaiseVector3Event();
            }
        }

        private void RaiseVector3Event()
        {
            ActionVector3EventArgs args = new ActionVector3EventArgs
            {
                Vector = Vector3,
                PropertyName = PropertyName
            };
            ActionVector3Changed?.Invoke(this, args);
        }

        private void VectorY_Changed(object sender, EventArgs e)
        {
            if (float.TryParse(txtVectorY.Text, out float vectorY))
            {
                _y = vectorY;
                Vector3 newVector = new Vector3(_x, _y, _z);
                Vector3 = newVector;

                RaiseVector3Event();
            }
        }

        private void VectorZ_Changed(object sender, EventArgs e)
        {
            if (float.TryParse(txtVectorZ.Text, out float vectorZ))
            {
                _z = vectorZ;
                Vector3 newVector = new Vector3(_x, _y, _z);
                Vector3 = newVector;

                RaiseVector3Event();
            }
        }
    }
}
