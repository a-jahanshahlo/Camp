using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
 
using Camps.WebUI.ViewModels.Reservation;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;
 
namespace Camps.WebUI.Controllers.API
{
    public class ReservationController : ApiController
    {
        private readonly IReservationService _reservationService;
        private readonly IUnitOfWork _db;
        public ReservationController(IUnitOfWork db, IReservationService reservationService)
        {
            _db = db;
            _reservationService = reservationService;
        }
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ReservationIndexViewModel> Get(int skip, int pageSize, int userId)
        {

            List<Reservation> items = _reservationService
              .GetAll()
              .Include(x => x.User)
              .Where(x => x.UserId == userId)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Reservation>, IList<ReservationIndexViewModel>>(items);
            return models;

        }
        [HttpGet]
        public IEnumerable<ReservationIndexViewModel> Get(int skip, int pageSize)
        {

            List<Reservation> items = _reservationService
              .GetAll()
              .Include(x => x.User)
              .OrderByDescending(x => x.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToList();
            var models = Mapper.Map<IList<Reservation>, IList<ReservationIndexViewModel>>(items);
            return models;

        }
        [HttpPost]
        public HttpResponseMessage Post(ReservationCrateViewModel model)
        {

           
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            Reservation item = Mapper.Map<ReservationCrateViewModel, Reservation>(model);


            _reservationService.Add(item);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = _reservationService.Find(id);
            if (item != null)
            {
                _reservationService.Delete(item);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, ReservationEditViewModel model)
        {
            var itemVm = Mapper.Map<ReservationEditViewModel, Reservation>(model);

            if (itemVm != null)
            {

                _reservationService.Update(id, itemVm);
                _db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }


            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
