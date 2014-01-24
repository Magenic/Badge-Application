using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerAttachment
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        [DataMember(Name = "inline_url")]
        public string InlineUrl { get; set; }

        [DataMember(Name = "liked_by")]
        public YammerLikes Likes { get; set; }

        public YammerAttachment()
        {
            this.Likes = new YammerLikes();
        }
    }
}
