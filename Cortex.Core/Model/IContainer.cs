using System;
using System.Collections.Generic;
using Cortex.Core.Model.Serialization;

namespace Cortex.Core.Model
{
    public interface IContainer : IPersistable
    {
        IEnumerable<IElement> Elements { get; }
        IEnumerable<IConnection> Connections { get; }

        event Action<IContainer, IElement> ElementAdded;
        event Action<IContainer, IElement> ElementRemoved;
        event Action<IContainer, IConnection> ConnectionAdded;
        event Action<IContainer, IConnection> ConnectionRemoved;

        void AddElement(IElement element);
        void RemoveElement(IElement element);
        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);

        T GetMetaData<T>(INode element, string key);
        IDictionary<string, object> GetMetaData(INode element);
        void SetMetaData<T>(INode element, string key, T value);
    }
}