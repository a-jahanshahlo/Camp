using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Camps.WebUI.ViewModels.Camps;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;


namespace Camps.WebUI.Controllers.API
{
    public class LocationController : ApiController
    {
        private readonly IProvinceService _provinceService;
        private readonly ICityService _cityService;
        public LocationController(IProvinceService provinceService, ICityService cityService)
        {
            _cityService = cityService;
            _provinceService = provinceService;

        }
        [HttpGet]
        public async Task<IEnumerable<ProvinceViewModel>> GetProvinces()
        {
            var items = await _provinceService.GetAll().ToListAsync();
            var provinceViewModels = Mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceViewModel>>(items);

            return provinceViewModels;
        }
        [HttpGet]
        public async Task<IEnumerable<CityViewModel>> GetCities()
        {
            var items = await _cityService.GetAll().Include(x=>x.Province).ToListAsync();
            var cities = Mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(items);

            return cities;
        }    
        [HttpGet]
        public async Task<IEnumerable<CityViewModel>> GetCitiesByProvince(int id)
        {
            var items = await _cityService.GetAll().Include(x => x.Province).Where(x => x.Province.Id == id).ToListAsync();
            var cities = Mapper.Map<IEnumerable<City>, IEnumerable<CityViewModel>>(items);

            return cities;
        }
    }
}
