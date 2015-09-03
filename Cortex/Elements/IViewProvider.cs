using System.Windows.Controls;

namespace Cortex.Elements
{
    interface IViewProvider
    {
        UserControl View { get; }
    }
}
