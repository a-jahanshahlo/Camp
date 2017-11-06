using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfServiceService : EfGenericService<Service>, IServiceService
    {
        public EfServiceService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}