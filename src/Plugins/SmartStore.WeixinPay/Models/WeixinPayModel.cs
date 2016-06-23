using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;
using SmartStore.WeixinPay.Core;
using SmartStore.WeixinPay.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.WeixinPay.Models
{
    public class WeixinPayModel: ModelBase
    {
        //public string[] ConfigGroups { get; set; }

        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.APPID")]
        public string APPID { get; set; }    // = "wx2428e34e0e7dc6ef";
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.MCHID")]
        public string MCHID { get; set; } //= "1233410002";
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.KEY")]
        public string KEY { get; set; } // = "e10adc3849ba56abbe56e056f20f883e";
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.APPSECRET")]
        public string APPSECRET { get; set; } // = "51c56b886b5be869567dd389b3e5d1d6";

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.SSLCERT_PATH")]
        public string SSLCERT_PATH { get; set; } // = "cert/apiclient_cert.p12";
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.SSLCERT_PASSWORD")]
        public string SSLCERT_PASSWORD { get; set; } // = "1233410002";



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        //public const string NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        //public const string IP = "8.8.8.8";


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        //public const string PROXY_URL = "http://10.152.18.220:8080";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.ReportLevel")]
        public ReportLevel ReportLevel { get; set; }

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        //public const int LOG_LEVENL = 0;
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.LogLevel")]
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 沙箱模式，用于测试环境
        /// </summary>
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.UseSandbox")]
        public bool UseSandbox { get; set; }
        [SmartResourceDisplayName("Plugins.Payments.WeixinPay.Fields.AddOrderNotes")]
        public bool AddOrderNotes { get; set; }

        public void Copy(WeixinPaySettings settings, bool fromSettings)
        {
            if (fromSettings)
            {
                APPID = settings.APPID;
                MCHID = settings.MCHID;
                KEY = settings.KEY;
                APPSECRET = settings.APPSECRET;
                SSLCERT_PATH = settings.SSLCERT_PATH;
                SSLCERT_PASSWORD = settings.SSLCERT_PASSWORD;
            }
            else
            {
                settings.APPID = APPID;
                settings.MCHID = MCHID;
                settings.KEY = KEY;
                settings.APPSECRET = APPSECRET;
                settings.SSLCERT_PATH = SSLCERT_PATH;
                settings.SSLCERT_PASSWORD = SSLCERT_PASSWORD;
            }
        }
    }
}