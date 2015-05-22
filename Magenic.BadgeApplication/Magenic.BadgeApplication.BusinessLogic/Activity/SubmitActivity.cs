using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.BusinessLogic.Rules;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    /// <summary>
    /// Used for to submit activities into the system.  Also used by managers to
    /// approve or deny an existing activity submission.
    /// </summary>
    [Serializable]
    public sealed class SubmitActivity : BusinessBase<SubmitActivity>, ISubmitActivity, IHaveEntryType
    {

        #region Properties
        /// <summary>
        /// The Id for this activity submission.  Zero if new.
        /// </summary>
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { SetProperty(IdProperty, value); }
        }

        /// <summary>
        /// The id of the activity this submission is for.
        /// </summary>
        public static readonly PropertyInfo<int> ActivityIdProperty = RegisterProperty<int>(c => c.ActivityId);
        public int ActivityId
        {
            get { return GetProperty(ActivityIdProperty); }
            set { SetProperty(ActivityIdProperty, value); }
        }
        /// <summary>
        /// The date the activity occurred, should be set and saved in UTC.
        /// </summary>
        public static readonly PropertyInfo<DateTime> ActivitySubmissionDateProperty = RegisterProperty<DateTime>(c => c.ActivitySubmissionDate);
        public DateTime ActivitySubmissionDate
        {
            get { return GetProperty(ActivitySubmissionDateProperty); }
            set { SetProperty(ActivitySubmissionDateProperty, value); }
        }

        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(c => c.Notes);
        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }

        /// <summary>
        /// Tracks activity entry type
        /// </summary>
        public static readonly PropertyInfo<ActivityEntryType> ActivityEntryTypeProperty = RegisterProperty<ActivityEntryType>(c => c.EntryType);
        public ActivityEntryType EntryType
        {
            get { return GetProperty(ActivityEntryTypeProperty); }
            set { SetProperty(ActivityEntryTypeProperty, value); }
        }

        /// <summary>
        /// The AD user name of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            set { SetProperty(EmployeeIdProperty, value); }
        }

        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        public static readonly PropertyInfo<ActivitySubmissionStatus> StatusProperty = RegisterProperty<ActivitySubmissionStatus>(c => c.Status);
        public ActivitySubmissionStatus Status
        {
            get { return GetProperty(StatusProperty); }
            private set { SetProperty(StatusProperty, value); }
        }

        /// <summary>
        /// The AD user name of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        public static readonly PropertyInfo<int> ApprovedByIdProperty = RegisterProperty<int>(c => c.ApprovedById);
        public int ApprovedById
        {
            get { return GetProperty(ApprovedByIdProperty); }
            private set { SetProperty(ApprovedByIdProperty, value); }
        }

        /// <summary>
        /// A CSV list of the employee ids of the people this badge submission is for, if it is a multi submission.
        /// This property takes priority over EmployeeId if it is not null or white space.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static readonly PropertyInfo<string> EmployeeIdsProperty = RegisterProperty<string>(c => c.EmployeeIds);
        public string EmployeeIds {
            get { return GetProperty(EmployeeIdsProperty); }
            set { SetProperty(EmployeeIdsProperty, value); }
        }

        #endregion Properties

        #region Factory Methods

        public async static Task<ISubmitActivity> GetActivitySubmissionByIdAsync(int activitySubmissionId)
        {
            return await IoC.Container.Resolve<IObjectFactory<ISubmitActivity>>().FetchAsync(activitySubmissionId);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SubmitActivity" /> class.
        /// </summary>
        /// <param name="employeeId">The employee id that the activity submission is for.</param>
        /// <returns>
        /// An instance of a class the implements <see cref="ISubmitActivity" />.
        /// </returns>
        public static ISubmitActivity CreateActivitySubmission(int employeeId)
        {
            return IoC.Container.Resolve<IObjectFactory<ISubmitActivity>>().Create(employeeId);
        }

        #endregion Factory Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            this.BusinessRules.AddRule(new MinValue<int>(EmployeeIdProperty, 1));
            this.BusinessRules.AddRule(new MinValue<int>(ActivityIdProperty, 1));

            this.BusinessRules.AddRule(new CanCreateSubmission(AuthorizationActions.CreateObject));

            // Only run this rule if the associated properties are otherwise valid.
            this.BusinessRules.AddRule(new Rules.DefaultActivityStatus(ActivityIdProperty, StatusProperty, ApprovedByIdProperty) { Priority = 1 });
        }

        #endregion Rules

        #region Data Access

        [RunLocal]
        private void DataPortal_Create(int employeeId)
        {
            base.DataPortal_Create();
            this.LoadProperty(ActivitySubmissionDateProperty, DateTime.UtcNow);
            this.LoadProperty(StatusProperty, ActivitySubmissionStatus.AwaitingApproval);
            this.LoadProperty(EmployeeIdProperty, employeeId);
            this.LoadProperty(ActivityEntryTypeProperty, ActivityEntryType.Unset);
            this.BusinessRules.CheckRules();
        }

        private async Task DataPortal_Fetch(int badgeId)
        {
            var dal = IoC.Container.Resolve<ISubmitActivityDAL>();

            var result = await dal.GetActivitySubmissionByIdAsync(badgeId);
            this.LoadData(result);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            if (IsDeleted)
            {
                if (!IsNew)
                {
                    this.DataPortal_DeleteSelf();
                }
                return;
            }

            if (IsNew)
            {
                this.DataPortal_Insert();
            }
            else if (IsDirty)
            {
                IQueryable<BadgeAwardDTO> badgesToCreate = null;
                if (ShouldCreateBadges())
                {
                    badgesToCreate = CreateBadges(this);
                }
                var dal = IoC.Container.Resolve<ISubmitActivityDAL>();
                this.LoadData(dal.Update(this.UnloadData()));
                FieldManager.UpdateChildren();
                SaveBadges(badgesToCreate);
            }
            this.MarkClean();
            this.MarkOld();
        }

        private void SaveBadges(IQueryable<BadgeAwardDTO> badgesToCreate)
        {
            AwardBadges.SaveBadges(badgesToCreate);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "submitActivity")]
        private IQueryable<BadgeAwardDTO> CreateBadges(SubmitActivity submitActivity)
        {
            var activityInfo = new ActivityInfoDTO
            {
                ActivityId = this.ActivityId,
                EmployeeId = this.EmployeeId,
                Status = this.Status
            };
            var awardBadges = IoC.Container.Resolve<IAwardBadges>();
            var returnValue = awardBadges.CreateBadges(activityInfo);
            this.LoadProperty(StatusProperty, activityInfo.Status);
            return returnValue;
        }

        private bool ShouldCreateBadges()
        {
            return ((IsNew || IsDirty) && this.Status == ActivitySubmissionStatus.Approved);
        }


        private SubmitActivityDTO UnloadData()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new SubmitActivityDTO
                {
                    Id = this.Id,
                    ActivityId = this.ActivityId,
                    ActivitySubmissionDate = this.ActivitySubmissionDate,
                    ApprovedById = this.ApprovedById,
                    Notes = this.Notes,
                    Status = this.Status,
                    EmployeeId = this.EmployeeId
                };
                return returnValue;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "data")]
        private void LoadData(SubmitActivityDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.Id = data.Id;
                this.ActivityId = data.ActivityId;
                this.ActivitySubmissionDate = data.ActivitySubmissionDate;
                this.ApprovedById = data.ApprovedById;
                this.Notes = data.Notes;
                this.Status = data.Status;
                this.EmployeeId = data.EmployeeId;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_DeleteSelf()
        {
            base.DataPortal_DeleteSelf();
            var dal = IoC.Container.Resolve<ISubmitActivityDAL>();

            if (!IsNew)
            {
                this.DeleteChildren();
                dal.Delete(this.Id);
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void DeleteChildren()
        {
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Insert()
        {
            base.DataPortal_Insert();
            var dal = IoC.Container.Resolve<ISubmitActivityDAL>();

            IQueryable<BadgeAwardDTO> badgesToCreate = null;
            if (ShouldCreateBadges())
            {
                badgesToCreate = CreateBadges(this);
            }
            this.LoadData(dal.Insert(this.UnloadData()));
            FieldManager.UpdateChildren();
            SaveBadges(badgesToCreate);
        }

        #endregion Data Access
    }
}
