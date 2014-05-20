using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    public class ApproveActivityBadgeCollection : ReadOnlyListBase<ApproveActivityBadgeCollection, IApproveActivityBadgeItem>, IApproveActivityBadgeCollection
    {
        #region Data Access

        internal void LoadData(IEnumerable<ApproveActivityBadgeItemDTO> data)
        {
            foreach (ApproveActivityBadgeItemDTO item in data)
            {
                var newItem = new ApproveActivityBadgeItem();
                newItem.Load(item);
                this.Add(newItem);
            }
        }

        #endregion Data Access
    }
}