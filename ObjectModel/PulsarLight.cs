using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Primitives;
using Pulsar.ObjectModel.PropertiesModel;
using Urho;

namespace Pulsar.ObjectModel
{
    public class PulsarLight : PulsarComponent, IBaseEntity
    {
        private PulsarScene _scene;
        private Node _node;
        private readonly string _name;
        private BaseEntity _baseEntity;
        private Light _light;

        public bool InDesign { get; set; }

        public PulsarLight(string name, PulsarScene scene, DebugRenderer debugRenderer)
        {
            _name = name;
            _scene = scene;

            if (_scene != null)
            {
                _node = _scene.CreateChild(_name);
                if (_node != null)
                {
                    _baseEntity = _node.CreateComponent<BaseEntity>();
                    _baseEntity.Name = name;
                    _baseEntity.Node = _node;
                    _baseEntity.PulsarScene = scene;
                    base.BaseEntity = _baseEntity;
                    CreateEntity();
                    _baseEntity.SetDebugRenderer(debugRenderer);
                    _light.DrawDebugGeometry(debugRenderer, false);
                }
            }
        }

        public void SetDebugRenderer(DebugRenderer debugRenderer)
        {
            if (_baseEntity != null)
            {
                _baseEntity.SetDebugRenderer(debugRenderer);
            }
        }

        public DebugRenderer GetDebugRenderer()
        {
            if (_baseEntity != null)
            {
                return _baseEntity.GetDebugRenderer();
            }

            return null;
        }

        public void SetPosition(Vector3 position)
        {
            if (_baseEntity != null)
                _baseEntity.SetPosition(position);
        }

        public void CreateEntity()
        {
            _light = _node.CreateComponent<Light>();

            PulsarComponentClass = ComponentClass.Node;
            PulsarComponentType = ComponentType.Light;

            _baseEntity.SetScale(new Vector3(1, 1, 1));

            _baseEntity.CreateEntity();
            if (_baseEntity.HasGizmo)
                _baseEntity.GetGizmo().Enabled = false;
        }

        public void SetDirection(Vector3 direction)
        {
            if (_node != null)
            {
                _node.SetDirection(direction);
            }
        }

        public Light Light
        {
            get
            {
                return _light;
            }
            set
            {
                _light = value;
            }
        }

        public void SetNode(Node node)
        {
            _node = node;
        }

        public void SetScene(PulsarScene scene)
        {
            _scene = scene;
        }

        public void SetInDesign(bool isInDesign)
        {
            InDesign = isInDesign;
        }

        public void SetBaseEntity(BaseEntity baseEntity)
        {
            _baseEntity = baseEntity;
        }

        public Node GetNode()
        {
            return _node;
        }

        public PulsarScene GetScene()
        {
            return _scene;
        }

        public BaseEntity GetBaseEntity()
        {
            return _baseEntity;
        }

        public string GetEntityName()
        {
            return _name;
        }

        public object SetExtendedProperties()
        {
            LightProperties lightProperties = new LightProperties
            {
                PulsarComponentClass = ComponentClass.Properties,
                PulsarComponentType = ComponentType.LightProperties,
                Node = _light,
                BaseEntity = _baseEntity,
                AspectRatio = _light.AspectRatio,
                Brightness = _light.Brightness,
                Colour = _light.Color,
                EffectiveSpecularIntensity = _light.EffectiveSpecularIntensity,
                FadeDistance = _light.FadeDistance,
                FieldOfView = _light.Fov,
                Length = _light.Length,
                LightType = _light.LightType,
                PerVertex = _light.PerVertex,
                Radius = _light.Radius,
                Range = _light.Range,
                ShadowFadeDistance = _light.ShadowFadeDistance,
                ShadowIntensity = _light.ShadowIntensity,
                ShadowMaximumExtrusion = _light.ShadowMaxExtrusion,
                ShadowNearFarRatio = _light.ShadowNearFarRatio,
                ShadowResolution = _light.ShadowResolution,
                SpecularIntensity = _light.SpecularIntensity,
                Temperature = _light.Temperature,
                UsePhysicalValues = _light.UsePhysicalValues
            };

            if(_baseEntity != null && _baseEntity.ComponentProperties != null)
            {
                _baseEntity.ComponentProperties.Add(lightProperties);
            }

            return lightProperties;
        }

        protected override void Dispose(bool disposing)
        {
            _baseEntity?.Dispose();
            _light?.Dispose();
            _node?.Dispose();
            base.Dispose(disposing);
        }
    }
}
