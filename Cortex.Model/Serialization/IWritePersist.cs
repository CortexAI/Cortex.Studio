namespace Cortex.Model.Serialization
{
    public interface IWritePersist<in T>
    {
        void Set(string key, T value);
    }
}