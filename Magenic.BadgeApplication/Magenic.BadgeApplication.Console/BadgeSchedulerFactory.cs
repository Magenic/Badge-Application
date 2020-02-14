using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;

namespace Magenic.BadgeApplication.Console
{
    public static class BadgeSchedulerFactory
    {
        private const string GROUP = "ScheduleGroup";
        private static readonly Lazy<StdSchedulerFactory> _SchedulerFactory = new Lazy<StdSchedulerFactory>(GetSchedulerFactory, true);

        private static StdSchedulerFactory SchedulerFactory
        {
            get { return _SchedulerFactory.Value; }
        }

        public static void StartJob<T>(string cronSchedule) where T : IJob
        {
            StartJob<T>(false, cronSchedule);
        }

        public static void StartJob<T>() where T : IJob
        {
            StartJob<T>(true, null);
        }

        private static void StartJob<T>(bool runNow, string cronSchedule) where T : IJob
        {
            var schedule = SchedulerFactory.GetScheduler();
            schedule.Start();

            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(typeof(T).FullName, GROUP)
                .Build();

            ITrigger trigger = null;

            if (!runNow)
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{typeof(T).FullName}Trigger", GROUP)
                    .StartNow()
                    .WithCronSchedule(cronSchedule)
                    .Build();
            }
            else
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{typeof(T).FullName}Trigger", GROUP)
                    .StartNow()
                    .Build();
            }


            schedule.ScheduleJob(job, trigger);
        }

        private static StdSchedulerFactory GetSchedulerFactory()
        {
            var schedulerFactory = new StdSchedulerFactory();
            return schedulerFactory;
        }
    }
}
