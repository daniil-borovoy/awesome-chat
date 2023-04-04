using System;
using AwesomeDesktopChat.Database;
using ReactiveUI;

namespace AwesomeChat.ViewModels;

public class MessagesPageViewModel : ViewModelBase, IRoutableViewModel
{
    
    public string MessageContent { get; set; }
    public IScreen HostScreen { get; }
    
    public Database Database { get; set; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public MessagesPageViewModel(IScreen screen)
    {
        HostScreen = screen;
        Database = Program.diContainer.GetInstance<Database>();
        Database.LoadMessages();
    }

    public MessagesPageViewModel()
    {
        Database = Program.diContainer.GetInstance<Database>();
        Database.LoadMessages();
    }

    public async void SendMessage()
    {
        await Database.SendMessage(MessageContent);
    }
}