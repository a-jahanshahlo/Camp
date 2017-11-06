using System;
using Camps.WebUI.ViewModels.Department;

namespace Camps.WebUI.ViewModels.User
{
    public class UserIndexViewModel
    { 
        public int Id { get; set; }
        public string UserName { get; set; }
      
        //This can be mobile
        public string Email { get; set; }
        public string Phone { get; set; }
 
        public string Address { get; set; }
   
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
   
        public int Age { get; set; }
 
        public string Nid { get; set; }
 
        public int GenderId { get; set; }

        public virtual GenderIndexViewModel Gender { get; set; }
 
        public DateTime Birthday { get; set; }
  
    }
}