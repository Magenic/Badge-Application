using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Notification
{
    [Serializable]
    public class NotificationItemToPublish : BusinessBase<NotificationItemToPublish>, INotificationItemToPublish
    {
        #region Properties

        public static readonly PropertyInfo<int> NotificationItemIdProperty = RegisterProperty<int>(c => c.NotificationId);
        public int NotificationId
        {
            get { return GetProperty(NotificationItemIdProperty); }
            private set { SetProperty(NotificationItemIdProperty, value); }
        }

        public static readonly PropertyInfo<int> ActivitySubmissionIdProperty = RegisterProperty<int>(c => c.ActivitySubmissionId);
        public int ActivitySubmissionId
        {
            get { return GetProperty(ActivitySubmissionIdProperty); }
            private set { SetProperty(ActivitySubmissionIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> CreatedDateProperty = RegisterProperty<DateTime>(c => c.CreatedDate);
        public DateTime CreatedDate
        {
            get { return GetProperty(CreatedDateProperty); }
            private set { SetProperty(CreatedDateProperty, value); }
        }

        public static readonly PropertyInfo<int> NotificationStatusIdProperty = RegisterProperty<int>(c => c.NotificationStatusId);
        public int NotificationStatusId
        {
            get { return GetProperty(NotificationStatusIdProperty); }
            private set { SetProperty(NotificationStatusIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> NotificationSentDateProperty = RegisterProperty<DateTime>(c => c.NotificationSentDate);
        public DateTime? NotificationSentDate
        {
            get { return GetProperty(NotificationSentDateProperty); }
            private set { SetProperty(NotificationSentDateProperty, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompundWordsShouldBeCasedCorrectly")]
        public static readonly PropertyInfo<DateTime> UpdatedDateProperty = RegisterProperty<DateTime>(c => c.UpdatedDate);
        public DateTime? UpdatedDate
        {
            get { return GetProperty(UpdatedDateProperty); }
            private set { SetProperty(UpdatedDateProperty, value); }
        }

        public static readonly PropertyInfo<int> ActivityIdProperty = RegisterProperty<int>(c => c.ActivityId);
        public int ActivityId
        {
            get { return GetProperty(ActivityIdProperty); }
            private set { SetProperty(ActivityIdProperty, value); }
        }

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { SetProperty(EmployeeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> SubmissionDescriptionProperty = RegisterProperty<string>(c => c.SubmissionDescription);
        public string SubmissionDescription
        {
            get { return GetProperty(SubmissionDescriptionProperty); }
            private set { SetProperty(SubmissionDescriptionProperty, value); }
        }

        public static readonly PropertyInfo<int> SubmissionApprovedByIdProperty = RegisterProperty<int>(c => c.SubmissionApprovedById);
        public int? SubmissionApprovedById
        {
            get { return GetProperty(SubmissionApprovedByIdProperty); }
            private set { SetProperty(SubmissionApprovedByIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> SubmissionDateProperty = RegisterProperty<DateTime>(c => c.SubmissionDate);
        public DateTime SubmissionDate
        {
            get { return GetProperty(SubmissionDateProperty); }
            private set { SetProperty(SubmissionDateProperty, value); }
        }

        public static readonly PropertyInfo<int> SubmissionStatusIdProperty = RegisterProperty<int>(c => c.SubmissionStatusId);
        public int SubmissionStatusId
        {
            get { return GetProperty(SubmissionStatusIdProperty); }
            private set { SetProperty(SubmissionStatusIdProperty, value); }
        }

        public static readonly PropertyInfo<int> AwardValueProperty = RegisterProperty<int>(c => c.AwardValue);
        public int? AwardValue
        {
            get { return GetProperty(AwardValueProperty); }
            private set { SetProperty(AwardValueProperty, value); }
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

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            private set { SetProperty(FirstNameProperty, value); }
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            private set { SetProperty(LastNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);
        public string EmailAddress
        {
            get { return GetProperty(EmailAddressProperty); }
            private set { SetProperty(EmailAddressProperty, value); }
        }

        public static readonly PropertyInfo<string> ADNameProperty = RegisterProperty<string>(c => c.ADName);
        public string ADName
        {
            get { return GetProperty(ADNameProperty); }
            private set { SetProperty(ADNameProperty, value); }
        }
        #endregion Properties

        #region Factory Methods

        ///// <summary>
        ///// Asynchronously returns a list of all activities awaiting approval for a specific manager.
        ///// </summary>
        ///// <returns>A list of activities to approve.</returns>
        //public async static Task<IQueueItemToPublish> GetQueueItemToPublishByIdAsync(int QueueItemId)
        //{
        //    return await IoC.Container.Resolve<IObjectFactory<IQueueItemToPublish>>().FetchAsync(QueueItemId);
        //}

        #endregion Factory Methods

        #region Methods

        #endregion Methods

        #region Rules

        #endregion Rules

        #region Data Access

        internal void Load(NotificationItemToPublishDTO data, bool inCollection)
        {
            using (this.BypassPropertyChecks)
            {
                this.NotificationId = data.NotificationId;
                this.ActivitySubmissionId = data.ActivitySubmissionId;
                this.CreatedDate = data.CreatedDate;
                this.NotificationStatusId = data.NotificationStatusId;
                this.NotificationSentDate = data.NotificationSentDate;
                this.UpdatedDate = data.UpdatedDate;
                this.ActivityId = data.ActivityId;
                this.EmployeeId = data.EmployeeId;
                this.SubmissionDescription = data.SubmissionDescription;
                this.SubmissionApprovedById = data.SubmissionApprovedById;
                this.SubmissionDate = data.SubmissionDate;
                this.SubmissionStatusId = data.SubmissionStatusId;
                this.AwardValue = data.AwardValue;
                this.ActivityName = data.ActivityName;
                this.ActivityDescription = data.ActivityDescription;
                this.FirstName = data.FirstName;
                this.LastName = data.LastName;
                this.EmailAddress = data.EmailAddress;
                this.ADName = data.ADName;
            }
            this.MarkClean();
            this.MarkOld();
            if (inCollection)
            {
                this.MarkAsChild();
            }
        }

        #endregion Data Access
    }
}
