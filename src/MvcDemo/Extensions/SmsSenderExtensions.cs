using Myvas.AspNetCore.TencentSms;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.TencentSms
{
    public static class TencentSmsSenderExtensions
    {
        /// <summary>
        /// ������֤�����
        /// </summary>
        /// <param name="_smsSender"></param>
        /// <param name="vcode"></param>
        public static async Task<bool> SendVcodeAsync(this ISmsSender _smsSender, string mobile, string vcode)
        {
            var codeText = $"���¹����뻧��{vcode}Ϊ������֤�롣��Ǳ��˲���������Ա����š�";
            return await _smsSender.SendSmsAsync(mobile, codeText);
        }
    }
}
