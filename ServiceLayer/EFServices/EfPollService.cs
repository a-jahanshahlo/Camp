using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPollService : EfGenericService<Poll>, IPollService
    {
        public EfPollService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}