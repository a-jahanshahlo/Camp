using System.Collections.Generic;
using System.Linq;
using System.Web;
using Comps.DomainLayer;

namespace Camps.WebUI.ViewModels.User
{
    public class UserViewModel
    {     
        public string UserName { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}