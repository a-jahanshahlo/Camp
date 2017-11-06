using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Camps.WebUI.ViewModels.Department;
using Camps.WebUI.ViewModels.Festival;
using Camps.WebUI.ViewModels.User;
using Comps.DomainLayer;

namespace Camps.WebUI.ViewModels.Quota
{
    public class QuotaIndexViewModel
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime DeadLineTime { get; set; }
        public int PeriodId { get; set; }
      
        public int BossUserId { get; set; }
 
        public int OperatorUserId { get; set; }
 
        public int PassengerUserId { get; set; }
 
        public int DepartmentId { get; set; }
        public bool IsRefuse { get; set; }
        public int WhoRefuseId { get; set; }
        public DepartmentIndexViewModel   DepartmentIndexViewModel { get; set; }
        public UserIndexViewModel BossUserViewModel { get; set; }
        public UserIndexViewModel OperatorUserViewModel { get; set; }
        public UserIndexViewModel PassengerUserViewModel { get; set; }
        public PeriodIndexViewModel PeriodIndexViewModel { get; set; }

    }
    public class QuotaCreateViewModel
    {
        public QuotaCreateViewModel()
        {
            BossUserId = null;
            OperatorUserId = null;
            PassengerUserId = null;
        }
 
        public DateTime AddDate { get; set; }
        [Required]
        public DateTime DeadLineTime { get; set; }
        [Required]
        public int PeriodId { get; set; }

        public int? BossUserId { get; set; }

        public int? OperatorUserId { get; set; }

        public int? PassengerUserId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public bool IsRefuse { get; set; }
        public int? WhoRefuseId { get; set; }

    }
    public class QuotaEditViewModel
    {
        public DateTime AddDate { get; set; }
        public DateTime DeadLineTime { get; set; }
        public int PeriodId { get; set; }
        public int BossUserId { get; set; }
        public int OperatorUserId { get; set; }
        public int PassengerUserId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsRefuse { get; set; }
        public int? WhoRefuseId { get; set; }

    }
    public class ConfirmQuotaViewModel
    {
        public int Id { get; set; }
        public int BossUserId { get; set; }
        public int PassengerUserId { get; set; }
        public bool IsRefuse { get; set; }
        public int? WhoRefuseId { get; set; }

    }
    public class ConfirmQuotaEditViewModel
    {
       
        public int BossUserId { get; set; }
        public int PassengerUserId { get; set; }

    }
}