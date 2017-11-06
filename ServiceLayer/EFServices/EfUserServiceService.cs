using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfUserServiceService : EfGenericService<UserService>, IUserServiceService
    {
        public EfUserServiceService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}