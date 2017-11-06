using System.Collections.Generic;
using Camps.WebUI.ViewModels.Camps;
using Camps.WebUI.ViewModels.SuiteGrade;

namespace Camps.WebUI.ViewModels.Suites
{
    public class SuiteIndexViewModel
    {
        public int Id { get; set; }
        public string SuiteName { get; set; }
        public int SuiteNumber { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int RoomCount { get; set; }
        public virtual SuiteGradeIndexViewModel SuiteGrade { get; set; }
        public virtual SuiteTypeIndexViewModel SuiteType { get; set; }
        public virtual CampsIndexViewModel Camp { get; set; }
        public virtual SuiteOwnerIndexViewModel SuiteOwner { get; set; }

        //[Required ]
        public virtual GalleryShortViewModel Gallery { get; set; }
        public virtual IEnumerable<PhoneViewModel> Phones { get; set; }
 
       
    }
}