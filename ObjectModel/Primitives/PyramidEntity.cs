using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Shapes;

namespace Pulsar.ObjectModel.Primitives
{
    public class PyramidEntity : Pyramid
    {
        private PulsarScene _scene;
        private Node _node;
        private Model _model;
        private Material _material;
        private DebugRenderer _debugRenderer;
        private PulsarApplication _application;
        private float _scale = 1.0f;
        private string _name;

        public PyramidEntity(PulsarScene scene)
        {
            _scene = scene;
            CreateEntity();
        }

        public PyramidEntity(PulsarScene scene, PulsarApplication application)
        {
            _scene = scene;
            _application = application;
            CreateEntity();
        }

        public PyramidEntity(PulsarScene scene, PulsarApplication application, DebugRenderer debugRenderer)
        {
            _scene = scene;
            _application = application;
            _debugRenderer = debugRenderer;
            CreateEntity();
        }

        public string EntityName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public float EntityScale
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


        public PulsarScene ActiveScene
        {
            get
            {
                return _scene;
            }
            set
            {
                _scene = value;
            }
        }

        public Node EntityNode
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

        public Model EntityModel
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        public Material EntityMaterial
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

        public void CreateEntity()
        {
            _node = _scene.CreateChild(_name);
        }

        public void SetModel(string model)
        {
            _model = _application.ResourceCache.GetModel(model);
        }

        public void SetMaterial(string material)
        {
            _material = _application.ResourceCache.GetMaterial(material);
        }

    }
}
