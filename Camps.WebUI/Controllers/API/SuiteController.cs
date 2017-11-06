using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.SuiteGrade;
using Camps.WebUI.ViewModels.Suites;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    public class SuiteController : ApiController
    {
        private readonly ISuiteService _suiteService;
        private readonly IUnitOfWork _db;
        private readonly ISuiteGradeService _suiteGradeService;
        private readonly ISuiteOwnerService _suiteOwnerService;
        private readonly ISuiteTypeService _suiteTypeService;
        public SuiteController(
            ISuiteService suiteService,
            IUnitOfWork unitOfWork,
            ISuiteGradeService suiteGradeService,
            ISuiteOwnerService suiteOwnerService,
            ISuiteTypeService suiteTypeService
            )
        {
            _suiteService = suiteService;
            _db = unitOfWork;
            _suiteGradeService = suiteGradeService;
            _suiteOwnerService = suiteOwnerService;
            _suiteTypeService = suiteTypeService;
        }

        [HttpGet]
        public IEnumerable<SuiteIndexViewModel> Get(int skip, int pageSize)
        {

            var suites = _suiteService.GetAll()
                .Include(x => x.Gallery)
                .Include(x => x.Camp)
                .Include(x => x.SuiteGrade)
                .Include(x => x.SuiteOwner)
                .Include(x => x.SuiteType)
                .Include(x => x.Phones)
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
            var models = Mapper.Map<IList<Suite>, IList<SuiteIndexViewModel>>(suites);
            return models;
        }
        [HttpGet]
        public IEnumerable<SuiteOwnerIndexViewModel> GetAllSuiteOwner()
        {
            var suiteOwners = _suiteOwnerService.GetAll().ToList();
            var models = Mapper.Map<IList<SuiteOwner>, IList<SuiteOwnerIndexViewModel>>(suiteOwners);

            return models;
        }

        [HttpGet]
        public IEnumerable<SuiteGradeIndexViewModel> GetSuiteGrade()
        {
            var suiteGrades = _suiteGradeService.GetAll().ToList();
            var models = Mapper.Map<IList<SuiteGrade>, IList<SuiteGradeIndexViewModel>>(suiteGrades);

            return models;
        }
        [HttpGet]
        public IEnumerable<SuiteTypeIndexViewModel> GetSuiteType()
        {
            var suiteTypes = _suiteTypeService.GetAll().ToList();
            var models = Mapper.Map<IList<SuiteType>, IList<SuiteTypeIndexViewModel>>(suiteTypes);

            return models;
        }
        [HttpPost]
        public HttpResponseMessage PostSuite(SuiteCreateViewModel model)
        {
            if (model == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            Suite suite = Mapper.Map<SuiteCreateViewModel, Suite>(model);
            IList<Phone> phones;
            //if (suite.Phones!=null)
            //{
            //  phones=    suite.Phones.ToList();
            //}

            _suiteService.Add(suite);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage RemoveGallery(int id)
        {

            var suite = _suiteService.Find(id);
            suite.GalleryId = null;
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage DeleteSuite(int id)
        {
            var suite = _suiteService.Find(id);
            _suiteService.Delete(suite);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpPut]
        public HttpResponseMessage PutSuite(int id, SuiteEditViewModel model)
        {
            var suiteVm = Mapper.Map<SuiteEditViewModel, Suite>(model);
            var suite = _suiteService.Find(id);
            suite.Phones.Clear();
            _suiteService.Update(id,suiteVm);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}