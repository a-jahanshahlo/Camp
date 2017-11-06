using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfCityService : EfGenericService<City>, ICityService
    {
        public EfCityService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}