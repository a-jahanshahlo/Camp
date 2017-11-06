using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPackageGradeService : EfGenericService<PackageGrade>, IPackageGradeService
    {
        public EfPackageGradeService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}