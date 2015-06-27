using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Cortex.Model.Elements
{
    public abstract class BaseElement : IElement
    {
        public abstract string Category { get; }
        public abstract string Name { get; }
        public abstract Uri IconUri { get; }
        public abstract string Description { get; }
        public IList<InputPin> Inputs { get; private set; }
        public IList<OutputPin> Outputs { get; private set; }

        protected BaseElement()
        {
            Inputs = new List<InputPin>();
            Outputs = new List<OutputPin>();
        }
    }

    public class NetTypeElement<T> : BaseElement
    {
        public override string Description { get { return string.Format("{0}. Represents default .NET type", typeof (T).FullName); } }

        public override string Category { get { return "Types"; } }

        public override string Name { get { return typeof (T).Name; } }

        public override Uri IconUri { get { return null; } }
        
        public T Value { get; set; }

        public NetTypeElement()
        {
            Value = default(T);
            Outputs.Add(new OutputPin("Output", typeof(T), Action));
        }

        private object Action()
        {
            return Value;
        }
    }

    [Export(typeof(IElement))]
    public class DoubleElement : NetTypeElement<Double> { }

    [Export(typeof(IElement))]
    public class BoolElement : NetTypeElement<Boolean> { }

    [Export(typeof(IElement))]
    public class IntElement : NetTypeElement<Int32> { }

    [Export(typeof(IElement))]
    public class StringElement : NetTypeElement<String> { }
}
