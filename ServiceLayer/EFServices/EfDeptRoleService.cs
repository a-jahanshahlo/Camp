using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfDeptRoleService : EfGenericService<DeptRole>, IDeptRoleService
    {
        public EfDeptRoleService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}