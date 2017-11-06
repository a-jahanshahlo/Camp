using Camps.WebUI.ViewModels.Department;
using Camps.WebUI.ViewModels.DeptRoles;

namespace Camps.WebUI.ViewModels.DepartmentDeptRole
{
    public class DepartmentDeptRoleIndexViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DeptRoleId { get; set; }
        public bool IsActive { get; set; }
        public DepartmentIndexViewModel DepartmentIndexViewModel { get; set; }
        public DeptRoleIndexViewModel DeptRoleIndexViewModel { get; set; }

    }
    public class DepartmentDeptRoleShortViewModel
        {
        public int Id { get; set; }
        public string Text { get; set; }
 

    }
}