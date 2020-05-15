﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A user in the system.
    /// </summary>
    public interface IUserItem : IReadOnlyBase
    {
        /// <summary>
        /// The employee Id of the person.
        /// </summary>
        int EmployeeId { get; }
        /// <summary>
        /// The user's first name as reported by AD.
        /// </summary>
        string EmployeeFirstName { get; }
        /// <summary>
        /// The user's last name.
        /// </summary>
        string EmployeeLastName { get; }
        /// <summary>
        /// The user's full name
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// The user's email address.
        /// </summary>
        string EmployeeEmailAddress { get; }
    }
}
