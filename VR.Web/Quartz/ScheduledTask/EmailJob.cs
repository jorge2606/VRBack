using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snickler.EFCore;
using VR.Data.Model;
using Microsoft.Extensions.Configuration;
using VR.Data;
using System.IO;
using RazorLight;
using VR.Service.Services;
using System.Net;
namespace VR.Web.Quartz.ScheduledTask
{
    [DisallowConcurrentExecution]
    public class EmailJob : IJob
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailSender;
        public static string StaticFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

        public EmailJob(
            DataContext context,
            IEmailService emailSender
            )
        {
            _context = context;
            _emailSender = emailSender;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var solicitud = _context.GetNotificationAccountFor();
            solicitud.ForEach(async s =>
            {
                var body = "";
                var email = "";
                if (s.NotifySupervisor == 1)
                {
                    email = s.EmailSupervisor;
                    s.Messsage = "Han pasado " + (s.ElapsedDays * -1) + " desde que el usuario " + s.LastName + ", " + s.FirstName + " no presentó su rendición.";
                }
                else
                {
                    email = s.EmailAgent;
                    s.Messsage = "Debe rendir su solicitud, puede rendir hasta la fecha " + s.LastDate + " y le quedan " + s.ElapsedDays;
                }

               // await SendEmailAsync("Email/userMustAccountForSolicitationSupervisor.cshtml", s, email);
            });
            return Task.CompletedTask;
        }

        public async Task SendEmailAsync(
            string p_template, 
            SolicitationSubsidySendEmail solicitationHtml,
            string email
            )
        {
            string template = Path.Combine(StaticFilesDirectory, "Templates");
            var engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(template)
                .UseMemoryCachingProvider()
                .Build();

            string result = await engine.CompileRenderAsync(p_template, solicitationHtml);

            var emailSended = await _emailSender.SendEmail(solicitationHtml.EmailSupervisor, "Rendición de Solicitud", result);
        }

    }
}
