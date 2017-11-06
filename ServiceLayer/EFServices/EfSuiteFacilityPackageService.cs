using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfSuiteFacilityPackageService : EfGenericService<SuiteFacilityPackage>, ISuiteFacilityPackageService
    {
        public EfSuiteFacilityPackageService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}