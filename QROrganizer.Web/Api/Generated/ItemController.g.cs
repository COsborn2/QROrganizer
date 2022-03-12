
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
    [Route("api/Item")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class ItemController
        : BaseApiController<QROrganizer.Data.Models.Item, ItemDtoGen, QROrganizer.Data.AppDbContext>
    {
        public ItemController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.Item>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<ItemDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.Item> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<ItemDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.Item> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.Item> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize]
        public virtual Task<ItemResult<ItemDtoGen>> Save(
            ItemDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.Item> dataSource,
            IBehaviors<QROrganizer.Data.Models.Item> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<ItemDtoGen>> Delete(
            int id,
            IBehaviors<QROrganizer.Data.Models.Item> behaviors,
            IDataSource<QROrganizer.Data.Models.Item> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
