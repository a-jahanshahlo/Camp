using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfGenderService : EfGenericService<Gender>, IGenderService
    {
        public EfGenderService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}