using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Gender : DelEntity
    {
        public Gender()
        {
            this.UserInfos = new List<UserInfo>();
        }
        public string Name { get; set; }
        public virtual ICollection<UserInfo> UserInfos { get; set; }
    }
}