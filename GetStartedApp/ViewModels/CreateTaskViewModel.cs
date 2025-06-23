using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class CreateTaskViewModel : ViewModelBase
{
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private Employee? _selectedEmployee;
    [ObservableProperty] private Project? _selectedProject;
    [ObservableProperty] private Task? _selectedParentTask;
    [ObservableProperty] private DateTimeOffset? _startTime;
    [ObservableProperty] private DateTimeOffset? _endTime;
    [ObservableProperty] private string? _type;

    public ObservableCollection<Employee> AvailableEmployees { get; }
    public ObservableCollection<Project> AvailableProjects { get; }
    public ObservableCollection<Task> AvailableParentTasks { get; }

    private readonly MainWindowViewModel _main;

    public CreateTaskViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        AvailableEmployees = new ObservableCollection<Employee>(context.Employees);
        AvailableProjects = new ObservableCollection<Project>(context.Projects);

        var parentTasks = context.Tasks.ToList();
        parentTasks.Insert(0, new Task { Name = "No Parent Task", TaskId = -1 });
        AvailableParentTasks = new ObservableCollection<Task>(parentTasks);
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
        {
            Console.WriteLine("Name and description cannot be empty");
            return;
        }

        if (SelectedEmployee == null || SelectedProject == null)
        {
            Console.WriteLine("Not all field are filled");
            return;
        }

        var task = new Task
        {
            Name = Name.Trim(),
            Description = Description.Trim(),
            AssignedEmployeeId = SelectedEmployee.EmployeeId,
            ProjectId = SelectedProject.ProjectId,
            ParentTaskId = (SelectedParentTask != null && SelectedParentTask.TaskId != -1) ? SelectedParentTask.ProjectId : null,
            StartTime = StartTime?.DateTime ?? DateTime.Now,
            EndTime = EndTime?.DateTime,
            Type = Type ?? ""
        };

        using var context = new TimeTrackingContext();

        context.Tasks.Add(task);
        context.SaveChanges();

        Name = string.Empty;
        Description = string.Empty;

        _main.ShowTasks();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowTasks();
    }
}
