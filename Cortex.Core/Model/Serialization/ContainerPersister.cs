namespace Cortex.Core.Model.Serialization
{
    public static class ContainerPersister
    {
        public static void Serialize(IContainer container, string fileName)
        {
            using (var persister = new PersisterWriter(fileName))
            {
                persister.Set("Container", container);
            }
        }

        public static T Deserialize<T>(string fileName)
            where T : class, IContainer, new()
        {
            T container;

            using (var persister = new PersisterReader(fileName))
            {
                container = persister.Get<T>("Container");
            }

            return container;
        }
    }
}