
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
    [Route("api/UserService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class UserServiceController : Controller
    {
        protected QROrganizer.Data.Services.UserService Service { get; }

        public UserServiceController(QROrganizer.Data.Services.UserService service)
        {
            Service = service;
        }

        /// <summary>
        /// Method: UserInfo
        /// </summary>
        [HttpPost("UserInfo")]
        [Authorize]
        public virtual ItemResult<UserInfoDtoGen> UserInfo()
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(User);
            var _methodResult = Service.UserInfo(User);
            var _result = new ItemResult<UserInfoDtoGen>();
            _result.Object = Mapper.MapToDto<QROrganizer.Data.Services.UserInfo, UserInfoDtoGen>(_methodResult, _mappingContext, includeTree);
            return _result;
        }
    }
}
