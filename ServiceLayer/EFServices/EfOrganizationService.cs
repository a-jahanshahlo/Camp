using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfOrganizationService : EfGenericService<Organization>, IOrganizationService
    {
        public EfOrganizationService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}