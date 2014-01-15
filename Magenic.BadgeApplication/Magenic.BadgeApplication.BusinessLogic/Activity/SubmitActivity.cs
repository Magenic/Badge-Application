using Autofac;
using Csla;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    /// <summary>
    /// Used for to submit activities into the system.  Also used by managers to
    /// approve or deny an existing activity submission.
    /// </summary>
    [Serializable]
    public class SubmitActivity : BusinessBase<SubmitActivity>, ISubmitActivity
    {

        #region Properties
        /// <summary>
        /// The Id for this activity submission.  Zero if new.
        /// </summary>
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
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
        /// The AD user name of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);
        public string UserName
        {
            get { return GetProperty(UserNameProperty); }
            set { SetProperty(UserNameProperty, value); }
        }

        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        public static readonly PropertyInfo<ActivitySubmissionStatus> StatusProperty = RegisterProperty<ActivitySubmissionStatus>(c => c.Status);
        public ActivitySubmissionStatus Status
        {
            get { return GetProperty(StatusProperty); }
            private set { LoadProperty(StatusProperty, value); }
        }

        /// <summary>
        /// The AD user name of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        public static readonly PropertyInfo<string> ApprovedByUserNameProperty = RegisterProperty<string>(c => c.ApprovedByUserName);
        public string ApprovedByUserName
        {
            get { return GetProperty(ApprovedByUserNameProperty); }
            private set { LoadProperty(ApprovedByUserNameProperty, value); }
        }

        #endregion Properties

        #region Factory Methods

        public async static Task<ISubmitActivity> GetActivitySubmissionByIdAsync(int activitySubmissionId)
        {
            return await IoC.Container.Resolve<IObjectFactory<ISubmitActivity>>().FetchAsync(activitySubmissionId);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SubmitActivity"/> class.
        /// </summary>
        /// <param name="userName">The AD user name that the activity submission is for.</param>
        /// <returns>An instance of a class the implements <see cref="ISubmitActivity"/>.</returns>
        public static ISubmitActivity CreateActivitySubmission(string userName)
        {
            return IoC.Container.Resolve<IObjectFactory<ISubmitActivity>>().Create(userName);
        }

        #endregion Factory Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            this.BusinessRules.AddRule(new Required(UserNameProperty));
            this.BusinessRules.AddRule(new MaxLength(NotesProperty, 10));
        }

        #endregion Rules

        #region Data Access

        [RunLocal]
        protected void DataPortal_Create(string userName)
        {
            base.DataPortal_Create();
            this.LoadProperty(ActivitySubmissionDateProperty, DateTime.UtcNow);
            this.LoadProperty(StatusProperty, ActivitySubmissionStatus.Proposed);
            this.LoadProperty(UserNameProperty, userName);
            this.BusinessRules.CheckRules();
        }

        protected async Task DataPortal_Fetch(int badgeId)
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
                var dal = IoC.Container.Resolve<ISubmitActivityDAL>();
                this.LoadData(dal.Update(this.UnloadData()));
                FieldManager.UpdateChildren();
            }
            this.MarkClean();
            this.MarkOld();
        }

        private ISubmitActivityDTO UnloadData()
        {
            var returnValue = IoC.Container.Resolve<ISubmitActivityDTO>();
            using (this.BypassPropertyChecks)
            {
                returnValue.Id = this.Id;
                returnValue.ActivityId = this.ActivityId;
                returnValue.ActivitySubmissionDate = this.ActivitySubmissionDate;
                returnValue.ApprovedByUserName = this.ApprovedByUserName;
                returnValue.Notes = this.Notes;
                returnValue.Status = this.Status;
                returnValue.UserName = this.UserName;
            }
            return returnValue;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "data")]
        private void LoadData(ISubmitActivityDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.Id = data.Id;
                this.ActivityId = data.ActivityId;
                this.ActivitySubmissionDate = data.ActivitySubmissionDate;
                this.ApprovedByUserName = data.ApprovedByUserName;
                this.Notes = data.Notes;
                this.Status = data.Status;
                this.UserName = data.UserName;
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

            this.LoadData(dal.Insert(this.UnloadData()));
            FieldManager.UpdateChildren();
        }

        #endregion Data Access
    }
}
