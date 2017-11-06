using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfDepartmentDeptRoleService : EfGenericService<DepartmentDeptRole>, IDepartmentDeptRoleService
    {
        public EfDepartmentDeptRoleService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}