using System;
using Cortex.Model.Pins;

namespace Cortex.Model.Elements.MathElements
{
    [Serializable]
    public abstract class MathElement : BaseElement
    {
        public abstract double Calc(double a, double b);
        
        protected MathElement()
        {
            Inputs = new []
            {
                new InputPin("In 1", typeof (double), 0.0),
                new InputPin("In 2", typeof (double), 0.0)
            };

            Outputs = new[]
            {
                new OutputPin("Output", typeof(double))
            };
        }

        private void Compute()
        {
            Outputs[0].Value = Calc((double) Inputs[0].Value, (double) Inputs[1].Value);
        }
    }
}