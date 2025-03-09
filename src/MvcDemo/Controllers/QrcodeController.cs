using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Myvas.AspNetCore.Weixin;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;

namespace Demo.Controllers
{
    public class QrcodeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IWeixinAccessTokenApi _accessToken;
        private readonly QrcodeApi _api;

        public QrcodeController(ILoggerFactory loggerFactory,
            IWeixinAccessTokenApi accessToken,
            QrcodeApi api)
        {
            _logger = loggerFactory?.CreateLogger<QrcodeController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _api = api;
            _accessToken = accessToken;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/[action]/{scene}")]
        public async Task<IActionResult> UrlWithScene(string scene)
        {
            var token = await _accessToken.GetTokenAsync();
            var createQrcodeResult = await _api.Create(token, "QR_LIMIT_STR_SCENE", scene);
            return Json(createQrcodeResult);
        }

        [HttpGet("[controller]/[action]/{scene}")]
        public async Task<IActionResult> QrcodeWithScene(string scene)
        {
            var token = await _accessToken.GetTokenAsync();
            var createQrcodeResult = await _api.Create(token, "QR_LIMIT_STR_SCENE", scene);

            var url = _api.ShowQrcode(createQrcodeResult.ticket);

            return Content(url);
        }
    }
}