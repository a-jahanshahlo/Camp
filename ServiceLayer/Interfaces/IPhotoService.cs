using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces.ExtendInterface;

namespace Comps.ServiceLayer.Interfaces
{
    public interface IFileService : IGenericService<Binary>, ICheckTypeService
    { }


}