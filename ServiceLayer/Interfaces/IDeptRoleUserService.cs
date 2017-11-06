using Comps.DomainLayer;

namespace Comps.ServiceLayer.Interfaces
{
    public interface IUserInDeptRolesService : IGenericService<UserInDeptRole>
    {
        bool UpdateConfirmer(int userId, bool isConfirmer);
        bool IsConfirmer(string userId);
    }
}