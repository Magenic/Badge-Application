using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerEmailAddresses
    {
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        public YammerEmailAddresses() { }

        public YammerEmailAddresses(string address, string type)
        {
            this.Address = address;
            this.Type = type;
        }
    }
}
