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
using Camps.WebUI.ViewModels.Festival;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    /// <summary>
    /// 
    /// </summary>
    public class PeriodController : ApiController
    {
        private readonly IPeriodService _periodService;
        private readonly IUnitOfWork _db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="periodService"></param>
        public PeriodController(IUnitOfWork db, IPeriodService periodService)
        {
            _db = db;
            _periodService = periodService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="festivalId"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PeriodIndexViewModel> Get(int skip, int pageSize, int festivalId)
        {

            List<Period> items = _periodService
              .GetAll()
              .Include(x => x.Festival)
              .Include(x => x.Camp)
              .Where(x => x.FestivalId == festivalId)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Period>, IList<PeriodIndexViewModel>>(items);
            return models;

        }
        [HttpGet]
        public IEnumerable<PeriodShortIndexViewModel> GetByFestival(int q)
        {

            List<Period> items = _periodService
              .GetAll()
              .Where(x => x.FestivalId == q)
              .OrderByDescending(x => x.Id)
              .ToList();
            var models = Mapper.Map<IList<Period>, IList<PeriodShortIndexViewModel>>(items);
            return models;

        }
        [HttpGet]
        public IEnumerable<PeriodIndexViewModel> Get(int skip, int pageSize)
        {

            List<Period> items = _periodService
              .GetAll()
              .Include(x => x.Festival)
              .Include(x => x.Camp)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Period>, IList<PeriodIndexViewModel>>(items);
            return models;

        }
        [HttpPost]
        public IHttpActionResult Post(PeriodCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Period item = Mapper.Map<PeriodCreateViewModel, Period>(model);
            if (!_periodService.IsValid(item))
            {
                ModelState.AddError(_periodService.Errors);
                return BadRequest(ModelState);
            }



            _periodService.Add(item);
            _db.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = _periodService.Find(id);
            if (item != null)
            {
                _periodService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, PeriodEditViewModel model)
        {
            var itemVm = Mapper.Map<PeriodEditViewModel, Period>(model);
            var suite = _periodService.Find(id);
            if (suite != null && itemVm != null)
            {

                _periodService.Update(id, itemVm);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }
}
