using Demo.Applications;
using Demo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Myvas.AspNetCore.Authentication;
using Myvas.AspNetCore.Weixin;
using Myvas.AspNetCore.Weixin.Models;

namespace Demo;

public static class HostExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var Configuration = builder.Configuration;

        builder.Services.AddControllersWithViews();

        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

        builder.Services.AddDbContext<DemoDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(migrationsAssembly)));

        builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<DemoDbContext>();
        builder.Services.Configure<IdentityOptions>(options =>
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
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        builder.Services.AddAuthentication()
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

        builder.Services.AddTencentSms(options =>
        {
            options.SdkAppId = Configuration["TencentSms:SdkAppId"];
            options.AppKey = Configuration["TencentSms:AppKey"];
        });

        builder.Services.AddViewDivert();

        builder.Services.AddWeixin(options =>
            {
                options.AppId = Configuration["Weixin:AppId"];
                options.AppSecret = Configuration["Weixin:AppSecret"];
            })
            .AddAccessToken(o =>
            {
                o.Configuration = Configuration.GetConnectionString("RedisConnection");
                o.InstanceName = migrationsAssembly;
            })
            .AddWeixinSite<DemoWeixinEventSink, Subscriber, DemoDbContext>(o =>
            {
                o.Debug = true;
                o.WebsiteToken = Configuration["Weixin:WebsiteToken"];
                //o.EncodingAESKey = Configuration["Weixin:EncodingAESKey"];
            })
            .AddJssdk();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var env = app.Environment;
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

        return app;
    }
}
