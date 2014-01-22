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
    public class DefaultActivityStatus : BusinessRule 
    {
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

            this.InputProperties = new List<IPropertyInfo> { activityIdProperty, activityStatusProperty, approvedByIdProperty };
            this.AffectedProperties.Add(activityStatusProperty);
            this.IsAsync = true;
        }

        protected async override void Execute(RuleContext context)
        {
            try
            {
                var activityIdValue = (int)context.InputPropertyValues[PrimaryProperty];
                var activityStatusProperty = this.InputProperties.Single(p => p.Name == "Status");
                var activtyStatusValue = (ActivitySubmissionStatus)context.InputPropertyValues[activityStatusProperty];
                var approvedByIdProperty = this.InputProperties.Single(p => p.Name == "ApprovedById");
                var approvedByIdValue = (ActivitySubmissionStatus)context.InputPropertyValues[approvedByIdProperty];

                if (approvedByIdValue == 0
                    && (activtyStatusValue == ActivitySubmissionStatus.Unset
                    || activtyStatusValue == ActivitySubmissionStatus.AwaitingApproval
                    || activtyStatusValue == ActivitySubmissionStatus.Approved))
                {
                    try
                    {
                        var activity = await IoC.Container.Resolve<IObjectFactory<IActivityEdit>>().FetchAsync(activityIdValue);
                        context.AddOutValue(activityStatusProperty,
                            activity.RequiresApproval
                                ? ActivitySubmissionStatus.AwaitingApproval
                                : ActivitySubmissionStatus.Approved);
                    }
                    catch (Exception)
                    {
                        context.AddErrorResult(PrimaryProperty,
                            string.Format("Activity id {0} was not able to be retrieved.", activityIdValue));
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