using System.ComponentModel.DataAnnotations.Schema;

namespace QROrganizer.Data.Models;

public class SubscriptionLevel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string SubscriptionName { get; set; }

    public SubscriptionFeature SubscriptionFeature { get; set; }
}
