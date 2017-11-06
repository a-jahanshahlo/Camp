using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.CommonLib.ExtentionMethods;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Accounts;
using Camps.WebUI.ViewModels.Quota;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.Interfaces;
using Comps.ServiceLayer.Security;
using Microsoft.AspNet.Identity;

namespace Camps.WebUI.Controllers.API
{
    public class ConfirmQuotaController : ApiController
    {

        private readonly IQuotaService _quotaService;
        private readonly IUnitOfWork _db;
 
        private readonly IUserInDeptRolesService _userInDeptRolesService;
        private readonly IApplicationUserManager _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotaService"></param>
        /// <param name="db"></param>
        /// <param name="userInDeptRolesService"></param>
        /// <param name="userManager"></param>
        public ConfirmQuotaController(IQuotaService quotaService, IUnitOfWork db, IUserInDeptRolesService userInDeptRolesService, IApplicationUserManager userManager)
        {
            _db = db;
            _userInDeptRolesService = userInDeptRolesService;
            _userManager = userManager;
            _quotaService = quotaService;

        }
        [HttpGet]
        public IEnumerable<UserFindViewModel> GetPassenger(string q, int deptId)
        {
            List<ApplicationUser> users = _userManager.GetUsers()
                .Where(x => x.UserInfo.FirstName.Contains(q) || x.UserInfo.LastName.Contains(q))
                //.Where(x => x.UserInDeptRoles.Any(c => c.DepartmentDeptRole.DepartmentId == deptId))
                .ToList();

            var models = Mapper.Map<IList<ApplicationUser>, IList<UserFindViewModel>>(users);
            return models;

        }
        [HttpGet]
        public IEnumerable<QuotaIndexViewModel> GetMyDeptQuota(int skip, int pageSize)
        {
            if (!_userInDeptRolesService.IsConfirmer(User.Identity.GetUserId()))
            {
                ModelState.AddError(_userInDeptRolesService.Errors);
                throw new HttpResponseException(Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState));

            }


            List<Quota> items = _quotaService
              .GetMyDeptQuota(User.Identity.GetUserId())
              .Include(x => x.Department)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Quota>, IList<QuotaIndexViewModel>>(items);
            return models;

        }
        [HttpDelete]
        public HttpResponseMessage DeleteRefuse(int id)
        {
            _quotaService.Refuse(User.Identity.GetUserId(), id);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, ConfirmQuotaEditViewModel model)
        {

            model.BossUserId = int.Parse(User.Identity.GetUserId());
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            Quota item = Mapper.Map<ConfirmQuotaEditViewModel, Quota>(model);


            _quotaService.UpdateConfirmQuota(User.Identity.GetUserId(), id, item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


    }
}
