using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerGroupsAndFeeds
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "select_name")]
        public string SelectName { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "feed_description")]
        public string Description { get; set; }

        [DataMember(Name = "ordering_index")]
        public int OrderingIndex { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "group_id")]
        public string GroupID { get; set; }

        [DataMember(Name = "private")]
        public bool IsPrivate { get; set; }
    }
}
