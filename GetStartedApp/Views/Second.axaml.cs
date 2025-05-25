using Avalonia.Controls;
using Avalonia.Interactivity;
using GetStartedApp.ViewModels;

namespace GetStartedApp.Views;

public partial class SecondView : UserControl
{
    public SecondView()
    {
        InitializeComponent();
    }

    private void OnBackToLoginButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as SecondViewModel)?.NavigateToLogin();
    }
}