using System;

namespace Cortex.Core.Elements.MathElements
{
    [Serializable]
    public class SubtractionElement : MathElement
    {
        public override double Calc(double a, double b)
        {
            return a - b;
        }
    }
}