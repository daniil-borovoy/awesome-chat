using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AwesomeChat.ViewModels;

namespace AwesomeChat.Views.Pages;

public partial class UsersPage : ReactiveUserControl<UsersPageViewModel>
{
    public UsersPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}