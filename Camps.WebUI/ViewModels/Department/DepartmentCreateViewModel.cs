using System.ComponentModel.DataAnnotations;

namespace Camps.WebUI.ViewModels.Department
{
    public class DepartmentCreateViewModel
    {
        [Required]
        [StringLength(250, ErrorMessage ="طول رشته عنوان دپارتمان بیش از 250 حرف است")]
        [Display(Name = "عنوان دپارتمان")]
        public string DepTitle { get; set; }
    }
}