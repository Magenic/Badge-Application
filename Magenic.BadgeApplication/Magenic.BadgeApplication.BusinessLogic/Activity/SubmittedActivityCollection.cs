using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class SubmittedActivityCollection : ReadOnlyListBase<SubmittedActivityCollection, ISubmittedActivityItem>, ISubmittedActivityCollection
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

        #endregion Criteria

        #region Factory Methods

        public async static Task<ISubmittedActivityCollection> GetSubmittedActivitiesByEmployeeIdAsync(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            return await IoC.Container.Resolve<IObjectFactory<ISubmittedActivityCollection>>().FetchAsync(new SubmittedActivityCriteria(employeeId, startDate, endDate));
        }

        #endregion Factory Methods

        #region Data Access

        protected async Task DataPortal_Fetch(SubmittedActivityCriteria criteria)
        {
            var dal = IoC.Container.Resolve<ISubmittedActivityCollectionDAL>();

            var result = await dal.GetSubmittedActivitiesForEmployeeIdAsync(criteria.EmployeeId, criteria.StartDate, criteria.EndDate);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<ISubmittedActivityItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (ISubmittedActivityItemDTO item in data)
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
