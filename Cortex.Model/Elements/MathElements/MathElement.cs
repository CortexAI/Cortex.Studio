using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.MathElements
{
    [Serializable]
    public abstract class MathElement : BaseElement
    {
        protected MathElement()
        {
            AddInputPin(new DataInputPin("In 1", typeof (double)));
            AddInputPin(new DataInputPin("In 2", typeof (double)));
            AddOutputPin(new DynamicDataOutputPin("Value", typeof (double), Compute));
        }

        public abstract double Calc(double a, double b);

        private object Compute()
        {
            return Calc(GetInputData<double>(0), GetInputData<double>(1));
        }
    }
}