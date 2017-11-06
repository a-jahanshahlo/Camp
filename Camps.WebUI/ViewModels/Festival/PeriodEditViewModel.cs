using System;

namespace Camps.WebUI.ViewModels.Festival
{
    public class PeriodEditViewModel
    {
        public int CampId { get; set; }
        public string PeriodTitle { get; set; }
        public int FestivalId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
 
    }
}