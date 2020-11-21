using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using Pulsar.ObjectModel.PropertiesModel;
using Urho;
using Urho.Physics;

namespace Pulsar.ObjectModel.Primitives
{
    public class BoxEntity : PulsarComponent, IBaseEntity, IRegisterMessage
    {
        #region Private variables
        private Vector3 _scale = new Vector3();
        private PulsarScene _scene;
        private Node _node;
        private readonly string _name;
        private BaseEntity _baseEntity;
        private PulsarMessage _receivedMessage;
        #endregion

        #region Public accessors
        public bool InDesign { get; set; }

        public Vector3 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                if (_baseEntity != null)
                {
                    _baseEntity.Scale = _scale;
                }
            }
        }
        #endregion

        #region Constructor
        public BoxEntity(string name, PulsarScene scene, DebugRenderer debugRenderer, bool inDesign)
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
                    _baseEntity.SetDebugRenderer(debugRenderer);
                    InDesign = inDesign;
                    _baseEntity.InDesign = inDesign;
                    base.BaseEntity = _baseEntity;
                    CreateEntity();
                }
            }
            PulsarComponentClass = ComponentClass.Node;
            PulsarComponentType = ComponentType.NodeProperties;

            RegisterForMessages();
        }
        #endregion

        #region Public DebugRenderer methods
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
        #endregion

        #region Public property Set methods
        public void SetPosition(Vector3 position)
        {
            if (_baseEntity != null)
                _baseEntity.SetPosition(position);
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
        #endregion

        #region Public property Get methods
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
        #endregion

        #region Public methods
        public override void OnAttachedToNode(Node node)
        {
            base.OnAttachedToNode(node);

            if(_baseEntity != null)
            {
                _baseEntity.Position = node.Position;
                _baseEntity.Rotation = node.Rotation.ToEulerAngles();
                _baseEntity.Scale = node.Scale;
            }
        }

        public void CreateEntity()
        {
            _baseEntity.CreateEntity();

            _node.Scale = _scale;

            if (!InDesign)
            {
                RigidBody rigidBody = _node.CreateComponent<RigidBody>();
                rigidBody.Mass = 1.0f;
                rigidBody.SetAngularFactor(Vector3.Zero);
                CollisionShape boxShape = _node.CreateComponent<CollisionShape>(CreateMode.Local);
                boxShape.SetBox(_scale, _baseEntity.Position, Quaternion.Identity);
                rigidBody.DrawDebugGeometry(GetDebugRenderer(), false);
            }
        }

        public string RegistrantName()
        {
            return _name;
        }

        public void CallBack(PulsarMessage message)
        {
            if(message != null)
            {
                _receivedMessage = message;
                Application.InvokeOnMain(ProcessMessageMainThread);
            }
        }

        public void ProcessMessageMainThread()
        {
            if (_receivedMessage != null)
            {
                //is this message for me?
                if (_receivedMessage.Destination == _name)
                {
                    switch (_receivedMessage.Type)
                    {
                        case PulsarMessage.MessageType.ModelChanged:
                            _receivedMessage.Properties.TryGetValue("newModelName", out object newModelName);
                            //grab the model
                            if (_baseEntity != null)
                            {
                                var model = (PulsarModel)_baseEntity.Components.Find(mod => mod.PulsarComponentClass == ComponentClass.Node && mod.PulsarComponentType == ComponentType.Model);
                                if (model != null)
                                {
                                    model.SetModel((string)newModelName);
                                    //remove any previously associated materials
                                    _baseEntity.Components.RemoveAll(mat => mat.PulsarComponentClass == ComponentClass.Node && mat.PulsarComponentType == ComponentType.Material);
                                    //we need to update the model properties stored in the baseEntity
                                    var modelProperties = (ModelProperties)_baseEntity.ComponentProperties.Find(mod => mod.PulsarComponentClass == ComponentClass.Properties && mod.PulsarComponentType == ComponentType.ModelProperties);
                                    if (modelProperties != null)
                                    {
                                        modelProperties.ModelName = (string)newModelName;
                                    }
                                }
                            }
                            break;
                        case PulsarMessage.MessageType.MaterialChanged:
                            if (_receivedMessage.Destination == _name)
                            {
                                _receivedMessage.Properties.TryGetValue("newMaterialName", out object newMaterialName);
                                if (_baseEntity != null)
                                {
                                    //if there is an existing material with this model then change that, otherwise add new material to model
                                    var material = (PulsarMaterial)_baseEntity.Components.Find(mat => mat.PulsarComponentClass == ComponentClass.Node && mat.PulsarComponentType == ComponentType.Material);
                                    if (material != null)
                                    {
                                        material.SetMaterial((string)newMaterialName);
                                    }
                                    else
                                    {
                                        PulsarMaterial pulsarMaterial = new PulsarMaterial(_name)
                                        {
                                            PulsarComponentClass = ComponentClass.Node,
                                            PulsarComponentType = ComponentType.Material
                                        };
                                        _baseEntity.AddComponent(pulsarMaterial);
                                        pulsarMaterial.SetMaterial((string)newMaterialName);
                                        //we need to update the model properties stored in the baseEntity
                                        var modelProperties = (ModelProperties)_baseEntity.ComponentProperties.Find(mod => mod.PulsarComponentClass == ComponentClass.Properties && mod.PulsarComponentType == ComponentType.ModelProperties);
                                        if (modelProperties != null)
                                        {
                                            modelProperties.MaterialName = (string)newMaterialName;
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        public object Registrant()
        {
            return this;
        }

        public object SetExtendedProperties()
        {
            //model properties are handled by the PulsarModel component when it is added to the baseEntity
            return null;
        }
        #endregion

        #region Private and protected methods
        private void RegisterForMessages()
        {
            if (_scene != null)
            {
                var application = _scene.MainApplication;
                if (application != null)
                {
                    Registrant registrant = new Registrant
                    {
                        Subscriber = this,
                        Type = PulsarMessage.MessageType.ModelChanged
                    };
                    application.MessageQueue.RegisterForMessage(registrant);

                    registrant = new Registrant
                    {
                        Subscriber = this,
                        Type = PulsarMessage.MessageType.MaterialChanged
                    };
                    application.MessageQueue.RegisterForMessage(registrant);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            _baseEntity?.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
