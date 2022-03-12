using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QROrganizer.Web.Models
{
    public partial class LogDtoGen : GeneratedDto<QROrganizer.Data.Models.Log>
    {
        public LogDtoGen() { }

        private int? _Id;
        private string _Application;
        private string _Date;
        private string _Level;
        private string _Message;
        private string _CallSite;
        private string _Exception;
        private string _User;
        private string _Url;
        private string _UrlReferrer;
        private string _Browser;

        public int? Id
        {
            get => _Id;
            set { _Id = value; Changed(nameof(Id)); }
        }
        public string Application
        {
            get => _Application;
            set { _Application = value; Changed(nameof(Application)); }
        }
        public string Date
        {
            get => _Date;
            set { _Date = value; Changed(nameof(Date)); }
        }
        public string Level
        {
            get => _Level;
            set { _Level = value; Changed(nameof(Level)); }
        }
        public string Message
        {
            get => _Message;
            set { _Message = value; Changed(nameof(Message)); }
        }
        public string CallSite
        {
            get => _CallSite;
            set { _CallSite = value; Changed(nameof(CallSite)); }
        }
        public string Exception
        {
            get => _Exception;
            set { _Exception = value; Changed(nameof(Exception)); }
        }
        public string User
        {
            get => _User;
            set { _User = value; Changed(nameof(User)); }
        }
        public string Url
        {
            get => _Url;
            set { _Url = value; Changed(nameof(Url)); }
        }
        public string UrlReferrer
        {
            get => _UrlReferrer;
            set { _UrlReferrer = value; Changed(nameof(UrlReferrer)); }
        }
        public string Browser
        {
            get => _Browser;
            set { _Browser = value; Changed(nameof(Browser)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(QROrganizer.Data.Models.Log obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.Id = obj.Id;
            this.Application = obj.Application;
            this.Date = obj.Date;
            this.Level = obj.Level;
            this.Message = obj.Message;
            this.CallSite = obj.CallSite;
            this.Exception = obj.Exception;
            this.User = obj.User;
            this.Url = obj.Url;
            this.UrlReferrer = obj.UrlReferrer;
            this.Browser = obj.Browser;
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(QROrganizer.Data.Models.Log entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Id))) entity.Id = (Id ?? entity.Id);
            if (ShouldMapTo(nameof(Application))) entity.Application = Application;
            if (ShouldMapTo(nameof(Date))) entity.Date = Date;
            if (ShouldMapTo(nameof(Level))) entity.Level = Level;
            if (ShouldMapTo(nameof(Message))) entity.Message = Message;
            if (ShouldMapTo(nameof(CallSite))) entity.CallSite = CallSite;
            if (ShouldMapTo(nameof(Exception))) entity.Exception = Exception;
            if (ShouldMapTo(nameof(User))) entity.User = User;
            if (ShouldMapTo(nameof(Url))) entity.Url = Url;
            if (ShouldMapTo(nameof(UrlReferrer))) entity.UrlReferrer = UrlReferrer;
            if (ShouldMapTo(nameof(Browser))) entity.Browser = Browser;
        }
    }
}
