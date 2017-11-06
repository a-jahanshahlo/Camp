using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfFacilityUnitService : EfGenericService<FacilityUnit>, IFacilityUnitService
    {
        public EfFacilityUnitService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}