using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.DepartmentBoss
{
    public class UserInDeptRoleEditViewModel
    {

        public int DeptId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserId { get; set; }
 
        public bool IsActive { get; set; }
    }
}