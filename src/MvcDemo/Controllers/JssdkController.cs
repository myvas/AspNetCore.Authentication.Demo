using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Myvas.AspNetCore.Weixin;
using System;

namespace Demo.Controllers
{
    public class JssdkController : Controller
    {
        private readonly IWeixinAccessToken _weixinAccessToken;
		private readonly IWeixinJsapiTicket _weixinJsapiTicket;
		private readonly WeixinJssdkOptions _options;

        public JssdkController(
            IWeixinAccessToken weixinAccessToken,
			IWeixinJsapiTicket weixinJsapiTicket,
            IOptions<WeixinJssdkOptions> optionsAccessor)
		{
			_weixinAccessToken = weixinAccessToken ?? throw new ArgumentNullException(nameof(weixinAccessToken));
			_weixinJsapiTicket = weixinJsapiTicket ?? throw new ArgumentNullException(nameof(weixinJsapiTicket));
			_options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        public IActionResult Index()
        {
            var vm = new ShareJweixinViewModel();

            var config = new WeixinJsConfig()
            {
                debug = true,
                appId = _options.AppId
            };
            var jsapiTicket = _weixinJsapiTicket.GetTicket();
            var refererUrl = Request.GetAbsoluteUri();// Url.AbsoluteContent(Url.Action());
            vm.ConfigJson = config.ToJson(jsapiTicket, refererUrl);

            vm.Title = "链接分享测试";
            vm.Url = "http://demo.auth.myvas.com/jssdk";
            vm.Description = "链接分享测试";
            vm.ImgUrl = "http://demo.auth.myvas.com/img/mp-test.jpg";
            return View(vm);
        }
    }
}