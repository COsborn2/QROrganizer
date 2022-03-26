
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Api;
using IntelliTect.Coalesce.Api.Controllers;
using IntelliTect.Coalesce.Api.DataSources;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Mapping.IncludeTrees;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.TypeDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QROrganizer.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QROrganizer.Web.Api
{
    [Route("api/SubscriptionLevel")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class SubscriptionLevelController
        : BaseApiController<QROrganizer.Data.Models.SubscriptionLevel, SubscriptionLevelDtoGen, QROrganizer.Data.AppDbContext>
    {
        public SubscriptionLevelController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.SubscriptionLevel>();
        }

        [HttpGet("get/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<SubscriptionLevelDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionLevel> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ListResult<SubscriptionLevelDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionLevel> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionLevel> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<SubscriptionLevelDtoGen>> Save(
            SubscriptionLevelDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionLevel> dataSource,
            IBehaviors<QROrganizer.Data.Models.SubscriptionLevel> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<SubscriptionLevelDtoGen>> Delete(
            int id,
            IBehaviors<QROrganizer.Data.Models.SubscriptionLevel> behaviors,
            IDataSource<QROrganizer.Data.Models.SubscriptionLevel> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
