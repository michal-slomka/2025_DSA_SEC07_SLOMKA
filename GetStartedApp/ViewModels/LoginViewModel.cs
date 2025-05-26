using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetStartedApp.Models;

namespace GetStartedApp.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action<string, string, string>? LoginSucceeded;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    public ObservableCollection<User> Users { get; } = new();

    [RelayCommand]
    private void Login()
    {
        using var context = new TimeTrackingContext();

        // Szukamy użytkownika w bazie danych
        var matchingUser = context.Users
            .FirstOrDefault(u => u.Name == Username && u.Password == Password);

        if (matchingUser != null)
        {
            Console.WriteLine($"✅ Zalogowano jako: {matchingUser.Name}");

            // Dodajemy użytkownika do kolekcji (opcjonalnie)
            if (!Users.Any(u => u.Name == matchingUser.Name))
                Users.Add(matchingUser);

            LoginSucceeded?.Invoke(matchingUser.Name, matchingUser.Password, matchingUser.Type);
        }
        else
        {
            Console.WriteLine("❌ Nieprawidłowy login lub hasło");
        }
    }
}
