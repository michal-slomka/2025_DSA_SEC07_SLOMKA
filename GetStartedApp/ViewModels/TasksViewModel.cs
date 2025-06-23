using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;
using Microsoft.EntityFrameworkCore; 

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

        var adminTasks = context.Tasks
            .Include(t => t.AssignedEmployee); 

        var otherTasks = context.Tasks
            .Where(task => task.AssignedEmployeeId == Main.CurrentUserId)
            .Include(t => t.AssignedEmployee); 

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
    private void ShowLogTimeView()
    {
        Main.CurrentPage = new LogTimeViewModel(Main);
    }

    [RelayCommand]
    private void EditTask()
    {
        Main.CurrentPage = new EditTaskViewModel(Main);
    }
}
