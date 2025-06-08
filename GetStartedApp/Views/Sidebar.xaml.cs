using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GetStartedApp.Views;

public partial class Sidebar : UserControl
{
    public Sidebar()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
