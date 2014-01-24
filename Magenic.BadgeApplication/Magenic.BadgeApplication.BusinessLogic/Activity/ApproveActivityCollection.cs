﻿using System.Linq;
using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class ApproveActivityCollection : BusinessListBase<ApproveActivityCollection, IApproveActivityItem>, IApproveActivityCollection
    {
        #region Factory Methods

        /// <summary>
        /// Asynchronously returns a list of all activities awaiting approval for a specific manager.
        /// </summary>
        /// <param name="managerEmployeeId">The employee Id of the manager to retrieve all activities for.</param>
        /// <returns>A list of activities to approve.</returns>
        public async static Task<IApproveActivityCollection> GetAllActivitiesToApproveAsync(int managerEmployeeId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IApproveActivityCollection>>().FetchAsync(managerEmployeeId);
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(int managerEmployeeId)
        {
            var dal = IoC.Container.Resolve<IApproveActivityCollectionDAL>();

            var result = await dal.GetActivitiesToApproveForManagerAsync(managerEmployeeId);
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
            foreach (ApproveActivityItem i in this)
            {
                if (i.Status == ActivitySubmissionStatus.Approved)
                {
                    var activityInfo = new AwardBadges.ActivityInfo
                    {
                        ActivityId = i.ActivityId,
                        EmployeeId = i.EmployeeId,
                        Status = i.Status
                    };
                    badgeList.AddRange(AwardBadges.CreateBadges(activityInfo));
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
