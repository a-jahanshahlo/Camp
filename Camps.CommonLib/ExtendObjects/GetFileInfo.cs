using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Camps.CommonLib.ExtendObjects
{
    public static class GetFileInfo
    {
        public static   string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public static string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
        public static string GetDeserializedFileName(HttpContent fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }
        public static MediaTypeHeaderValue GetContentType(HttpContent fileData)
        {

            return fileData.Headers.ContentType;
        }
        public static string GetFileName(HttpContent fileData)
        {
           
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}