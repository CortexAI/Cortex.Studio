using System.ComponentModel.Composition;

namespace Cortex.Model.Elements.Logic
{
    class ElementsDefenition
    {
        [Export]
        public static ElementGroupDefenition LogicElements = new ElementGroupDefenition("Logic");

        [Export]
        public static ElementItemDefenition StartPoint =
            new ElementItemDefenition<StartPoint>(LogicElements, "Start point", null, "Entry point of a process");

        [Export]
        public static ElementItemDefenition Subtract =
            new ElementItemDefenition<ForElement>(LogicElements, "For element", null, "Simple 'For' loop");

        [Export]
        public static ElementItemDefenition Division =
            new ElementItemDefenition<IfElement>(LogicElements, "If element", null, "Branching based on condition");
    }
}
