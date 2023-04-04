using System;
using AwesomeChat.ViewModels;
using AwesomeChat.Views;
using AwesomeChat.Views.Pages;
using AwesomeChat.Views.Windows;
using ReactiveUI;

namespace AwesomeChat;

public class AppViewLocator : IViewLocator
{
    public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
    {
        AuthWindowViewModel context => new AuthScreen { DataContext = context },
        MainWindowViewModel context => new MainWindow { DataContext = context },
        UsersPageViewModel context => new UsersPage { DataContext = context },
        MessagesPageViewModel context => new MessagesPage { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel)),
    };
}