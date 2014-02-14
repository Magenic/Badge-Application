using Csla;
using Csla.Core;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public sealed class CanDelete : Csla.Rules.AuthorizationRule
    {
        private string AllowedRole { get; set; }

        public CanDelete(string allowedRole) : base(Csla.Rules.AuthorizationActions.DeleteObject)
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
