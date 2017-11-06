using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Department;
using Camps.WebUI.ViewModels.DeptRoles;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    public class DeptRolesController : ApiController
    {
           private readonly IUnitOfWork _db;
        private readonly IDeptRoleService _deptRoleService;



        public DeptRolesController(IUnitOfWork unitOfWork, IDeptRoleService deptRoleService)
        {
            _db = unitOfWork;
            _deptRoleService = deptRoleService;
        }
        [HttpGet]
        public IEnumerable<DeptRoleIndexViewModel> Get(int skip, int pageSize)
        {


            List<DeptRole> items = _deptRoleService
                          .GetAll()
                          .OrderByDescending(x => x.Id)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToList();
            var models = Mapper.Map<IList<DeptRole>, IList<DeptRoleIndexViewModel>>(items);
            return models;
        }

        [HttpPost]
        public HttpResponseMessage Post(DeptRoleCreateViewModel model)
        {
            if (model == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            DeptRole item = Mapper.Map<DeptRoleCreateViewModel, DeptRole>(model);


            _deptRoleService.Add(item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = _deptRoleService.Find(id);
            if (item != null)
            {
                _deptRoleService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, DeptRoleEditViewModel model)
        {
            var itemVm = Mapper.Map<DeptRoleEditViewModel, DeptRole>(model);
            var suite = _deptRoleService.Find(id);
            if (suite != null && itemVm != null)
            {

                _deptRoleService.Update(id,itemVm);
                _db.SaveChanges();
            }


            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
