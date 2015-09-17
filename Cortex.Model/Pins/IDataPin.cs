using System;

namespace Cortex.Model.Pins
{
    public interface IDataPin : IPin
    {
        Object Value { get; }

        Type Type { get; }
    }
}