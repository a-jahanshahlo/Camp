using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfFacilityService : EfGenericService<Facility>, IFacilityService
    {
        public EfFacilityService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}