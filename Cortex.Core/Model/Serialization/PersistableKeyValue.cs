namespace Cortex.Core.Model.Serialization
{
    internal class PersistableKeyValue<TKey, TVal> : IPersistable
    {
        public TKey Key;
        public TVal Value;

        public void Save(IPersisterWriter persister)
        {
            persister.Set("Key", Key);
            persister.Set("Value", Value);
        }

        public void Load(IPersisterReader persister)
        {
            Key = persister.Get<TKey>("Key");
            Value = persister.Get<TVal>("Value");
        }
    }
}