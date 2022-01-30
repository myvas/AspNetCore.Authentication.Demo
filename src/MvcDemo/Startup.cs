using Demo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Myvas.AspNetCore.Weixin;
using System.Reflection;

namespace Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration config, IWebHostEnvironment environment)
        {
            Configuration = config;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            if (Environment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }
            else
            {
                services.AddControllersWithViews();
            }

            services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<DemoDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false,
                    RequireDigit = false
                };
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.SignIn.RequireConfirmedPhoneNumber = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            //services.AddAuthentication();
            //    .AddWeixinOpen(options =>
            //    {
            //        options.AppId = Configuration["WeixinOpen:AppId"];
            //        options.AppSecret = Configuration["WeixinOpen:AppSecret"];
            //        options.SaveTokens = true;
            //    })
            //    .AddWeixinAuth(options =>
            //    {
            //        options.AppId = Configuration["WeixinAuth:AppId"];
            //        options.AppSecret = Configuration["WeixinAuth:AppSecret"];
            //        options.SilentMode = false; //不采用静默模式
            //        //options.SaveTokens = true;
            //    })
            //    .AddQQConnect(options =>
            //    {
            //        options.AppId = Configuration["QQConnect:AppId"];
            //        options.AppKey = Configuration["QQConnect:AppKey"];
            //        //options.SaveTokens = true;

            //        QQConnectScopes.TryAdd(options.Scope,
            //            QQConnectScopes.get_user_info,
            //            QQConnectScopes.list_album,
            //            QQConnectScopes.upload_pic,
            //            QQConnectScopes.do_like);
            //    });

            services.AddTencentSms(options =>
            {
                options.SdkAppId = Configuration["TencentSms:SdkAppId"];
                options.AppKey = Configuration["TencentSms:AppKey"];
            });

            services.AddViewDivert();
            services.AddWeixinApi(options =>
            {
                options.AppId = Configuration["Weixin:AppId"];
                options.AppSecret = Configuration["Weixin:AppSecret"];
            });
            services.AddWeixinJssdk(options =>
            {
                options.AppId = Configuration["Weixin:AppId"];
            });
            services.AddWeixinSite(options =>
            {
                options.AppId = Configuration["Weixin:AppId"];
                options.AppSecret = Configuration["Weixin:AppSecret"];
                //options.Debug = true;
                options.WebsiteToken = Configuration["Weixin:WebsiteToken"];
                //options.EncodingAESKey = Configuration["Weixin:EncodingAESKey"];
                //options.Path = "/wx"; //default is "/wx"
                //options.Events = new WeixinEvents()
                //{
                //    OnTextMessageReceived = ctx => weixinEventSink.OnTextMessageReceived(ctx.Sender, ctx.Args),
                //    OnLinkMessageReceived = ctx => weixinEventSink.OnLinkMessageReceived(ctx.Sender, ctx.Args),
                //    OnClickMenuEventReceived = ctx => weixinEventSink.OnClickMenuEventReceived(ctx.Sender, ctx.Args),
                //    OnImageMessageReceived = ctx => weixinEventSink.OnImageMessageReceived(ctx.Sender, ctx.Args),
                //    OnLocationEventReceived = ctx => weixinEventSink.OnLocationEventReceived(ctx.Sender, ctx.Args),
                //    OnLocationMessageReceived = ctx => weixinEventSink.OnLocationMessageReceived(ctx.Sender, ctx.Args),
                //    OnQrscanEventReceived = ctx => weixinEventSink.OnQrscanEventReceived(ctx.Sender, ctx.Args),
                //    OnSubscribeEventReceived = ctx => weixinEventSink.OnSubscribeEventReceived(ctx.Sender, ctx.Args),
                //    OnUnsubscribeEventReceived = ctx => weixinEventSink.OnUnsubscribeEventReceived(ctx.Sender, ctx.Args),
                //    OnVideoMessageReceived = ctx => weixinEventSink.OnVideoMessageReceived(ctx.Sender, ctx.Args),
                //    OnShortVideoMessageReceived = ctx => weixinEventSink.OnShortVideoMessageReceived(ctx.Sender, ctx.Args),
                //    OnViewMenuEventReceived = ctx => weixinEventSink.OnViewMenuEventReceived(ctx.Sender, ctx.Args),
                //    OnVoiceMessageReceived = ctx => weixinEventSink.OnVoiceMessageReceived(ctx.Sender, ctx.Args)
                //};
            }).AddEntityFrameworkCores<DemoDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseWeixinSite();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("Identity", "Identity", "Identity/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
