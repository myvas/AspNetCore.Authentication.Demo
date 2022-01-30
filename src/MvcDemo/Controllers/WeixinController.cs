using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Myvas.AspNetCore.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class WeixinController : Controller
    {
        private readonly DemoDbContext _db;
        private readonly ILogger<HomeController> _logger;
        private readonly IWeixinAccessToken _weixinAccessToken;
        private readonly IWeixinUserApi _api;
        private readonly ICustomerSupportApi Custom;
        private readonly IWeixinSubscriberStore _subscriberStore;
        private readonly IWeixinReceivedMessageStore _messageStore;

        public WeixinController(
            DemoDbContext db,
            ILoggerFactory loggerFactory,
            IWeixinUserApi api,
            ICustomerSupportApi csApi,
            IWeixinSubscriberStore subscriberStore,
            IWeixinReceivedMessageStore messageStore,
            IWeixinAccessToken smsSender)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = loggerFactory?.CreateLogger<HomeController>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _api = api;
            Custom = csApi;
            _subscriberStore = subscriberStore ?? throw new ArgumentNullException(nameof(subscriberStore));
            _messageStore = messageStore ?? throw new ArgumentNullException(nameof(messageStore));
            _weixinAccessToken = smsSender ?? throw new ArgumentNullException(nameof(smsSender));
        }

        public async Task<IActionResult> Index()
        {
            var subscriberCount = await _subscriberStore.GetCountAsync();
            var receivedTextCount = await _messageStore.GetCountAsync();
            var vm = new WeixinIndexViewModel() { SubscriberCount = subscriberCount, ReceivedTextCount = receivedTextCount };

            return View(vm);
        }

        public async Task<IActionResult> Subscribers()
        {
            var vm = new ReturnableViewModel<IList<UserInfoJson>>();

            var subscribers = await _api.GetAllUserInfoAsync();
            vm.Item = subscribers;

            return View(vm);
        }


        public async Task<IActionResult> ReceivedText()
        {
            var count = await _messageStore.GetCountAsync();
            var items = await _messageStore.GetItemsAsync(count, 0);
            _logger.LogDebug($"微信文本消息在数据库中共{items.Count()}条记录。");
            return View(items);
        }

        public async Task<IActionResult> SendWeixin(string openId)
        {
            if (string.IsNullOrEmpty(openId))
            {
                return View();
            }

            var vm = new SendWeixinViewModel
            {
                Received = await _messageStore.Items.Where(x => x.ToUserName == openId).ToListAsync(),
                OpenId = openId
            };
            return View(vm);
        }

        [HttpPost, ActionName(nameof(SendWeixin))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendWeixin_Post(SendWeixinViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var token = _weixinAccessToken.GetToken();
            var result = await Custom.SendTextAsync(vm.OpenId, vm.Content);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.errmsg);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}