using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QROrganizer.Data.Models;

public class SubscriptionFeature
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ICollection<SubscriptionLevel> SubscriptionLevels { get; set; }
}
