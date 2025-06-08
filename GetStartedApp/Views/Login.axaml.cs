using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace GetStartedApp.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        Loaded += FocusEmailTextBox;
    }

    private void FocusEmailTextBox(object? sender, RoutedEventArgs e)
    {
        EmailTextBox.Focus();
    }

    private void PasswordTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        LoginButton.Command!.Execute(null);
    }
}
