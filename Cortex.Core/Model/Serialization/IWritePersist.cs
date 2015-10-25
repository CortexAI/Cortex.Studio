namespace Cortex.Core.Model.Serialization
{
    public interface IWritePersist<in T>
    {
        void Set(string key, T value);
    }
}