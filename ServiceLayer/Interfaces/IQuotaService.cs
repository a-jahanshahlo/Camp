using System.Linq;
using Comps.DomainLayer;

namespace Comps.ServiceLayer.Interfaces
{
    public interface IQuotaService : IGenericService<Quota>
    {
        IQueryable<Quota> GetMyDeptQuota(string userId);
        bool UpdateConfirmQuota(string userId, int id, Quota quota);
        bool Refuse(string userId, int id);
    }
}