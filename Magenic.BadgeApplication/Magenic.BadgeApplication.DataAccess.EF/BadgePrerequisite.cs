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
    
    public partial class BadgePrerequisite
    {
        public int PrerequisiteID { get; set; }
        public Nullable<int> BadgeId { get; set; }
        public Nullable<int> RequiredBadgeId { get; set; }
    
        public virtual Badge Badge { get; set; }
        public virtual Badge Badge1 { get; set; }
    }
}