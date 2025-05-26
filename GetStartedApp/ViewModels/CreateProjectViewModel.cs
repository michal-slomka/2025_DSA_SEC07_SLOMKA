using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class CreateProjectViewModel : ViewModelBase
{
    [ObservableProperty] private string _projectName = string.Empty;
    [ObservableProperty] private string _projectDescription = string.Empty;
    [ObservableProperty] private string _newAssignee = string.Empty;

    /// <summary>
    /// The list you’re building up in the “Add” UI.
    /// </summary>
    public ObservableCollection<string> AssignedUsers { get; } = new();

    private readonly MainWindowViewModel _main;

    public CreateProjectViewModel(MainWindowViewModel main)
    {
        _main = main ?? throw new ArgumentNullException(nameof(main));
        AddAssigneeCommand = new RelayCommand(AddAssignee);
        SaveCommand = new RelayCommand(Save);
        CancelCommand = new RelayCommand(GoBack);
    }

    public IRelayCommand AddAssigneeCommand { get; }
    public IRelayCommand SaveCommand { get; }
    public IRelayCommand CancelCommand { get; }

    private void AddAssignee()
    {
        var name = NewAssignee.Trim();
        if (!string.IsNullOrWhiteSpace(name) && !AssignedUsers.Contains(name))
        {
            AssignedUsers.Add(name);
            NewAssignee = string.Empty;
        }
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(ProjectName))
        {
            return;
        }

        // 1) Auto-add whatever is still in the textbox
        var last = NewAssignee.Trim();
        if (!string.IsNullOrWhiteSpace(last) && !AssignedUsers.Contains(last))
        {
            AssignedUsers.Add(last);
        }

        // 2) Build the ProjectItem from your full list
        var p = new ProjectItem
        {
            Name = ProjectName.Trim(),
            Description = ProjectDescription.Trim(),
            AssignedUsers = new ObservableCollection<string>(AssignedUsers)
        };

        // 3) Drop it into the master list and go back
        _main.ProjectsView ??= new ProjectsViewModel(_main);
        _main.ProjectsView.Projects.Add(p);

        // 4) Clear out the form for next time
        ProjectName = string.Empty;
        ProjectDescription = string.Empty;
        AssignedUsers.Clear();
        NewAssignee = string.Empty;

        _main.CurrentPage = _main.ProjectsView;
    }

    private void GoBack()
    {
        _main.CurrentPage = _main.ProjectsView!;
    }
}
