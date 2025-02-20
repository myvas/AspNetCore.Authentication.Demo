using Demo.Applications;
using Demo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Myvas.AspNetCore.Authentication;
using Myvas.AspNetCore.Weixin;

namespace Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("Demo")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddUserManager<AppUserManager>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddDefaultTokenProviders();
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
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/LogOff";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            services.AddAuthentication()
                .AddWeixinOpen(options =>
                {
                    options.AppId = Configuration["WeixinOpen:AppId"];
                    options.AppSecret = Configuration["WeixinOpen:AppSecret"];
                    options.SaveTokens = true;
                })
                .AddWeixinAuth(options =>
                {
                    options.AppId = Configuration["WeixinAuth:AppId"];
                    options.AppSecret = Configuration["WeixinAuth:AppSecret"];
                    options.SilentMode = false; //不采用静默模式
                                                //options.SaveTokens = true;
                })
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
                options.WebsiteToken = Configuration["Weixin:WebsiteToken"];
                //options.EncodingAESKey = Configuration["Weixin:EncodingAESKey"];
                options.Path = "/wx";
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
            });
          
            services.AddControllersWithViews();
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

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
          
            app.UseWeixinSite();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
