using System.ComponentModel.Composition;
using Cortex.Core.Model;

namespace Cortex.Core.Elements.Logic
{
    internal class ElementsDefenition
    {
        [Export] public static ElementGroupDefenition LogicElements = new ElementGroupDefenition("Logic");

        [Export] public static ElementItemDefenition StartPoint =
            new ElementItemDefenition<StartPoint>(LogicElements, "Start point", null, "Entry point of a process");

        [Export] public static ElementItemDefenition Repeat =
            new ElementItemDefenition<Repeat>(LogicElements, "Repeat", null, "Simple 'For' loop");

        [Export] public static ElementItemDefenition Condition =
            new ElementItemDefenition<Condition>(LogicElements, "If element", null, "Branching based on condition");
    }
}