using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntelliTect.Coalesce.DataAnnotations;

namespace QROrganizer.Data.Models;

[Create(Roles = Roles.Admin)]
[Delete(Roles = Roles.Admin)]
[Edit(Roles = Roles.Admin)]
[Read(Roles = Roles.Admin)]
public class SubscriptionFeature
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ICollection<SubscriptionLevel> SubscriptionLevels { get; set; }
}
