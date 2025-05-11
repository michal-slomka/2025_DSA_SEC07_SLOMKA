using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private const string DEFAULT_GREETING = "Hello World from Avalonia.Samples";

    public string Username
    {
        get => field;
        set => SetProperty(ref field, value);
    }
    public string Password
    {
        get => field;
        set => SetProperty(ref field, value);
    }
    public MainWindowViewModel()
    {
        ButtonClickedCommand = new RelayCommand(ButtonClicked);
    }

    public string Name
    {
        get;
        set
        {
            SetProperty(ref field, value);
            Greeting = value;
        }
    } = "";

    public string Greeting
    {
        get;
        set
        {
            SetProperty(ref field, value == "" ? DEFAULT_GREETING : $"Hello {value}");
        }
    } = DEFAULT_GREETING;

    public ObservableCollection<string> Lines { get; } = [];

    public ICommand ButtonClickedCommand { get; }

    private void ButtonClicked()
    {
        // WARN: crashuje przy pustym lub nieunikatowym inpucie
        using (var context = new TimeTrackingContext())
        {
            var newUser = new User
            {
                Name = $"{Name}",
                Password = $"{Greeting}",
                Type = "admin",
            };

            context.Users.Add(newUser);
            context.SaveChanges();
        }
        Lines.Add(Greeting);
    }
}
