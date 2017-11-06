using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfSuiteService : EfGenericService<Suite>, ISuiteService
    {
        public EfSuiteService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}