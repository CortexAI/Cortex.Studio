using System.Windows;
using System.Windows.Controls;

namespace Cortex.Studio.Modules.ProjectExplorer.Controls
{
    /// <summary>
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock : UserControl
    {
        public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(
            "IsEditing", typeof (bool), typeof (EditableTextBlock), new PropertyMetadata(default(bool), IsEditingChanged));

        private static void IsEditingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var val = dependencyPropertyChangedEventArgs.NewValue;
            var control = dependencyObject as EditableTextBlock;
        }

        public bool IsEditing
        {
            get { return (bool) GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }



        public EditableTextBlock()
        {
            InitializeComponent();
        }
    }
}
