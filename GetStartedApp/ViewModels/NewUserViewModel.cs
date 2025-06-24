using System;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class NewUserViewModel : ViewModelBase
{
    public NewUserViewModel(MainWindowViewModel main)
    {
        Main = main;
    }

    public string[] UserTypes { get; } = ["employee", "project_manager", "admin"];

    public MainWindowViewModel Main { get; }

    public string Username
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public string Password
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public string UserType
    {
        get;
        set => SetProperty(ref field, value);
    } = "employee";

    [RelayCommand]
    private void RegisterUser()
    {
        using var context = new TimeTrackingContext();

        var newUser = new User
        {
            Name = Username,
            Password = Password,
            Type = UserType
        };

        var userWithSameName = context.Users.SingleOrDefault(u => u.Name == newUser.Name);

        if (userWithSameName != null)
        {
            Console.WriteLine($"User with the provided name already exists");
            return;
        }

        context.Users.Add(newUser);
        context.SaveChanges();
    }

    [RelayCommand]
    private void Cancel()
    {
        Username = string.Empty;
        Password = string.Empty;
    }
}
