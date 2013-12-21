using Csla;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class SubmittedActivityItem : ReadOnlyBase<SubmittedActivityItem>, ISubmittedActivityItem
    {
        #region Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.ActivityName);
        public string ActivityName
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> ActivitySubmissionDateProperty = RegisterProperty<DateTime>(c => c.ActivitySubmissionDate);
        public DateTime ActivitySubmissionDate
        {
            get { return GetProperty(ActivitySubmissionDateProperty); }
            private set { LoadProperty(ActivitySubmissionDateProperty, value); }
        }

        public static readonly PropertyInfo<string> SubmissionNotesProperty = RegisterProperty<string>(c => c.SubmissionNotes);
        public string SubmissionNotes
        {
            get { return GetProperty(SubmissionNotesProperty); }
            private set { LoadProperty(SubmissionNotesProperty, value); }
        }

        public static readonly PropertyInfo<string> UserADNameProperty = RegisterProperty<string>(c => c.UserADName);
        public string UserADName
        {
            get { return GetProperty(UserADNameProperty); }
            private set { LoadProperty(UserADNameProperty, value); }
        }

        public static readonly PropertyInfo<Common.Enums.ActivitySubmissionStatus> StatusProperty = RegisterProperty<Common.Enums.ActivitySubmissionStatus>(c => c.Status);
        public Common.Enums.ActivitySubmissionStatus Status
        {
            get { return GetProperty(StatusProperty); }
            private set { LoadProperty(StatusProperty, value); }
        }

        public static readonly PropertyInfo<string> ApprovedByUserNameProperty = RegisterProperty<string>(c => c.ApprovedByUserName);
        public string ApprovedByUserName
        {
            get { return GetProperty(ApprovedByUserNameProperty); }
            private set { LoadProperty(ApprovedByUserNameProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(ISubmittedActivityItemDTO item)
        {
            this.Id = item.Id;
            this.ActivityName = item.ActivityName;
            this.ActivitySubmissionDate = item.ActivitySubmissionDate;
            this.ApprovedByUserName = item.ApprovedByUserName;
            this.Status = item.Status;
            this.SubmissionNotes = item.SubmissionNotes;
            this.ActivitySubmissionDate = item.ActivitySubmissionDate;
            this.UserADName = item.UserName;
        }

        #endregion Methods
    }
}
