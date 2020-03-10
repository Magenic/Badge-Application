using System.Threading.Tasks;
using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Queue
{
    [Serializable]
    public sealed class QueueItemToPublish : BusinessBase<QueueItemToPublish>, IQueueItemToPublish
    {
        #region Properties

        public static readonly PropertyInfo<int> QueueItemIdProperty = RegisterProperty<int>(c => c.QueueItemId);
        public int QueueItemId
        {
            get { return GetProperty(QueueItemIdProperty); }
            private set { SetProperty(QueueItemIdProperty, value); }
        }


        public static readonly PropertyInfo<int> BadgeAwardIdProperty = RegisterProperty<int>(c => c.BadgeAwardId);
        public int BadgeAwardId
        {
            get { return GetProperty(BadgeAwardIdProperty); }
            private set { SetProperty(BadgeAwardIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> QueueItemCreatedProperty = RegisterProperty<DateTime>(c => c.QueueItemCreated);
        public DateTime QueueItemCreated
        {
            get { return GetProperty(QueueItemCreatedProperty); }
            private set { SetProperty(QueueItemCreatedProperty, value); }
        }

        public static readonly PropertyInfo<int> BadgeIdProperty = RegisterProperty<int>(c => c.BadgeId);
        public int BadgeId
        {
            get { return GetProperty(BadgeIdProperty); }
            private set { SetProperty(BadgeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> BadgeNameProperty = RegisterProperty<string>(c => c.BadgeName);
        public string BadgeName
        {
            get { return GetProperty(BadgeNameProperty); }
            private set { SetProperty(BadgeNameProperty, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompundWordsShouldBeCasedCorrectly")]
        public static readonly PropertyInfo<string> BadgeTaglineProperty = RegisterProperty<string>(c => c.BadgeTagline);
        public string BadgeTagline
        {
            get { return GetProperty(BadgeTaglineProperty); }
            private set { SetProperty(BadgeTaglineProperty, value); }
        }

        public static readonly PropertyInfo<string> BadgePathProperty = RegisterProperty<string>(c => c.BadgePath);
        public string BadgePath
        {
            get { return GetProperty(BadgePathProperty); }
            private set { SetProperty(BadgePathProperty, value); }
        }

        public static readonly PropertyInfo<string> BadgeDescriptionProperty = RegisterProperty<string>(c => c.BadgeDescription);
        public string BadgeDescription
        {
            get { return GetProperty(BadgeDescriptionProperty); }
            private set { SetProperty(BadgeDescriptionProperty, value); }
        }

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { SetProperty(EmployeeIdProperty, value); }
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

        internal void Load(QueueItemToPublishDTO data, bool inCollection)
        {
            using (this.BypassPropertyChecks)
            {
                this.QueueItemId = data.QueueItemId;
                this.BadgeAwardId = data.BadgeAwardId;
                this.QueueItemCreated = data.QueueItemCreated;
                this.BadgeId = data.BadgeId;
                this.BadgeName = data.BadgeName;
                this.BadgeTagline = data.BadgeTagline;
                this.BadgePath = data.BadgePath;
                this.BadgeDescription = data.BadgeDescription;
                this.EmployeeId = data.EmployeeId;
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