using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.Reservation
{
    public class ReservationIndexViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SuiteId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}