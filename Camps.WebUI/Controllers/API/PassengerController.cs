using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Passengers;
using Camps.WebUI.ViewModels.Suites;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    public class PassengerController : ApiController
    {
        private readonly IPassengerService _passengerService;
        private IUnitOfWork _db;
        public PassengerController(
            IUnitOfWork db,
            IPassengerService passengerService
            )
        {
            _passengerService = passengerService;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<PassengerIndexViewModel> Get(int skip, int pageSize)
        {

            var data = _passengerService.GetAll()
                .Include(x => x.User)
                .OrderByDescending(x => x.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
            var models = Mapper.Map<IList<FreePassenger>, IList<PassengerIndexViewModel>>(data);
            return models;
        }
    }
}
