using System.Text.Json.Serialization;

namespace XYS.Standard.Mail.AliCloud.Models.Request.Mail
{
    public class SingleSendMailRequest
    {
        /// <summary>
        /// 发信地址
        /// </summary>
        [JsonPropertyName("accountName")]
        public string AccountName { get; set; }
        /// <summary>
        /// 地址类型：0为随机账号，1为发信地址
        /// </summary>
        [JsonPropertyName("addressType")]
        public string AddressType { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [JsonPropertyName("tagName")]
        public string TagName { get; set; }
        [JsonPropertyName("replyToAddress")]
        public string ReplyToAddress { get; set; }
        [JsonPropertyName("toAddress")]
        public string ToAddress { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("htmlBody")]
        public string HtmlBody { get; set; }
        [JsonPropertyName("testBody")]
        public string TestBody { get; set; }
        [JsonPropertyName("fromAlias")]
        public string FromAlias { get; set; }
        [JsonPropertyName("replyAddress")]
        public string ReplyAddress { get; set; }
        [JsonPropertyName("replyAddressAlias")]
        public string ReplyAddressAlias { get; set; }
        [JsonPropertyName("clickTrace")]
        public string ClickTrace { get; set; }
    }
}
