using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GetStartedApp.Views;

public partial class CreateTaskView : UserControl
{
    public CreateTaskView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
