using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerMessageContent
    {
        [DataMember(Name = "parsed")]
        public string ParsedText { get; set; }

        [DataMember(Name = "plain")]
        public string PlainText { get; set; }

        [DataMember(Name = "rich")]
        public string RichText { get; set; }
    }
}
