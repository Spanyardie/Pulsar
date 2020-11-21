using Pulsar.ObjectModel.Primitives;
using System;
using Urho;
using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar.EventArguments
{
    public class ActionPropertyEventArgs : EventArgs
    {
        public object Data { get; set; }
        public ActionDataTypes DataType { get; set; }
        public string PropertyName { get; set; }
    }

    public class ActionVector3EventArgs : EventArgs
    {
        public Vector3 Vector { get; set; }
        public string PropertyName { get; set; }
    }

    public class ActionVector4EventArgs : EventArgs
    {
        public PulsarVector4RGBA Vector { get; set; }
        public string PropertyName { get; set; }
    }

    public class ActionBezierConfigEventArgs : EventArgs
    {
        public Vector3 ControlPoint1 { get; set; }
        public Vector3 ControlPoint2 { get; set; }
        public Vector3 EndPoint { get; set; }
        public string PropertyName { get; set; }
    }

    public class ActionTransformSpaceEventArgs : EventArgs
    {
        public TransformSpace TransformSpace { get; set; }
        public string PropertyName { get; set; }
    }

    public class ActionTargetEventArgs : EventArgs
    {
        public Node Target { get; set; }
        public string PropertyName { get; set; }
    }
}
