using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
 
 

namespace Camps.WebUI.ViewModels.Profile
{
    /// <summary>
    /// 
    /// </summary>
   
    public class ProfileCreateViewModel
    {
       
        public string Phone { get; set; }
     
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "فیلد نام اجباری است")]
        public string FirstName { get; set; }
          [Required(ErrorMessage = "فیلد نام خانوادگی اجباری است")]
        public string LastName { get; set; }
        public int Age { get; set; }
 
        public string Nid { get; set; }
        public int GenderId { get; set; }
        public DateTime Birthday { get; set; }
    }
}