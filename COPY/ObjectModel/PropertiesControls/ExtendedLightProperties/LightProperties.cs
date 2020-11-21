using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Urho;
using System.Runtime.InteropServices;

namespace ExtendedLightProperties
{
    public partial class LightProperties: UserControl
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

        public event EventHandler ColourChanged;
        public event EventHandler AspectRatioChanged;
        public event EventHandler BrightnessChanged;
        public event EventHandler TemperatureChanged;
        public event EventHandler UsePhysicalValuesChanged;
        public event EventHandler EffectiveSpecularIntensityChanged;
        public event EventHandler SpecularIntensityChanged;
        public event EventHandler FadeDistanceChanged;
        public event EventHandler FieldOfViewChanged;
        public event EventHandler LengthChanged;
        public event EventHandler LightTypeChanged;
        public event EventHandler PerVertexChanged;
        public event EventHandler RadiusChanged;
        public event EventHandler RangeChanged;
        public event EventHandler ShadowFadeDistanceChanged;
        public event EventHandler ShadowIntensityChanged;
        public event EventHandler ShadowMaximumExtrusionChanged;
        public event EventHandler ShadowNearFarRatioChanged;
        public event EventHandler ShadowResolutionChanged;
        public event EventHandler WindowRolled;

        private bool _externallySet;

        private LightPropertiesChangedEventArgs _eventArgs;

        private const int MINIMUM_HEIGHT = 33;
        private const int MAXIMUM_HEIGHT = 604;
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

        public string NodeName { get; set; }

        private LightType _lightType;
        public LightType Type 
        { 
            get
            {
                return _lightType;
            }
            set
            {
                _lightType = value;
                _eventArgs.LightType = value;
                lstLightType.SelectedIndex = (int)value;
            }
        }

        private Urho.Color _colour;
        public Urho.Color Colour 
        { 
            get
            {
                return _colour;
            }
            set
            {
                _colour = value;
                _eventArgs.Colour = value;
                lblColorView.BackColor = System.Drawing.Color.FromArgb((int)value.A, (int)value.R, (int)value.G, (int)value.B);
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

        private float _brightness;
        public float Brightness 
        { 
            get
            {
                return _brightness;
            }
            set
            {
                _brightness = value;
                _eventArgs.Brightness = value;
                txtBrightness.Text = value.ToString();
            }
        }

        private float _temperature;
        public float Temperature 
        { 
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                _eventArgs.Temperature = value;
                txtTemperature.Text = value.ToString();
            }
        }

        private bool _usePhysicalValues;
        public bool UsePhysicalValues 
        { 
            get
            {
                return _usePhysicalValues;
            }
            set
            {
                _usePhysicalValues = value;
                _eventArgs.UsePhysicalValues = value;
                chkUsePhysicalValues.Checked = value;
            }
        }

        private float _effectiveSpecularIntensity;
        public float EffectiveSpecularIntensity 
        { 
            get
            {
                return _effectiveSpecularIntensity;
            }
            set
            {
                _effectiveSpecularIntensity = value;
                _eventArgs.EffectiveSpecularIntensity = value;
                txtEffectiveSpecularIntensity.Text = value.ToString();
            }
        }

        private float _specularIntensity;
        public float SpecularIntensity 
        { 
            get
            {
                return _specularIntensity;
            }
            set
            {
                _specularIntensity = value;
                _eventArgs.SpecularIntensity = value;
                txtSpecularIntensity.Text = value.ToString();
            }
        }

        private float _fadeDistance;
        public float FadeDistance 
        { 
            get
            {
                return _fadeDistance;
            }
            set
            {
                _fadeDistance = value;
                _eventArgs.FadeDistance = value;
                txtFadeDistance.Text = value.ToString();
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

        private float _length;
        public float Length 
        { 
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                _eventArgs.Length = value;
                txtLength.Text = value.ToString();
            }
        }

        private bool _perVertex;
        public bool PerVertex 
        { 
            get
            {
                return _perVertex;
            }
            set
            {
                _perVertex = value;
                _eventArgs.PerVertex = value;
                chkPerVertex.Checked = value;
            }
        }

        private float _radius;
        public float Radius 
        { 
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                _eventArgs.Radius = value;
                txtRadius.Text = value.ToString();
            }
        }

        private float _range;
        public float Range 
        { 
            get
            {
                return _range;
            }
            set
            {
                _range = value;
                _eventArgs.Range = value;
                txtRange.Text = value.ToString();
            }
        }

        private float _shadowFadeDistance;
        public float ShadowFadeDistance 
        { 
            get
            {
                return _shadowFadeDistance;
            }
            set
            {
                _shadowFadeDistance = value;
                _eventArgs.ShadowFadeDistance = value;
                txtShadowFadeDistance.Text = value.ToString();
            }
        }

        private float _shadowIntensity;
        public float ShadowIntensity 
        { 
            get
            {
                return _shadowIntensity;
            }
            set
            {
                _shadowIntensity = value;
                _eventArgs.ShadowIntensity = value;
                txtShadowIntensity.Text = value.ToString();
            }
        }

        private float _shadowMaximumExtrusion;
        public float ShadowMaximumExtrusion 
        { 
            get
            {
                return _shadowMaximumExtrusion;
            }
            set
            {
                _shadowMaximumExtrusion = value;
                _eventArgs.ShadowMaximumExtrusion = value;
                txtShadowMaximumExtrusion.Text = value.ToString();
            }
        }

        private float _shadowNearFarRatio;
        public float ShadowNearFarRatio 
        { 
            get
            {
                return _shadowNearFarRatio;
            }
            set
            {
                _shadowNearFarRatio = value;
                _eventArgs.ShadowNearFarRatio = value;
                txtShadowNearFarRatio.Text = value.ToString();
            }
        }

        private float _shadowResolution;
        public float ShadowResolution 
        { 
            get
            {
                return _shadowResolution;
            }
            set
            {
                _shadowResolution = value;
                _eventArgs.ShadowResolution = value;
                txtShadowResolution.Text = value.ToString();
            }
        }

        public LightProperties()
        {
            InitializeComponent();

            _eventArgs = new LightPropertiesChangedEventArgs();
            InitialiseEventArgs();
        }

        private void InitialiseEventArgs()
        {
            _eventArgs.AspectRatio = 0;
            txtAspectRatio.Text = "0.0";
            _eventArgs.Brightness = 0;
            txtBrightness.Text = "0.0";
            _eventArgs.ColourFromTemperature = Urho.Color.White;
            _eventArgs.Colour = Urho.Color.White;
            lblColorView.BackColor = System.Drawing.Color.White;
            _eventArgs.EffectiveColour = Urho.Color.White;
            _eventArgs.EffectiveSpecularIntensity = 0;
            txtEffectiveSpecularIntensity.Text = "0.0";
            _eventArgs.FadeDistance = 0;
            txtFadeDistance.Text = "0.0";
            _eventArgs.FieldOfView = 0;
            txtFieldOfView.Text = "0.0";
            _eventArgs.Length = 0;
            txtLength.Text = "0.0";
            _eventArgs.LightType = LightType.Directional;
            lstLightType.SelectedIndex = (int)LightType.Directional;
            _eventArgs.PerVertex = false;
            chkPerVertex.Checked = false;
            _eventArgs.Radius = 0;
            txtRadius.Text = "0.0";
            _eventArgs.Range = 0;
            txtRange.Text = "0.0";
            _eventArgs.ShadowFadeDistance = 0;
            txtShadowFadeDistance.Text = "0.0";
            _eventArgs.ShadowIntensity = 0;
            txtShadowIntensity.Text = "0.0";
            _eventArgs.ShadowMaximumExtrusion = 0;
            txtShadowMaximumExtrusion.Text = "0.0";
            _eventArgs.ShadowNearFarRatio = 0;
            txtShadowNearFarRatio.Text = "0.0";
            _eventArgs.ShadowResolution = 0;
            txtShadowResolution.Text = "0.0";
            _eventArgs.SpecularIntensity = 0;
            txtSpecularIntensity.Text = "0.0";
            _eventArgs.Temperature = 0;
            txtTemperature.Text = "0.0";
            _eventArgs.UsePhysicalValues = false;
        }

        private void btnColorPick_Click(object sender, EventArgs e)
        {
            var dialogResult = colorPicker.ShowDialog(btnColorPick);

            if (dialogResult == DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                _colour = new Urho.Color(color.R, color.G, color.B, color.A);
                _eventArgs.Colour = _colour;
                lblColorView.BackColor = color;
                OnColorChanged();
            }
        }

        protected void OnColorChanged()
        {
            ColourChanged?.Invoke(this, _eventArgs);
        }

        private void LightProperties_Resize(object sender, EventArgs e)
        {
            if (Width > 312)
            {
                Size size = lblHeader.Size;
                size.Width = Width;
                lblHeader.Size = size;
            }
        }

        protected virtual void OnWindowRolled(WindowRollEventArgs e)
        {
            WindowRolled?.Invoke(this, e);
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
                Height = 652;
                _windowRoll = WindowRoll.RollUp;
                btnView.Text = "^";
                WindowRollEventArgs eventArgs = new WindowRollEventArgs
                {
                    WindowRoll = WindowRoll.RollDown
                };
                OnWindowRolled(eventArgs);
            }
        }

        private void LightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lightType = (LightType)lstLightType.SelectedIndex;
            _eventArgs.LightType = _lightType;
            OnLightTypeChanged();
        }

        protected void OnLightTypeChanged()
        {
            LightTypeChanged?.Invoke(this, _eventArgs);
        }

        private void ValidateBrightness(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtBrightness.Text, out _))
            {
                e.Cancel = true;
                txtBrightness.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtBrightness.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateTemperature(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtTemperature.Text, out _))
                e.Cancel = true;
        }

        private void ValidateEffectiveSpecularIntensity(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtEffectiveSpecularIntensity.Text, out _))
            {
                e.Cancel = true;
                txtEffectiveSpecularIntensity.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtEffectiveSpecularIntensity.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateSpecularIntensity(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtSpecularIntensity.Text, out _))
                e.Cancel = true;
        }

        private void ValidateFadeDistance(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtFadeDistance.Text, out _))
            {
                e.Cancel = true;
                txtFadeDistance.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtFadeDistance.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateFieldOfView(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtFieldOfView.Text, out _))
            {
                e.Cancel = true;
                txtFieldOfView.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtFieldOfView.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateLength(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtLength.Text, out _))
            {
                e.Cancel = true;
                txtLength.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtLength.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateRadius(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtRadius.Text, out _))
            {
                e.Cancel = true;
                txtRadius.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtRadius.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateRange(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtRange.Text, out _))
            {
                e.Cancel = true;
                txtRange.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtRange.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateShadowFadeDistance(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtShadowFadeDistance.Text, out _))
            {
                e.Cancel = true;
                txtShadowFadeDistance.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtShadowFadeDistance.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateShadowIntensity(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtShadowIntensity.Text, out _))
            {
                e.Cancel = true;
                txtShadowIntensity.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtShadowIntensity.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateShadowMaximumExtrusion(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtShadowMaximumExtrusion.Text, out _))
            {
                e.Cancel = true;
                txtShadowMaximumExtrusion.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtShadowMaximumExtrusion.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateShadowNearFarRatio(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtShadowNearFarRatio.Text, out _))
            {
                e.Cancel = true;
                txtShadowNearFarRatio.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtShadowNearFarRatio.BackColor = System.Drawing.Color.White;
            }
        }

        private void ValidateShadowResolution(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtShadowResolution.Text, out _))
            {
                e.Cancel = true;
                txtShadowResolution.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtShadowResolution.BackColor = System.Drawing.Color.White;
            }
        }

        private void OnUsePhysicalValuesChanged(object sender, EventArgs e)
        {
            _eventArgs.UsePhysicalValues = chkUsePhysicalValues.Checked;
            UsePhysicalValuesChanged?.Invoke(this, _eventArgs);
        }

        private void OnPerVertexChanged(object sender, EventArgs e)
        {
            _eventArgs.PerVertex = chkPerVertex.Checked;
            PerVertexChanged?.Invoke(this, _eventArgs);
        }

        private void OnAspectRatioChanged(object sender, EventArgs e)
        {
            //validation has already occurred by the time we get here, so it should be safe to parse a float
            float.TryParse(txtAspectRatio.Text, out _aspectRatio);
            _eventArgs.AspectRatio = _aspectRatio;
            AspectRatioChanged?.Invoke(this, _eventArgs);
        }

        private void OnBrightnessChanged(object sender, EventArgs e)
        {
            float.TryParse(txtBrightness.Text, out _brightness);
            _eventArgs.Brightness = _brightness;
            BrightnessChanged?.Invoke(this, _eventArgs);
        }

        private void OnTemperatureChanged(object sender, EventArgs e)
        {
            float.TryParse(txtTemperature.Text, out _temperature);
            _eventArgs.Temperature = _temperature;
            TemperatureChanged?.Invoke(this, _eventArgs);
        }

        private void OnEffectiveSpecularIntensityChanged(object sender, EventArgs e)
        {
            float.TryParse(txtEffectiveSpecularIntensity.Text, out _effectiveSpecularIntensity);
            _eventArgs.EffectiveSpecularIntensity = _effectiveSpecularIntensity;
            EffectiveSpecularIntensityChanged?.Invoke(this, _eventArgs);
        }

        private void OnSpecularIntensityChanged(object sender, EventArgs e)
        {
            float.TryParse(txtSpecularIntensity.Text, out _specularIntensity);
            _eventArgs.SpecularIntensity = _specularIntensity;
            SpecularIntensityChanged?.Invoke(this, _eventArgs);
        }

        private void OnFadeDistanceChanged(object sender, EventArgs e)
        {
            float.TryParse(txtFadeDistance.Text, out _fadeDistance);
            _eventArgs.FadeDistance = _fadeDistance;
            FadeDistanceChanged?.Invoke(this, _eventArgs);
        }

        private void OnFieldOfViewChanged(object sender, EventArgs e)
        {
            float.TryParse(txtFieldOfView.Text, out _fieldOfView);
            _eventArgs.FieldOfView = _fieldOfView;
            FieldOfViewChanged?.Invoke(this, _eventArgs);
        }

        private void OnLengthChanged(object sender, EventArgs e)
        {
            float.TryParse(txtLength.Text, out _length);
            _eventArgs.Length = _length;
            LengthChanged?.Invoke(this, _eventArgs);
        }

        private void OnRadiusChanged(object sender, EventArgs e)
        {
            float.TryParse(txtRadius.Text, out _radius);
            _eventArgs.Radius = _radius;
            RadiusChanged?.Invoke(this, _eventArgs);
        }

        private void OnRangeChanged(object sender, EventArgs e)
        {
            float.TryParse(txtRange.Text, out _range);
            _eventArgs.Range = _range;
            RangeChanged?.Invoke(this, _eventArgs);
        }

        private void OnShadowFadeDistanceChanged(object sender, EventArgs e)
        {
            float.TryParse(txtShadowFadeDistance.Text, out _shadowFadeDistance);
            _eventArgs.ShadowFadeDistance = _shadowFadeDistance;
            ShadowFadeDistanceChanged?.Invoke(this, _eventArgs);
        }

        private void OnShadowIntensityChanged(object sender, EventArgs e)
        {
            float.TryParse(txtShadowIntensity.Text, out _shadowIntensity);
            _eventArgs.ShadowIntensity = _shadowIntensity;
            ShadowIntensityChanged?.Invoke(this, _eventArgs);
        }

        private void OnShadowMaximumExtrusionChanged(object sender, EventArgs e)
        {
            float.TryParse(txtShadowMaximumExtrusion.Text, out _shadowMaximumExtrusion);
            _eventArgs.ShadowMaximumExtrusion = _shadowMaximumExtrusion;
            ShadowMaximumExtrusionChanged?.Invoke(this, _eventArgs);
        }

        private void OnShadowNearFarRatioChanged(object sender, EventArgs e)
        {
            float.TryParse(txtShadowNearFarRatio.Text, out _shadowNearFarRatio);
            _eventArgs.ShadowNearFarRatio = _shadowNearFarRatio;
            ShadowNearFarRatioChanged?.Invoke(this, _eventArgs);
        }

        private void OnShadowResolutionChanged(object sender, EventArgs e)
        {
            float.TryParse(txtShadowResolution.Text, out _shadowResolution);
            _eventArgs.ShadowResolution = _shadowResolution;
            ShadowResolutionChanged?.Invoke(this, _eventArgs);
        }

        private void ValidateAspectRatio(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(txtAspectRatio.Text, out _))
            {
                e.Cancel = true;
                txtAspectRatio.BackColor = System.Drawing.Color.MistyRose;
            }
            else
            {
                txtAspectRatio.BackColor = System.Drawing.Color.White;
            }
        }
    }
}
