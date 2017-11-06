using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPositionService : EfGenericService<UserInDeptRole>, IPositionService
    {
        public EfPositionService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}