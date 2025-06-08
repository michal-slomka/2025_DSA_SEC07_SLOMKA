using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class TasksViewModel : ViewModelBase
{
    private ObservableCollection<TaskItem> _tasks = [];
    [ObservableProperty] private ObservableCollection<TaskItem> _filteredTasks = [];

    public MainWindowViewModel Main { get; }

    public string Filter { get; set; } = "";

    public TasksViewModel(MainWindowViewModel main)
    {
        Main = main;
        LoadTasks();
    }

    [RelayCommand]
    private void FilterTasks()
    {
        FilteredTasks =
            new ObservableCollection<TaskItem>(_tasks.Where(task =>
                task.Title.StartsWith(Filter, System.StringComparison.CurrentCultureIgnoreCase)));
    }

    private void LoadTasks()
    {
        using var context = new TimeTrackingContext();

        // all tasks
        var adminTasks = from task in context.Tasks select task;
        // tasks assigned to the current user
        var otherTasks = from task in context.Tasks where task.AssignedEmployeeId == Main.CurrentUserId select task;

        var userIsAdmin = Main.CurrentUserType == "admin";

        var tasks = userIsAdmin ? adminTasks : otherTasks;

        var taskItems = tasks.Select(t => new TaskItem
        {
            Title = t.Name,
            Description = t.Description,
            Deadline = t.EndTime.HasValue ? t.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") : "No deadline",
            Project = $"In: {t.Project.Name}",
            TimeSpent = "",
            AssignedTo = userIsAdmin ? t.AssignedEmployee.Name : "You"
        });

        _tasks = new ObservableCollection<TaskItem>(taskItems);
        FilterTasks();
    }
}
