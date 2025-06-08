using System.Collections.ObjectModel;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public class TimeLogsViewModel : ViewModelBase
{
    public TimeLogsViewModel(MainWindowViewModel main)
    {
        Main = main;

        Logs = new ObservableCollection<TimeLogItem>
        {
            new() { TaskName = "Fix login bug", Duration = "45m", Date = "2025-05-20", User = "Alice" },
            new() { TaskName = "Design onboarding", Duration = "1h 30m", Date = "2025-05-21", User = "Bob" }
        };
    }

    public MainWindowViewModel Main { get; }

    public ObservableCollection<TimeLogItem> Logs { get; set; }
}
