using System.Windows.Controls;

namespace Cortex.Studio.Elements
{
    interface IViewProvider
    {
        UserControl CreateView();
    }
}
