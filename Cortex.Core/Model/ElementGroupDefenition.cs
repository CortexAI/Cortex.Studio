namespace Cortex.Core.Model
{
    public class ElementGroupDefenition
    {
        public ElementGroupDefenition(string name)
        {
            Name = name;
        }

        public ElementGroupDefenition(ElementGroupDefenition parentGroup, string name)
        {
            Name = name;
            ParentGroup = parentGroup;
        }

        public string Name { get; private set; }
        public ElementGroupDefenition ParentGroup { get; private set; }
    }
}