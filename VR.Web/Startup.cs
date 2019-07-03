using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Audit.Data;
using Audit.Core;
using Microsoft.AspNetCore.Http;
using VR.Web.Helpers;
using Audit.EntityFramework;
using Audit.Service.Interfaces;
using Audit.Service.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using VR.Common.Extensions;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Dto.User;
using VR.Identity.Identities;
using VR.Service.Interfaces;
using VR.Service.Services;
using VR.Web.Extensions;
using Service.Common;
using VR.Data.Stores_Procedures.StoresProcedures;
using VR.Data.Stores_Procedures.IStoresProcedures;
using VR.Common.Security;
using Quartz;
using Quartz.Spi;
using VR.Web.Quartz;
using VR.Web.Quartz.ScheduledTask;

namespace VR.Web
{
    public class Startup
    {

        private const string _defaultCorsPolicyName = "AllowOrigin";
        private const string enUSCulture = "es-ES";
        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; }

        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ASPDatabase")));
            services.AddDbContext<AuditContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuditDatabase")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddHttpContextAccessor();

            //begin Audit config
            Audit.Core.Configuration.Setup()
                .UseEntityFramework(ef => ef
                    .UseDbContext(x =>
                    {
                        var sp = services.BuildServiceProvider();
                        return sp.GetService<AuditContext>();
                    })
                    //.UseDbContext<AuditContext>(Configuration.GetConnectionString("AuditDatabase"))
                    .AuditTypeExplicitMapper(m => m
                        .Map<Notification, Audit_Notification>()
                        .Map<User, Audit_User>()
                        .AuditEntityAction<IAudit>((evt, entry, auditEntity) =>
                        {
                            var entityFrameworkEvent = evt.GetEntityFrameworkEvent();
                            MyService user = new MyService();
                            auditEntity.Id = new Guid();
                            auditEntity.AuditDate = DateTime.UtcNow;
                            auditEntity.AuditUser = user.getCurrentUserName();
                            auditEntity.AuditUserId = user.getCurrentUserId();
                            auditEntity.EntityId = (Guid) entry.PrimaryKey["Id"];
                            auditEntity.AuditAction = entry.Action;

                            //entityFrameworkEvent. 
                            // Insert, Update, Delete
                        })
                    )
                );

            services.AddLocalization();
            // MVC
            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            ).AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.ImplicitlyValidateChildProperties = true;
                }
            ).AddViewLocalization()
             .AddDataAnnotationsLocalization();

        //FluentValidation

            services.AddAutoMapper();

            services.AddCors(
               options => options.AddPolicy(
                   _defaultCorsPolicyName,
                   builder => builder
                       .WithOrigins(
                           // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                           Configuration["App:CorsOrigins"]
                               .Split(",", StringSplitOptions.RemoveEmptyEntries)
                               .Select(o => o.RemovePostFix("/"))
                               .ToArray()
                       )
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
               )
           );

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings").GetSection("Secret").Value;

            var key = Encoding.ASCII.GetBytes(appSettingsSection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser()
                   .Build();
                //solcitation
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateSolicitation, pol => pol.RequireClaim(SolicitationSubsidyClaims.CanCreateSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateCommissionSolicitation, comm => comm.RequireClaim(SolicitationSubsidyClaims.CanCreateCommissionSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanUpdateCommissionSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanUpdateCommissionSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewSolicitation, view => view.RequireClaim(SolicitationSubsidyClaims.CanViewSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanAcceptMySolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanAcceptMySolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanModerateSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanModerateSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanApproveAccountForSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanApproveAccountForSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.CanModerateRefund, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanModerateRefund));
                options.AddPolicy(SolicitationSubsidyClaims.canPrintSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.canPrintSolicitation));
                options.AddPolicy(SolicitationSubsidyClaims.canPrintAccountForSolicitation, catView => catView.RequireClaim(SolicitationSubsidyClaims.canPrintAccountForSolicitation));
                //users
                options.AddPolicy(SolicitationSubsidyClaims.CanViewUsers, seeUsers => seeUsers.RequireClaim(SolicitationSubsidyClaims.CanViewUsers));
                options.AddPolicy(SolicitationSubsidyClaims.CanAddSupervisorUsers, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanAddSupervisorUsers));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditUsers, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditUsers));
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateUsers, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateUsers));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteUsers, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteUsers));

                //category
                options.AddPolicy(SolicitationSubsidyClaims.CanViewCategory, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewCategory));
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateCategory, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateCategory));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteCategory, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteCategory));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditCategory, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditCategory));

                //Organism
                options.AddPolicy(SolicitationSubsidyClaims.CanViewOrganism, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewOrganism));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditOrganism, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditOrganism));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteOrganism, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteOrganism));
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateOrganism, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateOrganism));

                //Distribution
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateDistribution, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateDistribution));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditDistribution, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditDistribution));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteDistribution, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteDistribution));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewDistribution, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewDistribution));

                //Transport
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateTransport, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateTransport));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditTransport, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditTransport));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteTransport, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteTransport));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewTransport, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewTransport));

                //Holidays
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateHoliday, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateHoliday));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditHoliday, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditHoliday));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteHoliday, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteHoliday));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewHoliday, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewHoliday));

                //Expenditure
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateExpenditure, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanCreateExpenditure));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditExpenditure, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanEditExpenditure));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteExpenditure, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanDeleteExpenditure));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewExpenditure, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewExpenditure));

                //Roles
                options.AddPolicy(SolicitationSubsidyClaims.CanViewRoles, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewRoles));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditRoles, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanViewRoles));

                //Audit
                options.AddPolicy(SolicitationSubsidyClaims.CanAudits, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanAudits));

                //Supervisor User-Agent
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateRelationshipBeetwenSupervisorAndAgent, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanAudits));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteRelationshipBeetwenSupervisorAndAgent, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanAudits));
                options.AddPolicy(SolicitationSubsidyClaims.CanToSeeRelationshipBeetwenSupervisorAndAgent, catView => catView.RequireClaim(SolicitationSubsidyClaims.CanAudits));

                //Report
                options.AddPolicy(SolicitationSubsidyClaims.CanViewReport, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanViewReport));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewPendingSolicitations, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanViewPendingSolicitations));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewSolicitationsExpire, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanViewSolicitationsExpire));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewExpendituresReport, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanViewExpendituresReport));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewReportByOrganism, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanViewReportByOrganism));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewReportByUsers, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanViewReportByUsers));

                //AdministrativeResolution
                options.AddPolicy(SolicitationSubsidyClaims.CanCreateLegalRuling, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanCreateLegalRuling));
                options.AddPolicy(SolicitationSubsidyClaims.CanEditLegalRuling, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanCreateLegalRuling));
                options.AddPolicy(SolicitationSubsidyClaims.CanDeleteLegalRuling, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanCreateLegalRuling));
                options.AddPolicy(SolicitationSubsidyClaims.CanViewLegalRuling, opt => opt.RequireClaim(SolicitationSubsidyClaims.CanCreateLegalRuling));
        });

            services.AddIdentity<User, Role>(o => {     
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
             })
             .AddUserStore<UserStore>()
             .AddRoleStore<RoleStore>()
             .AddUserManager<UserManager>()
             .AddRoleManager<RoleManager>()
             .AddSignInManager<SignInManager>()
             .AddEntityFrameworkStores<DataContext>()
             .AddDefaultTokenProviders();
           
            // Identity Services
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IRoleStore<Role>, RoleStore>();

            services.AddScoped<UserManager, UserManager>();
            services.AddScoped<RoleManager, RoleManager>();

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsSender, EmailService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<IDistributionService, DistributionService>();
            services.AddScoped<IExpenditureTypeService, ExpenditureTypeService>();
            services.AddScoped<IOrganismService, OrganismService>();
            services.AddScoped<ISolicitationSubsidyService, SolicitationSubsidyService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPlaceService,PlaceService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICodeLiquidationService, CodeLiquidationService>();
            services.AddScoped<IExpenditureService,ExpenditureService>();
            services.AddScoped<IDestinyService, DestinyService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IAspNetRolesService, AspNetRolesService>();
            services.AddScoped<IAspNetUserRolesService, AspNetUserRolesService>();
            services.AddScoped<ISupervisorUserAgentService, SupervisorUserAgentService>();
            services.AddScoped<ISupplementaryCityService, SupplementaryCityService>();
            services.AddScoped<ISolcitationSubsidyProcedure, SolicitationSubsidyProcedure>();
            services.AddScoped<ISolicitationStateService, SolicitationStateService>();
            services.AddScoped<ILegalRulings, LegalRulingsService>();
            services.AddScoped<IReportService,ReportService>();
            services.AddScoped<IApproveOfAuthorityThatOrderCommissionsService, ApproveOfAuthorityThatOrderCommissionsService>();
            services.AddScoped<IApproveOfAuthority_SolicitationService, ApproveOfAuthority_SolicitationService>();
            services.AddScoped<IObservationService, ObservationService>();
            services.AddScoped<IRoleService, RoleService>();
            //Auditoria
            services.AddScoped<IUserAuditService, UserAuditService>();
            services.AddScoped<INotificationAuditService, NotificationAuditService>();
            //sender Email
            // Add application services.
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISmsSender, EmailService>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            //Validation
            services.AddTransient<IValidator<SaveUserDto>, UserValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginValidator>();
            services.AddTransient<IValidator<CategoryBaseDto>, CategoryValidator>();
            services.AddTransient<IValidator<DistributionBaseDto>, DistributionValidator>();
            services.AddTransient<IValidator<TransportBaseDto>, TransportValidator>();
            services.AddTransient<IValidator<ExpenditureTypeBaseDto>, ExpenditureTypeValidator>();
            services.AddTransient<IValidator<OrganismBaseDto>, OrganismValidator>();
            services.AddTransient<IValidator<CreateUserDto>, UserCreateValidator>();
            services.AddTransient<IValidator<SolicitationSubsidyBaseDto>, SolicitationSubsidyValidator>();
            services.AddTransient<IValidator<HolidayBaseDto>, HolidayValidator>();
            services.AddTransient<IValidator<ResetPassword>, ResetPasswordValidator>();
            //Quartz
            services.AddTransient<IJobFactory, CustomJobFactory>();
            services.AddTransient<EmailJob>();
            services.AddTransient<UpdateFinalizeDateSolicitationJob>();
            services.AddTransient<IJob,EmailJob>();
            services.AddTransient<IJob, UpdateFinalizeDateSolicitationJob>();
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("es-ES")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("es-ES"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseDefaultFiles();

            var staticFileDirectory =
                Path.Combine(FileService.StaticFilesDirectory);

            if (!Directory.Exists(staticFileDirectory))
            {
                Directory.CreateDirectory(staticFileDirectory);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFileDirectory),
                RequestPath = "/StaticFiles"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(staticFileDirectory, "Profile")),
                RequestPath = "/StaticFiles"
            });
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Job Factory through IOC container
            var jobFactory = (IServiceProvider)app.ApplicationServices.GetService(typeof(IServiceProvider));
            JobScheduler.StartAsync(jobFactory);

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            
            app.UseStaticHttpContext();

            app.UseMvc();
        }
    }
}
