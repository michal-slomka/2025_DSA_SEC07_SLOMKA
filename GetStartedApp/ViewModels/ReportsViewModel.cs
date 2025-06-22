using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class ReportsViewModel : ViewModelBase
{
    public MainWindowViewModel Main { get; }

    public ObservableCollection<TaskItem> Reports { get; set; }

    public ReportsViewModel(MainWindowViewModel main)
    {
        Main = main;

        Reports =
        [
            new TaskItem
            {
                Title = "Weekly summary",
                Description = "Compiled overall team performance",
                Project = "Operations",
                TimeSpent = "2h 30m",
                Deadline = "13/05/25",
                AssignedTo = "Eva"
            },

            new TaskItem
            {
                Title = "Bug tracking report",
                Description = "List of all unresolved bugs",
                Project = "QA",
                TimeSpent = "1h",
                Deadline = "12/05/25",
                AssignedTo = "Liam"
            }
        ];
    }

    [RelayCommand]
    private void OpenCreateReport()
    {
        Main.CurrentPage = new CreateReportViewModel(this, () => Main.CurrentPage = this);
    }
}
