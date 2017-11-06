using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using StructureMap;

namespace Camps.WebUI.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceActivator : IHttpControllerActivator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ServiceActivator(HttpConfiguration configuration) { }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = ObjectFactory.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}