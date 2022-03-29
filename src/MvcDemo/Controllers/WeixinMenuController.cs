using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Myvas.AspNetCore.Weixin;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Demo.Data;

namespace Demo.Controllers
{
    [Authorize]//(Policy = "WeixinMenuManager")]
    public class WeixinMenuController : Controller
    {
        private readonly DemoDbContext _context;
        private readonly IWeixinAccessToken _weixinAccessToken;
        private readonly ILogger<WeixinMenuController> _logger;
        private readonly MenuApi _api;

        public WeixinMenuController(DemoDbContext context,
            IWeixinAccessToken weixinAccessToken,
            MenuApi api,
            ILogger<WeixinMenuController> logger)
        {
            _context = context;
            _weixinAccessToken = weixinAccessToken;
            _api = api;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            var token = await _weixinAccessToken.GetTokenAsync();
            var menu = await _api.GetMenuAsync(token);

            var vm = new WeixinJsonViewModel
            {
                Json = WeixinJsonHelper.Serialize(menu)// JsonConvert.SerializeObject(resultJson, Formatting.Indented)
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWeixinMenu(WeixinJsonViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(vm.Json))
                {
                    var token = await _weixinAccessToken.GetTokenAsync();
                    var result = await _api.CreateMenuAsync(token, vm.Json);

                    _logger.LogDebug(result.ToString());

                    return View("UpdateMenuResult", result);
                }
            }

            // If we got this far, something failed; redisplay form.
            return RedirectToAction(nameof(Index), new { input = vm.Json });
        }
    }
}