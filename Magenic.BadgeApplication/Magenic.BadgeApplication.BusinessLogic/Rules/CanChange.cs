using Csla;
using Csla.Core;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public sealed class CanChange : Csla.Rules.AuthorizationRule
    {
        private string AllowedRole { get; set; }

        public CanChange(Csla.Rules.AuthorizationActions authorizationAction, string allowedRole)
            : base(authorizationAction)
        {
            AllowedRole = allowedRole;
        }

        protected override void Execute(Csla.Rules.AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            var badgeStatus = context.Target as IHaveBadgeStatus;
            var validStatus = true;
            if (badgeStatus != null)
            {
                validStatus = (badgeStatus.BadgeStatus == BadgeStatus.AwaitingApproval);
            }

            context.HasPermission = ApplicationContext.User.IsInRole(AllowedRole)
                || (((ICreateEmployee)context.Target).CreateEmployeeId == ((ICustomPrincipal)ApplicationContext.User).CustomIdentity().EmployeeId && validStatus);
        }
    }
}
