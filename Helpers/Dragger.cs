using Pulsar.ExceptionsHandling;
using Pulsar.ObjectModel;
using Pulsar.ObjectModel.Messaging;
using System;
using Urho;

namespace Pulsar.Helpers
{
    public class Dragger
    {
        #region Enumerations
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
        #endregion

        #region Private variables
        private IntVector2 _surfaceDimensions;
        private float _xDragFactor;
        private float _yDragFactor;
        private const float DRAG_PERCENTAGE = 2.3f;
        #endregion

        #region Public accessors
        public PulsarApplication Application { get; set; }

        public PulsarScene Scene { get; set; }

        public bool IsDragging { get; set; }

        public DragAxis DraggingAxis { get; set; }

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

        public DragType Type { get; set; }

        public Node MainNode { get; set; }

        public Node MainGizmoNode { get; set; }
        #endregion

        #region Constructors
        public Dragger() { }

        public Dragger(IntVector2 surfaceDimensions)
        {
            _surfaceDimensions = surfaceDimensions;

            DraggingAxis = DragAxis.X;
            Type = DragType.Translate;
        }

        public Dragger(IntVector2 surfaceDimensions, DragAxis dragAxis)
        {
            SurfaceDimensions = surfaceDimensions;
            DraggingAxis = dragAxis;

            Type = DragType.Translate;
        }

        public Dragger(IntVector2 surfaceDimensions, DragAxis dragAxis, DragType dragType)
        {
            SurfaceDimensions = surfaceDimensions;
            DraggingAxis = dragAxis;
            Type = dragType;
        }
        #endregion

        #region Private Methods
        private void SendTranslationChangeMessage(Node sceneObject)
        {
            PulsarMessage message;
            if (Application != null)
            {
                try
                {
                    message = new PulsarMessage()
                    {
                        Type = PulsarMessage.MessageType.NodeTranslationChange,
                        Iterations = 1
                    };
                }
                catch (PulsarMessageException createMessageException)
                {
                    createMessageException.Source = "=[Dragger:SendTranslationChangeMessage]";
                    createMessageException.Message += " - Unable to create PulsarMessage for message type NodeTranslationChange";
                    throw createMessageException;
                }

                if (message != null && sceneObject != null)
                {
                    try
                    {
                        message.Properties.Add("nodeName", sceneObject.Name);
                    }
                    catch(PulsarMessageException nodeNameException)
                    {
                        nodeNameException.Source = "[Dragger:SendTranslationChangeMessage]";
                        nodeNameException.Message += " - Unable to add message property 'nodeName'";
                        throw nodeNameException;
                    }

                    try
                    {
                        message.Properties.Add("changeType", PulsarMessage.MessageType.NodeTranslationChange);
                    }
                    catch (PulsarMessageException changeTypeException)
                    {
                        changeTypeException.Source = "[Dragger:SendTranslationChangeMessage]";
                        changeTypeException.Message += " - Unable to add message property 'changeType'";
                        throw changeTypeException;
                    }

                    try
                    {
                        message.Properties.Add("sceneObject", sceneObject);
                    }
                    catch (PulsarMessageException sceneObjectException)
                    {
                        sceneObjectException.Source = "[Dragger:SendTranslationChangeMessage]";
                        sceneObjectException.Message += " - Unable to add message property 'sceneObject'";
                        throw sceneObjectException;
                    }

                    try
                    {
                        message.Properties.Add("externallySet", true);
                    }
                    catch (PulsarMessageException externallySetException)
                    {
                        externallySetException.Source = "[Dragger:SendTranslationChangeMessage]";
                        externallySetException.Message += " - Unable to add message property 'externallySet'";
                        throw externallySetException;
                    }

                    if (Application.MessageQueue != null)
                    {
                        try
                        {
                            Application.MessageQueue.PushMessage(message);
                        }
                        catch (PulsarMessageException pushMessageException)
                        {
                            pushMessageException.Source = "[Dragger:SendTranslationChangeMessage]";
                            pushMessageException.Message += " - Unable to add message to application message queue";
                            throw pushMessageException;
                        }
                    }
                }
            }
        }

        private void SendRotationChangeMessage(Node sceneObject)
        {
            PulsarMessage message;
            if (Application != null)
            {
                try
                {
                    message = new PulsarMessage()
                    {
                        Type = PulsarMessage.MessageType.NodeRotationChange,
                        Iterations = 1
                    };
                }
                catch(PulsarMessageException createMessageException)
                {
                    createMessageException.Source = "[Dragger:SendRotationChangeMessage]";
                    createMessageException.Message += " - Unable to create PulsarMessage for message type NodeRotationChange";
                    throw createMessageException;
                }

                if (message != null && sceneObject != null)
                {

                    try
                    {
                        message.Properties.Add("nodeName", sceneObject.Name);
                    }
                    catch (PulsarMessageException nodeNameException)
                    {
                        nodeNameException.Source = "[Dragger:SendRotationChangeMessage]";
                        nodeNameException.Message += " - Unable to add message property 'nodeName'";
                        throw nodeNameException;
                    }

                    try
                    {
                        message.Properties.Add("changeType", PulsarMessage.MessageType.NodeRotationChange);
                    }
                    catch (PulsarMessageException changeTypeException)
                    {
                        changeTypeException.Source = "[Dragger:SendRotationChangeMessage]";
                        changeTypeException.Message += " - Unable to add message property 'changeType'";
                        throw changeTypeException;
                    }

                    try
                    {
                        message.Properties.Add("sceneObject", sceneObject);
                    }
                    catch (PulsarMessageException sceneObjectException)
                    {
                        sceneObjectException.Source = "[Dragger:SendRotationChangeMessage]";
                        sceneObjectException.Message += " - Unable to add message property 'sceneObject'";
                        throw sceneObjectException;
                    }

                    try
                    {
                        message.Properties.Add("externallySet", true);
                    }
                    catch (PulsarMessageException externallySetException)
                    {
                        externallySetException.Source = "[Dragger:SendRotationChangeMessage]";
                        externallySetException.Message += " - Unable to add message property 'externallySet'";
                        throw externallySetException;
                    }

                    if (Application.MessageQueue != null)
                    {
                        try
                        {
                            Application.MessageQueue.PushMessage(message);
                        }
                        catch (PulsarMessageException pushMessageException)
                        {
                            pushMessageException.Source = "[Dragger:SendRotationChangeMessage]";
                            pushMessageException.Message += " - Unable to add message to application message queue";
                            throw pushMessageException;
                        }
                    }
                }
            }
        }

        private void SendScaleChangeMessage(Node sceneObject)
        {
            PulsarMessage message;
            if (Application != null)
            {
                try
                {
                    message = new PulsarMessage()
                    {
                        Type = PulsarMessage.MessageType.NodeScaleChange,
                        Iterations = 1
                    };
                }
                catch (PulsarMessageException createMessageException)
                {
                    createMessageException.Source = "[Dragger:SendScaleChangeMessage]";
                    createMessageException.Message += " - Unable to create PulsarMessage for message type NodeScaleChange";
                    throw createMessageException;
                }

                if (message != null && sceneObject != null)
                {
                    try
                    {
                        message.Properties.Add("nodeName", sceneObject.Name);
                    }
                    catch (PulsarMessageException nodeNameException)
                    {
                        nodeNameException.Source = "[Dragger:SendScaleChangeMessage]";
                        nodeNameException.Message += " - Unable to add message property 'nodeName'";
                        throw nodeNameException;
                    }

                    try
                    {
                        message.Properties.Add("changeType", PulsarMessage.MessageType.NodeScaleChange);
                    }
                    catch (PulsarMessageException changeTypeException)
                    {
                        changeTypeException.Source = "[Dragger:SendScaleChangeMessage]";
                        changeTypeException.Message += " - Unable to add message property 'changeType'";
                        throw changeTypeException;
                    }

                    try
                    {
                        message.Properties.Add("sceneObject", sceneObject);
                    }
                    catch (PulsarMessageException sceneObjectException)
                    {
                        sceneObjectException.Source = "[Dragger:SendScaleChangeMessage]";
                        sceneObjectException.Message += " - Unable to add message property 'sceneObject'";
                        throw sceneObjectException;
                    }

                    try
                    {
                        message.Properties.Add("externallySet", true);
                    }
                    catch (PulsarMessageException externallySetException)
                    {
                        externallySetException.Source = "[Dragger:SendScaleChangeMessage]";
                        externallySetException.Message += " - Unable to add message property 'externallySet'";
                        throw externallySetException;
                    }

                    if (Application.MessageQueue != null)
                    {
                        try
                        {
                            Application.MessageQueue.PushMessage(message);
                        }
                        catch (PulsarMessageException pushMessageException)
                        {
                            pushMessageException.Source = "[Dragger:SendScaleChangeMessage]";
                            pushMessageException.Message += " - Unable to add message to application message queue";
                            throw pushMessageException;
                        }
                    }
                }
            }
        }

        private void PerformScaling(IntVector2 mouseCoOrdinates)
        {
            if (MainNode != null && MainGizmoNode != null)
            {
                MainGizmoNode.Rotation = MainNode.Rotation;
                var scale = MainNode.Scale;
                switch (DraggingAxis)
                {
                    case DragAxis.X:
                        if (scale.X + (mouseCoOrdinates.X / _xDragFactor) > -1000.0f && scale.X + (mouseCoOrdinates.X / _xDragFactor) < 1000.0f)
                        {
                            scale.X += mouseCoOrdinates.X / _xDragFactor;
                        }
                        break;
                    case DragAxis.Y:
                        if (scale.Y - (mouseCoOrdinates.Y / _yDragFactor) > 0.0f && scale.Y - (mouseCoOrdinates.Y / _yDragFactor) < 1000.0f)
                        {
                            scale.Y -= mouseCoOrdinates.Y / _yDragFactor;
                        }
                        break;
                    case DragAxis.Z:
                        if (scale.Z + (mouseCoOrdinates.X / _xDragFactor) > -1000.0f && scale.X + (mouseCoOrdinates.X / _xDragFactor) < 1000.0f)
                        {
                            scale.Z += mouseCoOrdinates.X / _xDragFactor;
                        }
                        break;
                }
                MainNode.Scale = scale;

                try
                {
                    SendScaleChangeMessage(MainNode);
                }
                catch(PulsarMessageException scaleChangeException)
                {
                    scaleChangeException.Source = "[Dragger:PerformScaling]";
                    scaleChangeException.Message += " - Unable to send scale change message!";
                    throw scaleChangeException;
                }
            }
        }

        private void PerformRotation(IntVector2 mouseCoOrdinates)
        {
            var localXFactor = _xDragFactor / 100;
            var localYFactor = _yDragFactor / 100;

            if (MainNode != null && MainGizmoNode != null)
            {
                MainGizmoNode.Rotation = MainNode.Rotation;
                var eulerAngles = new Vector3(0, 0, 0);
                switch (DraggingAxis)
                {
                    case DragAxis.X:
                        eulerAngles.X = mouseCoOrdinates.X / localXFactor;
                        break;
                    case DragAxis.Y:
                        eulerAngles.Y = mouseCoOrdinates.Y / localYFactor;
                        break;
                    case DragAxis.Z:
                        eulerAngles.Z = mouseCoOrdinates.X / localXFactor;
                        break;
                }

                MainNode.Rotate(new Quaternion(eulerAngles.X, eulerAngles.Y, eulerAngles.Z), TransformSpace.Local);
                MainGizmoNode.Rotate(new Quaternion(eulerAngles.X, eulerAngles.Y, eulerAngles.Z), TransformSpace.Local);
                try
                {
                    SendRotationChangeMessage(MainNode);
                }
                catch (PulsarMessageException rotationChangeException)
                {
                    rotationChangeException.Source = "[Dragger:PerformRotation]";
                    rotationChangeException.Message += " - Unable to send rotation change message!";
                    throw rotationChangeException;
                }
            }
        }

        private void PerformTranslation(IntVector2 mouseCoOrdinates)
        {
            if (MainNode != null && MainGizmoNode != null)
            {
                MainGizmoNode.Rotation = new Quaternion(0, 0, 0);
                var position = MainNode.Position;
                switch (DraggingAxis)
                {
                    case DragAxis.X:
                        if (position.X + (mouseCoOrdinates.X / _xDragFactor) > -1000.0f && position.X + (mouseCoOrdinates.X / _xDragFactor) < 1000.0f)
                        {
                            position.X += mouseCoOrdinates.X / _xDragFactor;
                        }
                        break;
                    case DragAxis.Y:
                        if (position.Y - (mouseCoOrdinates.Y / _yDragFactor) > 0.0f && position.Y - (mouseCoOrdinates.Y / _yDragFactor) < 1000.0f)
                        {
                            position.Y -= mouseCoOrdinates.Y / _yDragFactor;
                        }
                        break;
                    case DragAxis.Z:
                        if (position.Z + (mouseCoOrdinates.X / _xDragFactor) > -1000.0f && position.X + (mouseCoOrdinates.X / _xDragFactor) < 1000.0f)
                        {
                            position.Z += mouseCoOrdinates.X / _xDragFactor;
                        }
                        break;
                }
                MainNode.Position = position;
                MainGizmoNode.Position = position;

                try
                {
                    SendTranslationChangeMessage(MainNode);
                }
                catch (PulsarMessageException translatationChangeException)
                {
                    translatationChangeException.Source = "[Dragger:PerformTranslation]";
                    translatationChangeException.Message += " - Unable to send translation change message!";
                    throw translatationChangeException;
                }
            }
        }
        #endregion

        #region Public methods
        public void StartDragging()
        {
            IsDragging = true;
        }

        public void StopDragging()
        {
            IsDragging = false;
        }

        public void MoveNodes(IntVector2 mouseCoOrdinates)
        {
            switch (Type)
            {
                case DragType.Translate:
                    try
                    {
                        PerformTranslation(mouseCoOrdinates);
                    }
                    catch (PulsarMessageException translatationChangeException)
                    {
                        translatationChangeException.Source = "[Dragger:MoveNodes]";
                        translatationChangeException.Message += " - Unable to send translation change message!";
                        throw translatationChangeException;
                    }
                    break;
                case DragType.Rotate:
                    try
                    {
                        PerformRotation(mouseCoOrdinates);
                    }
                    catch (PulsarMessageException rotationChangeException)
                    {
                        rotationChangeException.Source = "[Dragger:MoveNodes]";
                        rotationChangeException.Message += " - Unable to send rotation change message!";
                        throw rotationChangeException;
                    }
                    break;
                case DragType.Scale:
                    try
                    {
                        PerformScaling(mouseCoOrdinates);
                    }
                    catch (PulsarMessageException scaleChangeException)
                    {
                        scaleChangeException.Source = "[Dragger:MoveNodes]";
                        scaleChangeException.Message += " - Unable to send scale change message!";
                        throw scaleChangeException;
                    }
                    break;
            }
        }
        #endregion
    }
}
