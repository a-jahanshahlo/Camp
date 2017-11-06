using System;

namespace Camps.WebUI.ViewModels.UserInDeptRole
{
    public class UserInDeptRoleCreateViewModel
    {

        public int DeptId { get; set; }
  
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

    }
}