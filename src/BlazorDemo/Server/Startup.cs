using Demo;
using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Myvas.AspNetCore.Authentication;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("BlazorDemo.Server")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddUserManager<AppUserManager>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                //options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
            });

            services.AddAuthentication()
                .AddWeixinOpen(options =>
                {
                    options.AppId = Configuration["WeixinOpen:AppId"];
                    options.AppSecret = Configuration["WeixinOpen:AppSecret"];
                    options.SaveTokens = true;
                })
                //.AddWeixinAuth(options =>
                //{
                //    options.AppId = Configuration["WeixinAuth:AppId"];
                //    options.AppSecret = Configuration["WeixinAuth:AppSecret"];
                //    options.SilentMode = false; //不采用静默模式
                //                                //options.SaveTokens = true;
                //})
                .AddQQConnect(options =>
                {
                    options.AppId = Configuration["QQConnect:AppId"];
                    options.AppKey = Configuration["QQConnect:AppKey"];
                    //options.SaveTokens = true;

                    QQConnectScopes.TryAdd(options.Scope,
                        QQConnectScopes.get_user_info,
                        QQConnectScopes.list_album,
                        QQConnectScopes.upload_pic,
                        QQConnectScopes.do_like);
                });

            services.AddTencentSms(options =>
            {
                options.SdkAppId = Configuration["TencentSms:SdkAppId"];
                options.AppKey = Configuration["TencentSms:AppKey"];
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

            //services.AddControllers().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
            //    AppDbInitializer.Initialize(serviceScope.ServiceProvider).Wait();
            //}

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}
