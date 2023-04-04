using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace AwesomeChat.ViewModels;

public class AuthWindowViewModel : ViewModelBase, IRoutableViewModel
{
    
    // Reference to IScreen that owns the routable view model.
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public AuthWindowViewModel(IScreen screen) => HostScreen = screen;

    public AuthWindowViewModel()
    {
        
    }
    public string Greeting => "Welcome to AwesomeChat!";

    public string GreetingSubtitle => "Sign up first";

    public string SignUp => "Sign up";

    private string _email;

    private string _password;

    private string _name;

    private string _surname;

    [Required]
    [EmailAddress]
    public string? Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    [Required(ErrorMessage = "Password is required")]
    [PasswordPropertyText]
    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public string? Name { get; set; }
    
    public string? Surname { get; set; }

    public void OnSignUpClicked()
    {
        
    }
}