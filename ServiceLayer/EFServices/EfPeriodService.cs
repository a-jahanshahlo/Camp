using System;
using System.Linq;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfPeriodService : EfGenericService<Period>, IPeriodService
    {
        public EfPeriodService(IUnitOfWork uow)
            : base(uow)
        {

        }

        public bool IsValid(Period entity)
        {
            TimeSpan ts = new TimeSpan(24, 0, 0);
            entity.FromDate = entity.FromDate.Date + ts;
            entity.ToDate  = entity.ToDate .Date + ts;

            if (Entities.Any(x=>x.CampId==entity.CampId
                && x.FestivalId==entity.FestivalId
                && entity.FromDate <=x.ToDate
                ))
            {
                Errors.Add("Overlap Date","در این تاریخ دوره دیگری ثبت شده است");
                return false;
            }
            if ( entity.FromDate > entity.ToDate ) 
            {
                Errors.Add("Conflict Date", "تاریخ شروع نمی تواند از تاریخ پایان پزرگتر باشد");
                return false;
            }
            if (Entities.Any(x => x.CampId == entity.CampId
                            && x.FestivalId == entity.FestivalId
                             && x.PeriodTitle.Contains(entity.PeriodTitle.Trim())
                            ))
            {
                Errors.Add("Same Title", "دوره ای با این عنوان وجود دارد");
                return false;
            }
            return true;
        }
    }
}