using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Camps.CommonLib.ExtentionMethods;

using Comps.DomainLayer;

namespace Comps.ServiceLayer.AppServices
{
    public class AppSettingService : XmlRepo<AppSetting>, IAppSettingService
    {
 
        public AppSettingService(AppSetting app)
            : base(app)
        {
           
            ThisObj = app;
        }
    }

}