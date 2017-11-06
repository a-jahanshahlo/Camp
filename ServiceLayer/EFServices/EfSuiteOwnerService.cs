using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfSuiteOwnerService : EfGenericService<SuiteOwner>, ISuiteOwnerService
    {
        public EfSuiteOwnerService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}