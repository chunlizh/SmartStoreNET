using SmartStore.Services;
using SmartStore.Services.Payments;
using SmartStore.Services.Stores;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using SmartStore.WeixinPay.Models;
using SmartStore.WeixinPay.Services;
using SmartStore.WeixinPay.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.WeixinPay.Controllers
{
    public class WeixinPayController :  PaymentControllerBase
    {
        private readonly IWeixinPayService _payService;
        private readonly ICommonServices _services;
        private readonly IStoreService _storeService;

        public WeixinPayController(
            IWeixinPayService apiService,
            ICommonServices services,
            IStoreService storeService)
        {
            _payService = apiService;
            _services = services;
            _storeService = storeService;
        }

        [NonAction]
        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            var warnings = new List<string>();
            return warnings;
        }

        [NonAction]
        public override ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest();
            return paymentInfo;
        }

        [AdminAuthorize]
        public ActionResult Configure()
        {
            var model = new WeixinPayModel();
            int storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _services.WorkContext);
            var settings = _services.Settings.LoadSetting<WeixinPaySettings>(storeScope);

            model.Copy(settings, true);

            _payService.SetupConfiguration(model);

            var storeDependingSettingHelper = new StoreDependingSettingHelper(ViewData);
            storeDependingSettingHelper.GetOverrideKeys(settings, model, storeScope, _services.Settings);

            return View(model);
        }

        [HttpPost, AdminAuthorize]
        public ActionResult Configure(WeixinPayModel model, FormCollection form)
        {
            if (!ModelState.IsValid)
                return Configure();

            ModelState.Clear();

            var storeDependingSettingHelper = new StoreDependingSettingHelper(ViewData);
            int storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _services.WorkContext);
            var settings = _services.Settings.LoadSetting<WeixinPaySettings>(storeScope);

            model.Copy(settings, false);

            storeDependingSettingHelper.UpdateSettings(settings, form, storeScope, _services.Settings);

            //_services.Settings.SaveSetting(settings, x => x.DataFetching, 0, false);
            //_services.Settings.SaveSetting(settings, x => x.PollingMaxOrderCreationDays, 0, false);

            //_apiService.DataPollingTaskUpdate(settings.DataFetching == AmazonPayDataFetchingType.Polling, model.PollingTaskMinutes * 60);

            _services.Settings.ClearCache();
            NotifySuccess(_services.Localization.GetResource("Plugins.Payments.WeixinPay.ConfigSaveNote"));

            return Configure();
        }

        [HttpPost]
        [ValidateInput(false)]
        [RequireHttpsByConfigAttribute(SslRequirement.Yes)]
        public ActionResult IPNHandler()
        {
            _payService.ProcessIpn(Request);
            return Content("OK");
        }
    }
}