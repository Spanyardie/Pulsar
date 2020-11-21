using Pulsar.EventArguments;
using Pulsar.ObjectModel.Primitives;
using System;
using System.Windows.Forms;
using Urho;

namespace Pulsar
{
    public partial class PulsarActionVector4 : UserControl
    {
        public event EventHandler ActionVector4PropertyChanged;

        private float _r;
        private float _g;
        private float _b;
        private float _a;
        private string _heading;

        public PulsarVector4RGBA Vector4
        {
            get
            {
                return new PulsarVector4RGBA(_r, _g, _b, _a);
            }
            set
            {
                _r = value.R;
                _g = value.G;
                _b = value.B;
                _a = value.A;

                txtVector4R.Text = _r.ToString();
                txtVector4G.Text = _g.ToString();
                txtVector4B.Text = _b.ToString();
                txtVector4A.Text = _a.ToString();
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
                groupVector4.Text = value;
            }
        }

        public string PropertyName { get; set; }

        public PulsarActionVector4()
        {
            InitializeComponent();
        }

        private void VectorR_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVector4R.Text, out _))
            {
                txtVector4R.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVector4R.BackColor = System.Drawing.Color.White;
            }
        }

        private void VectorG_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVector4G.Text, out _))
            {
                txtVector4G.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVector4G.BackColor = System.Drawing.Color.White;
            }
        }

        private void VectorB_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVector4B.Text, out _))
            {
                txtVector4B.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVector4B.BackColor = System.Drawing.Color.White;
            }
        }

        private void VectorA_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtVector4A.Text, out _))
            {
                txtVector4A.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtVector4A.BackColor = System.Drawing.Color.White;
            }
        }

        private void Vector4R_Changed(object sender, EventArgs e)
        {
            if (float.TryParse(txtVector4R.Text, out float vectorR))
            {
                _r = vectorR;
                PulsarVector4RGBA newVector = new PulsarVector4RGBA(_r, _g, _b, _a);
                Vector4 = newVector;
                RaiseEvent();
            }
        }

        private void Vector4G_Changed(object sender, EventArgs e)
        {
            if (float.TryParse(txtVector4G.Text, out float vectorG))
            {
                _g = vectorG;
                PulsarVector4RGBA newVector = new PulsarVector4RGBA(_r, _g, _b, _a);
                Vector4 = newVector;
                RaiseEvent();
            }
        }

        private void Vector4B_Changed(object sender, EventArgs e)
        {
            if (float.TryParse(txtVector4B.Text, out float vectorB))
            {
                _b = vectorB;
                PulsarVector4RGBA newVector = new PulsarVector4RGBA(_r, _g, _b, _a);
                Vector4 = newVector;
                RaiseEvent();
            }
        }

        private void Vector4A_Changed(object sender, EventArgs e)
        {
            if (float.TryParse(txtVector4A.Text, out float vectorA))
            {
                _a = vectorA;
                PulsarVector4RGBA newVector = new PulsarVector4RGBA(_r, _g, _b, _a);
                Vector4 = newVector;
                RaiseEvent();
            }
        }

        private void RaiseEvent()
        {
            ActionVector4EventArgs args = new ActionVector4EventArgs
            {
                Vector = Vector4,
                PropertyName = PropertyName
            };

            ActionVector4PropertyChanged?.Invoke(this, args);
        }
    }
}
