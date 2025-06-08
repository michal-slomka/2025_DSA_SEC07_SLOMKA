using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class CreateProjectViewModel : ViewModelBase
{
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string? _selectedManager;
    public ObservableCollection<string> AvailableManagers { get; }

    private readonly MainWindowViewModel _main;

    public CreateProjectViewModel(MainWindowViewModel main)
    {
        using var context = new TimeTrackingContext();

        var availableManagers = from manager in context.ProjectManagers select manager.Name;
        AvailableManagers = new ObservableCollection<string>(availableManagers);

        _main = main ?? throw new ArgumentNullException(nameof(main));
    }

    [RelayCommand]
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            return;
        }

        using var context = new TimeTrackingContext();

        if (SelectedManager == null)
        {
            Console.WriteLine("Manager not selected");
            return;
        }

        var manager = context.ProjectManagers.First(manager => manager.Name == SelectedManager);
        var project = new Project
        {
            Name = Name,
            Description = Description,
            ManagerId = manager.ProjectManagerId
        };
        context.Projects.Add(project);
        context.SaveChanges();

        var p = new ProjectItem
        {
            Name = Name.Trim(),
            Description = Description.Trim(),
            Tasks = [] // TODO: ...
        };
        // TODO: load all(?) views on startup
        _main.ProjectsView ??= new ProjectsViewModel(_main);
        _main.ProjectsView.Projects.Add(p);

        Name = string.Empty;
        Description = string.Empty;

        _main.CurrentPage = _main.ProjectsView;
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.CurrentPage = _main.ProjectsView!;
    }
}
