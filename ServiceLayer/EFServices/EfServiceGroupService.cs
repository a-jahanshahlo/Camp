using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfServiceGroupService : EfGenericService<ServiceGroup>, IServiceGroupService
    {
        public EfServiceGroupService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}