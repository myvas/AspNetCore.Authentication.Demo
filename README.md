# Myvas.AspNetCore.Authentication.Demo
This repo deployed on http://demo.auth.myvas.com (debian.9-x64)

## Components:

- Myvas.AspNetCore.Authentication.WeixinAuth [![NuGet](https://img.shields.io/nuget/v/Myvas.AspNetCore.Authentication.WeixinOpen.svg)](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinAuth) [github](https://github.com/myvas/AspNetCore.Authentication.WeixinAuth)
- Myvas.AspNetCore.Authentication.WeixinOpen [nuget](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinOpen) [github](https://github.com/myvas/AspNetCore.Authentication.WeixinOpen)
- Myvas.AspNetCore.Authentication.QQConnect [nuget](https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.QQConnect) [github](https://github.com/myvas/AspNetCore.Authentication.QQConnect)
- Myvas.AspNetCore.TencentSms [nuget](https://www.nuget.org/packages/Myvas.AspNetCore.TencentSms) [github](https://github.com/myvas/AspNetCore.Authentication.TencentSms)
- Myvas.AspNetCore.Email [nuget](https://www.nuget.org/packages/Myvas.AspNetCore.Email) [github](https://github.com/myvas/AspNetCore.Authentication.Email)
- Myvas.AspNetCore.ViewDivert [nuget](https://www.nuget.org/packages/Myvas.AspNetCore.ViewDivert) [github](https://github.com/myvas/AspNetCore.Authentication.ViewDivert)

## Qrcode to enter the demo weixin service account:

![alt QrCode](http://mmbiz.qpic.cn/mmbiz_jpg/lPe5drS9euRQR1eCK5cGXaibHYL6vBR4pGLB34ju2hXCiaMQiayOU8w5GMfEH7WZsVNTnhLTpnzAC9xfdWuTT89OA/0)

## Dev Tools
* Visual Studio 2019 and .NET Core SDK 2.2 (2.2.402)
* [微信开发者工具](https://mp.weixin.qq.com/debug/wxadoc/dev/devtools/download.html)

# Third-party OAuth account settings:

## 微信开放平台 WeixinOpen https://open.weixin.qq.com

(TODO:在此添加配置说明)

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

## QQ开放平台配置 QQConnect https://connect.qq.com

(TODO:在此添加配置说明)

