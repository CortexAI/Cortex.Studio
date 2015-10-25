using System;

namespace Cortex.Core.Model
{
    public abstract class ElementItemDefenition
    {
        protected ElementItemDefenition(ElementGroupDefenition group, string name, Uri icon, string description)
        {
            Group = group;
            Name = name;
            IconUri = icon;
            Description = description;
        }

        public ElementGroupDefenition Group { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Uri IconUri { get; private set; }

        public abstract IElement CreateElement();
    }

    public class ElementItemDefenition<T> : ElementItemDefenition
        where T : IElement, new()
    {
        public ElementItemDefenition(ElementGroupDefenition group, string name, Uri icon, string description)
            : base(group, name, icon, description)
        {
        }

        public override IElement CreateElement()
        {
            return new T();
        }
    }
}