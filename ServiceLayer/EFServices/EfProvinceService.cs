using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfProvinceService : EfGenericService<Province>, IProvinceService
    {
        public EfProvinceService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}