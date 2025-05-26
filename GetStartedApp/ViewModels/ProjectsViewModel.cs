using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels
{
    public partial class ProjectsViewModel : ViewModelBase
    {
        public ObservableCollection<ProjectItem> Projects { get; private set; } = [];

        public MainWindowViewModel Main { get; }

        public IRelayCommand OpenCreateProjectCommand { get; }

        public ProjectsViewModel(MainWindowViewModel main)
        {
            Main = main;
            OpenCreateProjectCommand = new RelayCommand(OpenCreateProject);
            LoadProjects();
        }

        private void LoadProjects()
        {
            using var context = new TimeTrackingContext();

            var projects = from p in context.Projects select p;

            var projectItems = projects.Select(p => new ProjectItem
                { Name = p.Name, Description = p.Description, AssignedUsers = new ObservableCollection<string>() });

            Projects = new ObservableCollection<ProjectItem>(projectItems);
        }

        private void OpenCreateProject()
        {
            Main.CurrentPage = new CreateProjectViewModel(Main);
        }
    }
}