using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.Festival
{
    public class FestivalCreateViewModel
    {
        [Required(ErrorMessage = "عنوان جشنواره اجباری است")]
        public string FestivalTitle { get; set; }
        [Required(ErrorMessage = "تاریخ شروع جشنواره اجباری است")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "تاریخ پایان جشنواره اجباری است ")]
        public DateTime ToDate { get; set; }

        public bool IsActive { get; set; }
    }
}