using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Quartz;
using System;
using System.Configuration;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public sealed class QueueProcessor
    {
        private IContainer _factory;

        private IItemProcessor _itemProcessor;
        private IQueueItemDAL _queueItemDAL;        

        private int SleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SleepIntervalInMilliseconds"]); }
        }

        private int ErrorSleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ErrorSleepIntervalInMilliseconds"]); }
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
            var consecutiveErrorCount = 0;
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
                    consecutiveErrorCount = 0;
                }
                catch (Exception ex)
                {
                    Logger.Error<QueueProcessor>(ex.Message, ex);
                    consecutiveErrorCount ++;
                    if (consecutiveErrorCount >= 5)
                    {
                        //Continuous logging of an error in a tight loop is bad, go to sleep and see if the system 
                        //recovers
                        Logger.InfoFormat<QueueProcessor>("Queue processor consecutive error limit exceeded, sleeping for {0} seconds", ErrorSleepInterval / 1000);  
                        Thread.Sleep(ErrorSleepInterval);
                    }
                }
            }            
        }
    }
}
