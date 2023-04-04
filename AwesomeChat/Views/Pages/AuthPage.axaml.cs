using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AwesomeChat.ViewModels;

namespace AwesomeChat.Views;

public partial class AuthScreen : ReactiveUserControl<AuthWindowViewModel>
{
    public AuthScreen()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}