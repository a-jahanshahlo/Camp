using System;
using System.ComponentModel.DataAnnotations;
using Camps.WebUI.ViewModels.DepartmentDeptRole;

namespace Camps.WebUI.ViewModels.UserInDeptRole
{
    public class UserInDeptRoleIndexViewModel
    {

        public int DeptId { get; set; }
        public string DepTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }


    }
    public class UserInDeptRoleConfirmViewModel
    {
        [Required]
        public bool IsConfirm { get; set; }
        [Required]
        public int UserId { get; set; }


    }
    public class UserInDeptRoleShortViewModel
    {
        [Required]
        public int DeptId { get; set; }
        [Required]
        public int UserId { get; set; }


    }
    public class UserPostViewModel
    {

        public string Text { get; set; }

        public int Id { get; set; }


    }
}