using Pulsar.ObjectModel.Primitives;
using Urho;

namespace Pulsar.ObjectModel.Interfaces
{
    public interface IBaseEntity
    {
        void SetNode(Node node);
        void SetScene(PulsarScene scene);
        void SetInDesign(bool isInDesign);
        void SetDebugRenderer(DebugRenderer debugRenderer);
        void SetBaseEntity(BaseEntity baseEntity);
        object SetExtendedProperties();

        Node GetNode();
        PulsarScene GetScene();
        DebugRenderer GetDebugRenderer();
        BaseEntity GetBaseEntity();
        string GetEntityName();
    }
}
