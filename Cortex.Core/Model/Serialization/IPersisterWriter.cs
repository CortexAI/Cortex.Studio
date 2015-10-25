using System;

namespace Cortex.Core.Model.Serialization
{
    public interface IPersisterWriter :
        IWritePersist<IPersistable>,
        IWritePersist<int>,
        IWritePersist<string>,
        IWritePersist<bool>,
        IWritePersist<double>,
        IWritePersist<Type>,
        IWritePersist<object>
    {
    }
}