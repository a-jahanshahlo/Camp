using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfSuiteGradeService : EfGenericService<SuiteGrade>, ISuiteGradeService
    {
        public EfSuiteGradeService(IUnitOfWork uow)
            : base(uow)
        {

        }
    }
}