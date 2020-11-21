using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ExtendedCameraProperties
{
    public partial class CameraProperties: UserControl
    {
        public enum WindowRoll
        {
            RollDown = 0,
            RollUp
        }
        private WindowRoll _windowRoll = WindowRoll.RollUp;

        #region API Imports
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);
        #endregion

        #region Event handlers
        public event EventHandler AspectRatioChanged;
        public event EventHandler AutoAspectRatioChanged;
        public event EventHandler FarClipChanged;
        public event EventHandler NearClipChanged;
        public event EventHandler UseClippingChanged;
        public event EventHandler FlipVerticalChanged;
        public event EventHandler SkewChanged;
        public event EventHandler OrthographicChanged;
        public event EventHandler OrthographicSizeChanged;
        public event EventHandler FieldOfViewChanged;
        public event EventHandler LODBiasChanged;
        public event EventHandler ZoomChanged;
        public event EventHandler WindowRolled;
        #endregion

        private CameraPropertiesChangedEventArgs _eventArgs;

        private const int MINIMUM_HEIGHT = 33;
        private const int MAXIMUM_HEIGHT = 385;

        #region Properties
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

        private float _aspectRatio;
        public float AspectRatio 
        { 
            get
            {
                return _aspectRatio;
            }
            set
            {
                _aspectRatio = value;
                _eventArgs.AspectRatio = value;
                txtAspectRatio.Text = value.ToString();
            }
        }
        private bool _autoAspectRatio;
        public bool AutoAspectRatio 
        { 
            get
            {
                return _autoAspectRatio;
            }
            set
            {
                _autoAspectRatio = value;
                _eventArgs.AutoAspectRatio = value;
                chkAutoAspectRatio.Checked = value;
            }
        }
        private float _farClip;
        public float FarClip 
        { 
            get
            {
                return _farClip;
            }
            set
            {
                _farClip = value;
                _eventArgs.FarClip = value;
                txtFarClip.Text = value.ToString();
            }
        }
        private float _nearClip;
        public float NearClip 
        { 
            get
            {
                return _nearClip;
            }
            set
            {
                _nearClip = value;
                _eventArgs.NearClip = value;
                txtNearClip.Text = value.ToString();
            }
        }
        private bool _useClipping;
        public bool UseClipping 
        { 
            get
            {
                return _useClipping;
            }
            set
            {
                _useClipping = value;
                _eventArgs.UseClipping = value;
                chkUseClipping.Checked = value;
            }
        }
        private bool _flipVertical;
        public bool FlipVerical 
        { 
            get
            {
                return _flipVertical;
            }
            set
            {
                _flipVertical = value;
                _eventArgs.FlipVertical = value;
                chkFlipVertical.Checked = value;
            }
        }
        private float _skew;
        public float Skew 
        { 
            get
            {
                return _skew;
            }
            set
            {
                _skew = value;
                _eventArgs.Skew = value;
                txtSkew.Text = value.ToString();
            }
        }
        private bool _orthographic;
        public bool Orthographic 
        { 
            get
            {
                return _orthographic;
            }
            set
            {
                _orthographic = value;
                _eventArgs.Orthographic = value;
                chkOrthographic.Checked = value;
            }
        }
        private float _orthographicSize;
        public float OrthographicSize 
        { 
            get
            {
                return _orthographicSize;
            }
            set
            {
                _orthographicSize = value;
                _eventArgs.OrthographicSize = value;
                txtOrthographicSize.Text = value.ToString();
            }
        }
        private float _fieldOfView;
        public float FieldOfView 
        { 
            get
            {
                return _fieldOfView;
            }
            set
            {
                _fieldOfView = value;
                _eventArgs.FieldOfView = value;
                txtFieldOfView.Text = value.ToString();
            }
        }
        private float _lodBias;
        public float LODBias 
        { 
            get
            {
                return _lodBias;
            }
            set
            {
                _lodBias = value;
                _eventArgs.LODBias = value;
                txtLODBias.Text = value.ToString();
            }
        }
        private float _zoom;
        public float Zoom 
        { 
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                _eventArgs.Zoom = value;
                txtZoom.Text = value.ToString();
            }
        }
        private bool _externallySet;
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
        #endregion

        private void InitialiseEventArgs()
        {
            _aspectRatio = 0;
            _autoAspectRatio = true;

            _farClip = 1000;
            _nearClip = 0.1f;
            _useClipping = false;

            _flipVertical = false;
            _skew = 0;

            _orthographic = false;
            _orthographicSize = 0;

            _fieldOfView = 45;
            _lodBias = 1;
            _zoom = 1;
        }

        private void SetDefaultControlValues()
        {
            txtAspectRatio.Text = _aspectRatio.ToString();
            chkAutoAspectRatio.Checked = _autoAspectRatio;
            txtFarClip.Text = _farClip.ToString();
            txtNearClip.Text = _nearClip.ToString();
            chkUseClipping.Checked = _useClipping;
            chkFlipVertical.Checked = _flipVertical;
            txtSkew.Text = _skew.ToString();
            chkOrthographic.Checked = _orthographic;
            txtOrthographicSize.Text = _orthographicSize.ToString();
            txtFieldOfView.Text = _fieldOfView.ToString();
            txtLODBias.Text = _lodBias.ToString();
            txtZoom.Text = _zoom.ToString();
        }

        #region Event invocation
        protected virtual void OnAspectRatioChanged(object sender, EventArgs e)
        {
            float.TryParse(txtAspectRatio.Text, out _aspectRatio);
            _eventArgs.AspectRatio = _aspectRatio;
            AspectRatioChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnAutoAspectRatioChanged(object sender, EventArgs e)
        {
            _eventArgs.AutoAspectRatio = chkAutoAspectRatio.Checked;
            AutoAspectRatioChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnFarClipChanged(object sender, EventArgs e)
        {
            float.TryParse(txtFarClip.Text, out _farClip);
            _eventArgs.FarClip = _farClip;
            FarClipChanged?.Invoke(this, _eventArgs);
        }
        protected virtual void OnNearClipChanged(object sender, EventArgs e)
        {
            float.TryParse(txtNearClip.Text, out _nearClip);
            _eventArgs.NearClip = _nearClip;
            NearClipChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnUseClippingChanged(object sender, EventArgs e)
        {
            _eventArgs.UseClipping = chkUseClipping.Checked;
            UseClippingChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnFlipVerticalChanged(object sender, EventArgs e)
        {
            _eventArgs.FlipVertical = _flipVertical;
            if (!_externallySet)
                FlipVerticalChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnSkewChanged(object sender, EventArgs e)
        {
            float.TryParse(txtSkew.Text, out _skew);
            _eventArgs.Skew = _skew;
            SkewChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnOrthographicChanged(object sender, EventArgs e)
        {
            _eventArgs.Orthographic = chkOrthographic.Checked;
            OrthographicChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnOrthographicSizeChanged(object sender, EventArgs e)
        {
            float.TryParse(txtOrthographicSize.Text, out _orthographicSize);
            _eventArgs.OrthographicSize = _orthographicSize;
            OrthographicSizeChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnFieldOfViewChanged(object sender, EventArgs e)
        {
            float.TryParse(txtFieldOfView.Text, out _fieldOfView);
            _eventArgs.FieldOfView = _fieldOfView;
            FieldOfViewChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnLODBiasChanged(object sender, EventArgs e)
        {
            float.TryParse(txtLODBias.Text, out _lodBias);
            _eventArgs.LODBias = _lodBias;
            LODBiasChanged?.Invoke(this, _eventArgs);
        }

        protected virtual void OnZoomChanged(object sender, EventArgs e)
        {
            float.TryParse(txtZoom.Text, out _zoom);
            _eventArgs.Zoom = _zoom;
            ZoomChanged?.Invoke(this, _eventArgs);
        }
        #endregion

        public string NodeName { get; set; }

        public CameraProperties()
        {
            _eventArgs = new CameraPropertiesChangedEventArgs();
            InitialiseEventArgs();
            InitializeComponent();
            SetDefaultControlValues();
        }

        #region Window Roll
        private void btnView_Click(object sender, EventArgs e)
        {
            if (_windowRoll == WindowRoll.RollUp)
            {
                Height = MINIMUM_HEIGHT;
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
                Height = MAXIMUM_HEIGHT;
                _windowRoll = WindowRoll.RollUp;
                btnView.Text = "^";
                WindowRollEventArgs eventArgs = new WindowRollEventArgs
                {
                    WindowRoll = WindowRoll.RollDown
                };
                OnWindowRolled(eventArgs);
            }
        }

        protected virtual void OnWindowRolled(WindowRollEventArgs e)
        {
            WindowRolled?.Invoke(this, e);
        }
        #endregion

        #region Validation
        private void ValidateAspectRatio(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtAspectRatio.Text, out _))
                e.Cancel = true;
        }

        private void ValidateFarClip(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtFarClip.Text, out _))
                e.Cancel = true;
        }

        private void ValidateNearClip(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtNearClip.Text, out _))
                e.Cancel = true;
        }

        private void ValidateSkew(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtSkew.Text, out _))
                e.Cancel = true;
        }

        private void ValidateOrthographicSize(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtOrthographicSize.Text, out _))
                e.Cancel = true;
        }

        private void ValidateFieldOfView(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtFieldOfView.Text, out _))
                e.Cancel = true;
        }

        private void ValidateLODBias(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtLODBias.Text, out _))
                e.Cancel = true;
        }

        private void ValidateZoom(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!float.TryParse(txtZoom.Text, out _))
                e.Cancel = true;
        }
        #endregion

        private void CameraProperties_Resize(object sender, EventArgs e)
        {
            if (Width > 312)
            {
                Size size = lblHeader.Size;
                size.Width = Width;
                lblHeader.Size = size;
            }
        }
    }
}
