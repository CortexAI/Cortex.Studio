using Cortex.Core.Model.Pins;

namespace Cortex.Core.Elements.MathElements
{
    public abstract class MathElement : BaseElement
    {
        private readonly DataInputPin<double> _input1 = new DataInputPin<double>("In 1");
        private readonly DataInputPin<double> _input2 = new DataInputPin<double>("In 2");

        protected MathElement()
        {
            AddInputPin(_input1);
            AddInputPin(_input2);
            AddOutputPin(new DynamicDataOutputPin<double>("Value", Compute));
        }

        public abstract double Calc(double a, double b);

        private object Compute()
        {
            return Calc(_input1.Value, _input2.Value);
        }
    }
}