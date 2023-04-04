using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AwesomeChat.ViewModels;

namespace AwesomeChat.Views.Pages;

public partial class MessagesPage : ReactiveUserControl<MessagesPageViewModel>
{
    private TextBox _messageInput;
    public MessagesPage()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        _messageInput = this.FindControl<TextBox>("MessageInput");
    }

    private void MessageInput_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        ViewModel?.SendMessage();
        _messageInput.Clear();
    }
}