using Comps.DomainLayer.Security;
using Microsoft.AspNet.Identity;

namespace Comps.ServiceLayer.Security
{
    public class CustomUserStore : ICustomUserStore
    {
        private readonly IUserStore<ApplicationUser, int> _userStore;

        public CustomUserStore(IUserStore<ApplicationUser, int> userStore)
        {
            _userStore = userStore;
        }


    }
}