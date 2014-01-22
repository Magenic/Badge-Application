using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class QueueItemDALTests : TransactionalTest
    {
        [TestMethod]
        public void TestAdd()
        {
            ExecuteWithTransaction(() => 
            {
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueItemDTO queueItem = new QueueItemDTO
                {
                    BadgeAwardId = 1
                };

                QueueItemDTO addedItem = queueItemDAL.AddItem(queueItem);

                Assert.IsTrue(addedItem.QueueItemId > 0);
            });            
        }

        [TestMethod]
        public void TestUpdate()
        {
            ExecuteWithTransaction(() =>
            {
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueItemDTO queueItem = new QueueItemDTO
                {
                    BadgeAwardId = 1
                };

                QueueItemDTO addedItem = queueItemDAL.AddItem(queueItem);               

                addedItem.BadgeAwardId = 2;

                QueueItemDTO updatedItem = queueItemDAL.UpdateItem(addedItem);
                Assert.IsTrue(updatedItem.BadgeAwardId == 2);
            });
        }
    }
}
