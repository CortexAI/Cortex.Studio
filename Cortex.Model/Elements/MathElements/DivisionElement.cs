using System;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements.MathElements
{
    [Serializable]
    public class DivisionElement : MathElement
    {
        public override double Calc(double a, double b)
        {
            return a / b;
        }
    }
}