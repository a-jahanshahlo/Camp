using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfItemsInFacilityPackageService : EfGenericService<ItemsInFacilityPackage>, IItemsInFacilityPackageService
    {
        public EfItemsInFacilityPackageService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}