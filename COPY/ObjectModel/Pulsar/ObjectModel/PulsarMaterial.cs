using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Primitives;
using Urho;

namespace Pulsar.ObjectModel
{
    public class PulsarMaterial : PulsarComponent, IBaseEntity
    {
        private BaseEntity _baseEntity;
        private DebugRenderer _debugRenderer;
        private Node _node;
        private PulsarScene _scene;
        private Material _material;
        private string _name;

        public bool InDesign { get; set; }

        public PulsarMaterial(string name)
        {
            _name = name;
        }

        public Material Material 
        { 
            get
            {
                return _material;
            }
            set
            {
                _material = value;
            }
        }

        public void SetMaterial(string material)
        {
            if(_baseEntity != null)
            {
                //get the model associated with this entity
                PulsarModel model = (PulsarModel)_baseEntity.Components.Find(mod => mod.PulsarComponentClass == ComponentClass.Node && mod.PulsarComponentType == ComponentType.Model);
                _material = _baseEntity.Application.ResourceCache.GetMaterial(material);
                if (model != null)
                {
                    model.SetMaterial(this);
                }
            }
        }

        public void SetBaseEntity(BaseEntity baseEntity)
        {
            _baseEntity = baseEntity;
        }

        public void SetDebugRenderer(DebugRenderer debugRenderer)
        {
            _debugRenderer = debugRenderer;
        }

        public void SetInDesign(bool isInDesign)
        {
            InDesign = isInDesign;
        }

        public void SetNode(Node node)
        {
            _node = node;
        }

        public void SetScene(PulsarScene scene)
        {
            _scene = scene;
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
            //currently, material properties are handled in the PulsarModel property set
            return null;
        }
    }
}
