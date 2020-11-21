using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Primitives;
using Pulsar.ObjectModel.PropertiesModel;
using System.Diagnostics;
using Urho;

namespace Pulsar.ObjectModel
{
    public class PulsarCamera : PulsarComponent, IBaseEntity
    {
        private PulsarScene _scene;
        private Node _node;
        private string _name;
        private BaseEntity _baseEntity;
        private Camera _camera;

        public bool InDesign { get; set; }

        public bool IsMainCamera { get; set; }

        public PulsarCamera(string name, PulsarScene scene)
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
                }
            }
        }
        public PulsarCamera(string name, PulsarScene scene, DebugRenderer debugRenderer)
        {
            Debug.Print("PulsarCamera_Constructor - Creating camera - " + name);
            _name = name;
            _scene = scene;

            if (_scene != null)
            {
                _node = _scene.CreateChild(_name);
                Debug.Print("PulsarCamera_Constructor - Created Node - " + name);
                if (_node != null)
                {
                    _baseEntity = _node.CreateComponent<BaseEntity>();
                    Debug.Print("PulsarCamera_Constructor - Created _baseEntity");
                    _baseEntity.Name = name;
                    Debug.Print("PulsarCamera_Constructor - Set baseEntity name to " + name);
                    _baseEntity.Node = _node;
                    Debug.Print("PulsarCamera_Constructor - Set baseEntity Node");
                    _baseEntity.PulsarScene = scene;
                    Debug.Print("PulsarCamera_Constructor - Calling CreateEntity...");
                    CreateEntity();
                    Debug.Print("PulsarCamera_Constructor - Setting debugRenderer");
                    _baseEntity.SetDebugRenderer(debugRenderer);
                    base.BaseEntity = _baseEntity;
                    _camera.DrawDebugGeometry(debugRenderer, false);
                    //CreateEntity();
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
            _camera = _node.CreateComponent<Camera>();
            Debug.Print("PulsarCamera.CreateEntity - Created Camera component");
            PulsarComponentClass = ComponentClass.Node;
            PulsarComponentType = ComponentType.Camera;

            Debug.Print("PulsarCamera.CreateEntity - Calling _baseEntity.CreateEntity...");
            _baseEntity.CreateEntity();
            if (_baseEntity.HasGizmo)
                _baseEntity.GetGizmo().Enabled = false;
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

        public object SetExtendedProperties()
        {
            CameraProperties cameraProperties = new CameraProperties
            {
                PulsarComponentClass = ComponentClass.Properties,
                PulsarComponentType = ComponentType.CameraProperties,
                Node = _camera,
                BaseEntity = _baseEntity,
                AspectRatio = _camera.AspectRatio,
                AutoAspectRatio = _camera.AutoAspectRatio,
                FarClip = _camera.FarClip,
                NearClip = _camera.NearClip,
                UseClipping = _camera.UseClipping,
                FlipVertical = _camera.FlipVertical,
                Skew = _camera.Skew,
                Orthographic = _camera.Orthographic,
                OrthographicSize = _camera.OrthoSize,
                FieldOfView = _camera.Fov,
                LevelOfDetailBias = _camera.LodBias,
                Zoom = _camera.Zoom
            };

            if(_baseEntity != null && _baseEntity.ComponentProperties != null)
            {
                _baseEntity.ComponentProperties.Add(cameraProperties);
            }

            return cameraProperties;
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

        public Camera Camera
        {
            get
            {
                return _camera;
            }
            set
            {
                _camera = value;
            }
        }

        public new Node Node
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
    }
}
