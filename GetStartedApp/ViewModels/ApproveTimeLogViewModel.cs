using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class ApproveTimeLogViewModel : ViewModelBase
{
    public ApproveTimeLogViewModel(MainWindowViewModel main)
    {
        _main = main;

        using var context = new TimeTrackingContext();

        AvailableTasks = new ObservableCollection<Task>(context.Tasks);
    }

    public ObservableCollection<Task> AvailableTasks { get; }

    [ObservableProperty] private ObservableCollection<TimeLog> _availableTimeLogs;

    [ObservableProperty] private bool _isApproved;

    private readonly MainWindowViewModel _main;

    public Task? SelectedTask
    {
        get;
        set
        {
            using var context = new TimeTrackingContext();

            var timeLogs = from tl in context.TimeLogs
                where value.TaskId == tl.TaskId
                select tl;
            AvailableTimeLogs =
                new ObservableCollection<TimeLog>(timeLogs);

            SetProperty(ref field, value);
        }
    }

    public TimeLog SelectedTimeLog
    {
        get;
        set
        {
            IsApproved = value.IsApproved != 0;
            SetProperty(ref field, value);
        }
    }

    [RelayCommand]
    private void Save()
    {
        using var context = new TimeTrackingContext();

        SelectedTimeLog.IsApproved = (sbyte)(IsApproved ? 1 : 0);

        context.TimeLogs.Update(SelectedTimeLog);
        context.SaveChanges();
    }

    [RelayCommand]
    private void Cancel()
    {
        _main.ShowTimeLogs();
    }
}
