using System.Collections.ObjectModel;
using System.Linq;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public class TasksViewModel : ViewModelBase
{
    public ObservableCollection<TaskItem> Tasks { get; private set; } = [];

    public MainWindowViewModel Main { get; }

    public TasksViewModel(MainWindowViewModel main)
    {
        Main = main;
        LoadTasks();
    }

    private void LoadTasks()
    {
        using var context = new TimeTrackingContext();

        // all tasks
        var adminTasks = from task in context.Tasks select task;
        // tasks assigned to the current user
        var otherTasks = from task in context.Tasks where task.AssignedEmployeeId == Main.CurrentUserId select task;

        var tasks = Main.CurrentUserType switch
        {
            "admin" => adminTasks,
            _ => otherTasks
        };

        var taskItems = tasks.Select(t => new TaskItem
        {
            Title = t.Name,
            Description = t.Description,
            Deadline = t.EndTime.HasValue ? t.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") : "No deadline",
            Project = $"In: {t.Project.Name}",
            TimeSpent = "",
            AssignedTo = Main.CurrentUserType == "admin" ? t.AssignedEmployee.Name : "You"
        });

        Tasks = new ObservableCollection<TaskItem>(taskItems);
    }
}
