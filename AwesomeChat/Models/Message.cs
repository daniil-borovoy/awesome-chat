using Postgrest.Attributes;
using Postgrest.Models;

namespace AwesomeDesktopChat.Models;

[Table("messages")]
public class Message : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("sender_id")]
    public int SenderId { get; set; }
    
    [Column("content")]
    public string Content { get; set; }
}