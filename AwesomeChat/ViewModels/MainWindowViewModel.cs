using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Threading.Tasks;
using AwesomeChat.Views.Windows;
using AwesomeDesktopChat.Database;
using ReactiveUI;
namespace AwesomeChat.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = new();

    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoToUsersPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoToMessagesPage { get; }
    
    public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

    public MainWindowViewModel()
    {
        GoNext = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new AuthWindowViewModel(this))
        );
        
        GoToUsersPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new UsersPageViewModel(this))
        );
        
        GoToMessagesPage = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new MessagesPageViewModel(this))
        );
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

    public async Task OnSignUpClicked()
    {
        if (_email.Trim() == "" || _password.Trim() == "")
            return;
        var database = Program.diContainer.GetInstance<Database>();
        await database.SignUp(_email, _password);
        GoToUsersPage.Execute();
    }

    public void OnSkipClicked()
    {
        GoToMessagesPage.Execute();
    }
}