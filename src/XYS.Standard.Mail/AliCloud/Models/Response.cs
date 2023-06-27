using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XYS.Standard.Mail.AliCloud.Models
{
    public abstract class Response
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
    }

    public abstract class PagerResponse : Response
    {
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
    }
}
