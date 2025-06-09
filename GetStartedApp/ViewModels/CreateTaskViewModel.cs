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
    [ObservableProperty] private string? _selectedEmployee;
    [ObservableProperty] private string? _selectedProject;
    [ObservableProperty] private string? _selectedParentTask;
    [ObservableProperty] private DateTimeOffset? _startTime;
    [ObservableProperty] private DateTimeOffset? _endTime;
    [ObservableProperty] private string? _type;

    public ObservableCollection<string> AvailableEmployees { get; }
    public ObservableCollection<string> AvailableProjects { get; }
    public ObservableCollection<string> AvailableParentTasks { get; }

    private readonly MainWindowViewModel _main;

    public CreateTaskViewModel(MainWindowViewModel main)
    {
        using var context = new TimeTrackingContext();

        AvailableEmployees = new ObservableCollection<string>(
            context.Employees.Select(e => e.Name).ToList()
        );
        AvailableProjects = new ObservableCollection<string>(
            context.Projects.Select(p => p.Name).ToList()
        );
        AvailableParentTasks = new ObservableCollection<string>(
            context.Tasks.Select(t => t.Name).ToList()
        );

        _main = main ?? throw new ArgumentNullException(nameof(main));
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return;

        using var context = new TimeTrackingContext();

        if (SelectedEmployee == null || SelectedProject == null)
        {
            Console.WriteLine("Employee or Project not selected");
            return;
        }

        var employee = context.Employees.First(e => e.Name == SelectedEmployee);
        var project = context.Projects.First(p => p.Name == SelectedProject);
        int? parentTaskId = null;
        if (!string.IsNullOrWhiteSpace(SelectedParentTask))
        {
            var parentTask = context.Tasks.FirstOrDefault(t => t.Name == SelectedParentTask);
            parentTaskId = parentTask?.TaskId;
        }

        var task = new Task
        {
            Name = Name.Trim(),
            Description = Description.Trim(),
            AssignedEmployeeId = employee.EmployeeId,
            ProjectId = project.ProjectId,
            ParentTaskId = parentTaskId,
            StartTime = StartTime?.DateTime ?? DateTime.Now, // Konwersja DateTimeOffset? → DateTime
            EndTime = EndTime?.DateTime,
            Type = Type
        };
        context.Tasks.Add(task);
        context.SaveChanges();

        Name = string.Empty;
        Description = string.Empty;
        
        _main.ShowTasks();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.CurrentPage = _main.TasksView!;
    }
}
