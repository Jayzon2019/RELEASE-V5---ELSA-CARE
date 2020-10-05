using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InLifeCMS.Controllers;
using InLifeCMS.Helpers;
using InLifeCMS.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InLifeCMS
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddSingleton<IHostingEnvironment, HostingEnvironment>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(o => o.LoginPath = new PathString("/Login/Login"));

            //this would have been done by the framework any way after this method call;
            //in this case you call the BuildServiceProvider manually to be able to use it
            var serviceProvider = services.BuildServiceProvider();

            //here is where you set you accessor
            var accessor = serviceProvider.GetService<IHttpContextAccessor>();
            //  var host = serviceProvider.GetService<IHostingEnvironment>();
            // UsersService.SetIhosting(host);
            LoginService.SetHttpContextAccessor(accessor);
            UsersService.SetHttpContextAccessor(accessor);
            Comman.SetHttpContextAccessor(accessor);
            HomeController.SetHttpContextAccessor(accessor);
            HeroService.SetHttpContextAccessor(accessor);
            LogsService.SetHttpContextAccessor(accessor);
            PrimeHeroService.SetHttpContextAccessor(accessor);
            FaqCategoriesService.SetHttpContextAccessor(accessor);
            FaqService.SetHttpContextAccessor(accessor);
            PrimeCareService.SetHttpContextAccessor(accessor);
            ProductsService.SetHttpContextAccessor(accessor);
            ProductDetailService.SetHttpContextAccessor(accessor);
            FooterLinksService.SetHttpContextAccessor(accessor);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
