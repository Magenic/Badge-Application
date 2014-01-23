using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class SubmittedActivityItem : ReadOnlyBase<SubmittedActivityItem>, ISubmittedActivityItem
    {
        #region Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<int> ActivityIdProperty = RegisterProperty<int>(c => c.ActivityId);
        public int ActivityId
        {
            get { return GetProperty(ActivityIdProperty); }
            private set { LoadProperty(ActivityIdProperty, value); }
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

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { LoadProperty(EmployeeIdProperty, value); }
        }

        public static readonly PropertyInfo<Common.Enums.ActivitySubmissionStatus> StatusProperty = RegisterProperty<Common.Enums.ActivitySubmissionStatus>(c => c.Status);
        public Common.Enums.ActivitySubmissionStatus Status
        {
            get { return GetProperty(StatusProperty); }
            private set { LoadProperty(StatusProperty, value); }
        }

        public static readonly PropertyInfo<int> ApprovedByIdProperty = RegisterProperty<int>(c => c.ApprovedById);
        public int ApprovedById
        {
            get { return GetProperty(ApprovedByIdProperty); }
            private set { LoadProperty(ApprovedByIdProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(SubmittedActivityItemDTO item)
        {
            this.Id = item.Id;
            this.ActivityId = item.ActivityId;
            this.ActivityName = item.ActivityName;
            this.ActivitySubmissionDate = item.ActivitySubmissionDate;
            this.ApprovedById = item.ApprovedById;
            this.Status = item.Status;
            this.SubmissionNotes = item.SubmissionNotes;
            this.ActivitySubmissionDate = item.ActivitySubmissionDate;
            this.EmployeeId = item.EmployeeId;
        }

        #endregion Methods
    }
}
