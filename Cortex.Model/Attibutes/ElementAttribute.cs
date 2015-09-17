using System;
using System.Linq;

namespace Cortex.Model.Attibutes
{
    [AttributeUsage(AttributeTargets.Class)]
    class ElementAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public Uri IconUri { get; private set; }

        public ElementAttribute(string name, string category, string description, string uriString)
        {
            Name = name;
            Category = category;
            Description = description;

            if(!string.IsNullOrEmpty(uriString))
                IconUri = new Uri(uriString);
        }
    }
}
