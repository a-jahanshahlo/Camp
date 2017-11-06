using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Camps.CommonLib.Exceptions;
using Camps.CommonLib.ExtentionMethods;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Accounts;
using Camps.WebUI.ViewModels.Festival;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{

    /// <summary>
    /// 
    /// </summary>
    public class FestivalController : ApiController
    {



        private readonly IFestivalService _festivalService;
        private readonly IUnitOfWork _db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbWork"></param>
        /// <param name="festivalService"></param>
        public FestivalController(IUnitOfWork dbWork, IFestivalService festivalService)
        {
            _festivalService = festivalService;
            _db = dbWork;
        }
        [HttpGet]
        public IEnumerable<FestivalShortIndexViewModel> GetFind(string q)
        {

            List<Festival> items = _festivalService
              .GetAll()
              .Where(x => x.FestivalTitle.Contains(q))
              .OrderByDescending(x => x.Id)

              .ToList();
            var models = Mapper.Map<IList<Festival>, IList<FestivalShortIndexViewModel>>(items);
            return models;

        }
        [HttpGet]
        public IEnumerable<FestivalIndexViewModel> Get(int skip, int pageSize)
        {

            List<Festival> items = _festivalService
              .GetAll()
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Festival>, IList<FestivalIndexViewModel>>(items);
            return models;

        }
        [HttpPost]
        public IHttpActionResult Post(FestivalCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Festival item = Mapper.Map<FestivalCreateViewModel, Festival>(model);
            if (!_festivalService.IsValid(item))
            {
                ModelState.AddError(_festivalService.Errors);
                return BadRequest(ModelState);
            }
            try
            {
                _festivalService.Add(item);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                string innerMessage = ex.GetInnerException();
                ModelState.AddModelError("Error", innerMessage );
                return BadRequest(ModelState);

            }

            return Ok();
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = _festivalService.Find(id);
            if (item != null)
            {
                try
                {
                    _festivalService.Delete(item);
                    _db.SaveChanges();

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    String innerMessage = ex.GetInnerException();
                    return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(ex.Message + " => " + innerMessage) };

                }
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, FestivalEditViewModel model)
        {
            var itemVm = Mapper.Map<FestivalEditViewModel, Festival>(model);
            var suite = _festivalService.Find(id);
            if (suite != null && itemVm != null)
            {

                _festivalService.Update(id, itemVm);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

    }
}
