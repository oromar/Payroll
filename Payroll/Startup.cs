using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Payroll
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");                    
                });

            services.AddScoped<BusinessObject<Occupation>>();
            services.AddScoped<BusinessObject<Currency>>();
            services.AddScoped<BusinessObject<LicenseType>>();
            services.AddScoped<BusinessObject<Company>>();
            services.AddScoped<BusinessObject<Workplace>>();
            services.AddScoped<BusinessObject<JobRole>>();
            services.AddScoped<BusinessObject<Function>>();
            services.AddScoped<BusinessObject<OccurrenceType>>();
            services.AddScoped<BusinessObject<Employee>>();
            services.AddScoped<BusinessObject<Department>>();
            services.AddScoped<BusinessObject<EmployeeHistory>>();
            services.AddScoped<ProjectBO>();

            services.AddScoped<GenericDAO<Occupation>>();
            services.AddScoped<GenericDAO<Currency>>();
            services.AddScoped<GenericDAO<LicenseType>>();
            services.AddScoped<GenericDAO<Company>>();
            services.AddScoped<GenericDAO<Workplace>>();
            services.AddScoped<GenericDAO<JobRole>>();
            services.AddScoped<GenericDAO<Function>>();
            services.AddScoped<GenericDAO<OccurrenceType>>();
            services.AddScoped<GenericDAO<Employee>>();
            services.AddScoped<GenericDAO<Department>>();
            services.AddScoped<GenericDAO<EmployeeHistory>>();
            services.AddScoped<GenericDAO<Project>>();

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddSingleton<Message>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Dashboard/Error");
                app.UseHsts();
            }

            app.UseRequestLocalization("pt-BR", "en-US", "fr-FR");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
