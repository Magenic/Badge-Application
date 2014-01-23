using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class ActivityNameExists: CommandBase<ActivityNameExists>
    {
        private bool NameExists { get; set; }
        private string Name { get; set; }
        private int Id { get; set; }

        #region Factory Methods

        public static bool NameAlreadyExists(int id, string name)
        {
            var nameExistsCommand = new ActivityNameExists {Name = name, Id = id};
            nameExistsCommand = IoC.Container.Resolve<IObjectFactory<ActivityNameExists>>().Execute(nameExistsCommand);
            return nameExistsCommand.NameExists;
        }

        #endregion Factory Methods

        #region Data Access

        protected override void DataPortal_Execute()
        {
            var dal = IoC.Container.Resolve<IActivityEditDAL>();
            this.NameExists = dal.ActivityNameExists(this.Id, this.Name);
        }

        #endregion Data Access
    }
}