using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerLikes
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "names")]
        public List<YammerLikeUser> Names { get; set; }

        public YammerLikes()
        {
            this.Names = new List<YammerLikeUser>();
        }
    }    
}
