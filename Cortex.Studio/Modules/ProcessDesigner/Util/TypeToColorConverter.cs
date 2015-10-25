using System;
using System.Collections.Generic;
using System.Windows.Media;
using Cortex.Core.Model.Pins;

namespace Cortex.Studio.Modules.ProcessDesigner.Util
{
    public static class TypeToColorConverter
    {
        public static Color GetColor(IPin pin)
        {
            if (pin is IFlowInputPin || pin is IFlowOutputPin)
                return Colors.SteelBlue;
            if (pin is IDataPin)
                return GetColor(((IDataPin) pin).Type);
            return Colors.AliceBlue;
        }

        public static Color GetColor(Type type)
        {
            var colorDict = new Dictionary<Type, Color> {
                { typeof(int), Colors.LightSkyBlue },
                { typeof(double), Colors.LightSkyBlue },
                { typeof(object), Colors.Purple },
                { typeof(bool), Colors.Red },
            };

            if (colorDict.ContainsKey(type))
                return colorDict[type];

            return Colors.Purple;
        }
    }
}
