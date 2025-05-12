using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public LoginViewModel()
    {
        LoginButtonClickedCommand = new RelayCommand(LoginButtonClicked);
    }

    public event Action<string, string>? LoginSucceeded;

    public string Username
    {
        get;
        set => SetProperty(ref field, value);
    } = "";

    public string Password
    {
        get;
        set => SetProperty(ref field, value);
    } = "";

    public ObservableCollection<User> Users { get; } = [];

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
            var u = matchingUsers.First();
            Console.WriteLine($"Logged in as: {u}");
            Users.Add(u);
            LoginSucceeded?.Invoke(Username, Password);
        }
        else
        {
            Console.WriteLine("Incorrect email or password");
        }
    }
}