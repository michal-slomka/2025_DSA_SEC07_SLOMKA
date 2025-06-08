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
        // TODO: load this once on startup
        // using var context = new TimeTrackingContext();

        _loginView.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = _loginView;
    }

    [ObservableProperty] private ViewModelBase _currentPage;

    public ProjectsViewModel? ProjectsView { get; set; }

    // TODO: refactor
    public int CurrentUserId { get; private set; }

    // public string CurrentUserName { get; private set; } = "";
    public string CurrentUserType { get; private set; } = "";

    private readonly LoginViewModel _loginView = new();

    private void OnLoginSucceeded(string username, string password, string type, int userId)
    {
        CurrentUserId = userId;
        // CurrentUserName = username;
        CurrentUserType = type;

        CurrentPage = new SecondViewModel(this, username, password);
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
        CurrentPage = new SecondViewModel(this, "admin", "admin");
    }

    [RelayCommand]
    private void ShowProjects()
    {
        ProjectsView ??= new ProjectsViewModel(this);

        ProjectsView.LoadProjects();
        CurrentPage = ProjectsView;
    }

    [RelayCommand]
    private void ShowTasks()
    {
        CurrentPage = new TasksViewModel(this, CurrentUserType);
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
