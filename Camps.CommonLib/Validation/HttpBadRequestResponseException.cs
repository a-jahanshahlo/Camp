using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Camps.CommonLib.Validation
{
    public sealed class HttpBadRequestResponseException : HttpResponseException
    {
        public HttpBadRequestResponseException()
            : this(string.Empty)
        {
        }

        public HttpBadRequestResponseException(string message)
            : base(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(message) })
        {
        }
    }
}