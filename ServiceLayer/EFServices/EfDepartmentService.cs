using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfDepartmentService : EfGenericService<Department>, IDepartmentService
    {
        public EfDepartmentService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}