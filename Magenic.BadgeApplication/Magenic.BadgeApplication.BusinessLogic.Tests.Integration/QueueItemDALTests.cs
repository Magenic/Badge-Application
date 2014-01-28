using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
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

                QueueItemDTO addedItem = queueItemDAL.Add(queueItem);

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

                QueueItemDTO addedItem = queueItemDAL.Add(queueItem);               

                addedItem.BadgeAwardId = 2;

                QueueItemDTO updatedItem = queueItemDAL.Update(addedItem);
                Assert.IsTrue(updatedItem.BadgeAwardId == 2);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void TestDelete()
        {
            ExecuteWithTransaction(() =>
            {
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueItemDTO queueItem = new QueueItemDTO
                {
                    BadgeAwardId = 1
                };

                QueueItemDTO addedItem = queueItemDAL.Add(queueItem);
                Assert.IsTrue(addedItem.QueueItemId > 0);

                queueItemDAL.Delete(addedItem.QueueItemId);

                queueItemDAL.Get(addedItem.QueueItemId);                
            });
        }

        [TestMethod]
        public void TestGetTopItem()
        {
            ExecuteWithTransaction(() =>
            {
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueItemDTO queueItem = new QueueItemDTO
                {
                    BadgeAwardId = 1
                };

                QueueItemDTO addedItem = queueItemDAL.Add(queueItem);

                QueueItemDTO topItem = queueItemDAL.Peek();
                Assert.IsTrue(topItem.QueueItemId == addedItem.QueueItemId);
            });
        }
    }
}
