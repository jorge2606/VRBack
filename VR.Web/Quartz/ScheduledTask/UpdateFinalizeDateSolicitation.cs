using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using VR.Data;

namespace VR.Web.Quartz.ScheduledTask
{
    [DisallowConcurrentExecution]
    public class UpdateFinalizeDateSolicitationJob : IJob
    {
        private readonly DataContext _context;
        public UpdateFinalizeDateSolicitationJob(
            DataContext context
        )
        {
            _context = context;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _context.UpdateFinalizeDateSolicitations();
            return Task.CompletedTask;
        }
    }
}
