using System.ComponentModel;
using Urho;
using Urho.Physics;

namespace Pulsar.ObjectModel.Primitives
{
    public class WirePlane
    {
        private Color _color;
        private float _scale;
        private int _size;
        private Node _node;
        private readonly PulsarScene _scene;
        private readonly string _name;
        private BaseEntity _baseEntity;
        private Urho.WirePlane _wirePlane;
        private uint _viewMask;

        public Vector3 Position { get; set; }

        public DebugRenderer DebugRenderer { get; set; }

        public uint ViewMask
        {
            get
            {
                return _viewMask;
            }
            set
            {
                _viewMask = value;
                if (_node != null)
                {
                    var drawable = _node.GetComponent<CustomGeometry>();
                    if (drawable != null)
                    {
                        drawable.ViewMask = value;
                    }
                }
            }
        }

        public Node Node
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

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        [Category("Bounds")]
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        [Category("Translation")]
        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
            }
        }

        public bool InDesign { get; set; }

        public WirePlane(string name, PulsarScene pulsarScene)
        {
            _name = name;
            _scene = pulsarScene;
        }

        public void CreateEntity()
        {
            if (_node == null)
            {
                if (_scene != null)
                {
                    _node = _scene.CreateChild(_name);
                }
            }

            if (_node != null)
            {
                if (_baseEntity == null)
                {
                    _baseEntity = _node.CreateComponent<BaseEntity>();
                }
            }

            _wirePlane = new Urho.WirePlane()
            {
                Scale = _scale,
                Size = _size,
                Color = _color
            };

            if (_node != null)
                _node.AddComponent(_wirePlane);

            if (!InDesign)
            {
                RigidBody rigidBody = Node.CreateComponent<RigidBody>();
                rigidBody.Mass = 1.0f;
                rigidBody.SetAngularFactor(Vector3.Zero);
                CollisionShape planeShape = Node.CreateComponent<CollisionShape>(CreateMode.Local);
                planeShape.SetStaticPlane(Position, Quaternion.Identity);
                rigidBody.DrawDebugGeometry(DebugRenderer, false);
            }
        }

    }
}
