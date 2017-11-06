using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Festival;
using Camps.WebUI.ViewModels.Quota;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camps.WebUI.Controllers.API
{
    public class QuotaController : ApiController
    {
        private readonly IQuotaService _quotaService;
        private readonly IUnitOfWork _db;
        public QuotaController(IQuotaService quotaService,IUnitOfWork db)
        {
            _db = db;
            _quotaService = quotaService;

        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="festivalId"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<QuotaIndexViewModel> Get(int skip, int pageSize, int quotaId)
        {

            List<Quota> items = _quotaService
              .GetAll()
              .Include(x=>x.BossUser)
              .Include(x => x.Department)
              .Include(x => x.OperatorUser)
              .Where(x=>x.Id==quotaId)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Quota>, IList<QuotaIndexViewModel>>(items);
            return models;

        }
        [HttpGet]
        public IEnumerable<QuotaIndexViewModel> Get(int skip, int pageSize)
        {

            List<Quota> items = _quotaService
              .GetAll()
              .Include(x => x.OperatorUser)
              .Include(x => x.Department)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Quota>, IList<QuotaIndexViewModel>>(items);
            return models;

        }
        [HttpPost]
        public HttpResponseMessage Post(QuotaCreateViewModel model)
        {

            model.OperatorUserId = int.Parse(User.Identity.GetUserId());
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            Quota item = Mapper.Map<QuotaCreateViewModel, Quota>(model);

           
            _quotaService.Add(item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = _quotaService.Find(id);
            if (item != null)
            {
                _quotaService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, QuotaEditViewModel model)
        {
            var itemVm = Mapper.Map<QuotaEditViewModel, Quota>(model);
          
            if ( itemVm != null)
            {

                _quotaService.Update(id, itemVm);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }
}
