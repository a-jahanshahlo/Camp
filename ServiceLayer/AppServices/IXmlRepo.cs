using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Camps.CommonLib;

namespace Comps.ServiceLayer.AppServices
{
    public interface IXmlRepo<T>
    {
        T Get();
        void Save(T value);
        T ThisObj { get; set; }

    }
}
