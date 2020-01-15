using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass, Ignore]
    public class QueueEventLogDALTests : TransactionalTest
    {
        [TestMethod]
        public void TestAdd()
        {
            ExecuteWithTransaction(() =>
            {
                IQueueEventLogDAL queueEventLogDAL = IoC.Container.Resolve<IQueueEventLogDAL>();
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueEventLogDTO addedItem = AddQueueEventLogRecord(queueEventLogDAL, queueItemDAL);

                Assert.IsTrue(addedItem.BadgeAwardId > 0);
            });
        }       

        [TestMethod]
        public void TestUpdate()
        {
            ExecuteWithTransaction(() =>
            {
                IQueueEventLogDAL queueEventLogDAL = IoC.Container.Resolve<IQueueEventLogDAL>();
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueEventLogDTO addedItem = AddQueueEventLogRecord(queueEventLogDAL, queueItemDAL);

                addedItem.Message = "Test Update";

                QueueEventLogDTO updatedItem = queueEventLogDAL.Update(addedItem);

                Assert.IsTrue(updatedItem.QueueEventLogId == addedItem.QueueEventLogId);
                Assert.IsTrue(updatedItem.Message == addedItem.Message);
            });
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void TestDelete()
        {
            ExecuteWithTransaction(() =>
            {
                IQueueEventLogDAL queueEventLogDAL = IoC.Container.Resolve<IQueueEventLogDAL>();
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueEventLogDTO addedItem = AddQueueEventLogRecord(queueEventLogDAL, queueItemDAL);

                queueEventLogDAL.Delete(addedItem.QueueEventLogId);

                queueEventLogDAL.Get(addedItem.QueueEventLogId);
            });
        }

        private static QueueEventLogDTO AddQueueEventLogRecord(IQueueEventLogDAL queueEventLogDAL, IQueueItemDAL queueItemDAL)
        {
            //Grab the top item
            QueueItemDTO queueItemDTO = queueItemDAL.Peek();

            Assert.IsNotNull(queueItemDTO);

            QueueEventLogDTO queueEventLogItem = new QueueEventLogDTO
            {
                Message = "Test Message",
                QueueEventCreated = DateTime.Now,
                QueueEventId = 1,
                BadgeAwardId = queueItemDTO.QueueItemId
            };

            QueueEventLogDTO addedItem = queueEventLogDAL.Add(queueEventLogItem);
            return addedItem;
        }
    }
}
