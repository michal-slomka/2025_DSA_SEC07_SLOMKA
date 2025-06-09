using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class EditTaskViewModel : ViewModelBase
{
    public ObservableCollection<Task> AvailableTasks { get; }

    public Task? SelectedTask
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

    public EditTaskViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        AvailableTasks = new ObservableCollection<Task>(context.Tasks);
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

        SelectedTask.Name = NewName;
        SelectedTask.Description = NewDescription;
        
        context.Tasks.Update(SelectedTask);
        context.SaveChanges();
        
        _main.ShowTasks();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowTasks();
    }
}
