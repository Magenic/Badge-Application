using System.Globalization;
using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public class NoDuplicates : BusinessRule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public delegate bool CheckForDuplicates(int currentId, string value);

        private CheckForDuplicates DuplicateCommand;
        private string IdPropertyName;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoDuplicates(IPropertyInfo primaryProperty, IPropertyInfo idProperty, CheckForDuplicates duplicateCommand)
            : base(primaryProperty)
        {
            if (primaryProperty == null)
            {
                throw new ArgumentException("primaryProperty cannot be null.");
            }
            if (idProperty == null)
            {
                throw new ArgumentException("idProperty cannot be null.");
            }
            if (duplicateCommand == null)
            {
                throw new ArgumentException("duplicateCommand cannot be null.");
            }
            if (primaryProperty.Type != typeof(string))
            {
                throw new ArgumentException("primaryProperty must be a string.");
            }
            if (idProperty.Type != typeof(int))
            {
                throw new ArgumentException("idProperty must be an int.");
            }

            this.IdPropertyName = idProperty.Name;
            this.InputProperties = new List<IPropertyInfo> { primaryProperty, idProperty };
            this.DuplicateCommand = duplicateCommand;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected override void Execute(RuleContext context)
        {
            var primaryPropertyValue = (string)context.InputPropertyValues[PrimaryProperty];
            var idProperty = this.InputProperties.Single(p => p.Name == this.IdPropertyName);
            var idValue = (int)context.InputPropertyValues[idProperty];

            if (DuplicateCommand(idValue, primaryPropertyValue))
            {
                context.AddErrorResult(string.Format(CultureInfo.CurrentCulture, "The value {0} already exists.", primaryPropertyValue));
            }
        }
    }
}