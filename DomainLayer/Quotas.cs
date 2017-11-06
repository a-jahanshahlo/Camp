using System;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class Quota  : DelEntity
    {
        public DateTime AddDate { get; set; }
        public DateTime DeadLineTime { get; set; }
        public int PeriodId { get; set; }
        public virtual Period Period { get; set; }
        public int? BossUserId { get; set; }
        public virtual ApplicationUser BossUser { get; set; }
        public int OperatorUserId { get; set; }
        public virtual ApplicationUser OperatorUser { get; set; }
        public int? PassengerUserId { get; set; }
        public virtual ApplicationUser PassengerUser { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public bool IsRefuse { get; set; }
        public int? WhoRefuseId { get; set; }
        public virtual ApplicationUser WhoRefuse { get; set; }
 
    }
}