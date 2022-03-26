
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
    [Route("api/ItemBarcodeInformation")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class ItemBarcodeInformationController
        : BaseApiController<QROrganizer.Data.Models.ItemBarcodeInformation, ItemBarcodeInformationDtoGen, QROrganizer.Data.AppDbContext>
    {
        public ItemBarcodeInformationController(QROrganizer.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<QROrganizer.Data.Models.ItemBarcodeInformation>();
        }

        [HttpGet("get/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<ItemBarcodeInformationDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.ItemBarcodeInformation> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ListResult<ItemBarcodeInformationDtoGen>> List(
            ListParameters parameters,
            IDataSource<QROrganizer.Data.Models.ItemBarcodeInformation> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<QROrganizer.Data.Models.ItemBarcodeInformation> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<ItemBarcodeInformationDtoGen>> Save(
            ItemBarcodeInformationDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<QROrganizer.Data.Models.ItemBarcodeInformation> dataSource,
            IBehaviors<QROrganizer.Data.Models.ItemBarcodeInformation> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public virtual Task<ItemResult<ItemBarcodeInformationDtoGen>> Delete(
            int id,
            IBehaviors<QROrganizer.Data.Models.ItemBarcodeInformation> behaviors,
            IDataSource<QROrganizer.Data.Models.ItemBarcodeInformation> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
