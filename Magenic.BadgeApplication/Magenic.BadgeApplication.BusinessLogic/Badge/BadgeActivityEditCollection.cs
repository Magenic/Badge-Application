using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public sealed class BadgeActivityEditCollection : BusinessListBase<BadgeActivityEditCollection, IBadgeActivityEdit>, IBadgeActivityEditCollection
    {
        public BadgeActivityEditCollection()
        {
            this.MarkAsChild();
        }

        internal void LoadChildren(IList<BadgeActivityEditDTO> badgeActivities)
        {
            foreach (var badgeActivity in badgeActivities)
            {
                var badgeActivityEdit = new BadgeActivityEdit();
                badgeActivityEdit.LoadData(badgeActivity);
                this.Add(badgeActivityEdit);
            }
        }

        internal IList<BadgeActivityEditDTO> UnloadChildren()
        {
            var returnValue = new List<BadgeActivityEditDTO>();
            foreach (var badgeActivityEdit in this)
            {
                returnValue.Add(((BadgeActivityEdit)badgeActivityEdit).UnloadData());
            }
            foreach (var badgeActivityEdit in this.DeletedList)
            {
                var newDTO = ((BadgeActivityEdit)badgeActivityEdit).UnloadData();
                newDTO.IsDeleted = true;
                returnValue.Add(newDTO);
            }
            return returnValue;
        }
    }
}
