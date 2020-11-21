using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace ExtendedLightProperties
{
    public class LightPropertiesChangedEventArgs : EventArgs
    {
        public Urho.Color Colour { get; set; }
        public Urho.Color ColourFromTemperature { get; set; }
        public Urho.Color EffectiveColour { get; set; }
        public float AspectRatio { get; set; }
        public float Brightness { get; set; }
        public float Temperature { get; set; }
        public bool UsePhysicalValues { get; set; }
        public float EffectiveSpecularIntensity { get; set; }
        public float SpecularIntensity { get; set; }
        public float FadeDistance { get; set; }
        public float FieldOfView { get; set; }
        public float Length { get; set; }
        public LightType LightType { get; set; }
        public bool PerVertex { get; set; }
        public float Radius { get; set; }
        public float Range { get; set; }
        public float ShadowFadeDistance { get; set; }
        public float ShadowIntensity { get; set; }
        public float ShadowMaximumExtrusion { get; set; }
        public float ShadowNearFarRatio { get; set; }
        public float ShadowResolution { get; set; }
    }
}
