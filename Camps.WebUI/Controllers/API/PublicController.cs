using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Camps.WebUI.ViewModels.Accounts;
using Camps.WebUI.ViewModels.Department;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.Interfaces;

namespace Camps.WebUI.Controllers.API
{
    /// <summary>
    /// 
    /// </summary>
    public class PublicController : ApiController
    {
        private readonly IGenderService _genderService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genderService"></param>
        public PublicController(IGenderService genderService)
        {
            _genderService = genderService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<GenderIndexViewModel> GetGender()
        {
            List<Gender> items = _genderService.GetAll().ToList();

            var models = Mapper.Map<IList<Gender>, IList<GenderIndexViewModel>>(items);
            return models;
        }
        //[HttpGet]
        //public async Task<IEnumerable<UserIndexViewModel>> Get(int skip, int pageSize)
        //{
        //    List<ApplicationUser> items = await _userManager.GetAllUsersAsync(skip, pageSize);

        //    var models = Mapper.Map<IList<ApplicationUser>, IList<UserIndexViewModel>>(items);
        //    return models;
        //}
    }
}
