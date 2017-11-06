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
    public class DepartmentController : ApiController
    {
        private readonly IUnitOfWork _db;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IUnitOfWork unitOfWork, IDepartmentService departmentService)
        {
            _db = unitOfWork;
            _departmentService = departmentService;
        }
        [HttpGet]
        public IEnumerable<DepartmentShortIndexViewModel> GetAll()
        {


            List<Department> items = _departmentService
                          .GetAll()
                          .OrderByDescending(x => x.Id)
 
                          .ToList();
            var models = Mapper.Map<IList<Department>, IList<DepartmentShortIndexViewModel>>(items);
            return models;
        }
        [HttpGet]
        public IEnumerable<DepartmentIndexViewModel> GetDept(int skip, int pageSize)
        {


            List<Department> items = _departmentService
                          .GetAll()
                          .OrderByDescending(x => x.Id)
                          .Skip(skip)
                          .Take(pageSize)
                          .ToList();
            var models = Mapper.Map<IList<Department>, IList<DepartmentIndexViewModel>>(items);
            return models;
        }

        [HttpPost]
        public HttpResponseMessage PostDept(DepartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            Department item = Mapper.Map<DepartmentCreateViewModel, Department>(model);


            _departmentService.Add(item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage DeleteDept(int id)
        {
            var item = _departmentService.Find(id);
            if (item != null)
            {
                _departmentService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        [HttpPut]
        public HttpResponseMessage PutDept(int id, DepartmentEditViewModel model)
        {
            var itemVm = Mapper.Map<DepartmentEditViewModel, Department>(model);
            var suite = _departmentService.Find(id);
            if (suite != null && itemVm != null)
            {

                _departmentService.Update(id, itemVm);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }
}