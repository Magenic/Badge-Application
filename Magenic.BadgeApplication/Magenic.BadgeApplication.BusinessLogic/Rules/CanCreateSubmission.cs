using Csla;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public sealed class CanCreateSubmission : Csla.Rules.AuthorizationRule
    {
        public CanCreateSubmission(Csla.Rules.AuthorizationActions authorizationAction)
            : base(authorizationAction)
        {

        }

        protected override void Execute(Csla.Rules.AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            var entryType = context.Target as IHaveEntryType;
            context.HasPermission = false;

            if (entryType == null)
            {
                context.HasPermission = true;
            }
            else
            {
                if (entryType.EntryType == ActivityEntryType.Any)
                {
                    context.HasPermission = true;
                }
                else
                {
                    var role = entryType.EntryType.ToString();
                    if (entryType.EntryType != ActivityEntryType.Unset)
                    {
                        context.HasPermission = ApplicationContext.User.IsInRole(role);
                    }
                }
            }

            if (ApplicationContext.User.IsInRole(PermissionType.Administrator.ToString()))
            {
                context.HasPermission = true;
            }
        }
    }
}
