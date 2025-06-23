using System;
using System.Collections.ObjectModel;
using System.Linq;
using GetStartedApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GetStartedApp.ViewModels;

public class TimeLogsViewModel : ViewModelBase
{
    public TimeLogsViewModel(MainWindowViewModel main)
    {
        Main = main;

        using var context = new TimeTrackingContext();

        var projects = (from p in context.Projects.Include(p => p.Tasks).ThenInclude(task => task.TimeLogs) select p);
        ProjectItems = new ObservableCollection<Project>(projects);
    }

    public MainWindowViewModel Main { get; }

    public ObservableCollection<Project> ProjectItems { get; set; }
}
