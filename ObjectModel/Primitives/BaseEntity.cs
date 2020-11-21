using Pulsar.ExceptionsHandling;
using Pulsar.Helpers;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.PropertiesModel;
using System.Collections.Generic;
using System.Diagnostics;
using Urho;

namespace Pulsar.ObjectModel.Primitives
{
    public class BaseEntity : Component
    {
        #region Private variables
        private string _name;
        private PulsarScene _scene;
        private Node _node;
        private DebugRenderer _debugRenderer;
        private PulsarActions _pulsarActions;
        private Vector3 _position;
        private Vector3 _scale;
        private Vector3 _rotation;
        private Quaternion _quaternionRotation;
        private readonly List<PulsarComponent> _components;
        private readonly List<PulsarComponent> _componentProperties;
        public List<PulsarComponent> ComponentProperties
        { 
            get
            {
                return _componentProperties;
            }
        }
        private bool _isSelected;
        private bool _previousSelectedValue;
        private Gizmo _gizmo;
        private bool _hasGizmo = false;
        private bool _isSystem = false;
        private bool _inDesign;
        #endregion

        #region Public accessors
        public bool InDesign 
        { 
            get
            {
                return _inDesign;
            }
            set
            {
                _inDesign = value;
            }
        }

        public string Name
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

        public bool HasGizmo
        {
            get
            {
                return _hasGizmo;
            }
            private set { }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _previousSelectedValue = _isSelected;
                _isSelected = value;
                //when changed also change the gizmo for this object
                if (_gizmo != null)
                {
                    if (_previousSelectedValue != _isSelected)
                    {
                        if (value)
                        {
                            SetAsSelected();
                        }
                        else
                        {
                            UnSelect();
                        }
                    }
                }
            }
        }

        public bool IsSystem 
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

        public PulsarScene PulsarScene
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

        public Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                if (_gizmo != null) { _gizmo.Node.Position = value; }
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                _quaternionRotation = new Quaternion(_rotation.X, _rotation.Y, _rotation.Z, 1);

            }
        }

        public Quaternion QuaternionRotation
        {
            get
            {
                return _quaternionRotation;
            }
            private set { }
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
                //_pulsarActions.Node = value;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                if (_gizmo != null)
                {
                    _gizmo.Size = _scale;
                    ResetGizmo();
                }
            }
        }

        public List<PulsarComponent> Components
        {
            get
            {
                return _components;
            }
            private set { }
        }
        #endregion

        #region constructors
        public BaseEntity()
        {
            _components = new List<PulsarComponent>();
            //Debug.Print("BaseEntity_Constructor - Created _components ");
            _componentProperties = new List<PulsarComponent>();
            _pulsarActions = new PulsarActions();
            ReceiveSceneUpdates = true;
        }

        public BaseEntity(string name, PulsarScene scene)
        {
            _components = new List<PulsarComponent>();
            _componentProperties = new List<PulsarComponent>();

            _name = name;
            _scene = scene;

            if (scene != null)
            {
                _node = scene.CreateChild(_name);
                if (_node != null)
                {
                    _node.AddComponent(this);
                    _pulsarActions = new PulsarActions(_node);
                }
            }
            ReceiveSceneUpdates = true;
        }

        public BaseEntity(string name, PulsarScene scene, DebugRenderer debugRenderer)
        {
            _components = new List<PulsarComponent>();
            _componentProperties = new List<PulsarComponent>();

            _name = name;
            _scene = scene;
            _debugRenderer = debugRenderer;

            if (scene != null)
            {
                _node = scene.CreateChild(_name);
                if (_node != null)
                {
                    _node.AddComponent(this);
                    _pulsarActions = new PulsarActions(_node);
                }
            }
            ReceiveSceneUpdates = true;
        }

        public BaseEntity(string name, PulsarScene scene, DebugRenderer debugRenderer, Vector3 position)
        {
            _components = new List<PulsarComponent>();
            _componentProperties = new List<PulsarComponent>();

            _name = name;
            _scene = scene;
            _debugRenderer = debugRenderer;

            if (scene != null)
            {
                _node = scene.CreateChild(_name);
                if (_node != null)
                {
                    _node.AddComponent(this);
                    _pulsarActions = new PulsarActions(_node);
                }
            }

            SetPosition(position);
            CreateEntity();
            ReceiveSceneUpdates = true;
        }

        public BaseEntity(string name, PulsarScene scene, DebugRenderer debugRenderer, Vector3 position, Vector3 rotation)
        {
            _components = new List<PulsarComponent>();
            _componentProperties = new List<PulsarComponent>();

            _name = name;
            _scene = scene;
            _debugRenderer = debugRenderer;

            if (scene != null)
            {
                _node = scene.CreateChild(_name);
                if (_node != null)
                {
                    _node.AddComponent(this);
                    _pulsarActions = new PulsarActions(_node);
                }
            }

            SetPosition(position);
            SetRotation(rotation);
            CreateEntity();
            ReceiveSceneUpdates = true;
        }

        public BaseEntity(string name, PulsarScene scene, DebugRenderer debugRenderer, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            _components = new List<PulsarComponent>();
            _componentProperties = new List<PulsarComponent>();

            _name = name;
            _scene = scene;
            _debugRenderer = debugRenderer;

            if (scene != null)
            {
                _node = scene.CreateChild(_name);
                if (_node != null)
                {
                    _node.AddComponent(this);
                    _pulsarActions = new PulsarActions(_node);
                }
            }

            SetPosition(position);
            SetRotation(rotation);
            SetScale(scale);
            CreateEntity();
            ReceiveSceneUpdates = true;
        }
        #endregion

        #region Public methods
        public virtual void SetPosition(Vector3 position)
        {
            if (_node != null)
            {
                _node.Position = position;
                _position = position;
            }
        }

        public virtual void SetRotation(Vector3 rotation)
        {
            if (_node != null)
            {
                _node.Rotation = new Quaternion(rotation.X, rotation.Y, rotation.Z);
                _rotation = rotation;
            }
        }

        public virtual void SetScale(Vector3 scale)
        {
            if (_node != null)
            {
                _node.Scale = scale;
                _scale = scale;
            }
        }

        public virtual void SetDebugRenderer(DebugRenderer renderer)
        {
            _debugRenderer = renderer;
        }

        public virtual DebugRenderer GetDebugRenderer()
        {
            return _debugRenderer;
        }

        public virtual void AddComponent(PulsarComponent component)
        {
            if (component != null)
            {
                // make sure this component does not already exist in the list
                var existingEntity = _components.Find(entity => entity.Equals(component));

                if (existingEntity == null)
                {
                    IBaseEntity baseEntity = (IBaseEntity)component;
                    if (baseEntity != null)
                    {
                        baseEntity.SetDebugRenderer(_debugRenderer);
                        baseEntity.SetInDesign(InDesign);
                        baseEntity.SetNode(_node);
                        baseEntity.SetScene(_scene);
                        baseEntity.SetBaseEntity(this);
                    }
                    _node.AddComponent(component);
                    _components.Add(component);
                }
            }
        }

        public virtual void RemoveComponent(PulsarComponent component)
        {
            if (component != null)
            {
                _components.Remove(component);
            }
        }

        public virtual void SetAsSelected()
        {
            try
            {
                GizmoHelper.SetAsSelected(_node, _gizmo, _scene);
            }
            catch(PulsarGizmoException selectedGizmoException)
            {
                selectedGizmoException.Source = "[BaseEntity:SetAsSelected]";
                selectedGizmoException.Message = "Failed to set Gizmo as selected.";
                throw selectedGizmoException;
            }
            _isSelected = true;
        }

        public virtual void UnSelect()
        {
            Debug.Print("BaseEntity.UnSelect - Calling GizmoHelper.UnSelect...");
            try
            {
                GizmoHelper.UnSelect(_node, _gizmo);
            }
            catch (PulsarGizmoException unSelectGizmoException)
            {
                unSelectGizmoException.Source = "[BaseEntity:SetAsSelected]";
                unSelectGizmoException.Message = "Failed to set Gizmo as selected.";
                throw unSelectGizmoException;
            }

            Debug.Print("BaseEntity.UnSelect - Setting _isSelected of " + Name + " to False");
            _isSelected = false;
        }

        public void CreateGizmo()
        {
            Node gizmoNode;
            try
            {
                gizmoNode = _scene.CreateChild(_node.Name + "_gizmo");
            }
            catch(PulsarGizmoException sceneNodeCreateException)
            {
                sceneNodeCreateException.Source = "[BaseEntity:CreateGizmo]";
                sceneNodeCreateException.Message = "Failed to create Gizmo child node in Scene.";
                throw sceneNodeCreateException;
            }

            try
            {
                _gizmo = new Gizmo(_node.Name + "_gizmo", _scene, gizmoNode, this);
            }
            catch(PulsarGizmoException createGizmoException)
            {
                createGizmoException.Source = "[BaseEntity:CreateGizmo]";
                createGizmoException.Message = "Failed to create Gizmo component.";
                throw createGizmoException;
            }

            gizmoNode.AddComponent(_gizmo);

            if (_gizmo.Node != null)
            {
                _gizmo.Node.Position = _node.Position;
            }
            //grab the bounds of the entity
            _gizmo.Size = Vector3.One;

            try
            {
                _gizmo.Initialise();
            }
            catch(PulsarGizmoException initialiseGizmoException)
            {
                initialiseGizmoException.Source = "[BaseEntity:CreateGizmo]";
                initialiseGizmoException.Message = "Failed to initialise Gizmo.";
                throw initialiseGizmoException;
            }

            _hasGizmo = true;
        }

        public virtual void CreateEntity()
        {
            if(_pulsarActions == null)
            {
                _pulsarActions = new PulsarActions(_node)
                {
                    BaseEntity = this
                };
            }
            else
            {
                _pulsarActions.BaseEntity = this;
            }

            if (_pulsarActions != null && _pulsarActions.Node == null)
                _pulsarActions.Node = _node;

            ReceiveSceneUpdates = true;
        }

        public Gizmo GetGizmo()
        {
            return _gizmo;
        }

        public void SetupProperties()
        {
            if (_scene != null)
            {
                NodeProperties nodeProperties = new NodeProperties
                {
                    Name = _name,
                    BaseEntity = this,
                    Scene = _scene,
                    Enabled = true,
                    Node = _node,
                    Position = _position,
                    PulsarApplication = _scene.MainApplication,
                    Rotation = new Quaternion(_rotation.X, _rotation.Y, _rotation.Z),
                    Scale = _scale,
                    InCreation = false,
                    PulsarComponentClass = PulsarComponent.ComponentClass.Properties,
                    PulsarComponentType = PulsarComponent.ComponentType.NodeProperties
                };

                if (_componentProperties != null)
                {
                    _componentProperties.Add(nodeProperties);
                }

                ProcessExtendedProperties();
            }

        }

        public PulsarActions Actions
        {
            get
            {
                return _pulsarActions;
            }
            private set { }
        }

        public void AddAction(PulsarAction action)
        {
            if (_pulsarActions != null)
            {
                _pulsarActions.AddAction(action);
            }
        }
        #endregion

        #region Private methods
        private void ProcessExtendedProperties()
        {
            foreach (PulsarComponent pulsarComponent in _components)
            {
                ((IBaseEntity)pulsarComponent).SetExtendedProperties();
            }
        }

        private void ResetGizmo()
        {
            _gizmo.RemoveGeometry();
            _gizmo.Initialise();
        }
        #endregion

        #region Protected overrides
        protected override void OnUpdate(float timeStep)
        {
            base.OnUpdate(timeStep);

            if(_pulsarActions != null && _pulsarActions.ActionList.Count > 0)
            {
                _pulsarActions.Update(timeStep);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _gizmo?.Dispose();
            _pulsarActions?.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
