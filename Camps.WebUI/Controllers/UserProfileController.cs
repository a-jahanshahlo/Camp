using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
using AutoMapper;
using Camps.CommonLib.ExtentionMethods;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.User;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.AppServices;
using Comps.ServiceLayer.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camps.WebUI.Controllers
{
    public class UserProfileController : ApiController
    {
        private readonly ApplicationUser _iUser;
        private IUnitOfWork _unitOfWork;

        public UserProfileController( IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current.Cache["usermy"] == null)
            {
              _iUser = new ApplicationUser
            {
                UserName = "Test UserName", 
                Email = "a.jahanshahlo@gmail.com",
                UserInfo = new UserInfo()
                {
                    Phone = "123456",
                    LastName = "Jahansahlo",
                    FirstName = "علیرضا"
                   
                }
                ,
                PersonalSetting = new PersonalSetting() 
            };
                HttpContext.Current.Cache["usermy"] = _iUser;
            }
            _iUser = (ApplicationUser)HttpContext.Current.Cache["usermy"];
            //_appSettingService.Insert(new AppSetting() { Id = 1, DesktopImagePath = "/myPath/youpath" });

            //_appSettingService.Save();
        
            _unitOfWork = unitOfWork;
        }
        // GET api/Camps
         [HttpGet, ActionName("GetCurrentUser")]
        public HttpResponseMessage GetCurrentUser()
        {
            UserViewModel userViewModel = Mapper.Map<ApplicationUser, UserViewModel>(_iUser);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, userViewModel);

             return response;
        }
        [HttpGet, ActionName("GetBackgroundImage")]
        public HttpResponseMessage GetBackgroundImage()
        {
           ////    var findByIdAsync =  _userStore.FindByNameAsync(User.Identity.Name);
            //findByIdAsync.Result.PersonalSetting.DesktopImage
           // UserManager<ApplicationUser> mm=new UserManager<ApplicationUser>()
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,  _iUser.PersonalSetting.DesktopImage);
            return response;
        }
        [HttpPost, ActionName("SetBackgroundImage")]
        public HttpResponseMessage SetBackgroundImage([FromBody]string imagePath)
        {
            _iUser.PersonalSetting.DesktopImage = imagePath.Replace("thumb/","");
            ////    var findByIdAsync =  _userStore.FindByNameAsync(User.Identity.Name);
            //findByIdAsync.Result.PersonalSetting.DesktopImage
            // UserManager<ApplicationUser> mm=new UserManager<ApplicationUser>()
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, _iUser.PersonalSetting.DesktopImage);
            return response;
        }
    }
}
