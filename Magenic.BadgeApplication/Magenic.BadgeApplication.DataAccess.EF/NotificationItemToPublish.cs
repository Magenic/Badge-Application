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
    
    public partial class NotificationItemToPublish
    {
        public int NotificationId { get; set; }
        public int ActivitySubmissionId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int NotificationStatusId { get; set; }
        public Nullable<System.DateTime> NotificationSentDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int ActivityId { get; set; }
        public int EmployeeId { get; set; }
        public string SubmissionDescription { get; set; }
        public Nullable<int> SubmissionApprovedById { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public int SubmissionStatusId { get; set; }
        public Nullable<int> AwardValue { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ADName { get; set; }
    }
}