using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Capstone
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

            string connectionString = Configuration.GetConnectionString("MidwestPodcast");
            services.AddScoped<IEventSqlDal>(j => new EventSqlDal(connectionString));
            services.AddScoped<IGenreSqlDal>(j => new GenreSqlDal(connectionString));
            services.AddScoped<IPodcasterAvailabilitySqlDal>(j => new PodcasterAvailabilitySqlDal(connectionString));
            services.AddScoped<IPodcastSqlDal>(j => new PodcastSqlDal(connectionString));
            services.AddScoped<IUserSqlDal>(j => new UserSqlDal(connectionString));
            services.AddScoped<IVenueSqlDal>(j => new VenueSqlDal(connectionString));
            services.AddScoped<ITagsSqlDal>(j => new TagsSqlDal(connectionString));
            services.AddScoped<IUserEventSqlDal>(j => new UserEventSqlDal(connectionString));
            services.AddScoped<ITicketSqlDal>(j => new TicketSqlDal(connectionString));


            // Indicates Session should be saved "in memory"
            services.AddDistributedMemoryCache();


            // Sets the options on Session
            services.AddSession(options =>
            {
                // Sets session expiration to 20 minuates
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });

            services.AddMvc();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            // Tells our application to use session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
