using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MusicData;
using MusicData.Models;
using MusicWorld.Models;
using MusicWorld.Services;
using MusicWorld.Services.Cart;
using Stripe;

namespace MusicWorld
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddTransient<FormattingService>();
            services.AddTransient<IProduct, UserProductService>();
            



            services.AddTransient<FeatureToggle>(x => new FeatureToggle()
            {
                DevelopersExceptions = _config.GetValue<bool>("FeatureToggles:DeveloperExceptions")

            });
           

            services.AddDbContextPool<MusicContext>(options =>
            {
                var connectionString = _config.GetConnectionString("MusicContext");

                options.UseSqlServer(connectionString);

            });

           



            //IdentityUser has properties like username,email etc. and a collection of user claims
            //IdentityRole->provides authorization information like access rights
            //AddEntityFrameworkStores retrieves user and roles from our Db
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 7;   // override de default rules for Password prop
            }).AddEntityFrameworkStores<MusicContext>();

            services.AddSession(options =>
            {
                options.Cookie.Name = "Cart";
                options.Cookie.MaxAge = TimeSpan.FromDays(365);

            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //we registered the secret key on our backend middleware
            StripeConfiguration.ApiKey = _config.GetSection("Stripe")["SecretKey"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,FeatureToggle features)
        {
            app.UseExceptionHandler("/Home/Error");

            if (features.DevelopersExceptions)
            {
                app.UseDeveloperExceptionPage();
            }
          

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "blog",
                   template: "{controller=Blog}/{action=Post}/{year}/{month}/{key}");
            }
              
        );

           
        }
    }
}


