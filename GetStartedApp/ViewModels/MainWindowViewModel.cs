using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace GetStartedApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
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
        private set { SetProperty(ref field, value); }
    }

    private void OnLoginSucceeded(string username, string password, string type)
    {
        if (type == "admin")
        {
            CurrentPage = new NewUserViewModel();
        }
        else if (type == "employee")
        {
            CurrentPage = new SecondViewModel(username, password);
        }
        else
        {
            throw new Exception();
        }

    }
}
