using Quartz;
using Quartz.Spi;
using System;
using Microsoft.EntityFrameworkCore;

namespace VR.Web.Quartz
{
    public class CustomJobFactory : IJobFactory
    {
        private readonly IServiceProvider _factory;

        public CustomJobFactory(IServiceProvider factory)
        {
            _factory = factory;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _factory.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
