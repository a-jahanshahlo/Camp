using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.CommonLib.ExtentionMethods;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.DepartmentBoss;
using Camps.WebUI.ViewModels.UserInDeptRole;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    public class UserInDeptRolesController : ApiController
    {
        private readonly IUnitOfWork _db;
        private readonly IUserInDeptRolesService _userInDeptRolesService;

        public UserInDeptRolesController(IUnitOfWork db, IUserInDeptRolesService deptRoleUserService)
        {
            _db = db;
            _userInDeptRolesService = deptRoleUserService;
        }
        [HttpPost]
        public IHttpActionResult ConfirmerPost(UserInDeptRoleConfirmViewModel model)
        {
            if (!ModelState.IsValid)
            {
              
                return BadRequest(ModelState);
            }
            if (!_userInDeptRolesService.UpdateConfirmer(model.UserId, model.IsConfirm))
            {
                ModelState.AddError(_userInDeptRolesService.Errors);
                return BadRequest(ModelState);
            }
             

            return Ok();
        }
        [HttpPost]
        public IHttpActionResult AddToPost(UserInDeptRoleShortViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userInDeptRole = _userInDeptRolesService.GetAll().FirstOrDefault(x => x.DepartmentDeptRoleId == model.DeptId);
            _userInDeptRolesService.Delete(userInDeptRole, true);
            _db.SaveChanges();

            UserInDeptRole item = Mapper.Map<UserInDeptRoleShortViewModel, UserInDeptRole>(model);
            _userInDeptRolesService.Add(item);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public IHttpActionResult RemoveFromPost(UserInDeptRoleShortViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userInDeptRole = _userInDeptRolesService.GetAll().FirstOrDefault(x => x.DepartmentDeptRoleId == model.DeptId);
            _userInDeptRolesService.Delete(userInDeptRole, true);
            _db.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public IEnumerable<UserPostViewModel> GetUserDept(int userId)
        {


            List<DepartmentDeptRole> items = _userInDeptRolesService
                          .GetAll()
                          .Include(x => x.DepartmentDeptRole)
                          .Where(x => x.UserId == userId)
                          .OrderByDescending(x => x.Id)
                          .Select(x => x.DepartmentDeptRole)
                          .ToList();
            var models = Mapper.Map<IList<DepartmentDeptRole>, IList<UserPostViewModel>>(items);
            return models;
        }
        [HttpGet]
        public IEnumerable<UserInDeptRoleIndexViewModel> Get(int skip, int pageSize)
        {


            List<UserInDeptRole> items = _userInDeptRolesService
                          .GetAll()
                          .OrderByDescending(x => x.Id)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToList();
            var models = Mapper.Map<IList<UserInDeptRole>, IList<UserInDeptRoleIndexViewModel>>(items);
            return models;
        }

        [HttpPost]
        public HttpResponseMessage PostDept(UserInDeptRoleCreateViewModel model)
        {
            if (model == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            UserInDeptRole item = Mapper.Map<UserInDeptRoleCreateViewModel, UserInDeptRole>(model);


            _userInDeptRolesService.Add(item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage DeleteDept(int id)
        {
            var item = _userInDeptRolesService.Find(id);
            if (item != null)
            {
                _userInDeptRolesService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPut]
        public HttpResponseMessage PutDept(int id, UserInDeptRoleEditViewModel model)
        {
            var itemVm = Mapper.Map<UserInDeptRoleEditViewModel, UserInDeptRole>(model);
            var suite = _userInDeptRolesService.Find(id);
            if (suite != null && itemVm != null)
            {

                _userInDeptRolesService.Update(itemVm);
                _db.SaveChanges();
            }


            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}