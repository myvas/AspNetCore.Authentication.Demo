# Myvas.AspNetCore.Authentication.Demo
Demo of a series of authentication client middlewares:
- WeixinOpen for https://open.weixin.qq.com
- WeixinAuth for https://mp.weixin.qq.com
- QQConnect for https://connect.qq.com

## NuGet
- https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinAuth
- https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.WeixinOpen
- https://www.nuget.org/packages/Myvas.AspNetCore.Authentication.QQConnect
- https://www.nuget.org/packages/Myvas.AspNetCore.TencentSms
- https://www.nuget.org/packages/Myvas.AspNetCore.Email

## Demo Online
- http://demo.auth.myvas.com (debian.9-x64)
- Qrcode to enter the demo weixin service account:

![alt QrCode](http://mmbiz.qpic.cn/mmbiz_jpg/lPe5drS9euRQR1eCK5cGXaibHYL6vBR4pGLB34ju2hXCiaMQiayOU8w5GMfEH7WZsVNTnhLTpnzAC9xfdWuTT89OA/0)

## How to Build
* Visual Studio 2019 and .NET Core SDK 2.1

## Dev Tools
* [微信开发者工具](https://mp.weixin.qq.com/debug/wxadoc/dev/devtools/download.html)

# 微信开放平台/微信公众平台/QQ开放平台配置

### WeixinAuth
（1）微信公众平台-测试账号
/开发/开发者工具/公众平台测试号/...
- 开通功能：网页服务/网页授权获取用户基本信息。
- 设置好授权回调页面域名：例如，demo.auth.myvas.com。

（2）微信公众平台-正式账号
/开发/接口权限/...
- 开通功能：网页服务/网页授权获取用户基本信息。
- 设置好网页授权域名：例如，demo.auth.myvas.com。
- 将文件MP_verify_xxxxxxxxx.txt上传至wwwroot目录下。

### WeixinOpen
(TODO:在此添加配置说明)

### QQConnect
(TODO:在此添加配置说明）

