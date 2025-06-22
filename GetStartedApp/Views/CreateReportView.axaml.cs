using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;

namespace GetStartedApp.Views;

public partial class CreateReportView : UserControl
{
    public CreateReportView()
    {
        InitializeComponent();
    }

    public async void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as ViewModels.CreateReportViewModel;
        if (vm == null) return;
        var dialog = new SaveFileDialog
        {
            Title = "Zapisz raport jako...",
            Filters = { new FileDialogFilter { Name = "CSV", Extensions = { "csv" } } },
            InitialFileName = $"Report_{vm.Title}.csv"
        };
        var window = this.VisualRoot as Window;
        var path = await dialog.ShowAsync(window);
        if (!string.IsNullOrWhiteSpace(path))
        {
            vm.SaveToPath(path);
        }
    }
}
