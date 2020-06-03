using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public class BadgeApplicationProcessor
    {
        private Timer processTimer;
        private IList<int> queueHours;
        private IList<int> submitHours;
        private IList<int> badgeRequestsHours;

        private int lastQueueHour = 0;
        private int lastSubmitHour = 0;
        private int lastBadgeRequestsHour = 0;

        private IContainer _factory;

        private int TimerInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["TimerIntervalInMilliseconds"]); }
        }

        private string QueueProcessingHours
        {
            get { return ConfigurationManager.AppSettings["QueueProcessingHours"]; }
        }

        private DayOfWeek NotificationDay
        {
            get { return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), ConfigurationManager.AppSettings["NotificationDay"]); }
        }

        private int NotificationHourOfDay
        {
            get { return Int32.Parse(ConfigurationManager.AppSettings["NotificationHourOfDay"]); }
        }

        private string SubmissionNotifyProcessingHours
        {
            get { return ConfigurationManager.AppSettings["SubmissionNotifyProcessingHours"]; }
        }

        private string BadgeRequestsProcessingHours
        {
            get { return ConfigurationManager.AppSettings["BadgeRequestsProcessingHours"]; }
        }

        public BadgeApplicationProcessor() : this(IoC.Container)
        {

        }

        public BadgeApplicationProcessor(IContainer factory)
        {
            _factory = factory;

            queueHours = new List<int>();
            submitHours = new List<int>();
            badgeRequestsHours = new List<int>();
        }

        public void Start()
        {
            Logger.Info<BadgeApplicationProcessor>("The Badge Application Processor was started");

            var queueList = QueueProcessingHours.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in queueList)
            {
                queueHours.Add(int.Parse(item));
            }

            var submitList = SubmissionNotifyProcessingHours.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in submitList)
            {
                submitHours.Add(int.Parse(item));
            }

            var badgeRequestsList = BadgeRequestsProcessingHours.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in badgeRequestsList)
            {
                badgeRequestsHours.Add(int.Parse(item));
            }

            processTimer = new Timer();
            processTimer.Interval = TimerInterval;
            processTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            processTimer.AutoReset = true;
            processTimer.Start();
        }

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            processTimer.Stop() ;

            var hourOfDay = DateTime.Now.Hour;

            if (queueHours.Contains(hourOfDay) && !lastQueueHour.Equals(hourOfDay))
            {
                lastQueueHour = hourOfDay;
                try
                {
                    var queueProcessor = new QueueProcessor();
                    queueProcessor.Process();
                }
                catch (Exception ex)
                {
                    Logger.Error<QueueProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
                }
            }

            if (DateTime.Now.DayOfWeek == NotificationDay && DateTime.Now.Hour == NotificationHourOfDay && DateTime.Now.Minute == 0)
            {
                try
                {
                    var notificationProcessor = new NotificationProcessor();
                    notificationProcessor.Process();
                }
                catch (Exception ex)
                {
                    Logger.Error<NotificationProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
                }
            }

            if (submitHours.Contains(hourOfDay) && !lastSubmitHour.Equals(hourOfDay))
            {
                lastSubmitHour = hourOfDay;
                try
                {
                    var submissionProcessor = new SubmissionNotifyProcessor();
                    submissionProcessor.Process();
                }
                catch(Exception ex)
                {
                    Logger.Error<SubmissionNotifyProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
                }
            }

            if (badgeRequestsHours.Contains(hourOfDay) && !lastBadgeRequestsHour.Equals(hourOfDay))
            {
                lastBadgeRequestsHour = hourOfDay;
                try
                {
                    var badgeRequestProcessor = new BadgeRequestProcessor();
                    badgeRequestProcessor.Process();
                }
                catch (Exception ex)
                {
                    Logger.Error<BadgeRequestProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
                }
            }

            processTimer.Start();

        }
    }
}
