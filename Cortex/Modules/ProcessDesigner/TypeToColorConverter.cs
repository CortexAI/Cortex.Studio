using System;
using System.Windows.Media;
using Cortex.Model;

namespace Cortex.Modules.ProcessDesigner
{
    public static class TypeToColorConverter
    {
        public static Color GetColor(Type type)
        {
            if (type == typeof(double))
                return Colors.LightSkyBlue;
            if (type == typeof(Flow))
                return Colors.White;

            return Colors.Black;
        }
    }
}
