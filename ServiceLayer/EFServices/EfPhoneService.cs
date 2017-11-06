using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPhoneService : EfGenericService<Phone>, IPhoneService
    {
        public EfPhoneService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}