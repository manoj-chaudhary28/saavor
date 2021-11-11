using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using saavor.Application;
using saavor.ApplicationDapper;
using saavor.EntityFrameworkCore.Context;
using saavor.Shared;
using saavor.Shared.Constants;
using saavor.Shared.AppSettings;
using saavor.Web.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace saavor.Web
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                 {
                     config.Cookie.Name = CommonConstants.CookieSaavorUsers;
                     config.LoginPath = "/auth/signin";
                 });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CookieAuthenticationDefaults.AuthenticationScheme, policy => policy.RequireClaim("SaavorUserId"));
            });
            services.AddControllersWithViews()
              .AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDBContext>(option =>
                  option.UseSqlServer(Configuration.GetConnectionString("SaavorDBConnection")));
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddApplication(Configuration);
            services.AddDapper();
            services.AddShared(Configuration);
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            var dataProtectionSection = Configuration.GetSection("DataProtection");
            services.Configure<DataProtection>(dataProtectionSection);
            var pageSize = Configuration.GetSection("PageSize");
            services.Configure<PageSize>(pageSize);
            services.AddScoped<UtilitieService>();
            var smtp = Configuration.GetSection("Smtp");
            services.Configure<Smtp>(smtp);
            var appUsers = Configuration.GetSection("AppUsers");
            services.Configure<AppUsers>(appUsers);
            services.AddScoped<FCMNotifications>();
            services.AddScoped<APNSNotification>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/Saavor-Logs-{Date}.txt");
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseExceptionHandler("/Home/Error");
            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/auth/signin");
                return Task.CompletedTask;
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // who are you?  
            app.UseAuthentication();

            // are you allowed?  
            app.UseAuthorization();
            app.UseSession();
            // live
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(@"C:\inetpub\wwwroot\saavoruserapi.saavor.com"),
            //    RequestPath = "/avatars"
            //});

            // demo
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(@"C:\inetpub\wwwroot\saavoruserapi.saavor.co"),
            //    RequestPath = "/avatars"
            //});

            // local
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(@"D:\saavoruserapi.saavor.com"),
                RequestPath = "/avatars"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     pattern: "{controller=Auth}/{action=SignIn}/{id?}/{name?}");
                //pattern: "{controller=kitchen}/{action=overview}/{id?}");
            });

        }
    }
}
