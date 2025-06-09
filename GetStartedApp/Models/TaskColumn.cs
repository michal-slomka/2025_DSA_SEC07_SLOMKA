using System.Collections.ObjectModel;

namespace GetStartedApp.Models;

public class TaskColumn
{
    public string Name { get; set; } = string.Empty;
    public ObservableCollection<TaskItem> Tasks { get; set; } = new();
}
