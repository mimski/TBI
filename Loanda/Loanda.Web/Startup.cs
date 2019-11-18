using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Web.Providers;
using Loanda.Web.Infrastracture;
using System.Reflection;
using Loanda.Services.Contracts;
using Loanda.Services;
using Loanda.Web.Mappers.Contracts;
using Loanda.EmailClient;
using Loanda.EmailClient.Contracts;
//using Loanda.Services.Mapper.Contracts;
//using Loanda.Services.Mapper;
using Loanda.Web.Mappers;
using Loanda.Web.Models.Email;
using Loanda.Services.DTOs;

namespace Loanda.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LoandaContext>(options =>
                options.UseNpgsql(Configuration.GetDefaultConnectionString()));

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<LoandaContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IUserManager<>), typeof(UserManagerWrapper<>));

            #region Register all services from the service layer
            services.AddBusiness();

            //services.Scan(x => x.FromAssemblies(Assembly.Load("Loanda.Service"))
            //    .AddClasses(classes => classes.Where(type => type.Name.EndWith("Service"))))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime();
            #endregion

            #region Register all mappers

            //services.AddSingleton<IMapper<BookViewModel, Book>, BookMapper>();

            //services.AddSingleton<IEmailDtoMapper, EmailDtoMapper>();


            services.AddSingleton<IMapper<ReceivedEmailEntity, EmailViewModel>, EmailViewModelMapper>();


            #endregion

            #region Register Apis

            services.AddScoped<IGmailApi, GmailApi>();

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandling(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "adminArea",
                 template: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.SeedData();
        }
    }
}
