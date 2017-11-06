using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Camps.WebUI.ViewModels.AppSettings;
using Camps.WebUI.ViewModels.User;
using Comps.DomainLayer;
using Comps.ServiceLayer.AppServices;
using Comps.ServiceLayer.IOServices;

namespace Camps.WebUI.Controllers
{
    public class AppSettingsController : ApiController
    {
        private readonly IAppSettingService _appSettingService;
        private readonly IInOutBinaryService _inOutBinaryService;
        public AppSettingsController(IAppSettingService appSettingService, IInOutBinaryService inOutBinaryService)
        {
            _appSettingService = appSettingService;
            _inOutBinaryService = inOutBinaryService;
        }

        [HttpGet]
        public HttpResponseMessage GetAppSettings()
        {
            AppSetting s = _appSettingService.Get();

            // _appSettingService.Save(s);
            AppSettingsViewModel appSettingsViewModel = Mapper.Map<AppSetting, AppSettingsViewModel>(s);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, appSettingsViewModel);
            return response;
        }
       [HttpGet]
        public HttpResponseMessage GetBackgroundImages()
        {
            var exts = new[] { "jpeg", "jpg", "png", "gif" };
           AppSetting appSetting = _appSettingService.Get();
           var files = _inOutBinaryService
                 .GetFiles(appSetting.DesktopThumbImagePath, "*.*", SearchOption.TopDirectoryOnly)
                 .Where(file => exts.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)));



            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, files);

            return response;

        }
       //[HttpGet]
       //public HttpResponseMessage GetDesktopImage()
       //{
       //    AppSetting appSetting = _appSettingService.Get();
       //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, appSetting.DefaultDesktopImage);
       //    return response;
       //}
       //[HttpPost]
       //public HttpResponseMessage SetDesktopImage(string path)
       //{
       //    AppSetting appSetting = _appSettingService.Get();
       //    appSetting.DefaultDesktopImage = path;

       //    _appSettingService.Save(appSetting);
       //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
       //    return response;
       //}

    }
}
