using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Myvas.AspNetCore.Weixin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeixinAccessTokenController : ControllerBase
    {
		private readonly IWeixinAccessTokenApi _api;

		public WeixinAccessTokenController(IWeixinAccessTokenApi api)
		{
			_api = api;
		}

		public string GetToken()
		{
			return _api.GetToken().AccessToken;
		}
    }
}