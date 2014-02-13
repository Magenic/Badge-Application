using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.PointsReport
{
    [Serializable]
    public sealed class PointsReportCollection : BusinessListBase<PointsReportCollection, IPointsReportItem>, IPointsReportCollection
    {
        #region Factory Methods

        /// <summary>
        /// Asynchronously returns a list of all employees and their points who have enough points to be paid out.
        /// </summary>
        /// <returns>A list of payouts that can be approved.</returns>
        public async static Task<IPointsReportCollection> GetAllPayoutsToApproveAsync()
        {
            if (BusinessRules.HasPermission(AuthorizationActions.GetObject, typeof(PointsReportCollection)))
            {
                return await IoC.Container.Resolve<IObjectFactory<IPointsReportCollection>>().FetchAsync();
            }
            else
            {
                throw new Csla.Security.SecurityException("Current user not allowed to retrieve a PointsReportCollection");
            }
        }

        #endregion Factory Methods

        #region Rules

        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(PointsReportCollection), new IsInRole(AuthorizationActions.GetObject, Common.Enums.PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(PointsReportCollection), new IsInRole(AuthorizationActions.EditObject, Common.Enums.PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(PointsReportCollection), new IsInRole(AuthorizationActions.CreateObject, string.Empty));
            BusinessRules.AddRule(typeof(PointsReportCollection), new IsInRole(AuthorizationActions.DeleteObject, string.Empty));
        }

        #endregion Rules

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<IPointsReportCollectionDAL>();

            var result = await dal.GetPointsReportItemsAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<PointsReportItemDTO> data)
        {
            foreach (PointsReportItemDTO item in data)
            {
                var newItem = new PointsReportItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.AllowNew = false;
            this.AllowRemove = false;
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            var saveList = new List<PointsReportItemDTO>();
            foreach (PointsReportItem i in this)
            {
                saveList.Add(i.Unload());
            }
            var dal = IoC.Container.Resolve<IPointsReportCollectionDAL>();
            var results = dal.Update(saveList);
            this.AllowRemove = true;
            this.AllowNew = true;
            this.Clear();
            this.DeletedList.Clear();
            this.LoadData(results);
        }

        #endregion Data Access

        #region Methods

        public void PayAllOut(int employeeId)
        {
            var paidDate = DateTime.UtcNow;
            foreach (var item in this)
            {
                item.Payout(employeeId, paidDate);                
            }
        }

        #endregion Methods
    }
}
