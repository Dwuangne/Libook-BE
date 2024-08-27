using System.Net;

namespace Libook_API.Models.Response
{
    public class ResponseObject
    {
        public HttpStatusCode status { get; set; }
        public String message { get; set; }
        public Object data{ get; set; }
    }
}
