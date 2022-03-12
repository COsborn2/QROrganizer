
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
    [Route("api/Container")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class ContainerController
        : BaseApiController<QROrganizer.Data.Models.Container, ContainerDtoGen, QROrganizer.Data.AppDbContext>
    {
        public ContainerController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.Container>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<ContainerDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.Container> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<ContainerDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.Container> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.Container> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize]
        public virtual Task<ItemResult<ContainerDtoGen>> Save(
            ContainerDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.Container> dataSource,
            IBehaviors<QROrganizer.Data.Models.Container> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<ContainerDtoGen>> Delete(
            int id,
            IBehaviors<QROrganizer.Data.Models.Container> behaviors,
            IDataSource<QROrganizer.Data.Models.Container> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
