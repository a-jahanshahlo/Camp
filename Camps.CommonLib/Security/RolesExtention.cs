using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Camps.Contract;

namespace Camps.CommonLib.Security
{

    public static class EnumHelper
    {

        public static string GetDescription<T>(this T source) where T : struct
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
        public static string GetAttributeDescription(this AccountTypeEnum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<DescriptionAttribute>();

            return attribute == null ? String.Empty : attribute.Description;
        }
        //public static string GetAttributeName(this ExpDataEnum enumValue)
        //{
        //    var attribute = enumValue.GetAttributeOfType<EnumDisplayNameAttribute>();

        //    return attribute == null ? String.Empty : attribute.EnumDisplayName;
        //}
 

    }

 
 
}
