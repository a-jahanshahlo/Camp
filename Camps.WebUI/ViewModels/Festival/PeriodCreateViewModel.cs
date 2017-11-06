using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Camps.WebUI.ViewModels.Festival
{
    public class PeriodCreateViewModel
    {
    
        [Required(ErrorMessage = " فیلد اردوگاه اجباری است")]
       
        public int CampId { get; set; }
        [Required(ErrorMessage = " فیلد عنوان دوره اجباری است")]
        public string PeriodTitle { get; set; }
        [Required(ErrorMessage = " فیلد عنوان جشنواره اجباری است")]
        public int FestivalId { get; set; }
        [Required(ErrorMessage = " فیلد تاریخ شروع اجباری است")]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = " فیلد تاریخ پایان اجباری است")]
        public DateTime ToDate { get; set; }
 
    }
}