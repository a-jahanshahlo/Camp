using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camps.WebUI.ViewModels.Booking;
using Camps.WebUI.ViewModels.Camps;
using Comps.DomainLayer;
 
namespace Camps.WebUI.ViewModels.Suites
{
    public class SuiteCreateViewModel
    {
        public int CampId { get; set; }
        public int? GalleryId { get; set; }
        public int SuiteOwnerId { get; set; }
        public string SuiteName { get; set; }
        public int SuiteNumber { get; set; }
        public string Description { get; set; }
        public int SuiteTypeId { get; set; }
        public int Capacity { get; set; }
        public int RoomCount { get; set; }
        public int SuiteGradeId { get; set; }

        public virtual ICollection<PhoneViewModel> Phones { get; set; }  
    }
    public class SuiteEditViewModel
    {
        public int Id { get; set; }
        public int CampId { get; set; }
        public int? GalleryId { get; set; }
        public int SuiteOwnerId { get; set; }
        public string SuiteName { get; set; }
        public int SuiteNumber { get; set; }
        public string Description { get; set; }
        public int SuiteTypeId { get; set; }
        public int Capacity { get; set; }
        public int RoomCount { get; set; }
        public int SuiteGradeId { get; set; }

        public virtual ICollection<PhoneViewModel> Phones { get; set; }
    }
}