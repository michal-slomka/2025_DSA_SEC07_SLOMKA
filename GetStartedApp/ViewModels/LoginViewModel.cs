using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action<string, string, string, int>? LoginSucceeded;

    [ObservableProperty] private string _username = string.Empty;

    [ObservableProperty] private string _password = string.Empty;

    [RelayCommand]
    private void Login()
    {
        using var context = new TimeTrackingContext();

        var matchingUser = context.Users
            .FirstOrDefault(u => u.Name == Username && u.Password == Password);

        if (matchingUser == null)
        {
            Console.WriteLine("Incorrect name or password");
            return;
        }

        LoginSucceeded?.Invoke(matchingUser.Name, matchingUser.Password, matchingUser.Type, matchingUser.UserId);
    }
}
