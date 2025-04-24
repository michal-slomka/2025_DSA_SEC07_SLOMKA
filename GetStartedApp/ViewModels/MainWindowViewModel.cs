using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace GetStartedApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private const string DEFAULT_GREETING = "Hello World from Avalonia.Samples";

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
        Lines.Add(Greeting);
    }
}
