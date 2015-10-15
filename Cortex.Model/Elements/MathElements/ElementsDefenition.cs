using System.ComponentModel.Composition;

namespace Cortex.Model.Elements.MathElements
{
    internal class ElementsDefenition
    {
        [Export] public static ElementGroupDefenition MathElements = new ElementGroupDefenition("Math Elements");

        [Export] public static ElementItemDefenition AddElement =
            new ElementItemDefenition<AdditionElement>(MathElements, "Addition (+)", null, "Simple addition");

        [Export] public static ElementItemDefenition Subtract =
            new ElementItemDefenition<SubtractionElement>(MathElements, "Subtraction (-)", null, "Simple subtraction");

        [Export] public static ElementItemDefenition Division =
            new ElementItemDefenition<DivisionElement>(MathElements, "Division (/)", null, "Simple division");

        [Export] public static ElementItemDefenition Multiply =
            new ElementItemDefenition<MultiplyElement>(MathElements, "Multiplication (*)", null, "Simple multiplication");
    }
}