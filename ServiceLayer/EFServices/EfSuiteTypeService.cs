using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfSuiteTypeService : EfGenericService<SuiteType>, ISuiteTypeService
    {
        public EfSuiteTypeService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}