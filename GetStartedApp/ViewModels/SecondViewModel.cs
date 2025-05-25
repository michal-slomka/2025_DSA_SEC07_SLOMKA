namespace GetStartedApp.ViewModels;

public class SecondViewModel(string username, string password) : ViewModelBase
{
    public string Username { get; } = $"Username: {username}";
    public string Password { get; } = $"Password: {password}";
}