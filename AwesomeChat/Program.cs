using Avalonia;
using Avalonia.ReactiveUI;
using System;
using AwesomeDesktopChat.Config;
using AwesomeDesktopChat.Database;
using SimpleInjector;

namespace AwesomeChat;

class Program
{
    public static Container diContainer;
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        const string envPath = "/home/daniil/RiderProjects/awesome-chat/AwesomeChat/AwesomeChat/.env";
        DotEnv.Load(envPath);
        diContainer = new Container();
        diContainer.Register<Database>(Lifestyle.Singleton);
        diContainer.Verify();
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}