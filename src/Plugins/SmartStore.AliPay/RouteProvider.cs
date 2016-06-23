using SmartStore.Web.Framework.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.AliPay
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //Notify
            routes.MapRoute("Plugin.Payments.AliPay.Notify",
                 "Plugins/PaymentAliPay/Notify",
                 new { controller = "PaymentAliPay", action = "Notify" },
                 new[] { "Nop.Plugin.Payments.AliPay.Controllers" }
            );

            //Notify
            routes.MapRoute("Plugin.Payments.AliPay.Return",
                 "Plugins/PaymentAliPay/Return",
                 new { controller = "PaymentAliPay", action = "Return" },
                 new[] { "Nop.Plugin.Payments.AliPay.Controllers" }
            );
        }
        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}