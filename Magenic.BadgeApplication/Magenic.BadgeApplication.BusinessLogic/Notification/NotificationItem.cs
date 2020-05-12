using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Notification
{
    [Serializable]
    public class NotificationItem : BusinessBase<NotificationItem>, INotificationItem
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<DateTime?> NotificationSentDateProperty = RegisterProperty<DateTime?>(c => c.NotificationSentDate);
        public DateTime? NotificationSentDate
        {
            get { return GetProperty(NotificationSentDateProperty); }
            private set { SetProperty(NotificationSentDateProperty, value); }
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<DateTime?> UpdatedDateProperty = RegisterProperty<DateTime?>(c => c.UpdatedDate);
        public DateTime? UpdatedDate
        {
            get { return GetProperty(UpdatedDateProperty); }
            private set { SetProperty(UpdatedDateProperty, value); }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Sets ActivitySubmissionId
        /// </summary>
        /// <param name="ActivitySubmissionId"></param>
        public void SetActivitySubmissionId(int activitySubmissionId)
        {
            this.LoadProperty(ActivitySubmissionIdProperty, activitySubmissionId);
        }
        #endregion

        #region Factory Methods
        public static INotificationItem CreateNotification()
        {
            return IoC.Container.Resolve<IObjectFactory<INotificationItem>>().Create();
        }
        #endregion Factory Methods

        #region Rules
        #endregion Rules

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
            this.LoadProperty(CreatedDateProperty, DateTime.UtcNow);
            this.LoadProperty(NotificationStatusIdProperty, 0);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Insert()
        {
            base.DataPortal_Insert();
            var dal = IoC.Container.Resolve<INotificationDAL>();

            this.LoadData(dal.Add(this.UnloadData()));
        }

        private NotificationDTO UnloadData()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new NotificationDTO
                {
                    NotificationId = this.NotificationId,
                    ActivitySubmissionId = this.ActivitySubmissionId,
                    CreatedDate = this.CreatedDate,
                    NotificationStatusId = this.NotificationStatusId,
                    NotificationSentDate = this.NotificationSentDate,
                    UpdatedDate = this.UpdatedDate

                };
                
                return returnValue;
            }
        }

        private void LoadData(NotificationDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.NotificationId = data.NotificationId;
                this.ActivitySubmissionId = data.ActivitySubmissionId;
                this.CreatedDate = data.CreatedDate;
                this.NotificationStatusId = data.NotificationStatusId;
                this.NotificationSentDate = data.NotificationSentDate;
                this.UpdatedDate = data.UpdatedDate;
            }
        }
        #endregion Data Access
    }
}
