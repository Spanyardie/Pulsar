using System;

namespace Pulsar.ExceptionsHandling
{
    [Serializable]
    public class PulsarMessageException : Exception
    {
        private string _source = "PulsarMessage:";
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

        protected PulsarMessageException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        public PulsarMessageException()
        {
        }

        public PulsarMessageException(string message) : base(message)
        {
        }

        public PulsarMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
