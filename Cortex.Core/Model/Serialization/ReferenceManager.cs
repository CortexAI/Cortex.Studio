using System.Collections.Generic;

namespace Cortex.Core.Model.Serialization
{
    internal class ReferenceManager
    {
        private readonly Dictionary<IPersistable, int> _cache;
        private int _lastId;

        public ReferenceManager()
        {
            _cache = new Dictionary<IPersistable, int>();
            _lastId = 0;
        }

        public int Add(IPersistable obj)
        {
            int id = _lastId++;
            _cache.Add(obj, id);
            return id;
        }

        public bool Exist(IPersistable obj)
        {
            return _cache.ContainsKey(obj);
        }

        public int GetId(IPersistable obj)
        {
            return _cache[obj];
        }
    }
}