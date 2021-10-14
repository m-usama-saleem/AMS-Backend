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
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ITranslatorService, TranslatorService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IFinanceService, FinanceService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
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
