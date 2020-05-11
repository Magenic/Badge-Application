using System;
using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    public class EarnedBadgeSortBy
    {
        private readonly string fieldName;
        private readonly SortDirection direction;

        public static string BuildString(string fieldName, SortDirection direction)
        {
            return fieldName + " " + direction.ToString();
        }

        public EarnedBadgeSortBy(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                this.fieldName = EarnedBadgeFields.BadgeName;
                this.direction = SortDirection.ASC;
                return;
            }

            var values = sort.Split(' ');

            switch (values[0])
            {
                case EarnedBadgeFields.BadgeName:
                    this.fieldName = EarnedBadgeFields.BadgeName;
                    break;

                case EarnedBadgeFields.EmployeeName:
                    this.fieldName = EarnedBadgeFields.EmployeeName;
                    break;

                case EarnedBadgeFields.BadgeEffectiveEnd:
                    this.fieldName = EarnedBadgeFields.BadgeEffectiveEnd;
                    break;

                case EarnedBadgeFields.AwardDate:
                    this.fieldName = EarnedBadgeFields.AwardDate;
                    break;

                default:
                    this.fieldName = EarnedBadgeFields.BadgeName;
                    break;
            }

            var direction = values.Length > 1 ? values[1] : "";

            if (!Enum.TryParse<SortDirection>(direction, out this.direction))
            {
                this.direction = SortDirection.ASC;
            }
        }

        public bool IsAscending()
        {
            return this.direction == SortDirection.ASC;
        }

        public bool SortByBadgeName()
        {
            return fieldName == EarnedBadgeFields.BadgeName;
        }

        public bool SortByEmployeeName()
        {
            return fieldName == EarnedBadgeFields.EmployeeName;
        }

        public bool SortByBadgeEffectiveEnd()
        {
            return fieldName == EarnedBadgeFields.BadgeEffectiveEnd;
        }

        public bool SortByBadgeAwardDate()
        {
            return fieldName == EarnedBadgeFields.AwardDate;
        }
    }
}
