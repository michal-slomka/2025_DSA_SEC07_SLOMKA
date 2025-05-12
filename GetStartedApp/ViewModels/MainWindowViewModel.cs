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

        NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
    }

    public ViewModelBase CurrentPage
    {
        get;
        private set { SetProperty(ref field, value); }
    }

    public ICommand NavigateToLoginCommand { get; }

    private void NavigateToLogin()
    {
        CurrentPage = new LoginViewModel();
    }

    private void OnLoginSucceeded(string username, string password)
    {
        CurrentPage = new SecondViewModel(username, password);
    }
}
