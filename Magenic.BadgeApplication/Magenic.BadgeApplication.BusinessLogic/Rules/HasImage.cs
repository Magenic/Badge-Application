using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public sealed class HasImage: BusinessRule
    {
        private IPropertyInfo BadgeImageProperty;
        public HasImage(IPropertyInfo badgePathProperty, IPropertyInfo badgeImageProperty) : base(badgePathProperty)
        {
            if (badgePathProperty == null)
            {
                throw new ArgumentException("badgePathProperty cannot be null.");
            }
            if (badgeImageProperty == null)
            {
                throw new ArgumentException("badgeImageProperty cannot be null.");
            }
            if (badgePathProperty.Type != typeof(string))
            {
                throw new ArgumentException("badgePathProperty must be a string.");
            }
            if (badgeImageProperty.Type != typeof(byte[]))
            {
                throw new ArgumentException("badgeImageProperty must be a byte[].");
            }

            this.BadgeImageProperty = badgeImageProperty;
            this.InputProperties = new List<IPropertyInfo> { badgePathProperty, BadgeImageProperty };
        }

        protected override void Execute(RuleContext context)
        {
            var badgePath = (string)context.InputPropertyValues[PrimaryProperty];
            var badgeImage = (byte[])context.InputPropertyValues[this.BadgeImageProperty];

            if (badgePath == string.Empty && (badgeImage == null || badgeImage.Length == 0))
            {
                context.AddErrorResult("There must be an image set for this badge.");
            }
        }
    }
}
