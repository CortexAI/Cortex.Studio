using System.Collections.Generic;

namespace Cortex.Model.Serialization
{
    class PersistableCollection<T> : List<T>, IPersistable
        where T : class, IPersistable
    {
        public PersistableCollection()
        {
            
        }

        public PersistableCollection(IEnumerable<T> collection)
        {
            this.AddRange(collection);
        }

        public void Save(IPersisterWriter persister)
        {
            var i = 0;
            persister.Set("Count", Count);
            foreach (var item in this)
            {
                persister.Set("Item" + i++, item);
            }
        }

        public void Load(IPersisterReader persister)
        {
            var count = persister.Get<int>("Count");
            for (var i = 0; i < count; i++)
            {
                Add(persister.Get<T>("Item"+i));
            }
        }
    }
}