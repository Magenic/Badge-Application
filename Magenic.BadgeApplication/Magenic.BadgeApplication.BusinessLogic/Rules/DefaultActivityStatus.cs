using System.Globalization;
using System.Threading.Tasks;
using Autofac;
using Csla.Core;
using Csla.Rules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Rules
{
    public sealed class DefaultActivityStatus : BusinessRule
    {
        private string StatusName;
        private string ApprovedByIdName;

        public DefaultActivityStatus(IPropertyInfo activityIdProperty, IPropertyInfo activityStatusProperty, IPropertyInfo approvedByIdProperty)
            : base(activityIdProperty)
        {
            if (activityIdProperty == null)
            {
                throw new ArgumentException("activityIdProperty cannot be null.");
            }
            if (activityStatusProperty == null)
            {
                throw new ArgumentException("activityStatusProperty cannot be null.");
            }
            if (approvedByIdProperty == null)
            {
                throw new ArgumentException("approvedByIdProperty cannot be null.");
            }
            if (activityIdProperty.Type != typeof(int))
            {
                throw new ArgumentException("activityIdProperty must be an int.");
            }
            if (activityStatusProperty.Type != typeof(ActivitySubmissionStatus))
            {
                throw new ArgumentException("activityStatusProperty must be a Common.Enums.ActivitySubmissionStatus.");
            }
            if (approvedByIdProperty.Type != typeof(int))
            {
                throw new ArgumentException("approvedByIdProperty must be a Common.Enums.ActivitySubmissionStatus.");
            }

            this.StatusName = activityStatusProperty.Name;
            this.ApprovedByIdName = approvedByIdProperty.Name;
            this.InputProperties = new List<IPropertyInfo> { activityIdProperty, activityStatusProperty, approvedByIdProperty };
            this.AffectedProperties.Add(activityStatusProperty);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected override void Execute(RuleContext context)
        {
            var activityIdValue = (int)context.InputPropertyValues[PrimaryProperty];
            var activityStatusProperty = this.InputProperties.Single(p => p.Name == this.StatusName);
            var activtyStatusValue = (ActivitySubmissionStatus)context.InputPropertyValues[activityStatusProperty];
            var approvedByIdProperty = this.InputProperties.Single(p => p.Name == this.ApprovedByIdName);
            var approvedByIdValue = (ActivitySubmissionStatus)context.InputPropertyValues[approvedByIdProperty];

            if (approvedByIdValue == 0
                && (activtyStatusValue == ActivitySubmissionStatus.Unset
                || activtyStatusValue == ActivitySubmissionStatus.AwaitingApproval
                || activtyStatusValue == ActivitySubmissionStatus.Approved))
            {
                try
                {
                    var activityTask = Task.Run(() => IoC.Container.Resolve<IObjectFactory<IActivityEdit>>().FetchAsync(activityIdValue));
                    var activity = activityTask.Result;
                    context.AddOutValue(activityStatusProperty,
                        activity.RequiresApproval
                            ? ActivitySubmissionStatus.AwaitingApproval
                            : ActivitySubmissionStatus.Approved);
                }
                catch (Exception)
                {
                    context.AddErrorResult(PrimaryProperty,
                        string.Format(CultureInfo.CurrentCulture, "Activity id {0} was not able to be retrieved.", activityIdValue));
                }
            }
        }
    }
}