using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Cortex.Model;
using Cortex.Model.Elements;

namespace Cortex.Elements
{
    [Export(typeof(IElement))]
    class LogElement : BaseElement
    {
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

            Inputs.Add(new FlowInputPin(Action));
            Inputs.Add(new InputPin("Object",typeof(object), null));
            Outputs.Add(new FlowOutputPin("Out"));
        }

        private void Action()
        {
            if (Inputs[1].Value != null)
            _log.Info(Inputs[1].Value.ToString());
            ((FlowOutputPin)Outputs[0]).Call();
        }
    }
}
