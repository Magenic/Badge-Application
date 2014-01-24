using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerEmployer
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "employer")]
        public string Employer { get; set; }

        [DataMember(Name = "end_year")]
        public string EndYear { get; set; }

        [DataMember(Name = "position")]
        public string Position { get; set; }

        [DataMember(Name = "start_year")]
        public string StartYear { get; set; }
    }
}
