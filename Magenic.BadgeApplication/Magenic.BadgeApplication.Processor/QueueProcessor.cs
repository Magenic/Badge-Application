using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Configuration;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public class QueueProcessor
    {
        private IContainer _factory;

        private IItemProcessor _itemProcessor;
        private IQueueItemDAL _queueItemDAL;        

        private int SleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SleepIntervalInMilliseconds"]); }
        }

        public QueueProcessor() : this(IoC.Container)
        {
            
        }

        public QueueProcessor(IContainer factory)
        {
            _factory = factory;

            _itemProcessor = _factory.Resolve<IItemProcessor>();
            _queueItemDAL = _factory.Resolve<IQueueItemDAL>();  
        }        

        /// <summary>
        /// This method runs the queue process
        /// </summary>
        public void Start()
        {
            Logger.Info<QueueProcessor>("The Queue Processor was started");

            while (true)
            {
                try
                {
                    QueueItemDTO latestItem = _queueItemDAL.Peek();

                    if (latestItem != null)
                    {
                        Logger.InfoFormat<QueueProcessor>("Processor peeked item with QueueItemId: {0} and BadgeAwardId: {1}, processing...",
                            latestItem.QueueItemId,
                            latestItem.BadgeAwardId);

                        _itemProcessor.ProcessItem(latestItem);
                    }
                    else
                    {
                        Logger.InfoFormat<QueueProcessor>("No items found in the queue, sleeping for {0} seconds", SleepInterval/1000);  

                        Thread.Sleep(SleepInterval);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error<QueueProcessor>(ex.Message, ex);
                }
            }            
        }        
    }
}
