using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class TasksViewModel : ViewModelBase
{
    private ObservableCollection<Task> _tasks = [];
    [ObservableProperty] private ObservableCollection<Task> _filteredTasks = [];

    public MainWindowViewModel Main { get; }

    public string Filter { get; set; } = string.Empty;

    public TasksViewModel(MainWindowViewModel main)
    {
        Main = main;
    }

    [RelayCommand]
    private void FilterTasks()
    {
        FilteredTasks =
            new ObservableCollection<Task>(_tasks.Where(task =>
                task.Name.StartsWith(Filter, StringComparison.CurrentCultureIgnoreCase)));
    }

    public void LoadTasks()
    {
        using var context = new TimeTrackingContext();

        var adminTasks = from task in context.Tasks select task;
        var otherTasks = from task in context.Tasks where task.AssignedEmployeeId == Main.CurrentUserId select task;

        var userIsAdmin = Main.CurrentUserType == "admin";

        var tasks = userIsAdmin ? adminTasks : otherTasks;

        _tasks = new ObservableCollection<Task>(tasks);
        FilterTasks();
    }
    
    public void Reset()
    {
        Filter = string.Empty;
    }

    [RelayCommand]
    private void ShowCreateTaskView()
    {
        Main.CurrentPage = new CreateTaskViewModel(Main);
    }
    
    [RelayCommand]
    private void EditTask()
    {
        Main.CurrentPage = new EditTaskViewModel(Main);
    }
}
