using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;
 

namespace Comps.ServiceLayer.EFServices
{
    public class EfBillService : EfGenericService<Bill>, IBillService
    {
        public EfBillService(IUnitOfWork uow)
            : base(uow)
        {

        }

        public override bool IsValid(Bill entity)
        {
          
            if (entity ==null)
            {
                this.Errors.Add("Entity","The is null");
                return false;
               
            }
            return base.IsValid(entity);
        }
    }
}