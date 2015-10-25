using System;

namespace Cortex.Core.Model.Pins
{
    public interface IDataPin : IPin
    {
        Object Value { get; }

        Type Type { get; }
    }
}