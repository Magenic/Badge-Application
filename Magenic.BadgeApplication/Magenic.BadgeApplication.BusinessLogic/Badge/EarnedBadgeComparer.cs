using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    public class EarnedBadgeComparer : IComparer<IEarnedBadgeItem>
    {
        private readonly EarnedBadgeSortBy SortBy;

        public EarnedBadgeComparer(EarnedBadgeSortBy sortBy)
        {
            this.SortBy = sortBy ?? new EarnedBadgeSortBy(string.Empty);
        }

        public int Compare(IEarnedBadgeItem x, IEarnedBadgeItem y)
        {
            return this.SortBy.IsAscending() ? CompareASC(x, y) : CompareDESC(x, y);
        }

        private int CompareASC(IEarnedBadgeItem item1, IEarnedBadgeItem item2)
        {
            if (this.SortBy.SortByBadgeName())
            {
                return string.Compare(item1.Name, item2.Name, StringComparison.CurrentCultureIgnoreCase);
            }

            if (this.SortBy.SortByEmployeeName())
            {
                return string.Compare(item1.EmployeeName, item2.EmployeeName, StringComparison.CurrentCultureIgnoreCase);
            }

            if (this.SortBy.SortByBadgeEffectiveEnd())
            {
                if (item1.BadgeEffectiveEnd.HasValue && item2.BadgeEffectiveEnd.HasValue)
                {
                    return DateTime.Compare(item1.BadgeEffectiveEnd.Value, item2.BadgeEffectiveEnd.Value);
                }

                if (!item1.BadgeEffectiveEnd.HasValue && item2.BadgeEffectiveEnd.HasValue)
                {
                    return -1;
                }

                if (item1.BadgeEffectiveEnd.HasValue && !item2.BadgeEffectiveEnd.HasValue)
                {
                    return 1;
                }
            }

            if (this.SortBy.SortByBadgeAwardDate())
            {
                return DateTime.Compare(item1.AwardDate, item2.AwardDate);
            }

            return 0;
        }

        private int CompareDESC(IEarnedBadgeItem item1, IEarnedBadgeItem item2)
        {
            if (this.SortBy.SortByBadgeName())
            {
                return CompareToReverse(string.Compare(item1.Name, item2.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            if (this.SortBy.SortByEmployeeName())
            {
                return CompareToReverse(string.Compare(item1.EmployeeName, item2.EmployeeName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (this.SortBy.SortByBadgeEffectiveEnd())
            {
                if (item1.BadgeEffectiveEnd.HasValue && item2.BadgeEffectiveEnd.HasValue)
                {
                    return CompareToReverse(DateTime.Compare(item1.BadgeEffectiveEnd.Value, item2.BadgeEffectiveEnd.Value));
                }

                if (!item1.BadgeEffectiveEnd.HasValue && item2.BadgeEffectiveEnd.HasValue)
                {
                    return 1;
                }

                if (item1.BadgeEffectiveEnd.HasValue && !item2.BadgeEffectiveEnd.HasValue)
                {
                    return -1;
                }
            }

            if (this.SortBy.SortByBadgeAwardDate())
            {
                return CompareToReverse(DateTime.Compare(item1.AwardDate, item2.AwardDate));
            }

            return 0;
        }

        private int CompareToReverse(int value)
        {
            var reverseValue = value == -1 ? 1 : -1;
            return value == 0 ? 0 : reverseValue;
        }
    }
}
