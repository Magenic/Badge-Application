using Csla;
using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using Magenic.BadgeApplication.BusinessLogic.Activity;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public class NoDuplicates : BusinessRule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public delegate bool CheckForDuplicates(string value);

        private CheckForDuplicates DuplicateCommand;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoDuplicates(IPropertyInfo primaryProperty, CheckForDuplicates duplicateCommand)
            : base(primaryProperty)
        {
            if (primaryProperty == null)
            {
                throw new ArgumentException("primaryProperty cannot be null.");
            }
            if (duplicateCommand == null)
            {
                throw new ArgumentException("duplicateCommand cannot be null.");
            }
            if (primaryProperty.Type != typeof(string))
            {
                throw new ArgumentException("primaryProperty must be a string.");
            }

            this.InputProperties = new List<IPropertyInfo> { PrimaryProperty };
            this.DuplicateCommand = duplicateCommand;
            this.IsAsync = true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected async override void Execute(RuleContext context)
        {
            var primaryPropertyValue = (string)context.InputPropertyValues[PrimaryProperty];
            var isNew = ((IBusinessBase)context.Target).IsNew;

            try
            {
                if (isNew)
                {
                    if (await ActivityNameExists.NameAlreadyExistsAsync(primaryPropertyValue))
                    {
                        context.AddErrorResult(string.Format("The Name {0} already exists.", primaryPropertyValue));
                    }
                }
            }
            finally
            {
                context.Complete();
            }
        }
    }
}
