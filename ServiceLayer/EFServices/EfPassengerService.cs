using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPassengerService : EfGenericService<FreePassenger>, IPassengerService
    {
        public EfPassengerService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}