using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class LogTimeViewModel : ViewModelBase
{
    public ObservableCollection<Task> AvailableTasks { get; }

    public Task? SelectedTask
    {
        get;
        set => SetProperty(ref field, value);
    }

    [ObservableProperty] private DateTimeOffset _date = DateTimeOffset.Now;
    [ObservableProperty] private decimal _time = 0.0m;

    public LogTimeViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        var availableTasks = from t in context.Tasks where t.AssignedEmployeeId == _main.CurrentUserId select t;

        AvailableTasks = new ObservableCollection<Task>(availableTasks);
    }

    private readonly MainWindowViewModel _main;

    [RelayCommand]
    private void Save()
    {
        if (SelectedTask is null)
        {
            Console.WriteLine("Task not selected");
            return;
        }
        // if (Date is null)
        // {
        //     Console.WriteLine("Date is empty");
        //     return;
        // }
        // if (Time is null)
        // {
        //     Console.WriteLine("Time is empty");
        //     return;
        // }

        using var context = new TimeTrackingContext();

        var timeLog = new TimeLog
        {
            TaskId = SelectedTask!.TaskId,
            Date = Date.DateTime,
            TimeSpent = new TimeSpan(0, (int)Time, 0),
            IsApproved = 0
        };

        context.TimeLogs.Add(timeLog);
        context.SaveChanges();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowTasks();
    }
}
