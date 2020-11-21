using Pulsar.Helpers;
using Pulsar.ObjectModel.Interfaces;
using Pulsar.ObjectModel.PropertiesModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using Urho;

namespace Pulsar.ObjectModel.Primitives
{
    public class BaseEntity : Urho.Component
    {
        private string _name;
        private PulsarScene _scene;
        private Node _node;
        private DebugRenderer _debugRenderer;

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
        private bool _hasGizmo = true;

        private bool _inDesign;
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

        private void ResetGizmo()
        {
            _gizmo.RemoveGeometry();
            _gizmo.Initialise();
        }

        public List<PulsarComponent> Components
        {
            get
            {
                return _components;
            }
            private set { }
        }

        public BaseEntity()
        {
            _components = new List<PulsarComponent>();
            Debug.Print("BaseEntity_Constructor - Created _components ");
            _componentProperties = new List<PulsarComponent>();
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
                    _node.AddComponent(this);
            }
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
                    _node.AddComponent(this);
            }
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
                    _node.AddComponent(this);
            }

            SetPosition(position);
            CreateEntity();
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
                    _node.AddComponent(this);
            }

            SetPosition(position);
            SetRotation(rotation);
            CreateEntity();
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
                    _node.AddComponent(this);
            }

            SetPosition(position);
            SetRotation(rotation);
            SetScale(scale);
            CreateEntity();
        }

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
            GizmoHelper.SetAsSelected(_node, _gizmo, _scene);
            _isSelected = true;
        }

        public virtual void UnSelect()
        {
            GizmoHelper.UnSelect(_node, _gizmo);
            _isSelected = false;
        }

        private void CreateGizmo()
        {
            var gizmoNode = _scene.CreateChild(_node.Name + "_gizmo");
            _gizmo = new Gizmo(_node.Name + "_gizmo", _scene, gizmoNode, this);
            gizmoNode.AddComponent(_gizmo);
            _gizmo.Node.Position = _node.Position;

            //grab the bounds of the entity
            _gizmo.Size = Vector3.One;
            _gizmo.GizmoEnabled = false;
            Debug.Print("BaseEntity.CreateGizmo - Set GizmoEnabled to false for gizmo '" + _gizmo.Name + "'");
            _gizmo.Initialise();
            Debug.Print("BaseEntity.CreateGizmo - Called gizmo.Initialisecfor gizmo '" + _gizmo.Name + "'");
            _gizmo.SetGizmoVisible(false);
            Debug.Print("BaseEntity.CreateGizmo - Called gizmo SetGizmoVisible with value false");

            _hasGizmo = true;
        }

        public virtual void CreateEntity()
        {
            if (_gizmo == null)
            {
                Debug.Print("BaseEntity.CreateEntity - Calling CreateGizmo...");
                CreateGizmo();
            }
        }

        public Gizmo GetGizmo()
        {
            return _gizmo;
        }

        public void SetupProperties()
        {
            if (_scene != null)
            {
                NodeProperties nodeProperties = new NodeProperties();
                nodeProperties.Name = _name;
                nodeProperties.BaseEntity = this;
                nodeProperties.Scene = _scene;
                nodeProperties.Enabled = true;
                nodeProperties.Node = _node;
                nodeProperties.Position = _position;
                nodeProperties.PulsarApplication = _scene.MainApplication;
                nodeProperties.Rotation = new Quaternion(_rotation.X, _rotation.Y, _rotation.Z);
                nodeProperties.Scale = _scale;
                nodeProperties.InCreation = false;
                nodeProperties.PulsarComponentClass = PulsarComponent.ComponentClass.Properties;
                nodeProperties.PulsarComponentType = PulsarComponent.ComponentType.NodeProperties;

                if(_componentProperties != null)
                {
                    Debug.Print("BaseEntity.SetupBaseNodeProperties - Added nodeProperties to _componentProperties");
                    _componentProperties.Add(nodeProperties);
                }

                ProcessExtendedProperties();
            }

        }

        private void ProcessExtendedProperties()
        {
            foreach (PulsarComponent pulsarComponent in _components)
            {
                ((IBaseEntity)pulsarComponent).SetExtendedProperties();
            }
        }
    }
}
