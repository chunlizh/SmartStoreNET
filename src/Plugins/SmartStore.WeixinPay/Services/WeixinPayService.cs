using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartStore.Services.Payments;
using SmartStore.WeixinPay.Models;
using SmartStore.Services;
using SmartStore.Services.Common;
using SmartStore.Services.Orders;
using SmartStore.Services.Directory;
using SmartStore.Core.Domain.Directory;
using SmartStore.Services.Customers;
using SmartStore.Services.Catalog;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Domain.Customers;
using SmartStore.Core.Data;
using SmartStore.Services.Tasks;
using SmartStore.Services.Messages;
using SmartStore.Core.Localization;
using SmartStore.Core.Logging;

namespace SmartStore.WeixinPay.Services
{
    public class WeixinPayService : IWeixinPayService
    {
        private readonly IWeixinPayApi _api;
        private readonly HttpContextBase _httpContext;
        private readonly ICommonServices _services;
        private readonly IPaymentService _paymentService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly ICurrencyService _currencyService;
        private readonly CurrencySettings _currencySettings;
        private readonly ICustomerService _customerService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly OrderSettings _orderSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly IOrderService _orderService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly IWorkflowMessageService _workflowMessageService;

        public WeixinPayService(
            IWeixinPayApi api,
            HttpContextBase httpContext,
            ICommonServices services,
            IPaymentService paymentService,
            IGenericAttributeService genericAttributeService,
            IOrderTotalCalculationService orderTotalCalculationService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            ICustomerService customerService,
            IPriceFormatter priceFormatter,
            OrderSettings orderSettings,
            RewardPointsSettings rewardPointsSettings,
            IOrderService orderService,
            IRepository<Order> orderRepository,
            IOrderProcessingService orderProcessingService,
            IScheduleTaskService scheduleTaskService,
            IWorkflowMessageService workflowMessageService)
        {
            _api = api;
            _httpContext = httpContext;
            _services = services;
            _paymentService = paymentService;
            _genericAttributeService = genericAttributeService;
            _orderTotalCalculationService = orderTotalCalculationService;
            _currencyService = currencyService;
            _currencySettings = currencySettings;
            _customerService = customerService;
            _priceFormatter = priceFormatter;
            _orderSettings = orderSettings;
            _rewardPointsSettings = rewardPointsSettings;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _orderProcessingService = orderProcessingService;
            _scheduleTaskService = scheduleTaskService;
            _workflowMessageService = workflowMessageService;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        private string GetPluginUrl(string action, bool useSsl = false)
        {
            string pluginUrl = "{0}Plugins/SmartStore.AmazonPay/AmazonPay/{1}".FormatWith(_services.WebHelper.GetStoreLocation(useSsl), action);
            return pluginUrl;
        }

        public void ApplyRewardPoints(bool useRewardPoints)
        {
            //throw new NotImplementedException();
        }

        public CapturePaymentResult Capture(CapturePaymentRequest request)
        {
            var result = new CapturePaymentResult()
            {
                NewPaymentStatus = request.Order.PaymentStatus
            };

            try
            {
                //var settings = _services.Settings.LoadSetting<AmazonPaySettings>(request.Order.StoreId);
                //var client = new AmazonPayClient(settings);

                //_api.Capture(client, request, result);
            }
            //catch (OffAmazonPaymentsServiceException exc)
            //{
            //    LogAmazonError(exc, errors: result.Errors);
            //}
            catch (Exception exc)
            {
                LogError(exc, errors: result.Errors);
            }
            return result;
        }

        public void DataPollingTaskDelete()
        {
            //throw new NotImplementedException();
        }

        public void DataPollingTaskInit()
        {
            //throw new NotImplementedException();
        }

        public void DataPollingTaskProcess()
        {
            //throw new NotImplementedException();
        }

        public void DataPollingTaskUpdate(bool enabled, int seconds)
        {
            //throw new NotImplementedException();
        }

        public string GetWidgetUrl()
        {
            return "";
        }

        public void LogError(Exception exception, string shortMessage = null, string fullMessage = null, bool notify = false, IList<string> errors = null)
        {
            //throw new NotImplementedException();
        }

        public void PostProcessPayment(PostProcessPaymentRequest request)
        {
            //throw new NotImplementedException();
        }

        public PreProcessPaymentResult PreProcessPayment(ProcessPaymentRequest request)
        {
            // fulfill the Amazon checkout
            var result = new PreProcessPaymentResult();

            //try
            //{
            //    var orderGuid = request.OrderGuid.ToString();
            //    var store = _services.StoreService.GetStoreById(request.StoreId);
            //    var customer = _customerService.GetCustomerById(request.CustomerId);
            //    var currency = store.PrimaryStoreCurrency;
            //    var settings = _services.Settings.LoadSetting<AmazonPaySettings>(store.Id);
            //    var state = _httpContext.GetAmazonPayState(_services.Localization);
            //    var client = new AmazonPayClient(settings);

            //    if (!IsActive(store.Id, true))
            //    {
            //        //_httpContext.ResetCheckoutState();

            //        result.AddError(T("Plugins.Payments.AmazonPay.PaymentMethodNotActive", store.Name));
            //        return result;
            //    }

            //    var preConfirmDetails = _api.SetOrderReferenceDetails(client, state.OrderReferenceId, request.OrderTotal, currency.CurrencyCode, orderGuid, store.Name);

            //    _api.GetConstraints(preConfirmDetails, result.Errors);

            //    if (!result.Success)
            //        return result;

            //    _api.ConfirmOrderReference(client, state.OrderReferenceId);

            //    // address and payment cannot be changed if order is in open state, amazon widgets then might show an error.
            //    //state.IsOrderConfirmed = true;

            //    var cart = customer.GetCartItems(ShoppingCartType.ShoppingCart, store.Id);
            //    var isShippable = cart.RequiresShipping();

            //    // note: billing address is only available after authorization is in a non-pending and non-declined state.
            //    var details = _api.GetOrderReferenceDetails(client, state.OrderReferenceId);

            //    _api.FindAndApplyAddress(details, customer, isShippable, false);

            //    if (details.IsSetBuyer() && details.Buyer.IsSetEmail() && settings.CanSaveEmailAndPhone(customer.Email))
            //    {
            //        customer.Email = details.Buyer.Email;
            //    }

            //    _customerService.UpdateCustomer(customer);

            //    if (details.IsSetBuyer() && details.Buyer.IsSetPhone() && settings.CanSaveEmailAndPhone(customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone, store.Id)))
            //    {
            //        _genericAttributeService.SaveAttribute<string>(customer, SystemCustomerAttributeNames.Phone, details.Buyer.Phone);
            //    }
            //}
            //catch (OffAmazonPaymentsServiceException exc)
            //{
            //    LogAmazonError(exc, errors: result.Errors);
            //}
            //catch (Exception exc)
            //{
            //    LogError(exc, errors: result.Errors);
            //}

            return result;
        }

        public void ProcessIpn(HttpRequestBase request)
        {
            //throw new NotImplementedException();
        }

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest request)
        {
            // initiate Amazon payment. We do not add errors to request.Errors cause of asynchronous processing.
            var result = new ProcessPaymentResult();
            //var errors = new List<string>();
            //bool informCustomerAboutErrors = false;
            //bool informCustomerAddErrors = false;

            //try
            //{
            //    var orderGuid = request.OrderGuid.ToString();
            //    var store = _services.StoreService.GetStoreById(request.StoreId);
            //    var currency = store.PrimaryStoreCurrency;
            //    var settings = _services.Settings.LoadSetting<AmazonPaySettings>(store.Id);
            //    var state = _httpContext.GetAmazonPayState(_services.Localization);
            //    var client = new AmazonPayClient(settings);

            //    informCustomerAboutErrors = settings.InformCustomerAboutErrors;
            //    informCustomerAddErrors = settings.InformCustomerAddErrors;

            //    _api.Authorize(client, result, errors, state.OrderReferenceId, request.OrderTotal, currency.CurrencyCode, orderGuid);
            //}
            //catch (OffAmazonPaymentsServiceException exc)
            //{
            //    LogAmazonError(exc, errors: errors);
            //}
            //catch (Exception exc)
            //{
            //    LogError(exc, errors: errors);
            //}

            //if (informCustomerAboutErrors && errors != null && errors.Count > 0)
            //{
            //    // customer needs to be informed of an amazon error here. hooking OrderPlaced.CustomerNotification won't work
            //    // cause of asynchronous processing. solution: we add a customer order note that is also send as an email.

            //    var state = new AmazonPayActionState() { OrderGuid = request.OrderGuid };

            //    if (informCustomerAddErrors)
            //    {
            //        state.Errors = new List<string>();
            //        state.Errors.AddRange(errors);
            //    }

            //    AsyncRunner.Run((container, ct, o) =>
            //    {
            //        var obj = o as AmazonPayActionState;
            //        container.Resolve<IAmazonPayService>().AddCustomerOrderNoteLoop(obj);
            //    },
            //    state, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            //}

            return result;
        }

        public RefundPaymentResult Refund(RefundPaymentRequest request)
        {
            var result = new RefundPaymentResult()
            {
                NewPaymentStatus = request.Order.PaymentStatus
            };

            //try
            //{
            //    var settings = _services.Settings.LoadSetting<AmazonPaySettings>(request.Order.StoreId);
            //    var client = new AmazonPayClient(settings);

            //    string amazonRefundId = _api.Refund(client, request, result);

            //    if (amazonRefundId.HasValue() && request.Order.Id != 0)
            //    {
            //        _genericAttributeService.InsertAttribute(new GenericAttribute()
            //        {
            //            EntityId = request.Order.Id,
            //            KeyGroup = "Order",
            //            Key = AmazonPayCore.AmazonPayRefundIdKey,
            //            Value = amazonRefundId,
            //            StoreId = request.Order.StoreId
            //        });
            //    }
            //}
            //catch (OffAmazonPaymentsServiceException exc)
            //{
            //    LogAmazonError(exc, errors: result.Errors);
            //}
            //catch (Exception exc)
            //{
            //    LogError(exc, errors: result.Errors);
            //}
            return result;
        }

        public void SetupConfiguration(WeixinPayModel model)
        {
            //throw new NotImplementedException();
        }

        public VoidPaymentResult Void(VoidPaymentRequest request)
        {
            var result = new VoidPaymentResult()
            {
                NewPaymentStatus = request.Order.PaymentStatus
            };

            //try
            //{
            //    if (request.Order.PaymentStatus == PaymentStatus.Pending || request.Order.PaymentStatus == PaymentStatus.Authorized)
            //    {
            //        var settings = _services.Settings.LoadSetting<AmazonPaySettings>(request.Order.StoreId);
            //        var client = new AmazonPayClient(settings);

            //        var orderAttribute = DeserializeOrderAttribute(request.Order);

            //        _api.CancelOrderReference(client, orderAttribute.OrderReferenceId);
            //    }
            //}
            //catch (OffAmazonPaymentsServiceException exc)
            //{
            //    LogAmazonError(exc, errors: result.Errors);
            //}
            //catch (Exception exc)
            //{
            //    LogError(exc, errors: result.Errors);
            //}
            return result;
        }
    }
}