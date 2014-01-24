using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerIM
    {
        [DataMember(Name = "provider")]
        public string Provider { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }
    }
}
