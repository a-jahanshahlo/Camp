using System;

namespace Camps.WebUI.ViewModels.Festival
{
    public class FestivalIndexViewModel
    {
        public int Id { get; set; }
        public string FestivalTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class FestivalShortIndexViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
 
    }
}