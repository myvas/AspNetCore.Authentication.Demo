using System;
using System.Diagnostics;
using System.Reflection;
using Demo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Myvas.AspNetCore.Authentication;
using Myvas.AspNetCore.Weixin;

namespace Demo;

public static class HostExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        Debug.WriteLine("Create a temporary logger for me.");
        // Build a temporary logger for me.
        using var loggerFactory = LoggerFactory.Create(logging =>
        {
            logging.AddConsole();
#if DEBUG
            logging.SetMinimumLevel(LogLevel.Trace);
#else
            logging.SetMinimumLevel(LogLevel.Information);
#endif
        });
        var logger = loggerFactory.CreateLogger<WebApplicationBuilder>();
        logger.LogDebug($"{MethodBase.GetCurrentMethod().Name}...");
        logger.LogInformation("Environment=" + builder.Environment.EnvironmentName);

        var Configuration = builder.Configuration;

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<DemoDbContext>(o =>
        {
            o.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<DemoDbContext>();
        builder.Services.Configure<IdentityOptions>(o =>
        {
            o.Password = new PasswordOptions
            {
                RequireLowercase = false,
                RequireUppercase = false,
                RequireNonAlphanumeric = false,
                RequireDigit = false
            };
            o.User.RequireUniqueEmail = false;
            o.SignIn.RequireConfirmedEmail = false;

            o.SignIn.RequireConfirmedPhoneNumber = true;
        });
        builder.Services.ConfigureApplicationCookie(o =>
        {
            o.LoginPath = "/Identity/Account/Login";
            o.LogoutPath = "/Identity/Account/Logout";
            o.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });

        logger?.LogInformation("WeixinOpen:AppId=" + Configuration["WeixinOpen:AppId"]);
        logger?.LogInformation("WeixinAuth:AppId=" + Configuration["WeixinAuth:AppId"]);
        logger?.LogInformation("QQConnect:AppId=" + Configuration["QQConnect:AppId"]);

        builder.Services.AddAuthentication()
        .AddWeixinOpen(o =>
        {
            o.AppId = Configuration["WeixinOpen:AppId"];
            o.AppSecret = Configuration["WeixinOpen:AppSecret"];
            o.SaveTokens = true;
        })
        .AddWeixinAuth(o =>
        {
            o.AppId = Configuration["WeixinAuth:AppId"];
            o.AppSecret = Configuration["WeixinAuth:AppSecret"];
            o.SilentMode = false; //不采用静默模式
            //options.SaveTokens = true;
        })
        .AddQQConnect(o =>
        {
            o.AppId = Configuration["QQConnect:AppId"];
            o.AppKey = Configuration["QQConnect:AppKey"];
            //options.SaveTokens = true;

            QQConnectScopes.TryAdd(o.Scope,
                QQConnectScopes.get_user_info,
                QQConnectScopes.list_album,
                QQConnectScopes.upload_pic,
                QQConnectScopes.do_like);
        });

        logger?.LogInformation("TencensSms:SdkAppId=" + Configuration["TencentSms:SdkAppId"]);

        builder.Services.AddTencentSms(o =>
        {
            o.SdkAppId = Configuration["TencentSms:SdkAppId"];
            o.AppKey = Configuration["TencentSms:AppKey"];
        });

        builder.Services.AddViewDivert();

        logger?.LogInformation("Weixin:AppId=" + Configuration["Weixin:AppId"]);

        builder.Services.AddWeixin(o =>
        {
            o.AppId = Configuration["Weixin:AppId"];
            o.AppSecret = Configuration["Weixin:AppSecret"];
        })
        .AddAccessTokenRedisCacheProvider(o =>
        {
            o.Configuration = Configuration.GetConnectionString("RedisConnection");
        })
        .AddJsapiTicketRedisCacheProvider()
        .AddWeixinSite(o =>
        {
            o.Debug = true; // for this demo for debugging
            //o.Path = "/wx"; // It's the default
            o.WebsiteToken = Configuration["Weixin:WebsiteToken"];
            //o.EncodingAESKey = Configuration["Weixin:EncodingAESKey"];
        })
        .AddWeixinEfCore<DemoDbContext>(o =>
        {
            o.EnableSyncForWeixinSubscribers = true;
            o.SyncIntervalInMinutesForWeixinSubscribers = 60;
        })
        .AddWeixinEventSink<DemoEventSink>();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var logger = app.Logger;
        logger.LogTrace($"{MethodBase.GetCurrentMethod().Name}...");
        logger.LogInformation($"Environment={app.Environment.EnvironmentName}");

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

        logger.LogTrace("UseWeixinSite...");
        app.UseWeixinSite();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        // Map area route for "Identity"
        app.MapAreaControllerRoute(
            name: "Identity",
            areaName: "Identity",
            pattern: "Identity/{controller=Home}/{action=Index}/{id?}");

        // Map default controller route
        app.MapDefaultControllerRoute();

        // Map Razor Pages
        app.MapRazorPages();

        return app;
    }
}
