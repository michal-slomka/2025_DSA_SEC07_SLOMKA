using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using System.Linq;

namespace GetStartedApp.ViewModels
{
    public partial class SecondViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; }

        public SecondViewModel()
        {
            GoBackCommand = new RelayCommand(OpenMainWindow);
        }

        private void OpenMainWindow()
{
    if (App.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime lifetime)
    {
        var currentWindow = lifetime.Windows.FirstOrDefault(w => w.IsActive);

        var mainWindow = new Views.MainWindow
        {
            DataContext = new MainWindowViewModel()
        };
        mainWindow.Show();

        currentWindow?.Close();
    }
}

    }
}
