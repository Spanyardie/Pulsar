﻿using Pulsar.Helpers;
using Pulsar.ObjectModel.Interfaces;
using System.ComponentModel;
using System.Drawing.Design;
using Urho;
using Urho.Physics;

namespace Pulsar.ObjectModel.Primitives
{
    public class ConeEntity : PulsarComponent, IBaseEntity
    {
        private float _diameter = 1.0f;
        private float _height = 1.0f;

        private Vector3 _scale = new Vector3();

        private PulsarScene _scene;
        private Node _node;
        private readonly string _name;
        private BaseEntity _baseEntity;
        public bool InDesign { get; set; }

        public float Diameter { get => _diameter; set => _diameter = value; }
        public float Height { get => _height; set => _height = value; }

        public ConeEntity(string name)
        {
            _name = name;
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

        public void CreateEntity()
        {
            _node.Scale = _scale;

            if (!InDesign)
            {
                RigidBody rigidBody = _node.CreateComponent<RigidBody>();
                rigidBody.Mass = 1.0f;
                rigidBody.SetAngularFactor(Vector3.Zero);
                CollisionShape coneShape = _node.CreateComponent<CollisionShape>(CreateMode.Local);
                coneShape.SetCone(_diameter, _height, Vector3.Zero, Quaternion.Identity);
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

        public object SetExtendedProperties()
        {
            throw new System.NotImplementedException();
        }
    }
}
