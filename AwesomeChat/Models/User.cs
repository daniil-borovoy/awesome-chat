using Postgrest.Attributes;
using Postgrest.Models;

namespace AwesomeDesktopChat.Models;

[Table("users")]
public class User : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
    
    [Column("surname")]
    public string Surname { get; set; }

    [Column("age")]
    public int Age { get; set; }
}