using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfStockService : EfGenericService<Stock>, IStockService
    {
        public EfStockService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}