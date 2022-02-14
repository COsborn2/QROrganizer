
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
    [Route("api/RestrictedAccessCode")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class RestrictedAccessCodeController
        : BaseApiController<QROrganizer.Data.Models.RestrictedAccessCode, RestrictedAccessCodeDtoGen, QROrganizer.Data.AppDbContext>
    {
        public RestrictedAccessCodeController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.RestrictedAccessCode>();
        }

        [HttpGet("get/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<RestrictedAccessCodeDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.RestrictedAccessCode> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ListResult<RestrictedAccessCodeDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.RestrictedAccessCode> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.RestrictedAccessCode> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize]
        public virtual Task<ItemResult<RestrictedAccessCodeDtoGen>> Save(
            RestrictedAccessCodeDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.RestrictedAccessCode> dataSource,
            IBehaviors<QROrganizer.Data.Models.RestrictedAccessCode> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<RestrictedAccessCodeDtoGen>> Delete(
            int id,
            IBehaviors<QROrganizer.Data.Models.RestrictedAccessCode> behaviors,
            IDataSource<QROrganizer.Data.Models.RestrictedAccessCode> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);

        // Methods from data class exposed through API Controller.

        /// <summary>
        /// Method: CreateUnlimitedUseAccessCode
        /// </summary>
        [HttpPost("CreateUnlimitedUseAccessCode")]
        [Authorize(Roles = "ADMIN")]
        public virtual async Task<ItemResult> CreateUnlimitedUseAccessCode([FromServices] QROrganizer.Data.AppDbContext db)
        {
            await QROrganizer.Data.Models.RestrictedAccessCode.CreateUnlimitedUseAccessCode(db);
            var _result = new ItemResult();
            return _result;
        }

        /// <summary>
        /// Method: CreateAccessCode
        /// </summary>
        [HttpPost("CreateAccessCode")]
        [Authorize(Roles = "ADMIN")]
        public virtual async Task<ItemResult> CreateAccessCode([FromServices] QROrganizer.Data.AppDbContext db, int numberOfUses)
        {
            await QROrganizer.Data.Models.RestrictedAccessCode.CreateAccessCode(db, numberOfUses);
            var _result = new ItemResult();
            return _result;
        }
    }
}
