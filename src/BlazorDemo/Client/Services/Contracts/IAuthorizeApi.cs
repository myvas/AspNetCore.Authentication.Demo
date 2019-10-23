using BlazorDemo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Client.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task Login(LoginInputModel loginParameters);
        Task Register(RegisterInputModel registerParameters);
        Task Logout();
        Task<UserInfo> GetUserInfo();
    }
}
