using System;

namespace Camps.WebUI.ViewModels.Festival
{
    public class FestivalEditViewModel
    {
        public string FestivalTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
    }
}