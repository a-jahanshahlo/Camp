using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Camps.WebUI.ViewModels.Camps;
using Comps.DomainLayer;
using Camps.DataLayer.Context;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers
{
    public class CampsController : ApiController
    {
        private const int Count = 100;
        private readonly ICampService _campService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;
        private readonly IGalleryService _galleryService;
        private readonly IPhoneService _phoneService;
        public CampsController(
            IPhoneService phoneService,
            IGalleryService galleryService,
            ICampService campService,
            IAddressService addressService,
            IUnitOfWork unitOfWork)
        {
            _campService = campService;
            _unitOfWork = unitOfWork;
            _addressService = addressService;
            _galleryService = galleryService;
            _phoneService = phoneService;
        }
        //  private MainContext db = new MainContext();

        // GET api/Camps
        [HttpGet]
        public CampsViewModel GetFind(int id)
        {

            var camp = _campService.GetAll()
                .Include(x => x.Address)
                .Include(x => x.CampFacilities)
           
                .Include(x => x.Galleries)
                .Include(x => x.Phones)
                .Include(x => x.Suites)
                .FirstOrDefault(x => x.Id == id);

            var model = Mapper.Map<Camp, CampsViewModel>(camp);

            return model;
        }

        [HttpGet]
        public IEnumerable<CampsExistViewModel> GetAll()
        {
            List<Camp> camps = _campService.GetAll().ToList();
            var model = Mapper.Map<IList<Camp>, IList<CampsExistViewModel>>(camps);

            return model;
        }

        [HttpGet]
        public IEnumerable<CampsViewModel> Get(int skip)
        {
            var skp = skip < 0 ? 0 : skip;
            List<Camp> camps = _campService.GetAll()
                .Include(x => x.Address)
                .Include(x => x.CampFacilities)
 
                .Include(x => x.Galleries)
                .Include(x => x.Phones)
                .Include(x => x.Suites)
                .OrderByDescending(x => x.Id)
                .Skip(skp)
                .Take(Count)
                .ToList();

            IList<CampsViewModel> campsViewModel = Mapper.Map<IList<Camp>, IList<CampsViewModel>>(camps);

            return campsViewModel;
        }

        // PUT api/Camps/5
        [HttpPut]

        public HttpResponseMessage Put(int id, [FromBody] CampsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }



            var camp = Mapper.Map<CampsViewModel, Camp>(model);
            var existCamp = _campService.Find(id);
            existCamp.Phones.Clear();

            foreach (var item in camp.Phones)
            {
                // var find = _campService.Find(c => c.Phones.Any(x => x.PhoneNumber == item.PhoneNumber));
                var item1 = item;
                var result = _phoneService.Find(x => x.PhoneNumber == item1.PhoneNumber);
                _phoneService.Delete(result);

            }
            if (existCamp.AddressId != null)
            {
                var address = _addressService.Find(existCamp.AddressId.Value);
                existCamp.AddressId = null;
                existCamp.Address = null;
                _addressService.Delete(address);
            }

            try
            {
                _unitOfWork.SaveChanges();

                camp.Address.CityId = camp.Address.City.Id;
                camp.Id = id;
                camp.CampFacilities = null;
                camp.Galleries = null;
                camp.Suites = null;
                camp.Address.City = null;

                //var existItem = _campService.Find(id);
                //if (existItem == null)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest);
                //}

                //existItem.Address = camp.Address;


                _campService.Update(camp);


                // db.Entry(camp).State = EntityState.Modified;


                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampExists(id))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                }
                else
                {
                    throw;
                }
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);

        }

        // POST api/Camps
        [HttpPost]
        public HttpResponseMessage Post(CampsCreateViewModel camp)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            var cmp = Mapper.Map<CampsCreateViewModel, Camp>(camp);
 
            var galleries = cmp.Galleries.ToList();

            cmp.Galleries.Clear();
            cmp.Address.CityId = cmp.Address.City.Id;
            cmp.Address.City = null;
            _campService.Add(cmp);

            foreach (var item in galleries)
            {
                var item1 = item;
                var gallery = _galleryService.Find(x => x.Id == item1.Id);
                cmp.Galleries.Add(gallery);
            }

            //_campService.Update(cmp);
            _unitOfWork.SaveChanges();

            //  return CreatedAtRoute("DefaultApi", new { id = camp.Id }, camp);
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // DELETE api/Camps/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Camp camp = _campService.Find(id);
            if (camp == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
            camp.CampFacilities.Clear();
            camp.Phones.Clear();
            //   camp.Galleries.Clear();
            camp.Suites.Clear();
            // var key = 0;
            //if (camp.AddressId != null)
            //{
            //  key = camp.Addresses.;
            //}
            _addressService.Delete(camp.Address);
            _campService.Delete(camp);
            // _addressService.Delete(camp.Addresses);


            //  var address = _addressService.Find(key);
            //  _addressService.Delete(address);

            _unitOfWork.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //     db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CampExists(int id)
        {
            // return db.Camps.Count(e => e.Id == id) > 0;
            return _campService.Exists(id);
        }
    }
}