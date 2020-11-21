using System;
using System.Linq;
using System.Windows.Forms;
using Urho;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ExtendedNodeProperties
{
    public partial class BasicNodeProperties : UserControl
    {
        public enum WindowRoll
        {
            RollDown = 0,
            RollUp
        }
        private WindowRoll _windowRoll = WindowRoll.RollUp;

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd); 

        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        public event EventHandler PositionChanged;
        public event EventHandler RotationChanged;
        public event EventHandler ScaleChanged;
        public event EventHandler NodeNameChanged;
        public new event EventHandler EnabledChanged;
        public event EventHandler WindowRolled;

        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _scale;
        private string _name;
        private string _originalName;
        private bool _enabled;
        private bool _externallySet;

        private const int MINIMUM_HEIGHT = 33;
        private const int MAXIMUM_HEIGHT = 269;
        public int MinimumHeight
        {
            get
            {
                return MINIMUM_HEIGHT;
            }
        }

        public int MaximumHeight
        {
            get
            {
                return MAXIMUM_HEIGHT;
            }
        }

        public bool ExternallySet 
        { 
            get
            {
                return _externallySet;
            }
            set
            {
                _externallySet = value;
            }
        }

        public Vector3 Position 
        { 
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                txtPositionX.Text = _position.X.ToString();
                txtPositionY.Text = _position.Y.ToString();
                txtPositionZ.Text = _position.Z.ToString();
            }
        }

        public Vector3 Rotation 
        { 
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                txtRotationX.Text = string.Format("{0:##0.0###}", _rotation.X.ToString());
                txtRotationY.Text = _rotation.Y.ToString();
                txtRotationZ.Text = _rotation.Z.ToString();
            }
        }
        public new Vector3 Scale 
        { 
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                txtScaleX.Text = _scale.X.ToString();
                txtScaleY.Text = _scale.Y.ToString();
                txtScaleZ.Text = _scale.Z.ToString();
            }
        }
        public string NodeName 
        { 
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                txtName.Text = _name;
                _originalName = _name;
            }
        }
        public new bool Enabled 
        { 
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                chkEnabled.Checked = _enabled;
            }
        }

        private int _lastMouseXPosition;

        private const float MIN_POS = -1000;
        private const float MAX_POS = 1000;
        private const float MIN_ROT = -180;
        private const float MAX_ROT = 180;
        private const float MIN_SCALE = 1;
        private const float MAX_SCALE = 1000;

        private const float POS_DELTA = 1;
        private const float ROT_DELTA = 0.5f;
        private const float SCALE_DELTA = 0.5f;

        public BasicNodeProperties()
        {
            InitializeComponent();
            Resize += BasicNodeProperties_Resize;
        }

        private void BasicNodeProperties_Resize(object sender, EventArgs e)
        {
            if(Width > 312)
            {
                Size size = lblHeader.Size;
                size.Width = Width;
                lblHeader.Size = size;
            }
        }

        private void HideTextBoxCaret(TextBox textBox)
        {
            HideCaret(textBox.Handle);
        }

        private void ShowTextBoxCaret(TextBox textBox)
        {
            ShowCaret(textBox.Handle);
        }

        protected virtual void OnPositionChanged(ValueChangedEventArgs e)
        {
            if(!_externallySet)
                PositionChanged?.Invoke(this, e);
        }

        protected virtual void OnRotationChanged(ValueChangedEventArgs e)
        {
            if (!_externallySet)
                RotationChanged?.Invoke(this, e);
        }

        protected virtual void OnScaleChanged(ValueChangedEventArgs e)
        {
            if (!_externallySet)
                ScaleChanged?.Invoke(this, e);
        }

        protected virtual void OnNodeNameChanged(NodeNameChangedEventArgs e)
        {
            if (!_externallySet)
                NodeNameChanged?.Invoke(this, e);
        }

        protected virtual void OnNodeEnabledChanged(NodeEnabledChangedEventArgs e)
        {
            if (!_externallySet)
                EnabledChanged?.Invoke(this, e);
        }

        protected virtual void OnWindowRolled(WindowRollEventArgs e)
        {
            WindowRolled?.Invoke(this, e);
        }

        private void ValidatePosition(object sender, EventArgs e)
        {
            //world length is 1000, 1000, 1000
            TextBox textBox = (TextBox)sender;
            if(textBox != null)
            {
                if (!float.TryParse(textBox.Text, out float textValue))
                {
                    textBox.SelectionStart = 0;
                    textBox.SelectionLength = textBox.Text.Length;
                    textBox.BackColor = System.Drawing.Color.MistyRose;
                }
                else
                {
                    if (textValue < MIN_POS || textValue > MAX_POS)
                    {
                        textBox.SelectionStart = 0;
                        textBox.SelectionLength = textBox.Text.Length;
                        textBox.BackColor = System.Drawing.Color.MistyRose;
                    }
                    else
                    {
                        textBox.BackColor = System.Drawing.Color.White;
                        switch (textBox.Tag)
                        {
                            case "X":
                                _position = new Vector3(textValue, _position.Y, _position.Z);
                                break;
                            case "Y":
                                _position = new Vector3(_position.X, textValue, _position.Z);
                                break;
                            case "Z":
                                _position = new Vector3(_position.X, _position.Y, textValue);
                                break;
                        }
                        ValueChangedEventArgs args = new ValueChangedEventArgs()
                        {
                            ValueChanged = _position
                        };
                        OnPositionChanged(args);
                    }
                }
            }

        }

        private void ValidateRotation(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (!float.TryParse(textBox.Text, out float textValue))
                {
                    textBox.SelectionStart = 0;
                    textBox.SelectionLength = textBox.Text.Length;
                    textBox.BackColor = System.Drawing.Color.MistyRose;
                }
                else
                {
                    if (textValue < MIN_ROT || textValue > MAX_ROT)
                    {
                        textBox.SelectionStart = 0;
                        textBox.SelectionLength = textBox.Text.Length;
                        textBox.BackColor = System.Drawing.Color.MistyRose;
                    }
                    else
                    {
                        textBox.BackColor = System.Drawing.Color.White;
                        switch (textBox.Tag)
                        {
                            case "X":
                                _rotation = new Vector3(textValue, _rotation.Y, _rotation.Z);
                                break;
                            case "Y":
                                _rotation = new Vector3(_rotation.X, textValue, _rotation.Z);
                                break;
                            case "Z":
                                _rotation = new Vector3(_rotation.X, _rotation.Y, textValue);
                                break;
                        }
                        ValueChangedEventArgs args = new ValueChangedEventArgs()
                        {
                            ValueChanged = _rotation
                        };
                        OnRotationChanged(args);
                    }
                }
            }

        }

        private void ValidateScale(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (!float.TryParse(textBox.Text, out float textValue))
                {
                    textBox.SelectionStart = 0;
                    textBox.SelectionLength = textBox.Text.Length;
                    textBox.BackColor = System.Drawing.Color.MistyRose;
                }
                else
                {
                    if (textValue < MIN_SCALE || textValue > MAX_SCALE)
                    {
                        textBox.SelectionStart = 0;
                        textBox.SelectionLength = textBox.Text.Length;
                        textBox.BackColor = System.Drawing.Color.MistyRose;
                    }
                    else
                    {
                        textBox.BackColor = System.Drawing.Color.White;
                        switch (textBox.Tag)
                        {
                            case "X":
                                _scale = new Vector3(textValue, _scale.Y, _scale.Z);
                                break;
                            case "Y":
                                _scale = new Vector3(_scale.X, textValue, _scale.Z);
                                break;
                            case "Z":
                                _scale = new Vector3(_scale.X, _scale.Y, textValue);
                                break;
                        }
                        ValueChangedEventArgs args = new ValueChangedEventArgs()
                        {
                            ValueChanged = _scale
                        };
                        OnScaleChanged(args);
                    }
                }
            }

        }

        private void Position_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = new Cursor(Properties.Resources.ValueDragHorizS.GetHicon());

            _lastMouseXPosition = MousePosition.X;

            TextBox textBox = (TextBox)sender;
            if(textBox != null)
            {
                HideTextBoxCaret(textBox);
            }
        }

        private void Position_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                ShowTextBoxCaret(textBox);
            }
        }

        private void Position_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(sender != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (float.TryParse(textBox.Text, out float textValue))
                    {
                        if (textValue > MIN_POS && textValue < MAX_POS)
                        {
                            if (e.X < _lastMouseXPosition)
                            {
                                textValue -= POS_DELTA;
                            }
                            else if (e.X > _lastMouseXPosition)
                            {
                                textValue += POS_DELTA;
                            }
                            textBox.Text = textValue.ToString();
                        }
                        else
                        {
                            if(textValue == MIN_POS)
                            {
                                if (e.X > _lastMouseXPosition)
                                    textValue += POS_DELTA;
                            }
                            if(textValue == MAX_POS)
                            {
                                if (e.X < _lastMouseXPosition)
                                    textValue -= POS_DELTA;
                            }
                            textBox.Text = textValue.ToString();
                        }
                    }
                    textBox.SelectionLength = 0;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
            _lastMouseXPosition = e.X;
        }

        private void Rotation_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = new Cursor(Properties.Resources.ValueDragHorizS.GetHicon());

            _lastMouseXPosition = MousePosition.X;

            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                HideTextBoxCaret(textBox);
            }
        }

        private void Rotation_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (sender != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (float.TryParse(textBox.Text, out float textValue))
                    {
                        if (textValue > MIN_ROT && textValue < MAX_ROT)
                        {
                            if (e.X < _lastMouseXPosition)
                            {
                                textValue -= ROT_DELTA;
                            }
                            else if (e.X > _lastMouseXPosition)
                            {
                                textValue += ROT_DELTA;
                            }
                            textBox.Text = string.Format("{0:##0.##}", textValue);
                        }
                        else
                        {
                            if (textValue == MIN_ROT)
                            {
                                if (e.X > _lastMouseXPosition)
                                    textValue += ROT_DELTA;
                            }
                            if (textValue == MAX_ROT)
                            {
                                if (e.X < _lastMouseXPosition)
                                    textValue -= ROT_DELTA;
                            }
                            textBox.Text = string.Format("{0:##0.##}", textValue);
                        }
                    }
                    textBox.SelectionLength = 0;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
            _lastMouseXPosition = e.X;
        }

        private void Rotation_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                ShowTextBoxCaret(textBox);
            }
        }

        private void Scale_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = new Cursor(Properties.Resources.ValueDragHorizS.GetHicon());

            _lastMouseXPosition = MousePosition.X;

            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                HideTextBoxCaret(textBox);
            }
        }

        private void Scale_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (sender != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (float.TryParse(textBox.Text, out float textValue))
                    {
                        if (textValue > MIN_SCALE && textValue < MAX_SCALE)
                        {
                            if (e.X < _lastMouseXPosition)
                            {
                                textValue -= SCALE_DELTA;
                            }
                            else if (e.X > _lastMouseXPosition)
                            {
                                textValue += SCALE_DELTA;
                            }
                            textBox.Text = textValue.ToString();
                        }
                        else
                        {
                            if (textValue == MIN_SCALE)
                            {
                                if (e.X > _lastMouseXPosition)
                                    textValue += SCALE_DELTA;
                            }
                            if (textValue == MAX_SCALE)
                            {
                                if (e.X < _lastMouseXPosition)
                                    textValue -= SCALE_DELTA;
                            }
                            textBox.Text = textValue.ToString();
                        }
                    }
                    textBox.SelectionLength = 0;
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
            _lastMouseXPosition = e.X;
        }

        private void Scale_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                ShowTextBoxCaret(textBox);
            }
        }

        private void NodeNameKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                if(ValidateNodeName())
                {
                    txtName.BackColor = System.Drawing.Color.White;
                    _name = txtName.Text.Trim();
                    NodeNameChangedEventArgs nodeNameChangedEventArgs = new NodeNameChangedEventArgs
                    {
                        NodeName = txtName.Text.Trim(),
                        OldNodeName = _originalName
                    };
                    OnNodeNameChanged(nodeNameChangedEventArgs);
                    _originalName = _name;
                    e.Handled = true;
                }
                else
                {
                    txtName.BackColor = System.Drawing.Color.MistyRose;
                    txtName.Text = _originalName;
                    txtName.SelectAll();
                }
            }
        }

        private bool ValidateNodeName()
        {
            if(txtName.Text.Length > 0)
            {
                //mustn't begin with a number
                string firstChar = txtName.Text.Substring(0, 1);
                if(int.TryParse(firstChar, out _)) { return false; }
                //mustn't be empty
                if (txtName.Text.Trim().Length == 0) return false;
                //no spaces allowed
                if (txtName.Text.Contains(' ')) return false;
            }
            return true;
        }

        private void NodeNameLeave(object sender, EventArgs e)
        {
            KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs((char)Keys.Return);
            NodeNameKeyPress(sender, keyPressEventArgs);
        }

        private void NodeEnabledChanged(object sender, EventArgs e)
        {
            NodeEnabledChangedEventArgs nodeEnabledChangedEventArgs = new NodeEnabledChangedEventArgs
            {
                NodeEnabled = chkEnabled.Checked
            };
            OnNodeEnabledChanged(nodeEnabledChangedEventArgs);
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            //this bit being set as Handled suppresses the Enter keypress from propagating (i.e no beep unless
            //there is an invalid entry
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (_windowRoll == WindowRoll.RollUp)
            {
                Height = 33;
                _windowRoll = WindowRoll.RollDown;
                btnView.Text = "v";
                WindowRollEventArgs eventArgs = new WindowRollEventArgs
                {
                    WindowRoll = WindowRoll.RollUp
                };
                OnWindowRolled(eventArgs);
            }
            else
            {
                Height = 269;
                _windowRoll = WindowRoll.RollUp;
                btnView.Text = "^";
                WindowRollEventArgs eventArgs = new WindowRollEventArgs
                {
                    WindowRoll = WindowRoll.RollDown
                };
                OnWindowRolled(eventArgs);
            }
        }
    }
}
