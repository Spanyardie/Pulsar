using System.ComponentModel;
using System.Linq;
using Urho;

namespace Pulsar.ObjectModel.PropertiesModel
{
    public class PlaneProperties : PulsarComponent
    {
        //private readonly Node _node;
        //private readonly PulsarApplication _application;
        //private readonly PulsarScene _scene;
        private bool _castShadows;
        private float _shadowDistance;
        private uint _shadowMask;
        private float _drawDistance;
        private uint _maxLights;
        private bool _occludee;
        private bool _occluder;
        private uint _lightMask;
        private uint _viewMask;
        private uint _zoneMask;
        private bool _animationEnabled;
        private bool _blockEvents;
        private float _sortValue;
        private float _lodBias;

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

        public bool CastShadows
        {
            get
            {
                return _castShadows;
            }
            set
            {
                _castShadows = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float ShadowDistance
        {
            get
            {
                return _shadowDistance;
            }
            set
            {
                _shadowDistance = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public uint ShadowMask
        {
            get
            {
                return _shadowMask;
            }
            set
            {
                _shadowMask = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float DrawDistance
        {
            get
            {
                return _drawDistance;
            }
            set
            {
                _drawDistance = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public uint MaxLights
        {
            get
            {
                return _maxLights;
            }
            set
            {
                _maxLights = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public bool Occludee
        {
            get
            {
                return _occludee;
            }
            set
            {
                _occludee = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public bool Occluder
        {
            get
            {
                return _occluder;
            }
            set
            {
                _occluder = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public uint LightMask
        {
            get
            {
                return _lightMask;
            }
            set
            {
                _lightMask = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public uint ViewMask
        {
            get
            {
                return _viewMask;
            }
            set
            {
                _viewMask = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public uint ZoneMask
        {
            get
            {
                return _zoneMask;
            }
            set
            {
                _zoneMask = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public new bool AnimationEnabled
        {
            get
            {
                return _animationEnabled;
            }
            set
            {
                _animationEnabled = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public new bool BlockEvents
        {
            get
            {
                return _blockEvents;
            }
            set
            {
                _blockEvents = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float SortValue
        {
            get
            {
                return _sortValue;
            }
            set
            {
                _sortValue = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public float LodBias
        {
            get
            {
                return _lodBias;
            }
            set
            {
                _lodBias = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public void Update()
        {
            if (Node != null)
            {
                if (Scene != null)
                {
                    var sceneNode = Scene.Children.ToList().Find(node => node.Name == Node.Name);
                    var planeNode = sceneNode.GetComponent<StaticModel>();
                    if (planeNode != null)
                    {
                        planeNode.CastShadows = CastShadows;
                        planeNode.ShadowDistance = ShadowDistance;
                        planeNode.ShadowMask = ShadowMask;

                        planeNode.DrawDistance = DrawDistance;

                        planeNode.MaxLights = MaxLights;

                        planeNode.Occludee = Occludee;
                        planeNode.Occluder = Occluder;

                        planeNode.LightMask = LightMask;
                        planeNode.ViewMask = ViewMask;
                        planeNode.ZoneMask = ZoneMask;

                        planeNode.AnimationEnabled = AnimationEnabled;
                        planeNode.BlockEvents = BlockEvents;
                        planeNode.SortValue = SortValue;
                        planeNode.LodBias = LodBias;
                    }
                }
            }
        }
    }
}
