using System.Collections.Generic;

namespace Cortex.Core.Model.Serialization
{
    internal class PersistableCollection<T> : List<T>, IPersistable
        where T : class, IPersistable
    {
        public PersistableCollection()
        {
        }

        public PersistableCollection(IEnumerable<T> collection)
        {
            AddRange(collection);
        }

        public void Save(IPersisterWriter persister)
        {
            int i = 0;
            persister.Set("Count", Count);
            foreach (T item in this)
            {
                persister.Set("Item" + i++, item);
            }
        }

        public void Load(IPersisterReader persister)
        {
            var count = persister.Get<int>("Count");
            for (int i = 0; i < count; i++)
            {
                Add(persister.Get<T>("Item" + i));
            }
        }
    }
}