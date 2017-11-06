using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Camps.DataLayer.Context;
using Camps.WebUI.ViewModels.Camps;
using Camps.WebUI.ViewModels.Position;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    public class PositionController : ApiController
    {
        private IUnitOfWork _db;
        private readonly IPositionService _positionService;

        public PositionController(IUnitOfWork unitOfWork, IPositionService positionService)
        {
            _db = unitOfWork;
            _positionService = positionService;
        }
        [HttpGet]
        public IEnumerable<PositionIndexViewModel> Get()
        {
            List<UserInDeptRole> items = _positionService.GetAll().ToList();
            var model = Mapper.Map<IList<UserInDeptRole>, IList<PositionIndexViewModel>>(items);

            return model;
        } 

    }
}
