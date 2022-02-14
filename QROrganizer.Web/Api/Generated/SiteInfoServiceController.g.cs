
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
    [Route("api/SiteInfoService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class SiteInfoServiceController : Controller
    {
        protected QROrganizer.Data.Services.Interface.ISiteInfoService Service { get; }

        public SiteInfoServiceController(QROrganizer.Data.Services.Interface.ISiteInfoService service)
        {
            Service = service;
        }

        /// <summary>
        /// Method: GetSiteInfo
        /// </summary>
        [HttpPost("GetSiteInfo")]
        [AllowAnonymous]
        public virtual ItemResult<SiteInfoDtoDtoGen> GetSiteInfo()
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(User);
            var _methodResult = Service.GetSiteInfo();
            var _result = new ItemResult<SiteInfoDtoDtoGen>();
            _result.Object = Mapper.MapToDto<QROrganizer.Data.Util.SiteInfoDto, SiteInfoDtoDtoGen>(_methodResult, _mappingContext, includeTree);
            return _result;
        }
    }
}
