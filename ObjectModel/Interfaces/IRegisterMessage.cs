using Pulsar.ObjectModel.Messaging;

namespace Pulsar.ObjectModel.Interfaces
{
    public interface IRegisterMessage
    {
        string RegistrantName();
        void CallBack(PulsarMessage message);
        object Registrant();
    }
}
