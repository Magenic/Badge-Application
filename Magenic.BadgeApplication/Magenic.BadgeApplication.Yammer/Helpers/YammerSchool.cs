using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerSchool
    {
        [DataMember(Name = "degree")]
        public string Degree { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "end_year")]
        public string EndYear { get; set; }

        [DataMember(Name = "start_year")]
        public string StartYear { get; set; }

        [DataMember(Name = "school")]
        public string School { get; set; }
    }
}
