using System;
using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using AwesomeDesktopChat.Database;
using Postgrest;
using ReactiveUI;
using Supabase.Gotrue;

namespace AwesomeChat.ViewModels;

public class UsersPageViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public Database Database { get; set; }
    
    public IEnumerable<AwesomeDesktopChat.Models.User> Table { get; set; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public UsersPageViewModel(IScreen screen)
    {
        HostScreen = screen;
        var database = Program.diContainer.GetInstance<Database>();
        this.Database = database;
    }
    
    public UsersPageViewModel()
    {
    }

    private List<User> _users;

    public void Load()
    {
        var database = Program.diContainer.GetInstance<Database>();
        database.LoadUsers();
    }
}