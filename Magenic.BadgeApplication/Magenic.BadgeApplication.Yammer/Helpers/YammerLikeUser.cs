using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerLikeUser
    {
        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "permalink")]
        public string PermaLink { get; set; }

        [DataMember(Name = "user_id")]
        public string UserID { get; set; }
    }
}
