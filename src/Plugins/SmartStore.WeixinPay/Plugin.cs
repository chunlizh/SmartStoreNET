using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Plugins;
using SmartStore.Services;
using SmartStore.Services.Configuration;
using SmartStore.Services.Localization;
using SmartStore.Services.Orders;
using SmartStore.Services.Payments;
using SmartStore.WeixinPay.Controllers;
using SmartStore.WeixinPay.Services;
using SmartStore.WeixinPay.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SmartStore.WeixinPay
{
    public class Plugin : PaymentPluginBase, IConfigurable
    {
        public static string SystemName = "SmartStore.WeixinPay";

        private readonly IWeixinPayService _payService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly ICommonServices _services;

        public Plugin(
            IWeixinPayService apiService,
            IOrderTotalCalculationService orderTotalCalculationService,
            ICommonServices services)
        {
            _payService = apiService;
            _orderTotalCalculationService = orderTotalCalculationService;
            _services = services;
        }

        public override void Install()
        {
            _services.Settings.SaveSetting<WeixinPaySettings>(new WeixinPaySettings());

            _services.Localization.ImportPluginResourcesFromXml(this.PluginDescriptor);

            _payService.DataPollingTaskInit();

            base.Install();
        }

        public override void Uninstall()
        {
            _payService.DataPollingTaskDelete();

            _services.Settings.DeleteSetting<WeixinPaySettings>();

            _services.Localization.DeleteLocaleStringResources(this.PluginDescriptor.ResourceRootKey);

            base.Uninstall();
        }

        public override PreProcessPaymentResult PreProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = _payService.PreProcessPayment(processPaymentRequest);
            return result;
        }

        public override ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = _payService.ProcessPayment(processPaymentRequest);
            return result;
        }

        public override void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            _payService.PostProcessPayment(postProcessPaymentRequest);
        }

        public override decimal GetAdditionalHandlingFee(IList<OrganizedShoppingCartItem> cart)
        {
            var result = decimal.Zero;
            //try
            //{
            //    var settings = _services.Settings.LoadSetting<WeixinPaySettings>(_services.StoreContext.CurrentStore.Id);

            //    result = this.CalculateAdditionalFee(_orderTotalCalculationService, cart, settings.AdditionalFee, settings.AdditionalFeePercentage);
            //}
            //catch (Exception exc)
            //{
            //    _apiService.LogError(exc);
            //}
            return result;

        }

        public override CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var result = _payService.Capture(capturePaymentRequest);
            return result;
        }

        public override RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var result = _payService.Refund(refundPaymentRequest);
            return result;
        }

        public override VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var result = _payService.Void(voidPaymentRequest);
            return result;
        }


        public override void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WeixinPay";
            routeValues = new RouteValueDictionary() { { "Namespaces", "SmartStore.WeixinPay.Controllers" }, { "area", SystemName } };
        }

        public override void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "ShoppingCart";
            controllerName = "WeixinPay";
            routeValues = new RouteValueDictionary() { { "Namespaces", "SmartStore.WeixinPay.Controllers" }, { "area", SystemName } };
        }

        public override Type GetControllerType()
        {
            return typeof(WeixinPayController);
        }

        public override bool SupportCapture
        {
            get { return false; }
        }

        public override bool SupportPartiallyRefund
        {
            get { return false; }
        }

        public override bool SupportRefund
        {
            get { return false; }
        }

        public override bool SupportVoid
        {
            get { return false; }
        }

        public override PaymentMethodType PaymentMethodType
        {
            get { return PaymentMethodType.StandardAndButton; }
        }
    }
}