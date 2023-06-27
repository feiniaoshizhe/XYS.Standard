using System.Net;

namespace XYS.Standard.Mail.AliCloud
{
    public class AliCloudException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ErrorResponse Error { get; set; }
        public AliCloudException(ErrorResponse error, HttpStatusCode statusCode)
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}
