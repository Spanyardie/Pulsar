using Pulsar.Helpers;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using Urho;
using Urho.Physics;

namespace Pulsar.ObjectModel.Primitives
{
    public class SphereEntity : PulsarComponent, IBaseEntity, IRegisterMessage
    {
        private BaseEntity _baseEntity;
        private float _diameter = 1.0f;
        private Vector3 _scale = new Vector3();
        public float Diameter { get => _diameter; set => _diameter = value; }
        private PulsarScene _scene;
        private readonly string _name;
        private Node _node;
        public bool InDesign { get; set; }

        private PulsarMessage _receivedMessage;

        public SphereEntity(string name, PulsarScene scene, DebugRenderer debugRenderer, bool inDesign)
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

        [Category("Translation")]
        [Editor(typeof(Vector3Editor), typeof(UITypeEditor))]
        [Vector3Editor.LabelText("Scale")]
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
            _baseEntity.CreateEntity();

            _node.Scale = _scale;

            if (!InDesign)
            {
                RigidBody rigidBody = _node.CreateComponent<RigidBody>();
                rigidBody.Mass = 1.0f;
                rigidBody.SetAngularFactor(Vector3.Zero);
                CollisionShape coneShape = _node.CreateComponent<CollisionShape>(CreateMode.Local);
                coneShape.SetSphere(_diameter, new Vector3(10, 2.5f, 0), Quaternion.Identity);
                rigidBody.DrawDebugGeometry(GetDebugRenderer(), false);
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
            return _name; ;
        }

        public override void OnAttachedToNode(Node node)
        {
            base.OnAttachedToNode(node);

            if (_baseEntity != null)
            {
                _baseEntity.Position = node.Position;
                _baseEntity.Rotation = node.Rotation.ToEulerAngles();
                _baseEntity.Scale = node.Scale;
            }
        }

        public string RegistrantName()
        {
            return _name;
        }

        public void CallBack(PulsarMessage message)
        {
            if (message != null)
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
                            //is this message for me?
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
                                }
                            }
                            break;
                        case PulsarMessage.MessageType.MaterialChanged:
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
    }
}
