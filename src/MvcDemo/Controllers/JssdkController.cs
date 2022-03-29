using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Myvas.AspNetCore.Weixin;
using System;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class JssdkController : Controller
    {
        private readonly IWeixinJsapiTicket _weixinJsapiTicket;
        private readonly WeixinOptions _options;

        public JssdkController(
            IWeixinJsapiTicket weixinJsapiTicket,
            IOptions<WeixinOptions> optionsAccessor)
        {
            _weixinJsapiTicket = weixinJsapiTicket ?? throw new ArgumentNullException(nameof(weixinJsapiTicket));
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        public async Task<IActionResult> Index()
        {
            var vm = new ShareJweixinViewModel();

            var config = new WeixinJsConfig()
            {
                debug = true,
                appId = _options.AppId
            };
            var jsapiTicket = await _weixinJsapiTicket.GetTicketAsync();
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