using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.EFServices;

namespace Comps.ServiceLayer.AppServices
{
    public interface IAppSettingService:IXmlRepo<AppSetting>
    {
    }

}
