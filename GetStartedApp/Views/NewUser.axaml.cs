using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GetStartedApp.Views;

public partial class NewUserView : UserControl
{
    public NewUserView()
    {
        InitializeComponent();
    }

    private void OnBackToLoginButtonClick(object? sender, RoutedEventArgs e)
    {
        // Logic to navigate back to the Login view
        (DataContext as dynamic)?.NavigateToLogin();
    }
}