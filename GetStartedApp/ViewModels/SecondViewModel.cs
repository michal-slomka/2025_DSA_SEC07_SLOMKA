using System.Collections.ObjectModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public class SecondViewModel : ViewModelBase
{
    public MainWindowViewModel? Main { get; }

    // ✅ Constructor used after login
    public SecondViewModel(string username, string password)
    {
        Username = $"Username: {username}";
        Password = $"Password: {password}";
        LoadMockData();
    }

    // ✅ Constructor used when navigating with command
    public SecondViewModel(string username, string password, MainWindowViewModel main)
        : this(username, password)
    {
        Main = main;
    }

    public string Username { get; }
    public string Password { get; }

    public ObservableCollection<TaskColumn> Columns { get; set; } = [];

    private void LoadMockData()
    {
        var toDoTasks = new ObservableCollection<TaskItem>
        {
            new() { Title = "Design login screen", Description = "Mock the login UI" },
            new() { Title = "Set up project", Description = "Initialize with Avalonia MVVM" }
        };

        var inProgressTasks = new ObservableCollection<TaskItem>
        {
            new() { Title = "Implement board layout", Description = "Match Figma design" }
        };

        var doneTasks = new ObservableCollection<TaskItem>
        {
            new() { Title = "Mock login logic", Description = "admin/admin login implemented" }
        };

        Columns.Add(new TaskColumn
        {
            Name = $"TO DO {toDoTasks.Count}",
            Tasks = toDoTasks
        });

        Columns.Add(new TaskColumn
        {
            Name = $"IN PROGRESS {inProgressTasks.Count}",
            Tasks = inProgressTasks
        });

        Columns.Add(new TaskColumn
        {
            Name = $"DONE {doneTasks.Count}",
            Tasks = doneTasks
        });
    }
}
