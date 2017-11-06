using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Comps.DomainLayer.Security
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

        public ApplicationUser()
        {
            this.UserInDeptRoles = new List<UserInDeptRole>();
            this.BossQuotas = new List<Quota>(); 
            this.OperatorQuotas = new List<Quota>();
            this.PassengerQuotas = new List<Quota>();
            this.WhoRefuseQuotas = new List<Quota>();
            this.Bookings = new List<Booking>();
            this.UserServices = new List<UserService>();
            Passengers = new List<FreePassenger>();
            this.Reserves = new List<Reservation>();
       
           
        }

        // سایر خواص اضافی در اینجا

        //[ForeignKey("AddressId")]
        //public virtual Address Address { get; set; }
        //public int? AddressId { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<UserService> UserServices { get; set; }
        public virtual PersonalSetting PersonalSetting { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<FreePassenger> Passengers { get; set; }
        public virtual ICollection<UserInDeptRole> UserInDeptRoles { get; set; }
        public virtual ICollection<Reservation> Reserves { get; set; }
        public virtual ICollection<Quota> BossQuotas { get; set; }
        public virtual ICollection<Quota> PassengerQuotas { get; set; }
        public virtual ICollection<Quota> OperatorQuotas { get; set; }
        public virtual ICollection<Quota> WhoRefuseQuotas { get; set; }

    }
}