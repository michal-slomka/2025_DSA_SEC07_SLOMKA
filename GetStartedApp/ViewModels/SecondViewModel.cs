namespace GetStartedApp.ViewModels;

public class SecondViewModel : ViewModelBase
{
    public SecondViewModel(string username, string password)
    {
        Username = $"Username: {username}";
        Password = $"Password: {password}";
    }

    public string Username { get; }
    public string Password { get; }
}