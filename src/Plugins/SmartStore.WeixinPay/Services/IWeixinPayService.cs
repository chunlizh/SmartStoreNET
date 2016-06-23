using SmartStore.Core.Domain.Orders;
using SmartStore.Services.Payments;
using SmartStore.WeixinPay.Models;
using SmartStore.WeixinPay.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.WeixinPay.Services
{
    public interface IWeixinPayService
    {
        void LogError(Exception exception, string shortMessage = null, string fullMessage = null, bool notify = false, IList<string> errors = null);
        //void LogAmazonError(OffAmazonPaymentsServiceException exception, bool notify = false, IList<string> errors = null);

        //void AddOrderNote(AmazonPaySettings settings, Order order, AmazonPayOrderNote note, string anyString = null, bool isIpn = false);

        void SetupConfiguration(WeixinPayModel model);

        string GetWidgetUrl();

        //AmazonPayViewModel ProcessPluginRequest(AmazonPayRequestType type, TempDataDictionary tempData, string orderReferenceId = null);

        void ApplyRewardPoints(bool useRewardPoints);

        //void AddCustomerOrderNoteLoop(AmazonPayActionState state);

        PreProcessPaymentResult PreProcessPayment(ProcessPaymentRequest request);

        ProcessPaymentResult ProcessPayment(ProcessPaymentRequest request);

        void PostProcessPayment(PostProcessPaymentRequest request);

        CapturePaymentResult Capture(CapturePaymentRequest request);

        RefundPaymentResult Refund(RefundPaymentRequest request);

        VoidPaymentResult Void(VoidPaymentRequest request);

        void ProcessIpn(HttpRequestBase request);

        void DataPollingTaskProcess();

        void DataPollingTaskInit();

        void DataPollingTaskUpdate(bool enabled, int seconds);

        void DataPollingTaskDelete();
    }
}