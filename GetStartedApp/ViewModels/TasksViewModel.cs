using System.Collections.ObjectModel;
using System.Linq;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        public ObservableCollection<TaskItem> Tasks { get; private set; } = [];

        public MainWindowViewModel Main { get; }

        public TasksViewModel(MainWindowViewModel main)
        {
            Main = main;
            LoadTasks();
        }

        private void LoadTasks()
        {
            using var context = new TimeTrackingContext();

            // Filter tasks for the current user
            var currentUserId = Main.CurrentUserId;

            var taskItems = context.Tasks
                .Where(t => t.AssignedEmployeeId == currentUserId)
                .Select(t => new TaskItem
                {
                    Title = t.Name,
                    Description = t.Description,
                    Deadline = t.EndTime.HasValue ? t.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") : "No deadline",
                    Project = "",
                    TimeSpent = "",
                    AssignedTo = t.AssignedEmployee.Name,
                });

            Tasks = new ObservableCollection<TaskItem>(taskItems);
        }
    }
}