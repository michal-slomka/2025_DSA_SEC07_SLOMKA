using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GetStartedApp.ViewModels;

/// <summary>
/// The main ViewModel controlling page navigation and state.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        _loginView.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = _loginView;
    }
    
    [ObservableProperty] private ViewModelBase _currentPage;

    public ProjectsViewModel? ProjectsView { get; set; }

    private readonly LoginViewModel _loginView = new();

    private void OnLoginSucceeded(string username, string password, string type)
    {
        if (type is "admin" or "employee")
        {
            CurrentPage = new SecondViewModel(username, password, this);
        }
        else
        {
            Console.WriteLine("Unknown user type");
        }
    }

    // Navigation methods
    [RelayCommand]
    private void LogOut()
    {
        CurrentPage = _loginView;
    }
    
    [RelayCommand]
    private void ShowBoard()
    {
        CurrentPage = new SecondViewModel("admin", "admin", this);
    }

    [RelayCommand]
    private void ShowProjects()
    {
        ProjectsView ??= new ProjectsViewModel(this);

        CurrentPage = ProjectsView;
    }
    
    [RelayCommand]
    private void ShowTasks()
    {
        CurrentPage = new TasksViewModel(this);
    }

    [RelayCommand]
    private void ShowTimeLogs()
    {
        CurrentPage = new TimeLogsViewModel(this);
    }

    [RelayCommand]
    private void ShowReports()
    {
        CurrentPage = new ReportsViewModel(this);
    }
}