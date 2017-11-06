using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Camps.DataLayer.Context;
using Camps.WebUI.AutoMapper;
using Camps.WebUI.Base;
using Comps.DomainLayer;
 
using StructureMap;
using StructureMap.Web.Pipeline;
 

namespace Camps.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            //Handling Circular Object References in json/xml serialize
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling =
            //    Newtonsoft.Json.PreserveReferencesHandling.All;
            //var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            //var dcs = new DataContractSerializer(typeof(City), null, int.MaxValue,
            //    false, /* preserveObjectReferences: */ true, null);
            //xml.SetSerializer<City>(dcs);

 

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, Configuration>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ConfigurationUserAccount>());
            IoCBinder.InitStructureMap();

           
            AreaRegistration.RegisterAllAreas();
        
            GlobalConfiguration.Configure(WebApiConfig.Register);
           // WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          

           // var projectCategoryService = ObjectFactory.GetInstance<IProjectCategoryService>();

            AutoMapperWebConfiguration.ConfigureUserMapping();
         

           
        }
        /// <summary>
        /// 
        /// </summary>
        protected void Application_EndRequest()
        {
            //this code dispose all resource like DbContext etc.
            HttpContextLifecycle.DisposeAndClearAll();
        }

    }
}