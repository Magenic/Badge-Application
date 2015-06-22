using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    /// <summary>
    /// Allows a manager to examine information about an activity they need to approve
    /// and then either approve or deny the activity.
    /// </summary>
    [Serializable]
    public sealed class ApproveActivityItemForManager : BusinessBase<ApproveActivityItemForManager>, IApproveActivityItemForManager
    {
        #region Properties

        public static readonly PropertyInfo<int> SubmissionIdProperty = RegisterProperty<int>(c => c.SubmissionId);
        public int SubmissionId
        {
            get { return GetProperty(SubmissionIdProperty); }
            private set { SetProperty(SubmissionIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> SubmissionDateProperty = RegisterProperty<DateTime>(c => c.SubmissionDate);
        public DateTime SubmissionDate
        {
            get { return GetProperty(SubmissionDateProperty); }
            private set { SetProperty(SubmissionDateProperty, value); }
        }

        public static readonly PropertyInfo<int> ActivityIdProperty = RegisterProperty<int>(c => c.ActivityId);
        public int ActivityId
        {
            get { return GetProperty(ActivityIdProperty); }
            private set { SetProperty(ActivityIdProperty, value); }
        }

        public static readonly PropertyInfo<string> ActivityNameProperty = RegisterProperty<string>(c => c.ActivityName);
        public string ActivityName
        {
            get { return GetProperty(ActivityNameProperty); }
            private set { SetProperty(ActivityNameProperty, value); }
        }

        public static readonly PropertyInfo<string> ActivityDescriptionProperty = RegisterProperty<string>(c => c.ActivityDescription);
        public string ActivityDescription
        {
            get { return GetProperty(ActivityDescriptionProperty); }
            private set { SetProperty(ActivityDescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> SubmissionNotesProperty = RegisterProperty<string>(c => c.SubmissionNotes);
        public string SubmissionNotes
        {
            get { return GetProperty(SubmissionNotesProperty); }
            private set { SetProperty(SubmissionNotesProperty, value); }
        }

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { SetProperty(EmployeeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeADNameProperty = RegisterProperty<string>(c => c.EmployeeADName);
        public string EmployeeADName
        {
            get { return GetProperty(EmployeeADNameProperty); }
            private set { SetProperty(EmployeeADNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeFirstNameProperty = RegisterProperty<string>(c => c.EmployeeFirstName);
        public string EmployeeFirstName
        {
            get { return GetProperty(EmployeeFirstNameProperty); }
            private set { SetProperty(EmployeeFirstNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeLastNameProperty = RegisterProperty<string>(c => c.EmployeeLastName);
        public string EmployeeLastName
        {
            get { return GetProperty(EmployeeLastNameProperty); }
            private set { SetProperty(EmployeeLastNameProperty, value); }
        }

        public static readonly PropertyInfo<ActivitySubmissionStatus> StatusProperty = RegisterProperty<ActivitySubmissionStatus>(c => c.Status);
        public ActivitySubmissionStatus Status
        {
            get { return GetProperty(StatusProperty); }
            private set { SetProperty(StatusProperty, value); }
        }

        public static readonly PropertyInfo<int> ApprovedByIdProperty = RegisterProperty<int>(c => c.ApprovedById);
        public int ApprovedById
        {
            get { return GetProperty(ApprovedByIdProperty); }
            private set { SetProperty(ApprovedByIdProperty, value); }
        }

        public static readonly PropertyInfo<IApproveActivityBadgeCollection> ApproveActivityBadgeCollectionProperty = RegisterProperty<IApproveActivityBadgeCollection>(c => c.ApproveActivityBadgeCollection);
        public IApproveActivityBadgeCollection ApproveActivityBadgeCollection
        {
            get { return GetProperty(ApproveActivityBadgeCollectionProperty); }
            private set { SetProperty(ApproveActivityBadgeCollectionProperty, value); }
        }

        #endregion Properties

        #region Methods

        public static readonly MethodInfo ApproveActivitySubmissionMethod = RegisterMethod(typeof(ApproveActivityItem), "ApproveActivitySubmission");
        /// <summary>
        /// Approves this activity submission.  Can only be called by a user with manager permission
        /// and when the status is not denied or error.
        /// </summary>
        /// <param name="approverId">The Id of the manager approving this activity submission.</param>
        public void ApproveActivitySubmission(int approverId)
        {
            if (CanExecuteMethod(ApproveActivitySubmissionMethod))
            {
                this.Status = ActivitySubmissionStatus.Approved;
                this.ApprovedById = approverId;
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

        /// <summary>
        /// Called when an approved activity item is saved.  If badges are created successfully the status is 
        /// set to complete.
        /// </summary>
        internal void CompleteActivitySubmission()
        {
            this.Status = ActivitySubmissionStatus.Complete;
        }

        internal void Load(ApproveActivityItemDTO item)
        {
            using (this.BypassPropertyChecks)
            {
                this.SubmissionId = item.SubmissionId;
                this.SubmissionDate = item.SubmissionDate;
                this.ActivityId = item.ActivityId;
                this.ActivityName = item.ActivityName;
                this.ActivityDescription = item.ActivityDescription;
                this.SubmissionNotes = item.SubmissionNotes;
                this.EmployeeId = item.EmployeeId;
                this.EmployeeADName = item.EmployeeADName;
                this.EmployeeFirstName = item.EmployeeFirstName;
                this.EmployeeLastName = item.EmployeeLastName;
                this.Status = item.Status;
                this.ApprovedById = item.ApprovedById;
            }
            this.ApproveActivityBadgeCollection = new ApproveActivityBadgeCollection();
            ((ApproveActivityBadgeCollection)this.ApproveActivityBadgeCollection).LoadData(item.ApproveActivityBadgeItemCollection);
            this.MarkClean();
            this.MarkOld();
            this.MarkAsChild();
        }

        internal ApproveActivityItemDTO Unload()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new ApproveActivityItemDTO
                {
                    SubmissionId = this.SubmissionId,
                    SubmissionDate = this.SubmissionDate,
                    ActivityId = this.ActivityId,
                    ActivityName = this.ActivityName,
                    ActivityDescription = this.ActivityDescription,
                    SubmissionNotes = this.SubmissionNotes,
                    EmployeeId = this.EmployeeId,
                    EmployeeADName = this.EmployeeADName,
                    EmployeeFirstName = this.EmployeeFirstName,
                    EmployeeLastName = this.EmployeeFirstName,
                    Status = this.Status,
                    ApprovedById = this.ApprovedById,
                };

                var list = new List<ApproveActivityBadgeItemDTO>();
                foreach (var item in this.ApproveActivityBadgeCollection)
                {
                    list.Add(new ApproveActivityBadgeItemDTO()
                    {
                        BadgeId = item.BadgeId,
                        BadgePriority = item.BadgePriority,
                        Name = item.Name,
                        Type = item.Type,
                        AwardValueAmount = item.AwardValueAmount,
                        ImagePath = item.ImagePath,
                    });
                }
                returnValue.ApproveActivityBadgeItemCollection = list;

                return returnValue;
            }
        }

        #endregion Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.ExecuteMethod, ApproveActivitySubmissionMethod, new string[] { PermissionType.Manager.ToString(), PermissionType.Administrator.ToString() }));
            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.ExecuteMethod, DenyActivitySubmissionMethod, new string[] { PermissionType.Manager.ToString(), PermissionType.Administrator.ToString() }));
        }

        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(IApproveActivityItemForManager), new IsInRole(AuthorizationActions.GetObject, new string[] { PermissionType.Manager.ToString(), PermissionType.Administrator.ToString() }));
            BusinessRules.AddRule(typeof(ApproveActivityItemForManager), new IsInRole(AuthorizationActions.GetObject, new string[] { PermissionType.Manager.ToString(), PermissionType.Administrator.ToString() }));

        }

        #endregion Rules
    }
}
