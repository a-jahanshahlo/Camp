using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Camps.Contract
{
    [DataContract]
    public enum AccountTypeEnum
    {
        [Description("مسافر")]
        [EnumMember]
        Passenger = 0,
        [Description("راهبر")]
        [EnumMember]
        Agent = 1,
        [EnumMember]
        [Description("مدیر سیستم")]
        Admin = 2,
        [EnumMember]
        [Description("مدیر واحد")]
        Boss = 3
    }
}
