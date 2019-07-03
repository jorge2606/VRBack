using System;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Collections.Specialized;

namespace VR.Web.Quartz.ScheduledTask
{
    public class JobScheduler
    {
        public static async void StartAsync(IServiceProvider jobFactory)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            });
            var scheduler = await schedulerFactory.GetScheduler();
            scheduler.JobFactory = new CustomJobFactory(jobFactory);
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailJob>().Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInHours(12)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            IJobDetail updateFinalizaDate = JobBuilder.Create<UpdateFinalizeDateSolicitationJob>().Build();
            ITrigger triggerUpdateFinalizaDate = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInHours(11)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
                .Build();

            await scheduler.ScheduleJob(updateFinalizaDate, triggerUpdateFinalizaDate);

        }
    }
}
