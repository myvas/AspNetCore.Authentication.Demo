# Myvas.AspNetCore.Authentication.Demo
This repo (Branch 6.0) deployed on http://demo.auth.myvas.com (debian.11-x64)

## Components:
- Myvas.AspNetCore.Authentication.WeixinAuth [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Authentication.WeixinAuth.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinAuth) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Myvas.AspNetCore.Authentication.WeixinAuth)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinAuth) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.Authentication.WeixinAuth?label=github)](https://github.com/myvas/AspNetCore.Authentication.WeixinAuth)
- Myvas.AspNetCore.Authentication.WeixinOpen [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Authentication.WeixinOpen.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinOpen) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Myvas.AspNetCore.Authentication.WeixinOpen)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinOpen) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.Authentication.WeixinOpen?label=github)](https://github.com/myvas/AspNetCore.Authentication.WeixinOpen)
- Myvas.AspNetCore.Authentication.QQConnect [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Authentication.QQConnect.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.QQConnect) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Myvas.AspNetCore.Authentication.QQConnect)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.QQConnect) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.Authentication.QQConnect?label=github)](https://github.com/myvas/AspNetCore.Authentication.QQConnect)
- Myvas.AspNetCore.TencentSms [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.TencentSms.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.TencentSms) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.TencentSms?label=github)](https://github.com/myvas/AspNetCore.TencentSms)
- Myvas.AspNetCore.Email [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Email.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Email) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.Email?label=github)](https://github.com/myvas/AspNetCore.Email)
- Myvas.AspNetCore.ViewDivert [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.ViewDivert.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.ViewDivert) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.ViewDivert?label=github)](https://github.com/myvas/AspNetCore.ViewDivert)
- Myvas.AspNetCore.Weixin [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Weixin.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Weixin) [![GitHub (Pre-)Release Date](https://img.shields.io/github/release-date-pre/myvas/AspNetCore.Weixin?label=github)](https://github.com/myvas/AspNetCore.Weixin)

## Qrcode to enter the demo with weixin service account:
![alt QrCode](http://mmbiz.qpic.cn/mmbiz_jpg/lPe5drS9euRQR1eCK5cGXaibHYL6vBR4pGLB34ju2hXCiaMQiayOU8w5GMfEH7WZsVNTnhLTpnzAC9xfdWuTT89OA/0)

## Dev
* Visual Studio 2022 with .NET Core SDK 6.0
* [微信开发者工具](https://mp.weixin.qq.com/debug/wxadoc/dev/devtools/download.html)

# Third-party OAuth account settings:

## 微信开放平台 WeixinOpen https://open.weixin.qq.com
(TODO:在此添加配置说明)

![Screenshot](assets/secrets-WeixinOpen.png)

## 微信公众平台 WeixinAuth https://mp.weixin.qq.com
（1）微信公众平台-测试账号
/开发/开发者工具/公众平台测试号/...
- 开通功能：网页服务/网页授权获取用户基本信息。
- 设置好授权回调页面域名：例如，demo.auth.myvas.com。

（2）微信公众平台-正式账号
/开发/接口权限/...
- 开通功能：网页服务/网页授权获取用户基本信息。
- 设置好网页授权域名：例如，demo.auth.myvas.com。
- 将文件MP_verify_xxxxxxxxx.txt上传至wwwroot目录下。

![Screenshot](assets/secrets-WeixinAuth.png)

## QQ开放平台配置 QQConnect https://connect.qq.com
(TODO:在此添加配置说明)

![Screenshot](assets/secrets-QQConnect.png)

