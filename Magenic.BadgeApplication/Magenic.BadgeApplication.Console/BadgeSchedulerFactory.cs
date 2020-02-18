using Magenic.BadgeApplication.Common;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;

namespace Magenic.BadgeApplication.Console
{
    public sealed class BadgeSchedulerFactory
    {
        private const int SECONDS_TO_START_FROM = 5;
        private const string GROUP = "ScheduleGroup";
        private static readonly Lazy<StdSchedulerFactory> _SchedulerFactory = new Lazy<StdSchedulerFactory>(GetSchedulerFactory, true);

        private static StdSchedulerFactory SchedulerFactory
        {
            get { return _SchedulerFactory.Value; }
        }

        public void StartJob<T>(string cronSchedule) where T : IJob
        {
            StartJob<T>(false, cronSchedule);
        }

        public void StartJob<T>() where T : IJob
        {
            StartJob<T>(true, null);
        }

        private void StartJob<T>(bool runNow, string cronSchedule) where T : IJob
        {
            Logger.Info<BadgeSchedulerFactory>($"Getting scheduler for type {typeof(T).FullName}.");
            var schedule = SchedulerFactory.GetScheduler();
            schedule.Start();

            Logger.Info<BadgeSchedulerFactory>($"Getting job for type {typeof(T).FullName}.");
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(typeof(T).FullName, GROUP)
                .Build();

            ITrigger trigger = null;

            Logger.Info<BadgeSchedulerFactory>($"Getting trigger for type {typeof(T).FullName}.");
            if (!runNow)
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{typeof(T).FullName}Trigger", GROUP)
                    .StartAt(DateTimeOffset.UtcNow.AddSeconds(SECONDS_TO_START_FROM))
                    .WithCronSchedule(cronSchedule)
                    .Build();
            }
            else
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{typeof(T).FullName}Trigger", GROUP)
                    .StartAt(DateTimeOffset.UtcNow.AddSeconds(SECONDS_TO_START_FROM))
                    .Build();
            }

            Logger.Info<BadgeSchedulerFactory>($"Scheduling job for type {typeof(T).FullName}.");
            schedule.ScheduleJob(job, trigger);
        }

        private static StdSchedulerFactory GetSchedulerFactory()
        {
            var schedulerFactory = new StdSchedulerFactory();
            return schedulerFactory;
        }
    }
}
