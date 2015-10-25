namespace Cortex.Core.Model.Serialization
{
    public interface IReadPersist<out T>
    {
        T Get(string key);
    }
}