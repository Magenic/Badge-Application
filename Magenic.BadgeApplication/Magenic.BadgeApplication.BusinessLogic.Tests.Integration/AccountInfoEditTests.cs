using Magenic.BadgeApplication.BusinessLogic.AccountInfo;
using Magenic.BadgeApplication.BusinessLogic.Tests.Integration.TestSupport;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass, Ignore]
    public class AccountInfoEditTests
    {
        [TestMethod]
        public async Task GetAccountInfoByUserName()
        {
            var accountInfoEdit = await AccountInfoEdit.GetAccountInfoForEmployee(Constants.KevinFUserId);

            Assert.IsNotNull(accountInfoEdit);
            Assert.AreEqual(Constants.KevinFUserName, accountInfoEdit.UserName);
        }

        [TestMethod]
        public async Task UpdateBadge()
        {
            var newValue = 200;
            var accountInfo = await AccountInfoEdit.GetAccountInfoForEmployee(Constants.KevinFUserId);
            var oldValue = accountInfo.PointPayoutThreshold;
            accountInfo.PointPayoutThreshold = newValue;

            accountInfo = (IAccountInfoEdit)accountInfo.Save();

            Assert.AreEqual(newValue, accountInfo.PointPayoutThreshold);

            //reset
            accountInfo.PointPayoutThreshold = oldValue;
            accountInfo.Save();
        }
    }
}
