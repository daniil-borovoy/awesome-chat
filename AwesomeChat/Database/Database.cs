using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Authentication;
using System.Threading.Tasks;
using AwesomeDesktopChat.Models;
using JetBrains.Annotations;
using Supabase.Realtime.PostgresChanges;
using Client = Supabase.Client;
using User = AwesomeDesktopChat.Models.User;

namespace AwesomeDesktopChat.Database;

public class Database : INotifyPropertyChanged
{
    private readonly Client _client;
    
    public Database()
    {
        // В конструкторе создаем массив с пользователями и сообщениями,
        // в котором будут храниться все строки из таблицы
        Table = new List<User>();
        MessagesTable = new List<Message>();

        // Подключаемся к базе данных
        var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true,
        };
        _client = new Client(url, key, options);
        _client.InitializeAsync().Wait();
        
        // И подписываемся на события изменения в базе данных
        var table = _client.From<User>();
        table.On(Client.ChannelEventType.All, UsersChanged);
        _client.From<Message>().On(Client.ChannelEventType.All, MessagesChanged);
    }

    // Клиент для обращения к базе данных
    private Client Client { get; }

    public IEnumerable<User> Table { get; set; }
    public IEnumerable<Message> MessagesTable { get; set; }

    // Событие изменения массива для обновления интерфейса
    public event PropertyChangedEventHandler? PropertyChanged;

    // При изменении данных в талице на сервере просто подгружаем данные из нее
    private void UsersChanged(object sender, PostgresChangesEventArgs a)
    {
        LoadUsers();
    }
    
    private void MessagesChanged(object sender, PostgresChangesEventArgs e)
    {
        LoadMessages();
    }

    // А вот так просходит загрузка данных из таблицы
    // на сервере Supabase в массив нашей программы
    public async void LoadUsers()
    {
        // Берем данные из таблицы и помещаем их в массив
        var data = await _client.From<User>().Get();
        Table = data.Models;
        
        // Вызов этой функции необходим для автоматического обновления
        // интерфейса программы при изменении данных в массиве со студентами
        OnPropertyChanged(nameof(Table));
    }

    public async void LoadMessages()
    {
        var messagesData = await _client.From<Message>().Get();
        MessagesTable = messagesData.Models;
        
        OnPropertyChanged(nameof(MessagesTable));
    }

    // Реализация интерфейса INotifyPropertyChanged необходима для обновления формы программы
    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task SignUp(string email, string password)
    {
        // TODO: save user session
        var session = await _client.Auth.SignUp(email, password);
    }

    public Task GetAllUsers()
    {
        var accessToken = _client.Auth.CurrentSession?.AccessToken;
        if (accessToken is null)
        {
            throw new AuthenticationException();
        }
        return _client.Auth.ListUsers(accessToken);
    }

    public bool HasSession()
    {
        _client.Auth.RefreshSession();
        return false;
    }

    public async Task SendMessage(string message)
    {
        var newMessage = new Message()
        {
            Content = message,
            SenderId = 1
        };
        await _client.From<Message>().Insert(newMessage);
    }
}