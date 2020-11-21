using System.Linq;
using Urho;

namespace Pulsar.ObjectModel.PropertiesModel
{
    public class LightProperties : PulsarComponent
    {
        private Color _color;

        private float _aspectRatio;
        private float _brightness;
        private float _temperature;
        private bool _usePhysicalValues;
        private float _effectiveSpecularIntensity;
        private float _specularIntensity;
        private float _fadeDistance;
        private float _fieldOfView;
        private float _length;
        private LightType _lightType;
        private bool _perVertex;
        private float _radius;
        private float _range;
        private float _shadowFadeDistance;
        private float _shadowIntensity;
        private float _shadowMaximumExtrusion;
        private float _shadowNearFarRatio;
        private float _shadowResolution;
        private Light _node;
        private bool _externallySet;

        private ExtendedLightProperties.LightProperties _container;
        public ExtendedLightProperties.LightProperties Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
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

        public new Light Node
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;
            }
        }

        public float AspectRatio
        {
            get
            {
                return _aspectRatio;
            }
            set
            {
                _aspectRatio = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float Brightness
        {
            get
            {
                return _brightness;
            }
            set
            {
                _brightness = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public Urho.Color Colour
        {
            get
            {
                Urho.Color color = Urho.Color.Transparent;

                if (_color != null)
                {
                    color = _color;
                }
                return color;
            }

            set
            {
                _color = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public bool UsePhysicalValues
        {
            get
            {
                return _usePhysicalValues;
            }
            set
            {
                _usePhysicalValues = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float EffectiveSpecularIntensity
        {
            get
            {
                return _effectiveSpecularIntensity;
            }
            set
            {
                _effectiveSpecularIntensity = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float SpecularIntensity
        {
            get
            {
                return _specularIntensity;
            }
            set
            {
                _specularIntensity = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float FadeDistance
        {
            get
            {
                return _fadeDistance;
            }
            set
            {
                _fadeDistance = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float FieldOfView
        {
            get
            {
                return _fieldOfView;
            }
            set
            {
                _fieldOfView = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public LightType LightType
        {
            get
            {
                return _lightType;
            }
            set
            {
                _lightType = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public bool PerVertex
        {
            get
            {
                return _perVertex;
            }
            set
            {
                _perVertex = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float Range
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float ShadowFadeDistance
        {
            get
            {
                return _shadowFadeDistance;
            }
            set
            {
                _shadowFadeDistance = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float ShadowIntensity
        {
            get
            {
                return _shadowIntensity;
            }
            set
            {
                _shadowIntensity = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float ShadowMaximumExtrusion
        {
            get
            {
                return _shadowMaximumExtrusion;
            }
            set
            {
                _shadowMaximumExtrusion = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float ShadowNearFarRatio
        {
            get
            {
                return _shadowNearFarRatio;
            }
            set
            {
                _shadowNearFarRatio = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float ShadowResolution
        {
            get
            {
                return _shadowResolution;
            }
            set
            {
                _shadowResolution = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public void Update()
        {
            if (Node != null)
            {
                var lightNode = Node.GetComponent<Light>();
                lightNode.AspectRatio = AspectRatio;
                lightNode.Brightness = Brightness;
                lightNode.Color = Colour;
                lightNode.Temperature = Temperature;
                lightNode.UsePhysicalValues = UsePhysicalValues;
                lightNode.SpecularIntensity = SpecularIntensity;
                lightNode.FadeDistance = FadeDistance;
                lightNode.Fov = FieldOfView;
                lightNode.Length = Length;
                lightNode.LightType = LightType;
                lightNode.PerVertex = PerVertex;
                lightNode.Range = Range;
                lightNode.Radius = Radius;
                lightNode.ShadowFadeDistance = ShadowFadeDistance;
                lightNode.ShadowIntensity = ShadowIntensity;
                lightNode.ShadowMaxExtrusion = ShadowMaximumExtrusion;
                lightNode.ShadowNearFarRatio = ShadowNearFarRatio;
                lightNode.ShadowResolution = ShadowResolution;

                //if (Scene != null)
                //{
                //    var sceneNode = Scene.Children.ToList().Find(node => node.Name == lightNode.Node.Name);
                //    if (sceneNode != null)
                //    {
                //        Light sceneLight = sceneNode.GetComponent<Light>();
                //        if (sceneLight != null)
                //        {
                //            sceneLight.AspectRatio = AspectRatio;
                //            sceneLight.Brightness = Brightness;
                //            sceneLight.Color = new Color(Colour.R, Colour.G, Colour.B, Colour.A);
                //            sceneLight.Temperature = Temperature;
                //            sceneLight.UsePhysicalValues = UsePhysicalValues;
                //            sceneLight.SpecularIntensity = SpecularIntensity;
                //            sceneLight.FadeDistance = FadeDistance;
                //            sceneLight.Fov = FieldOfView;
                //            sceneLight.Length = Length;
                //            sceneLight.LightType = LightType;
                //            sceneLight.PerVertex = PerVertex;
                //            sceneLight.Range = Range;
                //            sceneLight.Radius = Radius;
                //            sceneLight.ShadowFadeDistance = ShadowFadeDistance;
                //            sceneLight.ShadowIntensity = ShadowIntensity;
                //            sceneLight.ShadowMaxExtrusion = ShadowMaximumExtrusion;
                //            sceneLight.ShadowNearFarRatio = ShadowNearFarRatio;
                //            sceneLight.ShadowResolution = ShadowResolution;
                //        }
                //    }
                //}
            }
        }
    }
}
