using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerContactInfo
    {
        [DataMember(Name = "has_fake_email")]
        public bool HasFakeEmail { get; set; }

        [DataMember(Name = "email_addresses")]
        public List<YammerEmailAddresses> EmailAddresses { get; set; }

        [DataMember(Name = "phone_numbers")]
        public List<YammerPhoneNumbers> PhoneNumbers { get; set; }

        [DataMember(Name = "im")]
        public YammerIM IM { get; set; }

        public YammerContactInfo()
        {
            this.EmailAddresses = new List<YammerEmailAddresses>();
            this.PhoneNumbers = new List<YammerPhoneNumbers>();
            this.IM = new YammerIM();
        }
    }
}
