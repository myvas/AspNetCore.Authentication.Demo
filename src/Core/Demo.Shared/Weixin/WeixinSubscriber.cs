﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
    public class WeixinSubscriber : Entity
    {
        public string OpenId { get; set; }
        public string Gender { get; set; }
        public string NickName { get; set; }
        public string AvatorImage { get; set; }
    }
}

