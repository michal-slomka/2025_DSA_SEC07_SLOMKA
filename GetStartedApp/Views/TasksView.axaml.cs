using Avalonia.Controls;
using Avalonia.Input;

namespace GetStartedApp.Views;

public partial class TasksView : UserControl
{
    public TasksView()
    {
        InitializeComponent();
    }

    private void FilterTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        FilterButton.Command!.Execute(null);
    }
}
