using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class SubmittedActivityCollection : ReadOnlyListBase<SubmittedActivityCollection, ISubmittedActivityItem>, ISubmittedActivityCollection
    {
        #region Criteria

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), Serializable]
        public class SubmittedActivityCriteria : CriteriaBase<SubmittedActivityCriteria>
        {
            public SubmittedActivityCriteria(int employeeId, DateTime? startDate, DateTime? endDate)
            {
                this.EmployeeId = employeeId;
                this.StartDate = startDate;
                this.EndDate = endDate;
            }

            public int EmployeeId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), Serializable]
        public class SubmittedActivityByIdCriteria : CriteriaBase<SubmittedActivityCriteria>
        {
            public SubmittedActivityByIdCriteria(int employeeId, int activityId, DateTime? startDate, DateTime? endDate)
            {
                this.EmployeeId = employeeId;
                this.ActivityId = activityId;
                this.StartDate = startDate;
                this.EndDate = endDate;
            }

            public int EmployeeId { get; set; }

            public int ActivityId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        #endregion Criteria

        #region Factory Methods

        public async static Task<ISubmittedActivityCollection> GetSubmittedActivitiesByEmployeeIdAsync(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            return await IoC.Container.Resolve<IObjectFactory<ISubmittedActivityCollection>>().FetchAsync(new SubmittedActivityCriteria(employeeId, startDate, endDate));
        }

        public async static Task<ISubmittedActivityCollection> GetSubmittedActivitiesByEmployeeIdAsync(int employeeId, int activityId, DateTime? startDate, DateTime? endDate)
        {
            return await IoC.Container.Resolve<IObjectFactory<ISubmittedActivityCollection>>().FetchAsync(new SubmittedActivityByIdCriteria(employeeId, activityId, startDate, endDate));
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(SubmittedActivityCriteria criteria)
        {
            var dal = IoC.Container.Resolve<ISubmittedActivityCollectionDAL>();

            var result = await dal.GetSubmittedActivitiesForEmployeeIdAsync(criteria.EmployeeId, criteria.StartDate, criteria.EndDate);
            this.LoadData(result);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(SubmittedActivityByIdCriteria criteria)
        {
            var dal = IoC.Container.Resolve<ISubmittedActivityCollectionDAL>();

            var result = await dal.GetSubmittedActivitiesForEmployeeIdByActivityIdAsync(criteria.EmployeeId, criteria.ActivityId, criteria.StartDate, criteria.EndDate);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<SubmittedActivityItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (SubmittedActivityItemDTO item in data)
            {
                var newItem = new SubmittedActivityItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }


        #endregion Data Access
    }
}
