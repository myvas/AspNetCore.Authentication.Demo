using Microsoft.Extensions.Logging;
using Myvas.AspNetCore.Weixin;
using Myvas.AspNetCore.Weixin.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Applications
{
    public class DemoWeixinEventSink : WeixinEventSink
    {
        public DemoWeixinEventSink(ILogger<IWeixinEventSink> logger, IWeixinResponseBuilder responseBuilder, IReceivedEntryStore<EventReceivedEntry> eventStore, IReceivedEntryStore<MessageReceivedEntry> messageStore) : base(logger, responseBuilder, eventStore, messageStore)
        {
        }

        public override async Task<bool> OnTextMessageReceived(WeixinEventContext<TextMessageReceivedXml> context)
        {
            await base.OnTextMessageReceived(context);

            var result = new StringBuilder();
            result.AppendFormat("收到一条微信文本消息：{0}", context.Xml.Content);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnLinkMessageReceived(WeixinEventContext<LinkMessageReceivedXml> context)
        {
            await base.OnLinkMessageReceived(context);

            var result = new List<Article>
            {
                new Article
                {
                    Title = "收到一条链接消息: " + context.Xml.Title,
                    Description = context.Xml.Description,
                    Url = context.Xml.Url
                }
            };
            await WeixinResponseBuilder.FlushNewsMessage(context.Context, context.Xml, result);
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnVideoMessageReceived(WeixinEventContext<VideoMessageReceivedXml> context)
        {
            await base.OnVideoMessageReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"收到一条视频消息，ID：" + context.Xml.MediaId);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnShortVideoMessageReceived(WeixinEventContext<ShortVideoMessageReceivedXml> context)
        {
            await base.OnShortVideoMessageReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"收到一条短视频消息，ID：" + context.Xml.MediaId);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnVoiceMessageReceived(WeixinEventContext<VoiceMessageReceivedXml> context)
        {
            await base.OnVoiceMessageReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"收到一条语音消息，ID：" + context.Xml.MediaId);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnImageMessageReceived(WeixinEventContext<ImageMessageReceivedXml> context)
        {
            await base.OnImageMessageReceived(context);

            var result = new List<Article>
            {
                new Article
                {
                    Title = "收到一条图片信息",
                    Description = "您发送的图片将会显示在边上",
                    PicUrl = context.Xml.PicUrl,
                    Url = "http://demo.auth.myvas.com"
                }
            };
            await WeixinResponseBuilder.FlushNewsMessage(context.Context, context.Xml, result);
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnLocationMessageReceived(WeixinEventContext<LocationMessageReceivedXml> context)
        {
            await base.OnLocationMessageReceived(context);

            var result = new List<Article>();
            var markersList = new List<GoogleMapMarkers>();
            markersList.Add(new GoogleMapMarkers()
            {
                Latitude = context.Xml.Latitude,
                Longitude = context.Xml.Longitude,
                Color = "red",
                Label = "S",
                Size = GoogleMapMarkerSize.Default,
            });
            var mapSize = "480x600";
            var mapUrl = GoogleMapHelper.GetGoogleStaticMap(19 /*requestMessage.Scale*//*微信和GoogleMap的Scale不一致，这里建议使用固定值*/,
                                                            markersList, mapSize);
            result.Add(new Article()
            {
                Description = string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
                              context.Xml.Latitude, context.Xml.Longitude,
                              context.Xml.Scale, context.Xml.Label),
                PicUrl = mapUrl,
                Title = "定位地点周边地图",
                Url = mapUrl
            });

            await WeixinResponseBuilder.FlushNewsMessage(context.Context, context.Xml, result);
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnLocationEventReceived(WeixinEventContext<LocationEventReceivedXml> context)
        {
            await base.OnLocationEventReceived(context);

            var result = new List<Article>();
            var markersList = new List<GoogleMapMarkers>();
            markersList.Add(new GoogleMapMarkers()
            {
                Latitude = context.Xml.Latitude,
                Longitude = context.Xml.Longitude,
                Color = "red",
                Label = "S",
                Size = GoogleMapMarkerSize.Default,
            });
            var mapSize = "480x600";
            var mapUrl = GoogleMapHelper.GetGoogleStaticMap(19 /*requestMessage.Scale*//*微信和GoogleMap的Scale不一致，这里建议使用固定值*/,
                                                            markersList, mapSize);
            result.Add(new Article()
            {
                Description = string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}, Precision: {2}",
                              context.Xml.Latitude, context.Xml.Longitude, context.Xml.Precision),
                PicUrl = mapUrl,
                Title = "定位地点周边地图",
                Url = mapUrl
            });

            await WeixinResponseBuilder.FlushNewsMessage(context.Context, context.Xml, result);
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnClickMenuEventReceived(WeixinEventContext<ClickMenuEventReceivedXml> context)
        {
            await base.OnClickMenuEventReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"点击了子菜单按钮({0}): {1}", context.Xml.FromUserName, context.Xml.EventKey);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnViewMenuEventReceived(WeixinEventContext<ViewMenuEventReceivedXml> context)
        {
            await base.OnViewMenuEventReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"点击了子菜单按钮({0}): {1}: {2}", context.Xml.FromUserName, context.Xml.EventKey, context.Xml.Url);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnUnsubscribeEventReceived(WeixinEventContext<UnsubscribeEventReceivedXml> context)
        {
            await  base.OnUnsubscribeEventReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"Unsubscribe({0})", context.Xml.FromUserName);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnQrscanEventReceived(WeixinEventContext<QrscanEventReceivedXml> context)
        {
            await base.OnQrscanEventReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"QrscanEvent({0}: {1}, {2})", context.Xml.FromUserName, context.Xml.EventKey,context.Xml.Ticket);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }

        public override async Task<bool> OnSubscribeEventReceived(WeixinEventContext<SubscribeEventReceivedXml> context)
        {
            await base.OnSubscribeEventReceived(context);

            var result = new StringBuilder();
            result.AppendFormat(@"Subscribe({0}: {1}, {2})", context.Xml.FromUserName, context.Xml.EventKey, context.Xml.Ticket);
            await WeixinResponseBuilder.FlushTextMessage(context.Context, context.Xml, result.ToString());
            _logger.LogDebug("FlushTextMessage: {content}", result.ToString());

            return true;
        }
    }
}
