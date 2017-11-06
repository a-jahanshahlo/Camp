using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfCampService : EfGenericService<Camp>, ICampService
    {
        public EfCampService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}