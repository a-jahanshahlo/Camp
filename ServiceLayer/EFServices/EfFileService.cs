using System.Collections.Generic;
using System.IO;
using Camps.CommonLib.ExtentionMethods;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;

namespace Comps.ServiceLayer.EFServices
{
    public class EfFileService : EfGenericService<Comps.DomainLayer.Binary>, IFileService
    {
        private readonly IList<string> _validType;
        public EfFileService(IUnitOfWork uow)
            : base(uow)
        {
            _validType = new List<string> { ".jpeg", ".gif", ".png", ".jpg", ".avi", ".mp4", ".wmv", ".mp3" };

        }

        public bool IsValidType(string filefame)
        {
            return _validType.Contains(Path.GetExtension(filefame.ToLower()));
        }
        public bool IsValidContent(string filefame)
        {
            filefame = filefame.ToLower();
            if (filefame.IsAudio())
            {
                return true;
            }
            else if (filefame.IsImage())
            {
                return true;
            }
            else if (filefame.IsVideo())
            {
                return true;
            }

            return false;
        }
    }
}