//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Magenic.BadgeApplication.DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Badge
    {
        public Badge()
        {
            this.BadgeActivities = new HashSet<BadgeActivity>();
            this.BadgeAwards = new HashSet<BadgeAward>();
            this.BadgePrerequisites = new HashSet<BadgePrerequisite>();
            this.BadgePrerequisites1 = new HashSet<BadgePrerequisite>();
        }
    
        public int BadgeId { get; set; }
        public string BadgeName { get; set; }
        public string BadgeTagLine { get; set; }
        public string BadgeDescription { get; set; }
        public int BadgeTypeId { get; set; }
        public string BadgePath { get; set; }
        public System.DateTime BadgeCreated { get; set; }
        public Nullable<System.DateTime> BadgeEffectiveStart { get; set; }
        public Nullable<System.DateTime> BadgeEffectiveEnd { get; set; }
        public int BadgePriority { get; set; }
        public bool MultipleAwardPossible { get; set; }
        public bool DisplayOnce { get; set; }
        public bool ManagementApprovalRequired { get; set; }
        public int ActivityPointsAmount { get; set; }
        public int BadgeAwardValueAmount { get; set; }
        public Nullable<int> BadgeApprovedById { get; set; }
        public Nullable<System.DateTime> BadgeApprovedDate { get; set; }
    
        public virtual BadgeType BadgeType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<BadgeActivity> BadgeActivities { get; set; }
        public virtual ICollection<BadgeAward> BadgeAwards { get; set; }
        public virtual ICollection<BadgePrerequisite> BadgePrerequisites { get; set; }
        public virtual ICollection<BadgePrerequisite> BadgePrerequisites1 { get; set; }
    }
}
