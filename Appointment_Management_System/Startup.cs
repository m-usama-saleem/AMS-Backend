using System;
using Appointment_Management_System.Middleware;
using Appointment_Management_System.Models;
using Appointment_Management_System.Services;
using Appointment_Management_System.Services.AppointmentModule;
using Appointment_Management_System.Services.Common;
using Appointment_Management_System.Services.FinanceModule;
using Appointment_Management_System.Services.InstitutionManagement;
using Appointment_Management_System.Services.TranslatorManagement;
using Appointment_Management_System.Services.UserManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Appointment_Management_System.Data;

namespace Appointment_Management_System
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Connection String  
            services.AddDbContext<DatabaseContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            #endregion


            //Inject Services to pipeline
            services.AddCors();

            services.AddControllers();
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ITranslatorService, TranslatorService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IFinanceService, FinanceService>();

            //Configure OpenAPI Swagger docs
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Environment type name: {0}", env.EnvironmentName);
            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            // Ensure database is created at startup for development only
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (env.IsEnvironment("Testing"))
                {
                    dbContext.Database.EnsureCreated();
                }
                // open this section if required for production,
                // but make sure to trigger ci/cd pipeline for app whenever migrations are pushed
                // else
                // {
                //     dbContext.Database.Migrate();
                // }

            }
            
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseCors(builder => builder.WithOrigins("http://localhost:3000", "http://localhost:8010", "http://NOUMANQURESHI:8010")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
