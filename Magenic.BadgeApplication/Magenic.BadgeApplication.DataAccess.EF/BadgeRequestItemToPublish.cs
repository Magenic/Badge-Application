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
    
    public partial class BadgeRequestItemToPublish
    {
        public int BadgeRequestId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ADName { get; set; }
        public string BadgeName { get; set; }
        public string BadgeDescription { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> NotifySentDate { get; set; }
    }
}
