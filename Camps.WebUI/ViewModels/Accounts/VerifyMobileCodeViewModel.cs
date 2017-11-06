using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.Accounts
{
    public class VerifyMobileCodeViewModel
    {

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
    }
}