using System.ComponentModel.DataAnnotations.Schema;
using IntelliTect.Coalesce.DataAnnotations;

namespace QROrganizer.Data.Models;

[Create(PermissionLevel = SecurityPermissionLevels.DenyAll)]
[Delete(PermissionLevel = SecurityPermissionLevels.DenyAll)]
[Edit(PermissionLevel = SecurityPermissionLevels.DenyAll)]
[Read(Roles = Roles.Admin)]
public class Log
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("Application")]
    public string Application { get; set; }

    [Column("Date")]
    public string Date { get; set; }

    [Column("Level")]
    public string Level { get; set; }

    [Column("Message")]
    public string Message { get; set; }

    [Column("CallSite")]
    public string CallSite { get; set; }

    [Column("Exception")]
    public string Exception { get; set; }

    [Column("User")]
    public string User { get; set; }

    [Column("Url")]
    public string Url { get; set; }

    [Column("UrlReferrer")]
    public string UrlReferrer { get; set; }

    [Column("Browser")]
    public string Browser { get; set; }
}
