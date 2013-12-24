using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    /// <summary>
    /// Allows a manager to examine information about an activity they need to approve
    /// and then either approve or deny the activity.
    /// </summary>
    [Serializable]
    public class ApproveActivityItem : BusinessBase<ApproveActivityItem>, IApproveActivityItem
    {
        #region Properties

        public static readonly PropertyInfo<int> SubmissionIdProperty = RegisterProperty<int>(c => c.SubmissionId);
        public int SubmissionId
        {
            get { return GetProperty(SubmissionIdProperty); }
            private set { LoadProperty(SubmissionIdProperty, value); }
        }

        public static readonly PropertyInfo<string> ActivityNameProperty = RegisterProperty<string>(c => c.ActivityName);
        public string ActivityName
        {
            get { return GetProperty(ActivityNameProperty); }
            private set { LoadProperty(ActivityNameProperty, value); }
        }

        public static readonly PropertyInfo<string> ActivityDescriptionProperty = RegisterProperty<string>(c => c.ActivityDescription);
        public string ActivityDescription
        {
            get { return GetProperty(ActivityDescriptionProperty); }
            private set { LoadProperty(ActivityDescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> SubmissionNotesProperty = RegisterProperty<string>(c => c.SubmissionNotes);
        public string SubmissionNotes
        {
            get { return GetProperty(SubmissionNotesProperty); }
            private set { LoadProperty(SubmissionNotesProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeUserNameProperty = RegisterProperty<string>(c => c.EmployeeUserName);
        public string EmployeeUserName
        {
            get { return GetProperty(EmployeeUserNameProperty); }
            private set { LoadProperty(EmployeeUserNameProperty, value); }
        }

        public static readonly PropertyInfo<ActivitySubmissionStatus> StatusProperty = RegisterProperty<ActivitySubmissionStatus>(c => c.Status);
        public ActivitySubmissionStatus Status
        {
            get { return GetProperty(StatusProperty); }
            private set { SetProperty(StatusProperty, value); }
        }

        public static readonly PropertyInfo<string> ApprovedByUserNameProperty = RegisterProperty<string>(c => c.ApprovedByUserName);
        public string ApprovedByUserName
        {
            get { return GetProperty(ApprovedByUserNameProperty); }
            private set { LoadProperty(ApprovedByUserNameProperty, value); }
        }

        #endregion Properties

        #region Methods

        public static readonly MethodInfo ApproveActivitySubmissionMethod = RegisterMethod(typeof(ApproveActivityItem), "ApproveActivitySubmission");
        /// <summary>
        /// Approves this activity submission.  Can only be called by a user with manager permission
        /// and when the status is not denied or error.
        /// </summary>
        /// <param name="approverUserName">The AD user name of the manager approving this activity submission.</param>
        public void ApproveActivitySubmission(string approverUserName)
        {
            if (CanExecuteMethod(ApproveActivitySubmissionMethod))
            {
                this.Status = ActivitySubmissionStatus.Approved;
                this.ApprovedByUserName = approverUserName;
            }
        }

        /// <summary>
        /// Denys the activity submission.  Can only be called by a user with the manager permission
        /// and when the status is not approved or error.
        /// </summary>
        public static readonly MethodInfo DenyActivitySubmissionMethod = RegisterMethod(typeof(ApproveActivityItem), "DenyActivitySubmission");
        public void DenyActivitySubmission()
        {
            if (CanExecuteMethod(DenyActivitySubmissionMethod))
            {
                this.Status = ActivitySubmissionStatus.Denied;
            }
        }

        internal void Load(IApproveActivityItemDTO item)
        {
            using (this.BypassPropertyChecks)
            {
                this.SubmissionId = item.SubmissionId;
                this.ActivityName = item.ActivityName;
                this.ActivityDescription = item.ActivityDescription;
                this.SubmissionNotes = item.SubmissionNotes;
                this.EmployeeUserName = item.EmployeeUserName;
                this.Status = item.Status;
                this.ApprovedByUserName = item.ApprovedByUserName;
            }
            this.MarkClean();
            this.MarkOld();
            this.MarkAsChild();
        }

        internal IApproveActivityItemDTO Unload()
        {
            var returnValue = IoC.Container.Resolve<IApproveActivityItemDTO>();
            using (this.BypassPropertyChecks)
            {
                returnValue.SubmissionId = this.SubmissionId;
                returnValue.ActivityName = this.ActivityName;
                returnValue.ActivityDescription = this.ActivityDescription;
                returnValue.SubmissionNotes = this.SubmissionNotes;
                returnValue.EmployeeUserName = this.EmployeeUserName;
                returnValue.Status = this.Status;
                returnValue.ApprovedByUserName = this.ApprovedByUserName;
            }
            return returnValue;
        }

        #endregion Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.ExecuteMethod, ApproveActivitySubmissionMethod, PermissionType.Manager.ToString()));
            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.ExecuteMethod, DenyActivitySubmissionMethod, PermissionType.Manager.ToString()));
        }

        #endregion Rules
    }
}
