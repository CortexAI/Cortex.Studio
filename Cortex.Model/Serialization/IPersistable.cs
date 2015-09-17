namespace Cortex.Model.Serialization
{
    public interface IPersistable
    {
        void Save(IPersisterWriter persister);
        void Load(IPersisterReader persister);
    }
}
