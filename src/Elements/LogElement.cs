using System;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Elements;
using Cortex.Model.Pins;

namespace Cortex.Elements
{
    [Export(typeof(IElement))]
    [Serializable]
    class LogElement : BaseElement
    {
        [NonSerialized]
        private ILog _log;

        public override string Description
        {
            get { return "Logs a value into IDE output view"; }
        }

        public override string Category
        {
            get { return ElementsCategory.CategoryName; }
        }

        public override string Name
        {
            get { return "Log"; }
        }

        public override Uri IconUri
        {
            get { return null; }
        }

        public LogElement()
        {
            _log = LogManager.GetLog(this.GetType());
            Inputs = new IInputPin[]
            {
                new FlowInputPin(Action),
                new InputPin("Object", typeof(object), null),
            };
            Outputs = new IOutputPin[]
            {
                new FlowOutputPin("Out")
            };
        }

        private void Action()
        {
            var val = Inputs[1].Value;
            if (val != null && _log != null)
                _log.Info("Output: " + Inputs[1].Value);
            ((FlowOutputPin)Outputs[0]).Call();
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            _log = LogManager.GetLog(this.GetType());
        }
    }
}
