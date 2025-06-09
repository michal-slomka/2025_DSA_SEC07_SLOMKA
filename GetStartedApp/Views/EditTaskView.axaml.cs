using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GetStartedApp.Views;

public partial class EditTaskView : UserControl
{
    public EditTaskView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
