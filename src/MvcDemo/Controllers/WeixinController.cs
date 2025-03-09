using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Myvas.AspNetCore.Weixin;
using Myvas.AspNetCore.Weixin.Models;
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
        private readonly IWeixinAccessTokenApi _weixinAccessToken;
        private readonly UserApi _api;
        private readonly CustomerSupportApi Custom;
        private readonly ISubscriberStore<Subscriber> _subscriberStore;
        private readonly IReceivedEntryStore<MessageReceivedEntry> _messageStore;

        public WeixinController(
            DemoDbContext db,
            ILoggerFactory loggerFactory,
            UserApi api,
            CustomerSupportApi csApi,
            ISubscriberStore<Subscriber> subscriberStore,
            IReceivedEntryStore<MessageReceivedEntry> messageStore,
            IWeixinAccessTokenApi smsSender)
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
            var subscriberCount = await _subscriberStore.GetSubscribersCountAsync();
            var receivedTextCount = await _messageStore.GetAllByReceivedTimeAsync(null, null);
            var vm = new WeixinIndexViewModel() { SubscriberCount = subscriberCount, ReceivedTextCount = receivedTextCount.Count() };

            return View(vm);
        }

        public async Task<IActionResult> Subscribers()
        {
            var vm = new ReturnableViewModel<IList<UserInfoJson>>();

            var token = await _weixinAccessToken.GetTokenAsync();
            var subscribers = await _api.GetAllUserInfo(token);
            vm.Item = subscribers;

            return View(vm);
        }


        public async Task<IActionResult> ReceivedText()
        {
            var items = await _messageStore.GetAllByReceivedTimeAsync(null, null);
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
                Received = await _messageStore.GetAllByToUserNameAsync(openId),
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

            var token = await _weixinAccessToken.GetTokenAsync();
            var result = await Custom.SendText(token, vm.OpenId, vm.Content);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", result.ErrorMessage);
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