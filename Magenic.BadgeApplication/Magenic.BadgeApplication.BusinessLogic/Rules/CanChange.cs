using Csla;
using Csla.Core;
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

            context.HasPermission = ((ICreateEmployee)context.Target).CreateEmployeeId == ((ICustomPrincipal)ApplicationContext.User).CustomIdentity().EmployeeId 
                || ApplicationContext.User.IsInRole(AllowedRole);
        }
    }
}
