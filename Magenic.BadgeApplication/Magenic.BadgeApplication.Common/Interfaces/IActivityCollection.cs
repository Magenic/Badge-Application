﻿using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read-only list of activity information.
    /// </summary>
    public interface IActivityCollection : IReadOnlyListBase<IActivityItem>
    {
    }
}
