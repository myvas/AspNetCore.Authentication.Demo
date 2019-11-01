﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class ShareJweixinViewModel : JweixinViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
    }
}
