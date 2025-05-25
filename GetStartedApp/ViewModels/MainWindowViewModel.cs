using System;

namespace GetStartedApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        var loginViewModel = new LoginViewModel();
        loginViewModel.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = loginViewModel;
    }

    public ViewModelBase CurrentPage
    {
        get;
        private set => SetProperty(ref field, value);
    }

    private void OnLoginSucceeded(string username, string password, string type)
    {
        CurrentPage = type switch
        {
            "admin" => new NewUserViewModel(),
            "employee" => new SecondViewModel(username, password),
            _ => throw new Exception()
        };
    }
}