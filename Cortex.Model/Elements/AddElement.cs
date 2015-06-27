using System;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements
{
    public abstract class MathElement : BaseElement
    {
        public override string Category { get { return "Math"; } }

        public abstract double Calc(double a, double b);

        protected MathElement()
        {
            Inputs.Add(new InputPin("In 1", typeof (double), 0.0));
            Inputs.Add(new InputPin("In 2", typeof (double), 0.0));
            Outputs.Add(new OutputPin("Output", typeof(double), Compute));
        }

        private object Compute()
        {
            return Calc((double) Inputs[0].Value, (double) Inputs[1].Value);
        }
    }

    [Export(typeof(IElement))]
    public class DivisionElement : MathElement
    {
        public override string Name { get { return "Division"; } }
        public override string Description { get { return "Simple Division element"; } }
        public override Uri IconUri { get { return null; } }
        public override double Calc(double a, double b)
        {
            return a / b;
        }
    }

    [Export(typeof(IElement))]
    public class MultiplyElement : MathElement
    {
        public override string Name { get { return "Multiply"; } }
        public override string Description { get { return "Simple Multiply element"; } }
        public override Uri IconUri { get { return null; } }
        public override double Calc(double a, double b)
        {
            return a * b;
        }
    }

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

    [Export(typeof(IElement))]
    public class SubtractionElement : MathElement
    {
        public override string Name { get { return "Subtraction"; } }
        public override string Description { get { return "Simple Subtraction element"; } }
        public override Uri IconUri { get { return null; } }
        public override double Calc(double a, double b)
        {
            return a - b;
        }
    }
}