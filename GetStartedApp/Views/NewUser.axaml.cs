using Avalonia.Controls;
using Avalonia.Interactivity;
using GetStartedApp.ViewModels;

namespace GetStartedApp.Views;

public partial class NewUserView : UserControl
{
    public NewUserView()
    {
        InitializeComponent();
    }

    private void OnBackToLoginButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as NewUserViewModel)?.NavigateToLogin();
    }
}