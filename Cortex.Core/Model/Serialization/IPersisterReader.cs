using System;

namespace Cortex.Core.Model.Serialization
{
    public interface IPersisterReader :
        IReadPersist<IPersistable>,
        IReadPersist<int>,
        IReadPersist<string>,
        IReadPersist<bool>,
        IReadPersist<double>,
        IReadPersist<object>,
        IReadPersist<Type>
    {
        T Get<T>(string key);
        object GetUnknown(string value);
    }
}