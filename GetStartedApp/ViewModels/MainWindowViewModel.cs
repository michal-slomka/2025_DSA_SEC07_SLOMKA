using System;

namespace GetStartedApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        _loginView = new LoginViewModel();
        _loginView.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = _loginView;
    }

    public ViewModelBase CurrentPage
    {
        get;
        private set => SetProperty(ref field, value);
    }

    private readonly LoginViewModel _loginView;

    public void NavigateToLogin()
    {
        CurrentPage = _loginView;
    }

    private void OnLoginSucceeded(string username, string password, string type)
    {
        CurrentPage = type switch
        {
            "admin" => new NewUserViewModel(this),
            "employee" => new SecondViewModel(username, password),
            _ => throw new Exception()
        };
    }
}