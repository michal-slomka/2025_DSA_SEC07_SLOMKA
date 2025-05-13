using System;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public class NewUserViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    public NewUserViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        RegisterUserCommand = new RelayCommand(RegisterUser);
    }

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

    public string UserType
    {
        get;
        set => SetProperty(ref field, value);
    } = "employee";

    public ICommand RegisterUserCommand { get; }

    private void RegisterUser()
    {
        if (Username == "" || Password == "")
        {
            Console.WriteLine($"Empty username or password. Cannot register user.");
            return;
        }

        using var context = new TimeTrackingContext();

        var newUser = new User
        {
            Name = $"{Username}",
            Password = $"{Password}",
            Type = UserType,
        };

        var userWithSameName = context.Users.SingleOrDefault(u => u.Name == newUser.Name);

        if (userWithSameName != null)
        {
            Console.WriteLine($"User with the provided name already exists");
            return;
        }

        context.Users.Add(newUser);
        context.SaveChanges();

        Console.WriteLine($"User registered");
    }

    public void NavigateToLogin()
    {
        _mainWindowViewModel.NavigateToLogin();
    }
}