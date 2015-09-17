using System;

namespace Cortex.Model.Serialization
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