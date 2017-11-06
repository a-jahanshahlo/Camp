using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfReservationService : EfGenericService<Reservation>, IReservationService
    {
        public EfReservationService(IUnitOfWork uow)
            : base(uow)
        {

        }

 
    }
}