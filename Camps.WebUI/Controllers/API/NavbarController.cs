using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Camps.WebUI.ViewModels.Accounts;
using Comps.DomainLayer.Security;
using Comps.ServiceLayer.Security;

namespace Camps.WebUI.Controllers.API
{
    public class NavbarController : ApiController
    {
        private readonly IApplicationUserManager _userManager;
        public NavbarController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<UserFindViewModel> GetUserDetails()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var models = Mapper.Map<ApplicationUser, UserFindViewModel>(user);
            return models;
        }
    }
}
