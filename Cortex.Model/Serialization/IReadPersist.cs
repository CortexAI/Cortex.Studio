namespace Cortex.Model.Serialization
{
    public interface IReadPersist<out T>
    {
        T Get(string key);
    }
}