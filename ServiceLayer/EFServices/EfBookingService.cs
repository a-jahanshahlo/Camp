using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfBookingService : EfGenericService<Booking>, IBookingService
    {
        public EfBookingService(IUnitOfWork uow) : base(uow)
        {

        }
    }


    //public override void Add(InvalidCheck entity)
        //{
        //    EfCheckGroupService efCheckGroupService = new EfCheckGroupService(_uow);
        //    CheckGroup relatedCheckGroup = efCheckGroupService.Find(c => c.BeginSerial <= entity.CheckNo &&
        //        c.EndSerial >= entity.CheckNo);

        //    if (relatedCheckGroup==null)
        //    {
        //        throw new InvalidOperationException("دسته چک مربوط به چک " +
        //            entity.CheckNo.ToString()+ " یافت نشد");
        //    }
        //    entity.CheckGroupId = relatedCheckGroup.Id;

        //    base.Add(entity);
        //}
}