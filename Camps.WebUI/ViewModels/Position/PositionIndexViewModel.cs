using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camps.WebUI.ViewModels.Position
{
    public class PositionIndexViewModel
    {
        public int Id { get; set; }
        public string PosTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}