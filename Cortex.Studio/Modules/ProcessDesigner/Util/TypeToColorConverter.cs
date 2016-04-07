using System;
using System.Collections.Generic;
using System.Windows.Media;
using Cortex.Core.Model;

namespace Cortex.Studio.Modules.ProcessDesigner.Util
{
    public static class TypeToColorConverter
    {
        private static Dictionary<Type, Color> _colors;

        static TypeToColorConverter()
        {
            _colors = new Dictionary<Type, Color> {
                { typeof(int), Colors.LightSkyBlue },
                { typeof(double), Colors.LightSkyBlue },
                { typeof(object), Colors.Purple },
                { typeof(bool), Colors.Red },
            };
        }

        public static Color GetColor(IPin pin)
        {
            return GetColor(pin.Type);
        }

        public static Color GetColor(Type type)
        {
            if (type != null)
            {
                if (_colors.ContainsKey(type))
                    return _colors[type];
            }

            return Colors.Purple;
        }
    }
}
