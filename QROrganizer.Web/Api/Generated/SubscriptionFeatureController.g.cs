
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
    [Route("api/SubscriptionFeature")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class SubscriptionFeatureController
        : BaseApiController<QROrganizer.Data.Models.SubscriptionFeature, SubscriptionFeatureDtoGen, QROrganizer.Data.AppDbContext>
    {
        public SubscriptionFeatureController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.SubscriptionFeature>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<SubscriptionFeatureDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionFeature> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<SubscriptionFeatureDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionFeature> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionFeature> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize]
        public virtual Task<ItemResult<SubscriptionFeatureDtoGen>> Save(
            SubscriptionFeatureDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.SubscriptionFeature> dataSource,
            IBehaviors<QROrganizer.Data.Models.SubscriptionFeature> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<SubscriptionFeatureDtoGen>> Delete(
            int id,
            IBehaviors<QROrganizer.Data.Models.SubscriptionFeature> behaviors,
            IDataSource<QROrganizer.Data.Models.SubscriptionFeature> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
