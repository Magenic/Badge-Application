using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
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
            while (true)
            {
                try
                {
                    QueueItemDTO latestItem = _queueItemDAL.Peek();

                    if (latestItem != null)
                    {
                        _itemProcessor.ProcessItem(latestItem);
                    }
                    else
                    {
                        Thread.Sleep(SleepInterval);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }        
    }
}
