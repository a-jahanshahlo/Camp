using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;
using Comps.ServiceLayer.Security;

namespace Comps.ServiceLayer.EFServices
{
    public class EfQuotaService : EfGenericService<Quota>, IQuotaService
    {
        private readonly IUserInDeptRolesService _userInDeptRolesService;
        public EfQuotaService(IUnitOfWork uow, IUserInDeptRolesService userInDeptRolesService)
            : base(uow)
        {
            _userInDeptRolesService = userInDeptRolesService;
        }

        public  void Update(int id, Quota entity)
        {
            var existingEntity = Entities.Find(id);
            existingEntity.DeadLineTime = entity.DeadLineTime;
            existingEntity.DepartmentId = entity.DepartmentId;
            existingEntity.PeriodId = entity.PeriodId;
      
        }

        public IQueryable<Quota> GetMyDeptQuota(string userId)
        {
            int id = int.Parse(userId);
            IQueryable<ICollection<Quota>> items = _userInDeptRolesService
                .GetAll()
                .Where(x => x.UserId == id)
                .Select(x => x.DepartmentDeptRole.Department.Quotas);

            var list = items.SelectMany(x => x.Select(c => c))
                .Where(x => x.DeadLineTime > DateTime.UtcNow && x.IsDeleted == false&&x.IsRefuse==false);

            return list;
        }

        public bool UpdateConfirmQuota(string userId, int id, Quota quota)
        {
                        int userid;
            bool res = int.TryParse(userId, out userid);
            if (res == false)
            {
                Errors.Add("UserId Con not convert to int", "شماره یکتای کاربر قابل تبدیل نمی باشد");
                return false;
            }
            if (quota == null)
            {
                Errors.Add("Null Object", "داده ارسالی معتبر نمی باشد");
                 return false;
            }
            Quota quo = Entities.Find(id);
            if (quo == null)
            {
               Errors.Add("Null Refrence", "رکورد مورد نظر در سیستم موجود نمی باشد");
                 return false;
            }
            
            quo.PassengerUserId = quota.PassengerUserId;
            quo.BossUserId = userid;
   

            return true;
        }

        public bool Refuse(string userId, int id)
        {
            int userid;
            bool res = int.TryParse(userId, out userid);
            if (res == false)
            {
                Errors.Add("UserId Con not convert to int", "شماره یکتای کاربر قابل تبدیل نمی باشد");
                return false;
            }
   
            Quota quo = Entities.Find(id);
            if (quo == null)
            {
                Errors.Add("Null Refrence", "رکورد مورد نظر در سیستم موجود نمی باشد");
                return false;
            }
            quo.IsRefuse = true;
            quo.WhoRefuseId = userid;

            return true;
        }
    }
}