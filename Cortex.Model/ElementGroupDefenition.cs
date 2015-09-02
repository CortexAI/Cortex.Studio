using System;

namespace Cortex.Model
{
    public class ElementGroupDefenition
    {
        public string Name { get; private set; }
        public ElementGroupDefenition ParentGroup { get; private set; }

        public ElementGroupDefenition(string name)
        {
            Name = name;
        }

        public ElementGroupDefenition(ElementGroupDefenition parentGroup, string name)
        {
            Name = name;
            ParentGroup = parentGroup;
        }
    }
}
