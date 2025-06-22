using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace GetStartedApp.ViewModels;

public partial class CreateReportViewModel : ViewModelBase
{
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _selectedProject = string.Empty;
    [ObservableProperty] private ObservableCollection<string> _availableProjects = new();

    private readonly ReportsViewModel _reportsViewModel;
    private readonly Action _onReportCreated;

    public CreateReportViewModel(ReportsViewModel reportsViewModel, Action onReportCreated)
    {
        _reportsViewModel = reportsViewModel;
        _onReportCreated = onReportCreated;
        using var context = new TimeTrackingContext();
        AvailableProjects = new ObservableCollection<string>(context.Projects.Select(p => p.Name));
    }

    [RelayCommand]
    private void Cancel()
    {
        _onReportCreated?.Invoke();
    }

    [RelayCommand]
    public void SaveToPath(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(SelectedProject))
            return;
        using var context = new TimeTrackingContext();
        var project = context.Projects.FirstOrDefault(p => p.Name == SelectedProject);
        if (project == null) return;
        var tasks = context.Tasks.Where(t => t.ProjectId == project.ProjectId).ToList();
        var employees = context.Employees.ToList();
        var timeLogs = context.TimeLogs.ToList();
        var rows = from t in tasks
                   join e in employees on t.AssignedEmployeeId equals e.EmployeeId
                   join tl in timeLogs on t.TaskId equals tl.TaskId into timeLogsGroup
                   from tl in timeLogsGroup.DefaultIfEmpty()
                   group tl by new { t.TaskId, e.EmployeeId, EmployeeName = e.Name, TaskName = t.Name } into g
                   select new ReportRow
                   {
                       Employee = g.Key.EmployeeName,
                       Task = g.Key.TaskName,
                       TimeSpent = g.Where(x => x != null).Aggregate(TimeSpan.Zero, (acc, x) => acc + (x?.TimeSpent ?? TimeSpan.Zero)).TotalHours
                   };
        using var writer = new StreamWriter(filePath);
        writer.WriteLine($"Raport;{Title}");
        writer.WriteLine($"Description;{Description}");
        writer.WriteLine($"Project;{SelectedProject}");
        writer.WriteLine();
        writer.WriteLine("Employee;Task;TimeSpent(hours)");
        foreach (var row in rows)
            writer.WriteLine($"{row.Employee};{row.Task};{row.TimeSpent}");
        _onReportCreated?.Invoke();
    }

    public class ReportRow
    {
        public string Employee { get; set; } = string.Empty;
        public string Task { get; set; } = string.Empty;
        public double TimeSpent { get; set; }
    }
}
