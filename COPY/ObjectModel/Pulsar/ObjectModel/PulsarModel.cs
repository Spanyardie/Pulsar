using Pulsar.Helpers;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Primitives;
using Pulsar.ObjectModel.PropertiesModel;
using System.Linq;
using System.Threading.Tasks;
using Urho;

namespace Pulsar.ObjectModel
{
    public class PulsarModel : PulsarComponent, IBaseEntity
    {
        private StaticModel _model;
        private string _newModel;
        private PulsarScene _scene;
        private Node _node;
        private DebugRenderer _debugRenderer;
        private string _name;
        private BaseEntity _baseEntity;
        private PulsarMaterial _material;

        public bool InDesign { get; set; }

        public PulsarModel(string name)
        {
            _name = name;

            _model = new StaticModel();
        }

        public StaticModel GetModel()
        {
            return _model;
        }

        public void SetModel(string model)
        {
            var existingModel = _node.Components.ToList().Find(existing => existing.Equals(_model));

            if (existingModel == null)
            {
                _node.AddComponent(_model);
            }
            _newModel = model;

            _model.Model = _baseEntity.Application.ResourceCache.GetModel(_newModel);
        }

        public void SetMaterial(PulsarMaterial material)
        {
            if(material != null)
            {
                _model.SetMaterial(material.Material);
                if(_baseEntity != null)
                {
                    Component existingComponent = _baseEntity.Components.ToList().Find(component => component.Equals(material));
                    if (existingComponent == null)
                    {
                        _baseEntity.AddComponent(material);
                    }
                    else
                    {
                        //if in select mode, the Gizmo handles the temporary material which will need updating
                        GizmoHelper.UpdateTemporaryMaterialStore(_model.Node, material.Material);
                    }
                }
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

        public void SetDebugRenderer(DebugRenderer debugRenderer)
        {
            _debugRenderer = debugRenderer;
        }

        public void SetBaseEntity(BaseEntity baseEntity)
        {
            base.BaseEntity = baseEntity;
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

        public DebugRenderer GetDebugRenderer()
        {
            return _debugRenderer;
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
            ModelProperties modelProperties = new ModelProperties
            {
                PulsarComponentClass = ComponentClass.Properties,
                PulsarComponentType = ComponentType.ModelProperties,
                ModelName = GetModel().Model.Name,
                BaseEntity = _baseEntity,
                AssetsFolder = _scene.MainApplication.Options.ResourcePaths[0],
                Node = _node,
                PulsarApplication = _scene.MainApplication,
                Scene = _scene,
                MaterialName = GetModel().GetMaterial().Name
            };
            if (_baseEntity != null && _baseEntity.ComponentProperties != null)
            {
                _baseEntity.ComponentProperties.Add(modelProperties);
            }

            return modelProperties;
        }
    }
}
