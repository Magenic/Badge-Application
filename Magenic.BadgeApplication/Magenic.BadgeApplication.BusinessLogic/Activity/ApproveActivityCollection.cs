using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class ApproveActivityCollection : BusinessListBase<ApproveActivityCollection, IApproveActivityItem>, IApproveActivityCollection
    {
        #region Criteria

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        [Serializable]
        public sealed class ApproveActivityCollectionCriteria : CriteriaBase<ApproveActivityCollectionCriteria>, IApproveActivityCollectionCriteria
        {

            public int ManagerEmployeeId { get; set; }

            public IAwardBadges AwardBadges { get; set; }

            public bool ShowAdminView { get; set; }
        }

        #endregion Criteria

        #region Factory Methods

        /// <summary>
        /// Asynchronously returns a list of all activities awaiting approval for a specific manager.
        /// </summary>
        /// <param name="managerEmployeeId">The employee Id of the manager to retrieve all activities for.</param>
        /// <returns>A list of activities to approve.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public async static Task<IApproveActivityCollection> GetAllActivitiesToApproveAsync(int managerEmployeeId, bool showAdminView = false)
        {
            var awardBadges = IoC.Container.Resolve<IAwardBadges>();
            var criteria = new ApproveActivityCollectionCriteria
            {
                ManagerEmployeeId = managerEmployeeId,
                AwardBadges = awardBadges,
                ShowAdminView = showAdminView
            };
            return await IoC.Container.Resolve<IObjectFactory<IApproveActivityCollection>>().FetchAsync(criteria);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public static IApproveActivityCollection GetAllActivitiesToApproveSync(int managerEmployeeId, bool showAdminView = false)
        {
            var awardBadges = IoC.Container.Resolve<IAwardBadges>();
            var criteria = new ApproveActivityCollectionCriteria
            {
                ManagerEmployeeId = managerEmployeeId,
                AwardBadges = awardBadges,
                ShowAdminView = showAdminView
            };
            return IoC.Container.Resolve<IObjectFactory<IApproveActivityCollection>>().Fetch(criteria);
            //return await IoC.Container.Resolve<IObjectFactory<IApproveActivityCollection>>().FetchAsync(criteria);
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(ApproveActivityCollectionCriteria criteria)
        {
            var dal = IoC.Container.Resolve<IApproveActivityCollectionDAL>();

            var result = await dal.GetActivitiesToApproveForManagerAsync(criteria);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<ApproveActivityItemDTO> data)
        {
            foreach (ApproveActivityItemDTO item in data)
            {
                var newItem = new ApproveActivityItem();
                newItem.Load(item);
                this.Add(newItem);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            var saveList = new List<ApproveActivityItemDTO>();
            var badgeList = new List<BadgeAwardDTO>();
            var awardBadges = IoC.Container.Resolve<IAwardBadges>();
            foreach (ApproveActivityItem i in this)
            {
                if (i.Status == ActivitySubmissionStatus.Approved)
                {
                    var activityInfo = new ActivityInfoDTO
                    {
                        ActivityId = i.ActivityId,
                        EmployeeId = i.EmployeeId,
                        Status = i.Status
                    };
                    badgeList.AddRange(awardBadges.CreateBadges(activityInfo));
                    if (activityInfo.Status == ActivitySubmissionStatus.Complete)
                    {
                        i.CompleteActivitySubmission();
                    }
                }
                saveList.Add(i.Unload());
            }
            var dal = IoC.Container.Resolve<IApproveActivityCollectionDAL>();
            var results = dal.Update(saveList);
            AwardBadges.SaveBadges(badgeList.AsQueryable());
            this.Clear();
            this.DeletedList.Clear();
            this.LoadData(results);
        }


        #endregion Data Access
    }
}
