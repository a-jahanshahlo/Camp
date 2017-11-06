using System.Linq;
using System.Web.UI.WebControls;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfFestivalService : EfGenericService<Festival>, IFestivalService
    {
        public EfFestivalService(IUnitOfWork uow)
            : base(uow)
        {

        }

        public bool IsValid(Festival entity)
        {

            if (entity==null)
            {
                Errors.Add("NullReference","رکورد تهی است");
                return false;
            }
            if (Entities.Any(x=>x.FestivalTitle.Contains(entity.FestivalTitle.Trim())))
            {
                Errors.Add("DuplicateTitle", "جشنواره ای با این عنوان ثبت شده است");
                return false;
            }

            return true;
        }
    }
}