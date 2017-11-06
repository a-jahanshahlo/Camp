using System;

namespace Camps.WebUI.ViewModels.Reservation
{
    public class ReservationEditViewModel
    {
    
        public int UserId { get; set; }
        public int SuiteId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}