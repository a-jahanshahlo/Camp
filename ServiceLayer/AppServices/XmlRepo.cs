using System.IO;
using System.Web;
using Camps.CommonLib.ExtentionMethods;
using Comps.ServiceLayer.IOServices;

namespace Comps.ServiceLayer.AppServices
{
    public class XmlRepo<T> : IXmlRepo<T>
    {
        public virtual string Path { get { return HttpContext.Current.Server.MapPath("/AppConfig/"); } }

        private readonly IInOutBinaryService _inOutBinaryService;

        public XmlRepo(T t)
        {
            _inOutBinaryService = new InOutBinaryService();
            // object obj =tt ;
            //var serialize = obj.Serialize();
            ThisObj = t;

        }
        public virtual T Get()
        {
            if (!ExistFile(FullPath()))
                CreateFile(FullPath());

            return GetCache();
        }

        private bool IsInCache
        {
            get { return HttpContext.Current.Cache[ThisObj.GetType().ToString()] != null; }
        }
        private void RemoveCache()
        {
            HttpContext.Current.Cache.Remove(ThisObj.GetType().ToString());
        }
        private void AddToCache()
        {
            HttpContext.Current.Cache[ThisObj.GetType().ToString()] = ThisObj.DeSerialize(FullPath());
        }
        private T GetCache()
        {
            if (!IsInCache)
            {
                AddToCache();
            }

            ThisObj = (T)HttpContext.Current.Cache[ThisObj.GetType().ToString()];
            return ThisObj;
        }
        public void Save(T value)
        {
            RemoveCache();

            var filepath = FullPath();
            string objcontent = ThisObj.Serialize();
            _inOutBinaryService
                .StreamWriterEx
                .WriteStream(filepath)
                .Write(objcontent)
                .Close()
                .Dispose();

            ThisObj = GetCache();
        }

        private bool CreateFile(string fullpath)
        {
            var filepath = fullpath;
            string objcontent = ThisObj.Serialize();
            _inOutBinaryService
                .StreamWriterEx
                .WriteStream(filepath)
                .Write(objcontent)
                .Close()
                .Dispose();

            return true;
        }
        public T ThisObj { get; set; }
        private bool ExistFile(string path)
        {
            return File.Exists(path);
        }

        private string FullPath()
        {
            return System.IO.Path.Combine(Path + FileName());
        }
        private string FileName()
        {
            return ThisObj.GetType() + ".xml";
        }
    }
}