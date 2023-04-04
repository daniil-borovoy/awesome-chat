using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace AwesomeChat.Views.Windows;

public partial class MainWindow : Window, IViewFor
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public object? ViewModel { get; set; }

    private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        var childWindow = new AboutWindow();
        childWindow.ShowDialog(this);
    }
}