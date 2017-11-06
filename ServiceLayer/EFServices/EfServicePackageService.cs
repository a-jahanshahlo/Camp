using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfServicePackageService : EfGenericService<ServicePackage>, IServicePackageService
    {
        public EfServicePackageService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}