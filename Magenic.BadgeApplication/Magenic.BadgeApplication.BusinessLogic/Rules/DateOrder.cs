using System.Globalization;
using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public class DateOrder : BusinessRule
    {
        private IPropertyInfo EndDateProperty;
        public DateOrder(IPropertyInfo startDateProperty, IPropertyInfo endDateProperty)
            : base(startDateProperty)
        {
            CheckDate(startDateProperty, true);
            CheckDate(endDateProperty, false);

            EndDateProperty = endDateProperty;
            InputProperties = new List<IPropertyInfo> { startDateProperty, endDateProperty };
        }

        protected override void Execute(RuleContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null");
            }

            var startDateValue = GetValue(context, this.PrimaryProperty);
            var endDateValue = GetValue(context, this.EndDateProperty);
            if (startDateValue.HasValue && endDateValue.HasValue)
            {
                if (endDateValue < startDateValue)
                {
                    context.AddErrorResult("The start date must be before the end date.");
                }
            }
        }

        private static DateTime? GetValue(RuleContext context, IPropertyInfo propertyInfo)
        {
            DateTime? returnValue = null;
            if (propertyInfo.Type == typeof (DateTime))
            {
                returnValue = (DateTime)context.InputPropertyValues[propertyInfo];
            }
            else if (propertyInfo.Type == typeof (DateTime?))
            {
                returnValue = (DateTime?)context.InputPropertyValues[propertyInfo];
            }
            return returnValue;
        }

        private static void CheckDate(IPropertyInfo property, bool isStartDate)
        {
            var startDate = "startDateProperty";
            var endDate = "endDateProperty";

            if (property == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "{0} cannot be null.", isStartDate ? startDate : endDate));
            }
            if (property.Type != typeof(DateTime?) && property.Type != typeof(DateTime))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "{0} must be a datetime or nullable datetime.", isStartDate ? startDate : endDate));
            }
        }
    }
}
