using System;
using Camps.WebUI.ViewModels.Camps;

namespace Camps.WebUI.ViewModels.Festival
{
 
    public class PeriodIndexViewModel
    {
        public int Id { get; set; }
        public string PeriodTitle { get; set; }
        public int FestivalId { get; set; }
        public int CampId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public FestivalIndexViewModel FestivalIndexViewModel { get; set; }

        public CampsIndexViewModel CampsIndexViewModel { get; set; }

    }
    public class PeriodShortIndexViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
 

    }
}