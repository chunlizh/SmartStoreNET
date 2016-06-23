using SmartStore.WeixinPay.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.WeixinPay.Core
{
    public enum PayUrl
    {
        /// <summary>
        /// 统一下单接口
        /// </summary>
        UnifiedOrder,
        /// <summary>
        /// 查询订单
        /// </summary>
        OrderQuery,
        /// <summary>
        /// 关闭订单
        /// </summary>
        CloseOrder,
        /// <summary>
        /// 申请退款
        /// 双向需要证书，参考：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_3
        /// </summary>
        Refund,
        /// <summary>
        /// 查询退款
        /// </summary>
        RefundQuery,
        /// <summary>
        /// 下载对账单，一次下载一天
        /// </summary>
        DownloadBill,
        /// <summary>
        /// 测速上报
        /// </summary>
        Report,
        /// <summary>
        /// 支付结果通用通知
        /// </summary>
        notify_url
    }


    public class WeixinPayCore
    {
        private static string unifiedorder_url = "https://api.mch.weixin.qq.com/{0}pay/unifiedorder";
        private static string orderquery_url = "https://api.mch.weixin.qq.com/{0}pay/orderquery";
        private static string closeorder_url = "https://api.mch.weixin.qq.com/{0}pay/closeorder";
        private static string refund_url = "https://api.mch.weixin.qq.com/secapi/{0}pay/refund";
        private static string refundquery_url = "https://api.mch.weixin.qq.com/{0}pay/refundquery";
        private static string downloadbill_url = "https://api.mch.weixin.qq.com/{0}pay/downloadbill";
        private static string report_url = "https://api.mch.weixin.qq.com/{0}payitil/report";

        public static string GetApiUrl(PayUrl payUrl, WeixinPaySettings settings)
        {
            string url;
            switch (payUrl)
            {
                case PayUrl.UnifiedOrder:
                    url = unifiedorder_url;
                    break;
                case PayUrl.OrderQuery:
                    url = orderquery_url;
                    break;
                case PayUrl.CloseOrder:
                    url = closeorder_url;
                    break;
                case PayUrl.Refund:
                    url = refund_url;
                    break;
                case PayUrl.RefundQuery:
                    url = refundquery_url;
                    break;
                case PayUrl.DownloadBill:
                    url = downloadbill_url;
                    break;
                case PayUrl.Report:
                    url = report_url;
                    break;
                case PayUrl.notify_url:
                    url = "";
                    break;
                default:
                    url = "";
                    break;
            }

            return string.Format(url, settings.UseSandbox ? "sandbox/" : "");
        }
    }

    /// <summary>
    /// 测速上报等级
    /// </summary>
    public enum ReportLevel
    {
        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        //public const int REPORT_LEVENL = 1;
        Close,
        OnlyError,
        Full
    }

    public enum LogLevel
    {
        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        //public const int LOG_LEVENL = 0;
        None,
        OnlyError,
        ErrorOrNormal,
        /// <summary>
        /// Include Error,Normal,Debug info
        /// </summary>
        Full
    }
}