using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfSuiteFacilitieService : EfGenericService<SuiteFacilitie>, ISuiteFacilitieService
    {
        public EfSuiteFacilitieService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}