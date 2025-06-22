using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class EditTaskViewModel : ViewModelBase
{
    public ObservableCollection<Task> AvailableTasks { get; }

    public ObservableCollection<User> AvailableEmployees { get; }
    public ObservableCollection<Project> AvailableProjects { get; }
    [ObservableProperty] private ObservableCollection<Task> _availableParentTasks;

    public Task? SelectedTask
    {
        get;
        set
        {
            using var context = new TimeTrackingContext();

            AvailableParentTasks = value is null
                ? []
                : new ObservableCollection<Task>(from t in context.Tasks where t.TaskId != value.TaskId select t);

            NewName = value?.Name ?? string.Empty;
            NewDescription = value?.Description ?? string.Empty;
            NewEmployee = value?.AssignedEmployee;
            NewProject = value?.Project;
            NewParentTask = value?.ParentTask;
            NewStartTime = value?.StartTime;
            NewEndTime = value?.EndTime;
            NewType = value?.Type;

            SetProperty(ref field, value);
        }
    }

    [ObservableProperty] private string _newName = string.Empty;
    [ObservableProperty] private string _newDescription = string.Empty;
    [ObservableProperty] private User? _newEmployee;
    [ObservableProperty] private Project? _newProject;
    [ObservableProperty] private Task? _newParentTask;
    [ObservableProperty] private DateTimeOffset? _newStartTime;
    [ObservableProperty] private DateTimeOffset? _newEndTime;
    [ObservableProperty] private string? _newType;

    private readonly MainWindowViewModel _main;

    public EditTaskViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        AvailableTasks = new ObservableCollection<Task>(context.Tasks);
        // TODO: stupid views
        AvailableEmployees =
            new ObservableCollection<User>(from u in context.Users where u.Type == "employee" select u);
        AvailableProjects = new ObservableCollection<Project>(context.Projects);
        AvailableParentTasks = [];
    }

    [RelayCommand]
    private void Save()
    {
        if (SelectedTask is null)
        {
            Console.WriteLine("No project selected");
            return;
        }

        using var context = new TimeTrackingContext();

        if (NewEmployee is null || NewProject is null)
        {
            Console.WriteLine($"The task must have an employee and project assigned");
            return;
        }

        var task = context.Tasks.First(t => t.TaskId == SelectedTask.TaskId);

        task.Name = NewName;
        task.Description = NewDescription;
        task.AssignedEmployeeId = NewEmployee.UserId;
        task.ProjectId = NewProject.ProjectId;
        task.ParentTaskId = NewParentTask?.TaskId;
        task.StartTime = NewStartTime?.DateTime ?? DateTime.Now;
        task.EndTime = NewEndTime?.DateTime;
        task.Type = NewType ?? "";

        context.Tasks.Update(task);
        context.SaveChanges();

        // _main.ShowTasks();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowTasks();
    }
}
