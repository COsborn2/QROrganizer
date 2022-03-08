using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Utilities;

namespace QROrganizer.Data.Models;

public class Item
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string BarcodeNumber { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    public int? ContainerId { get; set; }

    [ForeignKey(nameof(ContainerId))]
    public Container Container { get; set; }

    [Coalesce]
    [DefaultDataSource]
    public class DefaultDataSource : StandardDataSource<Item, AppDbContext>
    {
        public DefaultDataSource(CrudContext<AppDbContext> context) : base(context)
        { }

        public override IQueryable<Item> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(x => User.GetUserId() == x.UserId);
        }
    }

    [Coalesce]
    public class ItemsInContainer : DefaultDataSource
    {
        public ItemsInContainer(CrudContext<AppDbContext> context) : base(context)
        { }

        [Coalesce]
        public int ContainerId { get; set; }

        public override IQueryable<Item> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(x => x.ContainerId == ContainerId);
        }
    }
}
