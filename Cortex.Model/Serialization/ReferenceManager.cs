using System.Collections.Generic;

namespace Cortex.Model.Serialization
{
    class ReferenceManager
    {
        private int _lastId;
        private Dictionary<IPersistable, int> _cache;

        public ReferenceManager()
        {
            _cache = new Dictionary<IPersistable, int>();
            _lastId = 0;
        }

        public int Add(IPersistable obj)
        {
            var id = _lastId++;
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
