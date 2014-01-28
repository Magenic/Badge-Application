using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerPhoneNumbers
    {
        [DataMember(Name = "number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
