using System.Diagnostics;
using Csla;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public class CanSetBadgeType : Csla.Rules.AuthorizationRule
    {
        private string AllowedRole { get; set; }
        private Common.Enums.BadgeType BadgeType { get; set; }

        public CanSetBadgeType(Csla.Rules.AuthorizationActions action, IMemberInfo element, Common.Enums.BadgeType badgeType, string allowedRole)
            : base(action, element)
        {
            if (element == null || !(element is IPropertyInfo))
            {
                throw new ArgumentException("Parameter element must be of type IPropertyInfo.");
            }
            
            AllowedRole = allowedRole;
            BadgeType = badgeType;
        }

        protected override void Execute(Csla.Rules.AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }
            context.HasPermission = ((IBadgeEdit)context.Target).Type != BadgeType || ApplicationContext.User.IsInRole(AllowedRole);
        }
    }
}
