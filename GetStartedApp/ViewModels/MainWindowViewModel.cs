using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

/// <summary>
/// The main ViewModel controlling page navigation and state.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        // Makes the app connect to the database at startup
        // Otherwise, there's an ugly hangup when logging in for the first time
        using var context = new TimeTrackingContext();
        var users = from u in context.Users select u;
        _ = users.ToList();

        _loginView.LoginSucceeded += OnLoginSucceeded;
        CurrentPage = _loginView;
    }

    [ObservableProperty] private ViewModelBase _currentPage;

    public BoardViewModel? BoardView { get; private set; }
    public ProjectsViewModel? ProjectsView { get; private set; }
    public TasksViewModel? TasksView { get; private set; }
    public TimeLogsViewModel? TimeLogsView { get; private set; }
    public ReportsViewModel? ReportsView { get; private set; }

    public int CurrentUserId { get; private set; }
    public string CurrentUserType { get; private set; } = string.Empty;

    private readonly LoginViewModel _loginView = new();

    private void OnLoginSucceeded(string username, string password, string type, int userId)
    {
        CurrentUserId = userId;
        CurrentUserType = type;

        ShowBoard();
    }

    // Navigation methods
    [RelayCommand]
    private void LogOut()
    {
        CurrentPage = _loginView;
        ProjectsView?.Reset();
        TasksView?.Reset();
    }

    [RelayCommand]
    private void ShowBoard()
    {
        BoardView ??= new BoardViewModel(this);
        CurrentPage = BoardView;
    }

    [RelayCommand]
    public void ShowProjects()
    {
        ProjectsView ??= new ProjectsViewModel(this);
        ProjectsView.LoadProjects();
        CurrentPage = ProjectsView;
    }

    [RelayCommand]
    public void ShowTasks()
    {
        TasksView ??= new TasksViewModel(this);
        TasksView.LoadTasks();
        CurrentPage = TasksView;
    }

    [RelayCommand]
    private void ShowTimeLogs()
    {
        TimeLogsView ??= new TimeLogsViewModel(this);
        CurrentPage = TimeLogsView;
    }

    [RelayCommand]
    private void ShowReports()
    {
        ReportsView ??= new ReportsViewModel(this);
        CurrentPage = ReportsView;
    }
}
