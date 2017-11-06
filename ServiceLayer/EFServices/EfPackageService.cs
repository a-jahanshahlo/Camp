using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPackageService : EfGenericService<Package>, IPackageService
    {
        public EfPackageService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}