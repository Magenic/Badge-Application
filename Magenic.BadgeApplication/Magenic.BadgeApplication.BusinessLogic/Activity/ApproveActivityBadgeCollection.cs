using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class ApproveActivityBadgeCollection : ReadOnlyListBase<ApproveActivityBadgeCollection, IApproveActivityBadgeItem>, IApproveActivityBadgeCollection
    {
        #region Data Access

        internal void LoadData(IEnumerable<ApproveActivityBadgeItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (ApproveActivityBadgeItemDTO item in data)
            {
                var newItem = new ApproveActivityBadgeItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}