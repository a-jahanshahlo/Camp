using System.ComponentModel.DataAnnotations;

using Camps.WebUI.ViewModels.Profile;


namespace Camps.WebUI.ViewModels.Accounts
{

    public class RegisterFullViewModel
    {
         [Required(ErrorMessage = "فیلد نام کاربری اجباری است")]
        [Display(Name = "نام کاربری")]
        [StringLength(50, ErrorMessage = "نام کاربری می بایست حداقل 6 و حداکثر 50 کاراکتر باشد", MinimumLength = 6)]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
         
       
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "فیلد کلمه عبور اجباری است")]
        [StringLength(20, ErrorMessage = "کلمه عبور می بایست حداقل 6 و حداکثر 20 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Compare("Password",ErrorMessage = "کلمه عبور و تکرار آن برابر نیست")]
        [Required(ErrorMessage = "فیلد تکرار کلمه عبور اجباری است")]
        public string ConfirmPassword { get; set; }

        public ProfileCreateViewModel Profile { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class UserIndexViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]

        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Confirm password")]

        public string Mobile { get; set; }
    }
    public class UserFindViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Text { get; set; }
    }

    public class RoleIndexViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Role { get; set; }
    }
    public class UserInRoleCrateViewModel
    {
        [Required]

        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}