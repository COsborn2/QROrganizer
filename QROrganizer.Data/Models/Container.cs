using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.Utilities;

namespace QROrganizer.Data.Models;

public class Container
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DefaultOrderBy]
    public string ContainerName { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; }

    [InternalUse]
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    public ICollection<Item> Items { get; set; }

    [Coalesce]
    [DefaultDataSource]
    public class DefaultDataSource : StandardDataSource<Container, AppDbContext>
    {
        public DefaultDataSource(CrudContext<AppDbContext> context) : base(context)
        { }

        public override IQueryable<Container> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(x => User.GetUserId() == x.UserId);
        }
    }

    public class DefaultBehaviors : StandardBehaviors<Container, AppDbContext>
    {
        public DefaultBehaviors(CrudContext<AppDbContext> context) : base(context)
        {
        }

        public override ItemResult BeforeSave(SaveKind kind, Container oldItem, Container item)
        {
            if (User is null) return "Unauthorized";
            item.UserId = User.GetUserId();

            return true;
        }
    }
}
