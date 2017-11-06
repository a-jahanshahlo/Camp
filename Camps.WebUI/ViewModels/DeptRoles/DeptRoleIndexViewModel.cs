using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.DeptRoles
{
    public class DeptRoleIndexViewModel
    {
        public int Id { get; set; }
        public string RoleTitle { get; set; }
    }
    public class DeptRoleEditViewModel
    {
  
        public string RoleTitle { get; set; }

    }
    public class DeptRoleCreateViewModel
    {
        public string RoleTitle { get; set; }

    }

}