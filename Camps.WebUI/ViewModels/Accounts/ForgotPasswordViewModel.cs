using System.ComponentModel.DataAnnotations;

namespace Camps.WebUI.ViewModels.Accounts
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ResetPasswordByAdminViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "فیلد کلمه عبور اجباری است")]
        [StringLength(20, ErrorMessage = "کلمه عبور می بایست حداقل 6 و حداکثر 20 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار آن برابر نیست")]
        [Required(ErrorMessage = "فیلد تکرار کلمه عبور اجباری است")]
        public string ConfirmPassword { get; set; }
    }
}