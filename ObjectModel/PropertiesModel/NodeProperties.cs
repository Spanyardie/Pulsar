using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.Messaging;
using Pulsar.ObjectModel.Primitives;
using Urho;

namespace Pulsar.ObjectModel.PropertiesModel
{
    public class NodeProperties : PulsarComponent, IRegisterMessage
    {
        public virtual new Node Node { get; set; }

        private PulsarApplication _pulsarApplication;
        public virtual PulsarApplication PulsarApplication 
        { 
            get
            {
                return _pulsarApplication;
            }
            set
            {
                _pulsarApplication = value;
                RegisterForMessages();
            }
        }

        public virtual new PulsarScene Scene { get; set; }

        private ExtendedNodeProperties.BasicNodeProperties _container;
        public ExtendedNodeProperties.BasicNodeProperties Container 
        { 
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        private int _componentHeight;

        public virtual int ComponentHeight 
        { 
            get
            {
                return _componentHeight;
            }
            set
            {
                _componentHeight = value;
            }
        }

        public bool UpdateComplete { get; set; }

        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;
        private string _name;
        private string _previousNodeName;
        private bool _enabled;
        private bool _externallySet;
        private bool _inCreation;

        private const int DEFAULT_HEIGHT = 165;

        public NodeProperties()
        {
            _componentHeight = DEFAULT_HEIGHT;
        }

        public string NodeName
        {
            get
            {
                if (Node != null)
                {
                    return Node.Name;
                }
                else
                {
                    return "";
                }
            }
        }

        public bool InCreation
        {
            get
            {
                return _inCreation;
            }
            set
            {
                _inCreation = value;
            }
        }

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

        public virtual string PreviousNodeName
        {
            get
            {
                return _previousNodeName;
            }
            set
            {
                _previousNodeName = value;
            }
        }

        public virtual Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                //Debug.Print("NodeProperties.Position_Set - Received new value '" + value.ToString() + "'");
                //Debug.Print("NodeProperties.Position_Set - Calling Update via InvokeOnMain");
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public virtual Quaternion Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public virtual Vector3 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                Urho.Application.InvokeOnMain(Update);
            }
        }

        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!IsNameUniqueInScene(value)) return;

                _name = value;
                Application.InvokeOnMain(Update);
                if (!_externallySet)
                {
                    if (_previousNodeName != null)
                    {
                        PulsarMessage message = new PulsarMessage
                        {
                            Type = PulsarMessage.MessageType.NodeNameChanged,
                            Iterations = 1
                        };
                        message.Properties.Add("oldNodeName", _previousNodeName);
                        message.Properties.Add("newNodeName", _name);
                        //this change is dependent on Update completing first
                        Dependency dependency = new Dependency()
                        {
                            Source = this,
                            Property = "UpdateComplete",
                            Value = true
                        };
                        message.Dependencies.Add(dependency);
                        dependency = new Dependency()
                        {
                            Source = this,
                            Property = "NodeName",
                            Value = _name
                        };
                        message.Dependencies.Add(dependency);


                        if (message != null)
                            PulsarApplication.MessageQueue.PushMessage(message);
                    }
                }
                _previousNodeName = _name;
            }
        }

        private bool IsNameUniqueInScene(string value)
        {
            bool nameIsUnique = true;

            if (Scene != null)
            {
                foreach (Node node in Scene.Children)
                {
                    if (node != null)
                    {
                        if (!node.Equals(Node))
                        {
                            if (node.Name == value)
                            {
                                nameIsUnique = false;
                                break;
                            }
                        }
                    }
                }
            }

            return nameIsUnique;
        }

        public virtual new bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                Application.InvokeOnMain(Update);
            }
        }

        public virtual void Update()
        {
            if (_inCreation)
            {
                //Debug.Print("NodeProperties.Update - UPDATE OCCURRED DURING CREATION - EXITING!");
                return;
            }

            if (Node != null)
            {
                //Debug.Print("NodeProperties.Update - Node '" + Node.Name + "' is VALID");
                UpdateComplete = false;

                //Debug.Print("NodeProperties.Update - Externally set value is '" + _externallySet.ToString() + "'");
                if (!_externallySet)
                {
                    //Debug.Print("NodeProperties.Update - Set Node.Position from '" + Node.Position.ToString() + "' to '" + Position.ToString() + "'");
                    Node.Position = Position;
                    Node.Rotation = Rotation;
                    Node.Scale = Scale;

                    string oldNodeName = Node.Name;
                    if (Node.Name != Name)
                        //Debug.Print("NodeProperties.Update - Setting node '" + Node.Name + "' with new name '" + Name + "'");
                    Node.Name = Name;
                    Node.Enabled = Enabled;

                    if (Scene != null)
                    {
                        var sceneNode = Scene.GetChild(Node.Name);
                        if (sceneNode != null)
                        {
                            sceneNode.Name = Name;
                            sceneNode.Enabled = Enabled;
                            sceneNode.Position = Position;
                            sceneNode.Rotation = Rotation;
                            sceneNode.Scale = Scale;

                            //does this node contain any components that need updating?
                            if (sceneNode.Components.Count > 0)
                            {
                                var baseEntity = Node.GetComponent<BaseEntity>();
                                if (baseEntity != null)
                                {
                                    baseEntity.Name = Name;
                                    baseEntity.Position = Position;
                                    baseEntity.Rotation = Rotation.ToEulerAngles();
                                    baseEntity.Scale = Scale;
                                    baseEntity.Enabled = Enabled;
                                }
                            }
                            //if the name has changed also change the name of an associated gizmo
                            Node gizmo = Scene.GetChild(oldNodeName + "_gizmo");
                            if (gizmo != null)
                            {
                                gizmo.Name = Name + "_gizmo";
                                gizmo.Position = Position;
                                gizmo.Rotation = Rotation;
                            }
                        }
                    }
                    if (PulsarApplication != null && Scene != null)
                    {
                        PulsarApplication.DisplayScene = Scene;
                    }
                    UpdateComplete = true;
                }
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
                //Debug.Print("NodeProperties.CallBack - Received message...");
                switch(message.Type)
                {
                    case PulsarMessage.MessageType.DraggingStopped:
                        //Debug.Print("NodeProperties.CallBack - Message is type 'DraggingStopped'");
                        if (_container != null)
                        {
                            //Debug.Print("NodeProperties.CallBack - Container is set, externallySet flag changed to false");
                            //reset the 'externallySet' flag on the control itself
                            _container.ExternallySet = false;
                            _externallySet = false;
                        }
                        break;
                }
            }
        }

        public object Registrant()
        {
            return this;
        }

        private void RegisterForMessages()
        {
            //Debug.Print("NodeProperties.RegisterForMessages - Registering for message 'DraggingStopped'...");
            Registrant registrant = new Registrant
            {
                Subscriber = this,
                Type = PulsarMessage.MessageType.DraggingStopped
            };

            if(_pulsarApplication != null)
            {
                _pulsarApplication.MessageQueue.RegisterForMessage(registrant);
                //Debug.Print("NodeProperties.RegisterForMessages - Registration for message 'DraggingStopped' completed");
            }
            else
            {
                //Debug.Print("NodeProperties.RegisterForMessages - Pulsar application is NULL!");
            }
        }
    }
}
