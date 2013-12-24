using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class ActivityNameExists: CommandBase<ActivityNameExists>
    {
        private bool NameExists { get; set; }
        private string Name { get; set; }
        
        #region Factory Methods

        public async static Task<bool> NameAlreadyExistsAsync(string name)
        {
            var nameExistsCommand = new ActivityNameExists {Name = name};
            nameExistsCommand = await IoC.Container.Resolve<IDataPortal<ActivityNameExists>>().ExecuteAsync(nameExistsCommand);
            return nameExistsCommand.NameExists;
        }

        #endregion Factory Methods

        #region Data Access

        protected override void DataPortal_Execute()
        {
            this.NameExists = this.Name == "Foo";
        }

        #endregion Data Access
    }
}
