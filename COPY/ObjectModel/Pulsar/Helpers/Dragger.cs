using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Messaging;
using System;
using System.Diagnostics;
using Urho;

namespace Pulsar.Helpers
{
    public class Dragger
    {
        public enum DragAxis
        {
            X = 0,
            Y,
            Z
        }

        public enum DragType
        {
            Translate = 0,
            Rotate,
            Scale
        }

        private bool _isDragging;
        private DragAxis _dragAxis;
        private DragType _dragType;
        private IntVector2 _surfaceDimensions;
        private float _xDragFactor;
        private float _yDragFactor;

        private const float DRAG_PERCENTAGE = 2.3f;

        private Node _mainNode;
        private Node _mainGizmoNode;
        private PulsarApplication _pulsarApplication;
        private PulsarScene _pulsarScene;

        public Dragger() { }

        public Dragger(IntVector2 surfaceDimensions)
        {
            _surfaceDimensions = surfaceDimensions;

            _dragAxis = DragAxis.X;
            _dragType = DragType.Translate;
        }

        public Dragger(IntVector2 surfaceDimensions, DragAxis dragAxis)
        {
            SurfaceDimensions = surfaceDimensions;
            DraggingAxis = dragAxis;

            _dragType = DragType.Translate;
        }

        public Dragger(IntVector2 surfaceDimensions, DragAxis dragAxis, DragType dragType)
        {
            SurfaceDimensions = surfaceDimensions;
            DraggingAxis = dragAxis;
            _dragType = dragType;
        }

        public PulsarApplication Application
        {
            get
            {
                return _pulsarApplication;
            }
            set
            {
                _pulsarApplication = value;
            }
        }

        public PulsarScene Scene
        {
            get
            {
                return _pulsarScene;
            }
            set
            {
                _pulsarScene = value;
            }
        }

        public bool IsDragging
        {
            get
            {
                return _isDragging;
            }
            set
            {
                _isDragging = value;
            }
        }

        public DragAxis DraggingAxis
        {
            get
            {
                return _dragAxis;
            }
            set
            {
                _dragAxis = value;
            }
        }

        public IntVector2 SurfaceDimensions
        {
            get
            {
                return _surfaceDimensions;
            }
            set
            {
                _surfaceDimensions = value;
                //update the factors
                _xDragFactor = value.X * (DRAG_PERCENTAGE / 100);
                _yDragFactor = value.Y * (DRAG_PERCENTAGE / 100);
            }
        }

        public DragType Type
        {
            get
            {
                return _dragType;
            }
            set
            {
                _dragType = value;
            }
        }

        public Node MainNode
        {
            get
            {
                return _mainNode;
            }
            set
            {
                _mainNode = value;

            }
        }

        public Node MainGizmoNode
        {
            get
            {
                return _mainGizmoNode;
            }
            set
            {
                _mainGizmoNode = value;
            }
        }

        public void StartDragging()
        {
            _isDragging = true;
        }

        public void StopDragging()
        {
            _isDragging = false;
        }

        public void MoveNodes(IntVector2 mouseCoOrdinates)
        {
            switch (_dragType)
            {
                case DragType.Translate:
                    PerformTranslation(mouseCoOrdinates);
                    break;
                case DragType.Rotate:
                    PerformRotation(mouseCoOrdinates);
                    break;
                case DragType.Scale:
                    PerformScaling(mouseCoOrdinates);
                    break;
            }
        }

        private void SendTranslationChangeMessage(Node sceneObject)
        {
            if (_pulsarApplication != null)
            {
                PulsarMessage message = new PulsarMessage()
                {
                    Type = PulsarMessage.MessageType.NodeTranslationChange,
                    Iterations = 1
                };
                message.Properties.Add("nodeName", sceneObject.Name);
                message.Properties.Add("changeType", PulsarMessage.MessageType.NodeTranslationChange);
                message.Properties.Add("sceneObject", sceneObject);
                message.Properties.Add("externallySet", true);

                if (message != null)
                    _pulsarApplication.MessageQueue.PushMessage(message);
            }
        }

        private void SendRotationChangeMessage(Node sceneObject)
        {
            if (_pulsarApplication != null)
            {
                PulsarMessage message = new PulsarMessage()
                {
                    Type = PulsarMessage.MessageType.NodeRotationChange,
                    Iterations = 1
                };
                message.Properties.Add("nodeName", sceneObject.Name);
                message.Properties.Add("changeType", PulsarMessage.MessageType.NodeRotationChange);
                message.Properties.Add("sceneObject", sceneObject);
                message.Properties.Add("externallySet", true);

                if (message != null)
                    _pulsarApplication.MessageQueue.PushMessage(message);
            }
        }

        private void SendScaleChangeMessage(Node sceneObject)
        {
            if (_pulsarApplication != null)
            {
                PulsarMessage message = new PulsarMessage()
                {
                    Type = PulsarMessage.MessageType.NodeScaleChange,
                    Iterations = 1
                };
                message.Properties.Add("nodeName", sceneObject.Name);
                message.Properties.Add("changeType", PulsarMessage.MessageType.NodeScaleChange);
                message.Properties.Add("sceneObject", sceneObject);
                message.Properties.Add("externallySet", true);

                if (message != null)
                    _pulsarApplication.MessageQueue.PushMessage(message);
            }
        }

        private void PerformScaling(IntVector2 mouseCoOrdinates)
        {
            if (_mainNode != null && _mainGizmoNode != null)
            {
                _mainGizmoNode.Rotation = _mainNode.Rotation;
                var scale = _mainNode.Scale;
                switch (_dragAxis)
                {
                    case DragAxis.X:
                        if (scale.X + (((float)mouseCoOrdinates.X) / _xDragFactor) > -1000.0f && scale.X + (((float)mouseCoOrdinates.X) / _xDragFactor) < 1000.0f)
                        {
                            scale.X += ((float)mouseCoOrdinates.X) / _xDragFactor;
                        }
                        break;
                    case DragAxis.Y:
                        if (scale.Y - (((float)mouseCoOrdinates.Y) / _yDragFactor) > 0.0f && scale.Y - (((float)mouseCoOrdinates.Y) / _yDragFactor) < 1000.0f)
                        {
                            scale.Y -= ((float)mouseCoOrdinates.Y) / _yDragFactor;
                        }
                        break;
                    case DragAxis.Z:
                        if (scale.Z + (((float)mouseCoOrdinates.X) / _xDragFactor) > -1000.0f && scale.X + (((float)mouseCoOrdinates.X) / _xDragFactor) < 1000.0f)
                        {
                            scale.Z += ((float)mouseCoOrdinates.X) / _xDragFactor;
                        }
                        break;
                }
                _mainNode.Scale = scale;

                SendScaleChangeMessage(_mainNode);
            }
        }

        private void PerformRotation(IntVector2 mouseCoOrdinates)
        {
            var localXFactor = _xDragFactor / 100;
            var localYFactor = _yDragFactor / 100;

            if (_mainNode != null && _mainGizmoNode != null)
            {
                _mainGizmoNode.Rotation = _mainNode.Rotation;
                var eulerAngles = new Vector3(0, 0, 0);
                switch (_dragAxis)
                {
                    case DragAxis.X:
                        eulerAngles.X = ((float)mouseCoOrdinates.X) / localXFactor;
                        break;
                    case DragAxis.Y:
                        eulerAngles.Y = ((float)mouseCoOrdinates.Y) / localYFactor;
                        break;
                    case DragAxis.Z:
                        eulerAngles.Z = ((float)mouseCoOrdinates.X) / localXFactor;
                        break;
                }

                _mainNode.Rotate(new Quaternion(eulerAngles.X, eulerAngles.Y, eulerAngles.Z), TransformSpace.Local);
                _mainGizmoNode.Rotate(new Quaternion(eulerAngles.X, eulerAngles.Y, eulerAngles.Z), TransformSpace.Local);
                SendRotationChangeMessage(_mainNode);
            }
        }

        private void PerformTranslation(IntVector2 mouseCoOrdinates)
        {
            if (_mainNode != null && _mainGizmoNode != null)
            {
                _mainGizmoNode.Rotation = new Quaternion(0, 0, 0);
                var position = _mainNode.Position;
                switch (_dragAxis)
                {
                    case DragAxis.X:
                        if (position.X + (((float)mouseCoOrdinates.X) / _xDragFactor) > -1000.0f && position.X + (((float)mouseCoOrdinates.X) / _xDragFactor) < 1000.0f)
                        {
                            position.X += ((float)mouseCoOrdinates.X) / _xDragFactor;
                        }
                        break;
                    case DragAxis.Y:
                        if (position.Y - (((float)mouseCoOrdinates.Y) / _yDragFactor) > 0.0f && position.Y - (((float)mouseCoOrdinates.Y) / _yDragFactor) < 1000.0f)
                        {
                            position.Y -= ((float)mouseCoOrdinates.Y) / _yDragFactor;
                        }
                        break;
                    case DragAxis.Z:
                        if (position.Z + (((float)mouseCoOrdinates.X) / _xDragFactor) > -1000.0f && position.X + (((float)mouseCoOrdinates.X) / _xDragFactor) < 1000.0f)
                        {
                            position.Z += ((float)mouseCoOrdinates.X) / _xDragFactor;
                        }
                        break;
                }
                _mainNode.Position = position;
                _mainGizmoNode.Position = position;
                SendTranslationChangeMessage(_mainNode);
            }
        }
    }
}
