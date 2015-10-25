namespace Cortex.Core.Model.Serialization
{
    public interface IPersistable
    {
        void Save(IPersisterWriter persister);
        void Load(IPersisterReader persister);
    }
}