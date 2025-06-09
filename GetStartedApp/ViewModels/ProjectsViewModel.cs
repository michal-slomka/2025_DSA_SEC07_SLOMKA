using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class ProjectsViewModel : ViewModelBase
{
    private ObservableCollection<ProjectItem> _projects = [];
    [ObservableProperty] private ObservableCollection<ProjectItem> _filteredProjects = [];

    public MainWindowViewModel Main { get; }

    public string Filter { get; set; } = string.Empty;

    public ProjectsViewModel(MainWindowViewModel main)
    {
        Main = main;
    }

    [RelayCommand]
    private void FilterProjects()
    {
        FilteredProjects =
            new ObservableCollection<ProjectItem>(_projects.Where(project =>
                project.Name.StartsWith(Filter, StringComparison.CurrentCultureIgnoreCase)));
    }

    public void LoadProjects()
    {
        using var context = new TimeTrackingContext();

        // all projects
        var adminProjects = from p in context.Projects select p;
        // projects whose manager is the current user
        var projectManagerProjects =
            from p in context.Projects
            where p.ManagerId == Main.CurrentUserId
            select p;
        // projects in which the current user has a task assigned
        var employeeProjects =
            from p in context.Projects
            join t in context.Tasks on p.ProjectId equals t.ProjectId
            where Main.CurrentUserId == t.AssignedEmployeeId
            select p;

        var projects = Main.CurrentUserType switch
        {
            "admin" => adminProjects,
            "project_manager" => projectManagerProjects,
            "employee" => employeeProjects.Distinct(),
            _ => throw new Exception("Invalid user type")
        };

        var projectItems = projects.Select(p => new ProjectItem
        {
            Name = p.Name,
            Manager = $"Manager: {p.Manager.Name}",
            Description = p.Description,
            Tasks =
                new ObservableCollection<string>(
                    p.Tasks.AsEnumerable()
                        .Select(t => $"{t.Name} | {t.AssignedEmployee.Name}")
                )
        });

        _projects = new ObservableCollection<ProjectItem>(projectItems);
        FilterProjects();
    }

    public void Reset()
    {
        Filter = string.Empty;
    }

    [RelayCommand]
    private void OpenCreateProject()
    {
        Main.CurrentPage = new CreateProjectViewModel(Main);
    }
}
