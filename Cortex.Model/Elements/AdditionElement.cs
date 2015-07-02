using System;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements
{
    [Serializable]
    [Export(typeof(IElement))]
    public class AdditionElement : MathElement
    {
        public override string Name { get { return "Addition"; } }
        public override string Description { get { return "Simple addition element"; } }
        public override Uri IconUri { get { return null; } }
        public override double Calc(double a, double b)
        {
            return a + b;
        }
    }
}