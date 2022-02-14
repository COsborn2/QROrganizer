
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
using QROrganizer.Data.Services.Models;

namespace QROrganizer.Web.Api
{
    [Route("api/UserInfoService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class UserInfoServiceController : Controller
    {
        protected QROrganizer.Data.Services.Implementation.UserInfoService Service { get; }

        public UserInfoServiceController(QROrganizer.Data.Services.Implementation.UserInfoService service)
        {
            Service = service;
        }

        /// <summary>
        /// Method: GetUserInfo
        /// </summary>
        [HttpPost("GetUserInfo")]
        [AllowAnonymous]
        public virtual ItemResult<UserInfoDtoGen> GetUserInfo()
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(User);
            var _methodResult = Service.GetUserInfo(User);
            var _result = new ItemResult<UserInfoDtoGen>();
            _result.Object = Mapper.MapToDto<UserInfo, UserInfoDtoGen>(_methodResult, _mappingContext, includeTree);
            return _result;
        }

        /// <summary>
        /// Method: ConfirmEmail
        /// </summary>
        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public virtual async Task<ItemResult> ConfirmEmail(string userId, string confirmationToken)
        {
            var _methodResult = await Service.ConfirmEmail(userId, confirmationToken);
            var _result = new ItemResult(_methodResult);
            return _result;
        }
    }
}
