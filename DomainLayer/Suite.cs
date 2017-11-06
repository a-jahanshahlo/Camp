using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comps.DomainLayer
{
    public class Suite : DelEntity
    {

        public Suite()
        {
            this.Reservations = new List<Reservation>();
            this.SuiteFacilityPackages = new List<SuiteFacilityPackage>();
            this.Phones = new List<Phone>();
        }

 
 
        public int CampId { get; set; }
        public int? GalleryId { get; set; }
        public int SuiteOwnerId { get; set; }
        public virtual Camp Camp { get; set; }
        public virtual Gallery Gallery { get; set; }
 
        public virtual SuiteOwner SuiteOwner { get; set; }
        public virtual ICollection<SuiteFacilityPackage> SuiteFacilityPackages { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
 
        public string SuiteName { get; set; }
        public int SuiteNumber { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int RoomCount { get; set; }
        public int SuiteTypeId { get; set; }
        public SuiteType SuiteType { get; set; }
        public int SuiteGradeId { get; set; }
        public virtual SuiteGrade SuiteGrade { get; set; }
 

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }  

    }
}