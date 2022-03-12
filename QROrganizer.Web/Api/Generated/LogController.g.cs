
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
    [Route("api/Log")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class LogController
        : BaseApiController<QROrganizer.Data.Models.Log, LogDtoGen, QROrganizer.Data.AppDbContext>
    {
        public LogController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.Log>();
        }

        [HttpGet("get/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<LogDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.Log> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ListResult<LogDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.Log> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.Log> dataSource)
            => CountImplementation(parameters, dataSource);
    }
}
