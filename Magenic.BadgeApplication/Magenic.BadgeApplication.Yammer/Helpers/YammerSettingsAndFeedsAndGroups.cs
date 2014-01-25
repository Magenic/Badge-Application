using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerSettingsAndFeedsAndGroups
    {
        [DataMember(Name = "network_settings")]
        public YammerNetworkSettings NetworkSettings { get; set; }

        [DataMember(Name = "home_tabs")]
        public List<YammerGroupsAndFeeds> GroupsAndFeeds { get; set; }

        public YammerSettingsAndFeedsAndGroups()
        {
            this.NetworkSettings = new YammerNetworkSettings();
            this.GroupsAndFeeds = new List<YammerGroupsAndFeeds>();
        }
    }
}
