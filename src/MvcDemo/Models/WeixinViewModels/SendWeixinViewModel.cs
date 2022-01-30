using Myvas.AspNetCore.Weixin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class SendWeixinViewModel
    {
        public IList<WeixinReceivedMessage> Received { get; set; }

        [Required]
        public string OpenId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
