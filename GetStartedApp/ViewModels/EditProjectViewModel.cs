using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class EditProjectViewModel : ViewModelBase
{
    public ObservableCollection<Project> AvailableProjects { get; }
    public ObservableCollection<User> AvailableManagers { get; }
    [ObservableProperty] private string _notification = string.Empty;
    public Project? SelectedProject
    {
        get;
        set
        {
            NewName = value?.Name ?? string.Empty;
            NewDescription = value?.Description ?? string.Empty;
            SelectedManager = value?.Manager;
            SetProperty(ref field, value);
        }
    }

    [ObservableProperty] private string _newName = string.Empty;
    [ObservableProperty] private string _newDescription = string.Empty;
    [ObservableProperty] private User? _selectedManager;

    private readonly MainWindowViewModel _main;

    public EditProjectViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        AvailableProjects = new ObservableCollection<Project>(context.Projects);
        // janky views again
        AvailableManagers =
            new ObservableCollection<User>(from u in context.Users where u.Type == "project_manager" select u);
    }

    [RelayCommand]
    private void Save()
    {
        if (SelectedProject is null)
        {
            Console.WriteLine("No project selected");
            return;
        }

         if (string.IsNullOrWhiteSpace(NewName))
        {
           Console.WriteLine($"The project must have a name/description");
            return;
        }

        if (SelectedManager is null)
        {
            Console.WriteLine("No manager selected");
            return;
        }

        using var context = new TimeTrackingContext();

        var project = context.Projects.First(p => p.ProjectId == SelectedProject.ProjectId);

        project.Name = NewName;
        project.Description = NewDescription;
        project.ManagerId = SelectedManager.UserId;

        context.Projects.Update(project);
        context.SaveChanges();
        Notification = "Project edited successfully!"; 
        // _main.ShowProjects();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowProjects();
    }
}
