using Caliburn.Micro;
using Cortex.Core.Model;

namespace Cortex.Studio.Elements.LogElement
{
    class LogElement : Node
    {
        private readonly ILog _log;
        private readonly InputPin<object> _input;

        public LogElement()
        {
            _input = new InputPin<object>("Input");
            _log = LogManager.GetLog(GetType());
            AddInputPin(_input);
        }

        protected override void Handler()
        {
            var o = _input.Take();
            if(o != null)
                _log?.Info("Output: " + o);
        }
    }
}
