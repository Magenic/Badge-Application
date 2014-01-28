using Csla.Core;
using Csla.Rules;
using Magenic.BadgeApplication.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public sealed class DefaultBadgeStatus : BusinessRule
    {
        private string StatusName;
        private string ApprovedByIdName;

        public DefaultBadgeStatus(IPropertyInfo badgeTypeProperty, IPropertyInfo badgeStatusProperty, IPropertyInfo approvedByIdProperty)
            : base(badgeTypeProperty)
        {
            if (badgeTypeProperty == null)
            {
                throw new ArgumentException("badgeIdProperty cannot be null.");
            }
            if (badgeStatusProperty == null)
            {
                throw new ArgumentException("badgeStatusProperty cannot be null.");
            }
            if (approvedByIdProperty == null)
            {
                throw new ArgumentException("approvedByIdProperty cannot be null.");
            }
            if (badgeTypeProperty.Type != typeof(BadgeType))
            {
                throw new ArgumentException("badgeTypeProperty must be a Common.Enums.BadgeType.");
            }
            if (badgeStatusProperty.Type != typeof(BadgeStatus))
            {
                throw new ArgumentException("badgeStatusProperty must be a Common.Enums.BadgeStatus.");
            }
            if (approvedByIdProperty.Type != typeof(int))
            {
                throw new ArgumentException("approvedByIdProperty must be an int.");
            }

            this.StatusName = badgeStatusProperty.Name;
            this.ApprovedByIdName = approvedByIdProperty.Name;
            this.InputProperties = new List<IPropertyInfo> { badgeTypeProperty, badgeStatusProperty, approvedByIdProperty };
            this.AffectedProperties.Add(badgeStatusProperty);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected override void Execute(RuleContext context)
        {
            var badgeTypeValue = (BadgeType)context.InputPropertyValues[PrimaryProperty];
            var badgeStatusProperty = this.InputProperties.Single(p => p.Name == this.StatusName);
            var badgeStatusValue = (BadgeStatus)context.InputPropertyValues[badgeStatusProperty];
            var approvedByIdProperty = this.InputProperties.Single(p => p.Name == this.ApprovedByIdName);
            var approvedByIdValue = (int)context.InputPropertyValues[approvedByIdProperty];

            if (approvedByIdValue == 0 && (badgeStatusValue == BadgeStatus.AwaitingApproval
                || badgeStatusValue == BadgeStatus.Approved || badgeStatusValue == BadgeStatus.Unset))
            {
                context.AddOutValue(badgeStatusProperty,
                    badgeTypeValue == BadgeType.Community ? BadgeStatus.AwaitingApproval : BadgeStatus.Approved);
            }
        }
    }
}
