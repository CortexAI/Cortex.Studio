using System.ComponentModel.Composition;

namespace Cortex.Model.Elements.Types
{
    internal class ElementsDefenition
    {
        [Export] public static ElementGroupDefenition Types = new ElementGroupDefenition("Types");

        [Export] public static ElementItemDefenition Bool =
            new ElementItemDefenition<NetTypeElement<bool>>(Types, "Bool", null, "Just a bool element");

        [Export] public static ElementItemDefenition Double =
            new ElementItemDefenition<NetTypeElement<double>>(Types, "Double", null, "Just a double element");

        [Export] public static ElementItemDefenition Int =
            new ElementItemDefenition<NetTypeElement<int>>(Types, "Integer", null, "Just an int element");

        [Export] public static ElementItemDefenition String =
            new ElementItemDefenition<NetTypeElement<string>>(Types, "String", null, "Just a string element");
    }
}