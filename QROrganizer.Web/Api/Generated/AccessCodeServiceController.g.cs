
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
    [Route("api/AccessCodeService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class AccessCodeServiceController : Controller
    {
        protected QROrganizer.Data.Services.Interface.IAccessCodeService Service { get; }

        public AccessCodeServiceController(QROrganizer.Data.Services.Interface.IAccessCodeService service)
        {
            Service = service;
        }

        /// <summary>
        /// Method: IsAccessCodeValid
        /// </summary>
        [HttpPost("IsAccessCodeValid")]
        [AllowAnonymous]
        public virtual ItemResult<bool> IsAccessCodeValid(string code)
        {
            var _methodResult = Service.IsAccessCodeValid(code);
            var _result = new ItemResult<bool>();
            _result.Object = _methodResult;
            return _result;
        }
    }
}
