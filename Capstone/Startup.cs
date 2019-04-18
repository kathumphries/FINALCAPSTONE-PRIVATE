using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

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
            {
                services.AddScoped<SmtpClient>((serviceProvider) =>
                {
                    var config = serviceProvider.GetRequiredService<IConfiguration>();
                    return new SmtpClient()
                    {
                        Host = config.GetValue<String>("Email:Smtp:Host"),
                        Port = config.GetValue<int>("Email:Smtp:Port"),
                        Credentials = new System.Net.NetworkCredential(
                            config.GetValue<String>("Email:Smtp:Username"),
                            config.GetValue<String>("Email:Smtp:Password")
                        )
                    };
                });
                services.AddMvc();
            }
        
        





        // Session must be configured for our authentication
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This determines whether user consent for non-essential cookies
                //is needed.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Sets session expiration to 20 minuates
                //options.IdleTimeout = TimeSpan.FromMinutes(20);
                //options.Cookie.HttpOnly = true;
            });

            // Dependency Injection
            string connectionString = Configuration.GetConnectionString("MidwestPodcast");
            services.AddScoped<IEventSqlDal>(j => new EventSqlDal(connectionString));
            services.AddScoped<IGenreSqlDal>(j => new GenreSqlDal(connectionString));
            services.AddScoped<IPodcastSqlDal>(j => new PodcastSqlDal(connectionString));
            services.AddScoped<IUserSqlDal>(j => new UserSqlDal(connectionString));
            services.AddScoped<IVenueSqlDal>(j => new VenueSqlDal(connectionString));
            services.AddScoped<ITagsSqlDal>(j => new TagsSqlDal(connectionString));
            services.AddScoped<IUserEventSqlDal>(j => new UserEventSqlDal(connectionString));
            services.AddScoped<ITicketSqlDal>(j => new TicketSqlDal(connectionString));

            // For Authentication
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthProvider, SessionAuthProvider>();
            services.AddTransient<IUserSqlDal>(m => new UserSqlDal(connectionString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();


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

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Search}/{action=Index}/{id?}");
            });
        }




    }
}
