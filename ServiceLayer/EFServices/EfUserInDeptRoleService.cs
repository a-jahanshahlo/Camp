using System.Linq;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfUserInDeptRoleService : EfGenericService<UserInDeptRole>, IUserInDeptRolesService
    {
        public EfUserInDeptRoleService(IUnitOfWork uow)
            : base(uow)
        {

        }

        public bool UpdateConfirmer(int userId, bool isConfirmer)
        {
            var userInDeptRole = Entities.FirstOrDefault(x => x.UserId == userId);

            if (userInDeptRole == null)
            {
                Errors.Add("Null Post", "پستی برای کاربر مورد نظر ثبت نشده است");
                return false;
            }
            userInDeptRole.IsConfirmer = isConfirmer;
            Uow.SaveChanges();

            return true;
        }

        public bool IsConfirmer(string userId)
        {
        
            if (string.IsNullOrEmpty(userId))
            {
                Errors.Add("UserId is empty", "شماره یکتای کاربر تهی است");
                return false;
            }
            int id;
            bool res = int.TryParse(userId, out id);
            if (res == false)
            {
                Errors.Add("UserId Con not convert to int", "شماره یکتای کاربر قابل تبدیل نمی باشد");
                return false;
            }
            var userInDeptRole = Entities.FirstOrDefault(x => x.UserId == id);

            if (userInDeptRole == null)
            {
                Errors.Add("Null Post", "پستی برای کاربر مورد نظر ثبت نشده است");
                return false;
            }

            return true;
        }
    }
}