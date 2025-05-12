using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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

    public ObservableCollection<string> Lines { get; } = [];

    public ICommand LoginButtonClickedCommand { get; }
    public ICommand NavigateToSecondViewCommand { get; }

    public MainWindowViewModel()
    {
        LoginButtonClickedCommand = new RelayCommand(LoginButtonClicked);
        NavigateToSecondViewCommand = new RelayCommand(OpenSecondView);
    }

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
            OpenSecondView();
        }
        else
        {
            Console.WriteLine("Incorrect email or password");
        }
    }

    private void OpenSecondView()
{
    if (App.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime lifetime)
    {
        var currentWindow = lifetime.Windows.FirstOrDefault(w => w.IsActive);

        var secondView = new Views.SecondView
        {
            DataContext = new SecondViewModel()
        };
        secondView.Show();

        currentWindow?.Close();
    }
}


    private string field = "";
}
