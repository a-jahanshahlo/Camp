using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comps.ServiceLayer.Interfaces.ExtendInterface
{
    public interface ICheckTypeService
    {
        bool IsValidType(string filefame);
        bool IsValidContent(string contentType);
    }
}
