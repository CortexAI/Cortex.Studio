using System;
using System.Windows.Media;
using Cortex.Modules.BasicElements;

namespace Cortex.Modules.ProcessDesigner.ViewModels
{
    public class DynamicElement : ElementViewModel
    {
        private readonly IElement _element;

        public DynamicElement(IElement element)
        {
            _element = element;
            Name = _element.Name;
            IconUri = element.IconUri;
            
            foreach (var pin in _element.Inputs)
                AddInputConnector(pin.Name, GetColor(pin.Type));

            foreach (var pin in _element.Outputs)
                AddOutputConnector(pin.Name, GetColor(pin.Type));
        }

        private Color GetColor(Type t)
        {
            if (t == typeof (double))
                return Colors.LightSkyBlue;
            if (t == typeof (Flow))
                return Colors.White;

            return Colors.Black;
        }

        protected override void OnInputConnectorConnectionChanged()
        {
            base.OnInputConnectorConnectionChanged();
            _element.
        }
    }
}