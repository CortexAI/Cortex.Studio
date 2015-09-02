using System;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements.MathElements
{
    [Serializable]
    public class MultiplyElement : MathElement
    {
        public override double Calc(double a, double b)
        {
            return a * b;
        }
    }
}