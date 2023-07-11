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
        /// <summary>
        /// 回复地址
        /// 字符串 true 或者 false
        /// </summary>
        [JsonPropertyName("replyToAddress")]
        public string ReplyToAddress { get; set; } 
        /// <summary>
        /// 目标地址
        /// </summary>
        [JsonPropertyName("toAddress")]
        public string ToAddress { get; set; }
        /// <summary>
        /// 邮件驻地
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        /// <summary>
        /// 邮件HTML正文
        /// </summary>
        [JsonPropertyName("htmlBody")]
        public string HtmlBody { get; set; }
        /// <summary>
        /// 邮件Text正文
        /// </summary>
        [JsonPropertyName("textBody")]
        public string TextBody { get; set; }
        /// <summary>
        /// 发信昵称
        /// </summary>
        [JsonPropertyName("fromAlias")]
        public string FromAlias { get; set; }
        /// <summary>
        /// 回信地址
        /// </summary>
        [JsonPropertyName("replyAddress")]
        public string ReplyAddress { get; set; }
        /// <summary>
        /// 回信地址昵称
        /// </summary>
        [JsonPropertyName("replyAddressAlias")]
        public string ReplyAddressAlias { get; set; }
        /// <summary>
        /// 数据追踪
        /// 1，0
        /// </summary>
        [JsonPropertyName("clickTrace")]
        public string ClickTrace { get; set; }
    }
}
