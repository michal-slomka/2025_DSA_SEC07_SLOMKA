using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class EditProjectViewModel : ViewModelBase
{
    public ObservableCollection<Project> AvailableProjects { get; }

    public Project? SelectedProject
    {
        get;
        set
        {
            NewName = value?.Name ?? string.Empty;
            NewDescription = value?.Description ?? string.Empty;
            SetProperty(ref field, value);
        }
    }

    [ObservableProperty] private string _newName = string.Empty;
    [ObservableProperty] private string _newDescription = string.Empty;

    private readonly MainWindowViewModel _main;

    public EditProjectViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        AvailableProjects = new ObservableCollection<Project>(context.Projects);
    }

    [RelayCommand]
    private void Save()
    {
        if (SelectedProject is null)
        {
            Console.WriteLine("No project selected");
            return;
        }

        using var context = new TimeTrackingContext();

        SelectedProject.Name = NewName;
        SelectedProject.Description = NewDescription;
        
        context.Projects.Update(SelectedProject);
        context.SaveChanges();
        
        _main.ShowProjects();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowProjects();
    }
}
