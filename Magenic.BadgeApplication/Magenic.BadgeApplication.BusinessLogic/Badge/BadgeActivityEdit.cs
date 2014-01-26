using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    /// <summary>
    /// A prerequisite activity required for the associated badge.
    /// </summary>
    [Serializable]
    public sealed class BadgeActivityEdit : BusinessBase<BadgeActivityEdit>, IBadgeActivityEdit
    {
        public BadgeActivityEdit()
        {
            this.MarkAsChild();
        }

        public static readonly PropertyInfo<int> BadgeActivityIdProperty = RegisterProperty<int>(c => c.BadgeActivityId);
        public int BadgeActivityId
        {
            get { return GetProperty(BadgeActivityIdProperty); }
            private set { SetProperty(BadgeActivityIdProperty, value); }
        }

        public static readonly PropertyInfo<int> ActivityIdProperty = RegisterProperty<int>(c => c.ActivityId);
        public int ActivityId
        {
            get { return GetProperty(ActivityIdProperty); }
            set { SetProperty(ActivityIdProperty, value); }
        }

        public static IBadgeActivityEdit CreateBadgeActivity()
        {
            return IoC.Container.Resolve<IObjectFactory<IBadgeActivityEdit>>().Create();
        }

        internal void LoadData(Common.DTO.BadgeActivityEditDTO badgeActivity)
        {
            using (this.BypassPropertyChecks)
            {
                this.BadgeActivityId = badgeActivity.BadgeActivityId;
                this.ActivityId = badgeActivity.ActivityId;
                this.MarkAsChild();
                this.MarkOld();
                this.MarkClean();
            }
        }

        internal Common.DTO.BadgeActivityEditDTO UnloadData()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new Common.DTO.BadgeActivityEditDTO
                {
                    BadgeActivityId = this.BadgeActivityId,
                    ActivityId = this.ActivityId
                };
                return returnValue;
            }
        }
    }
}
