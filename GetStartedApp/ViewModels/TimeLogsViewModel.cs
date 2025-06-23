using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using GetStartedApp.Models;
using Microsoft.EntityFrameworkCore;
using ProjectItem = GetStartedApp.Models.TimeLogItems.ProjectItem;
using TaskItem = GetStartedApp.Models.TimeLogItems.TaskItem;
using TimeLogItem = GetStartedApp.Models.TimeLogItems.TimeLogItem;

namespace GetStartedApp.ViewModels;

public class TimeLogsViewModel : ViewModelBase
{
    public TimeLogsViewModel(MainWindowViewModel main)
    {
        Main = main;

        using var context = new TimeTrackingContext();

        var projects = from p in context.Projects
                .Include(p => p.Tasks)
                .ThenInclude(task => task.TimeLogs)
                .Include(p => p.Tasks)
                .ThenInclude(task => task.AssignedEmployee)
            select p;
        var projectItems = projects.Select(project => new ProjectItem
        {
            Project = project,
            TaskItems = project.Tasks.Select(task => new TaskItem
            {
                Task = task,
                TimeLogItems = task.TimeLogs.Select(timeLog => new TimeLogItem
                {
                    TimeLog = timeLog,
                    EmployeeName = task.AssignedEmployee.Name,
                    Color = timeLog.IsApproved == 1
                        ? Color.FromArgb(0x33, 0x00, 0xFF, 0x00).ToString()
                        : Color.FromArgb(0x33, 0xFF, 0x00, 0x00).ToString(),
                }).ToList()
            }).ToList(),
        });

        ProjectItems = new ObservableCollection<ProjectItem>(projectItems);
    }

    public MainWindowViewModel Main { get; }

    public ObservableCollection<ProjectItem> ProjectItems { get; set; }
}
