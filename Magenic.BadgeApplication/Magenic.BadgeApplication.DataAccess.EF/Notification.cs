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
    
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int ActivitySubmissionId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int NotificationStatusId { get; set; }
        public Nullable<System.DateTime> NotificationSentDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}