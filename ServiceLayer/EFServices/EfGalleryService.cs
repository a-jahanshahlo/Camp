using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfGalleryService : EfGenericService<Gallery>, IGalleryService
    {
        public EfGalleryService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}