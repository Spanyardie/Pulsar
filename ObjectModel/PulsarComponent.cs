using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Primitives;
using Urho;

namespace Pulsar.ObjectModel
{
    public class PulsarComponent : Component, ISystem
    {
        public enum ComponentClass
        {
            Node = 0,
            Properties
        }
        private ComponentClass _componentClass;
        public ComponentClass PulsarComponentClass 
        { 
            get
            {
                return _componentClass;
            }
            set
            {
                _componentClass = value;
            }
        }

        public enum ComponentType
        {
            Animation = 0,
            BaseEntity,
            Box,
            Camera,
            CameraProperties,
            CollisionShape,
            Cone,
            Constraint,
            Cylinder,
            Gizmo,
            Light,
            LightProperties,
            Material,
            Model,
            ModelProperties,
            NodeProperties,
            ParticleEmitter,
            RigidBody,
            SkyboxProperties,
            Sound,
            Sphere,
            Texture,
            Torus
        }
        private ComponentType _type;
        public ComponentType PulsarComponentType 
        { 
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        private bool _connectedToNode;
        public bool ConnectedToNode 
        { 
            get
            {
                return _connectedToNode;
            }
            set
            {
                _connectedToNode = value;
            }
        }

        private BaseEntity _baseEntity;
        public BaseEntity BaseEntity 
        { 
            get
            {
                return _baseEntity;
            }
            set
            {
                _baseEntity = value;
            }
        }

        private bool _isSystem = false;

        public bool IsSytem
        {
            get
            {
                return _isSystem;
            }
            set
            {
                _isSystem = value;
            }
        }

        public override void OnAttachedToNode(Node node)
        {
            base.OnAttachedToNode(node);
            _connectedToNode = true;
        }

    }
}
