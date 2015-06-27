using System;

namespace Cortex.Model
{
    public interface IPin
    {
        string Name { get; }
        Type Type { get; }
        Object Value { get; }
    }
}
