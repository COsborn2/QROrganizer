
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
    [Route("api/ItemScanningService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class ItemScanningServiceController : Controller
    {
        protected QROrganizer.Data.Services.Interface.IItemScanningService Service { get; }

        public ItemScanningServiceController(QROrganizer.Data.Services.Interface.IItemScanningService service)
        {
            Service = service;
        }

        /// <summary>
        /// Method: CreateItemForUpcCodeAndStartSearch
        /// </summary>
        [HttpPost("CreateItemForUpcCodeAndStartSearch")]
        [Authorize]
        public virtual async Task<ItemResult<ItemDtoGen>> CreateItemForUpcCodeAndStartSearch(string upcCode, int containerId)
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(User);
            var _methodResult = await Service.CreateItemForUpcCodeAndStartSearch(User, upcCode, containerId);
            var _result = new ItemResult<ItemDtoGen>(_methodResult);
            _result.Object = Mapper.MapToDto<QROrganizer.Data.Models.Item, ItemDtoGen>(_methodResult.Object, _mappingContext, includeTree);
            return _result;
        }
    }
}
