using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Username
    {
        get => field;
        set => SetProperty(ref field, value);
    } = "";

    public string Password
    {
        get => field;
        set => SetProperty(ref field, value);
    } = "";

    public MainWindowViewModel()
    {
        LoginButtonClickedCommand = new RelayCommand(LoginButtonClicked);
    }

    public ICommand LoginButtonClickedCommand { get; }

    private void LoginButtonClicked()
    {
        using var context = new TimeTrackingContext();

        var user = new User
        {
            Name = $"{Username}",
            Password = $"{Password}",
            Type = "admin",
        };

        var matchingUsers =
            from u in context.Users
            where u.Name == user.Name && u.Password == user.Password
            select u;

        if (matchingUsers.Count() == 1)
        {
            Console.WriteLine("Logged in");
        }
        else
        {
            Console.WriteLine("Incorrect email or password");
        }
    }
}
