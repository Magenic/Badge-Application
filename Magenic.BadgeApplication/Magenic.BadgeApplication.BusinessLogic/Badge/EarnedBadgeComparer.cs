using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    public enum Direction { ASC, DESC };

    public class EarnedBadgeComparer : IComparer<IEarnedBadgeItem>
    {
        private const string BadgeName = "Name";
        private const string EmployeeName = "EmployeeName";
        private const string BadgeEffectiveEnd = "BadgeEffectiveEnd";
        private const string AwardDate = "AwardDate";
        private readonly string fieldName;
        private readonly Direction sortDirection;

        public EarnedBadgeComparer(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                this.fieldName = BadgeName;
                this.sortDirection = Direction.ASC;
                return;
            }

            var values = sort.Split(' ');

            switch (values[0])
            {
                case BadgeName:
                    this.fieldName = BadgeName;
                    break;

                case EmployeeName:
                    this.fieldName = EmployeeName;
                    break;

                case BadgeEffectiveEnd:
                    this.fieldName = BadgeEffectiveEnd;
                    break;

                case AwardDate:
                    this.fieldName = AwardDate;
                    break;

                default:
                    this.fieldName = BadgeName;
                    break;
            }

            var direction = values.Length > 1 ? values[1] : "";

            if (!Enum.TryParse<Direction>(direction, out this.sortDirection))
            {
                this.sortDirection = Direction.ASC;
            }
        }

        public int Compare(IEarnedBadgeItem x, IEarnedBadgeItem y)
        {
            return this.sortDirection == Direction.ASC ? CompareASC(x, y) : CompareDESC(x, y);
        }

        private int CompareASC(IEarnedBadgeItem item1, IEarnedBadgeItem item2)
        {
            if (this.fieldName == BadgeName)
            {
                return string.Compare(item1.Name, item2.Name, StringComparison.CurrentCultureIgnoreCase);
            }

            if (this.fieldName == EmployeeName)
            {
                return string.Compare(item1.EmployeeName, item2.EmployeeName, StringComparison.CurrentCultureIgnoreCase);
            }

            if (this.fieldName == BadgeEffectiveEnd)
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

            if (this.fieldName == AwardDate)
            {
                return DateTime.Compare(item1.AwardDate, item2.AwardDate);
            }

            return 0;
        }

        private int CompareDESC(IEarnedBadgeItem item1, IEarnedBadgeItem item2)
        {
            if (this.fieldName == BadgeName)
            {
                return CompareToReverse(string.Compare(item1.Name, item2.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            if (this.fieldName == EmployeeName)
            {
                return CompareToReverse(string.Compare(item1.EmployeeName, item2.EmployeeName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (this.fieldName == BadgeEffectiveEnd)
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

            if (this.fieldName == AwardDate)
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
