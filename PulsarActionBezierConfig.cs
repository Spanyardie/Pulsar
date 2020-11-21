using Pulsar.EventArguments;
using System;
using System.Windows.Forms;
using Urho;

namespace Pulsar
{
    public partial class PulsarActionBezierConfig : UserControl
    {
        public event EventHandler ActionBezierChanged;

        private Vector3 _controlPoint1;
        private Vector3 _controlPoint2;
        private Vector3 _endPoint;

        public Vector3 ControlPoint1 
        { 
            get
            {
                return _controlPoint1;
            }
            set
            {
                _controlPoint1 = value;
                txtBezierConfigControlPoint1X.Text = value.X.ToString();
                txtBezierConfigControlPoint1Y.Text = value.Y.ToString();
                txtBezierConfigControlPoint1Z.Text = value.Z.ToString();
            }
        }

        public Vector3 ControlPoint2
        {
            get
            {
                return _controlPoint2;
            }
            set
            {
                _controlPoint1 = value;
                txtBezierConfigControlPoint2X.Text = value.X.ToString();
                txtBezierConfigControlPoint2Y.Text = value.Y.ToString();
                txtBezierConfigControlPoint2Z.Text = value.Z.ToString();
            }
        }

        public Vector3 EndPoint
        {
            get
            {
                return _endPoint;
            }
            set
            {
                _controlPoint1 = value;
                txtBezierConfigEndPointX.Text = value.X.ToString();
                txtBezierConfigEndPointY.Text = value.Y.ToString();
                txtBezierConfigEndPointZ.Text = value.Z.ToString();
            }
        }

        public string PropertyName { get; set; }

        public PulsarActionBezierConfig()
        {
            InitializeComponent();
        }

        private void ControlPoint1X_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigControlPoint1X.Text, out _))
            {
                txtBezierConfigControlPoint1X.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigControlPoint1X.BackColor = System.Drawing.Color.White;
            }
        }

        private void ControlPoint1Y_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigControlPoint1Y.Text, out _))
            {
                txtBezierConfigControlPoint1Y.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigControlPoint1Y.BackColor = System.Drawing.Color.White;
            }
        }

        private void ControlPoint1Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigControlPoint1Z.Text, out _))
            {
                txtBezierConfigControlPoint1Z.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigControlPoint1Z.BackColor = System.Drawing.Color.White;
            }
        }

        private void ControlPoint2X_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigControlPoint2X.Text, out _))
            {
                txtBezierConfigControlPoint2X.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigControlPoint2X.BackColor = System.Drawing.Color.White;
            }
        }

        private void ControlPoint2Y_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigControlPoint2Y.Text, out _))
            {
                txtBezierConfigControlPoint2Y.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigControlPoint2Y.BackColor = System.Drawing.Color.White;
            }
        }

        private void ControlPoint2Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigControlPoint2Z.Text, out _))
            {
                txtBezierConfigControlPoint2Z.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigControlPoint2Z.BackColor = System.Drawing.Color.White;
            }
        }

        private void EndPointX_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigEndPointX.Text, out _))
            {
                txtBezierConfigEndPointX.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigEndPointX.BackColor = System.Drawing.Color.White;
            }
        }

        private void EndPointY_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigEndPointY.Text, out _))
            {
                txtBezierConfigEndPointY.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigEndPointY.BackColor = System.Drawing.Color.White;
            }
        }

        private void EndPointZ_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtBezierConfigEndPointZ.Text, out _))
            {
                txtBezierConfigEndPointZ.BackColor = System.Drawing.Color.MistyRose;
                e.Cancel = true;
            }
            else
            {
                txtBezierConfigEndPointZ.BackColor = System.Drawing.Color.White;
            }
        }

        private void ControlPoint1X_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigControlPoint1X.Text, out float vectorX))
            {
                Vector3 newVector = new Vector3(vectorX, ControlPoint1.Y, ControlPoint1.Z);
                _controlPoint1 = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void ControlPoint1Y_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigControlPoint1Y.Text, out float vectorY))
            {
                Vector3 newVector = new Vector3(ControlPoint1.X, vectorY, ControlPoint1.Z);
                _controlPoint1 = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void ControlPoint1Z_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigControlPoint1Z.Text, out float vectorZ))
            {
                Vector3 newVector = new Vector3(ControlPoint1.X, ControlPoint1.Y, vectorZ);
                _controlPoint1 = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void ControlPoint2X_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigControlPoint2X.Text, out float vectorX))
            {
                Vector3 newVector = new Vector3(vectorX, ControlPoint2.Y, ControlPoint2.Z);
                _controlPoint2 = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void ControlPoint2Y_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigControlPoint2Y.Text, out float vectorY))
            {
                Vector3 newVector = new Vector3(ControlPoint2.X, vectorY, ControlPoint2.Z);
                _controlPoint2 = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void ControlPoint2Z_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigControlPoint2Z.Text, out float vectorZ))
            {
                Vector3 newVector = new Vector3(ControlPoint2.X, ControlPoint2.Y, vectorZ);
                _controlPoint2 = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void EndPointX_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigEndPointX.Text, out float vectorX))
            {
                Vector3 newVector = new Vector3(vectorX, EndPoint.Y, EndPoint.Z);
                _endPoint = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void EndPointY_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigEndPointY.Text, out float vectorY))
            {
                Vector3 newVector = new Vector3(EndPoint.X, vectorY, EndPoint.Z);
                _endPoint = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void EndPointZ_Changed(object sender, System.EventArgs e)
        {
            if (float.TryParse(txtBezierConfigEndPointZ.Text, out float vectorZ))
            {
                Vector3 newVector = new Vector3(EndPoint.X, EndPoint.Y, vectorZ);
                _endPoint = newVector;

                RaiseBezierChangedEvent();
            }
        }

        private void RaiseBezierChangedEvent()
        {
            ActionBezierConfigEventArgs args = new ActionBezierConfigEventArgs
            {
                ControlPoint1 = _controlPoint1,
                ControlPoint2 = _controlPoint2,
                EndPoint = _endPoint,
                PropertyName = "BezierConfig"
            };

            ActionBezierChanged?.Invoke(this, args);
        }
    }
}
