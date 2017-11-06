using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfAddressService : EfGenericService<Address>, IAddressService
    {
        public EfAddressService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}