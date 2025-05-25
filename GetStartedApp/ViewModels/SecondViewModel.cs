namespace GetStartedApp.ViewModels;

public class SecondViewModel : ViewModelBase
{
    public SecondViewModel(MainWindowViewModel mainWindowViewModel, string username, string password)
    {
        Username = $"Username: {username}";
        Password = $"Password: {password}";
        _mainWindowViewModel = mainWindowViewModel;
    }

    public string Username { get; }

    public string Password { get; }

    private readonly MainWindowViewModel _mainWindowViewModel;

    public void NavigateToLogin()
    {
        _mainWindowViewModel.NavigateToLogin();
    }
}