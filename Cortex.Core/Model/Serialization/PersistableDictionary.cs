using System.Collections.Generic;
using System.Linq;

namespace Cortex.Core.Model.Serialization
{
    internal class PersistableDictionary<T1, T2> : Dictionary<T1, T2>, IPersistable
    {
        public PersistableDictionary()
        {
        }

        public PersistableDictionary(IEnumerable<KeyValuePair<T1, T2>> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        public void Save(IPersisterWriter persister)
        {
            var d = new PersistableCollection<PersistableKeyValue<T1, T2>>();
            d.AddRange(this.Select(kvp => new PersistableKeyValue<T1, T2>
            {
                Key = kvp.Key,
                Value = kvp.Value
            }));

            persister.Set("Collection", d);
        }

        public void Load(IPersisterReader persister)
        {
            var collection = persister.Get<PersistableCollection<PersistableKeyValue<T1, T2>>>("Collection");
            foreach (var item in collection)
                Add(item.Key, item.Value);
        }
    }
}