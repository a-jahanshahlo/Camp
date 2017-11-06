using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Comps.DomainLayer.Security
{
    public class TestPhoneCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public DateTime AddedDate { get; set; }
    }
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }

        public CustomRole(string name)
        {
         
            Name = name;
        }

        public string TextFa { get; set; }
  
    }
}