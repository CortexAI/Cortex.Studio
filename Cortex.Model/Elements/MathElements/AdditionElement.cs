using System;

namespace Cortex.Model.Elements.MathElements
{
    [Serializable]
    public class AdditionElement : MathElement
    {
        public override double Calc(double a, double b)
        {
            return a + b;
        }
    }
}