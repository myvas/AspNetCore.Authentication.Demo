using Myvas.AspNetCore.Weixin;
using Myvas.AspNetCore.Weixin.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class SendWeixinViewModel
    {
        public IEnumerable<WeixinResponseMessageEntity> Received { get; set; }

        [Required]
        public string OpenId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
