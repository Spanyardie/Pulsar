using Pulsar.ObjectModel.Interfaces;

namespace Pulsar.ObjectModel.Messaging
{
    public class Registrant
    {
        public IRegisterMessage Subscriber { get; set; }
        public PulsarMessage.MessageType Type { get; set; }
    }
}
