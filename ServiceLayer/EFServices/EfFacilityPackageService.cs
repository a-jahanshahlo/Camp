using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfFacilityPackageService : EfGenericService<FacilityPackage>, IFacilityPackageService
    {
        public EfFacilityPackageService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}