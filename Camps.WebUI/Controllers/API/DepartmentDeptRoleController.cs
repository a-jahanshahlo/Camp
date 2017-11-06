using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.DepartmentBoss;
using Camps.WebUI.ViewModels.DepartmentDeptRole;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    public class DepartmentDeptRoleController : ApiController
    {
        private readonly IDepartmentDeptRoleService _departmentDeptRoleService;
        private readonly IUnitOfWork _db;


        public DepartmentDeptRoleController(IUnitOfWork unitOfWork, IDepartmentDeptRoleService departmentDeptRoleService)
        {
            _db = unitOfWork;
            _departmentDeptRoleService = departmentDeptRoleService;
        }  

        [HttpGet]
        public IEnumerable<DepartmentDeptRoleShortViewModel> GetByName(string q)
        {


            List<DepartmentDeptRole> items = _departmentDeptRoleService
                          .GetAll()
                          .Where(x => x.Department.DepTitle.Contains(q) || x.DeptRole.RoleTitle.Contains(q))
                          .Include(x => x.Department)
                          .Include(x => x.DeptRole)
                          .OrderByDescending(x => x.Id)
                          .ToList();
            var models = Mapper.Map<IList<DepartmentDeptRole>, IList<DepartmentDeptRoleShortViewModel>>(items);
            return models;
        }
        [HttpGet]
        public IEnumerable<DepartmentDeptRoleIndexViewModel> GetByDept(int q)
        {


            List<DepartmentDeptRole> items = _departmentDeptRoleService
                          .GetAll()
                          .Include(x => x.Department)
                          .Include(x => x.DeptRole)
                          .Where(x => x.DepartmentId == q)
                          .OrderByDescending(x => x.Id)
                          .ToList();
            var models = Mapper.Map<IList<DepartmentDeptRole>, IList<DepartmentDeptRoleIndexViewModel>>(items);
            return models;
        }
        [HttpGet]
        public IEnumerable<DepartmentDeptRoleIndexViewModel> Get(int skip, int pageSize)
        {


            List<DepartmentDeptRole> items = _departmentDeptRoleService
                          .GetAll()
                          .Include(x => x.Department)
                          .Include(x => x.DeptRole)
                          .OrderByDescending(x => x.Id)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToList();
            var models = Mapper.Map<IList<DepartmentDeptRole>, IList<DepartmentDeptRoleIndexViewModel>>(items);
            return models;
        }
        [HttpGet]
        public IEnumerable<DepartmentDeptRoleIndexViewModel> GetDept(int skip, int pageSize, int deptId)
        {


            List<DepartmentDeptRole> items = _departmentDeptRoleService
                          .GetAll()
                          .Where(x => x.DepartmentId == deptId)
                          .Include(x => x.Department)
                          .Include(x => x.DeptRole)
                          .OrderByDescending(x => x.Id)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToList();
            var models = Mapper.Map<IList<DepartmentDeptRole>, IList<DepartmentDeptRoleIndexViewModel>>(items);
            return models;
        }

        [HttpPost]
        public HttpResponseMessage Post(DepartmentDeptRoleCreateViewModel model)
        {
            if (model == null && !ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            DepartmentDeptRole item = Mapper.Map<DepartmentDeptRoleCreateViewModel, DepartmentDeptRole>(model);


            _departmentDeptRoleService.Add(item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = _departmentDeptRoleService.Find(id);
            if (item != null)
            {
                _departmentDeptRoleService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, DepartmentDeptRoleEditViewModel model)
        {
            var itemVm = Mapper.Map<DepartmentDeptRoleEditViewModel, DepartmentDeptRole>(model);
            var suite = _departmentDeptRoleService.Find(id);
            if (suite != null && itemVm != null)
            {

                _departmentDeptRoleService.Update(id, itemVm);
                _db.SaveChanges();
            }


            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
