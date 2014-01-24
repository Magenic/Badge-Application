using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerUser : SerializedJson<YammerUser>
    {
        [DataMember(Name = "id")]
        public string UserID { get; set; }

        [DataMember(Name = "network_id")]
        public string NetworkID { get; set; }

        [DataMember(Name = "state")]
        public string AccountStatus { get; set; }

        [DataMember(Name = "job_title")]
        public string JobTitle { get; set; }

        [DataMember(Name = "expertise")]
        public string Expertise { get; set; }
        
        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "url")]
        public string ApiUrl { get; set; }

        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        [DataMember(Name = "mugshot_url")]
        public string PhotoUrl { get; set; }

        [DataMember(Name = "mugshot_url_template")]
        public string PhotoTemplateUrl { get; set; }

        [DataMember(Name = "department")]
        public string Department { get; set; }

        [DataMember(Name = "contact")]
        public YammerContactInfo ContactInfo { get; set; }

        [DataMember(Name = "web_preferences")]
        public YammerSettingsAndFeedsAndGroups SettingsAndFeedsAndGroups { get; set; }

        [DataMember(Name = "previous_companies")]
        public List<YammerEmployer> PreviousEmployers { get; set; }

        [DataMember(Name = "schools")]
        public List<YammerSchool> Schools { get; set; }

        [DataMember(Name = "stats")]
        public YammerUserStats UserStats { get; set; }

        public YammerUser()
        {
            this.ContactInfo = new YammerContactInfo();
            this.SettingsAndFeedsAndGroups = new YammerSettingsAndFeedsAndGroups();
            this.PreviousEmployers = new List<YammerEmployer>();
            this.Schools = new List<YammerSchool>();
            this.UserStats = new YammerUserStats();
        }
    }    
}
