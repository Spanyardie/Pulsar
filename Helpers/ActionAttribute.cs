using System;
using static Pulsar.ObjectModel.PulsarAction;

namespace Pulsar.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ActionAttribute : Attribute
    {
        private readonly ActionTypes _type;
        private readonly ActionDataTypes _dataType;

        public ActionTypes Type
        {
            get { return _type; }
        }

        public ActionDataTypes DataType
        {
            get { return _dataType; }
        }

        public ActionAttribute(ActionTypes type, ActionDataTypes dataType)
        {
            _type = type;
            _dataType = dataType;
        }
    }
}
