using System;

namespace Pulsar.ExceptionsHandling
{
    [Serializable]
    public class PulsarGizmoException : Exception
    {
        private string _source = "PulsarGizmo:";
        private string _message;

        public override string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public new string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        protected PulsarGizmoException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        public PulsarGizmoException()
        {
        }

        public PulsarGizmoException(string message) : base(message)
        {
        }

        public PulsarGizmoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
