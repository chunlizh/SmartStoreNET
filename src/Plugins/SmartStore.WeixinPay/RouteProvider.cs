using SmartStore.Web.Framework.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.WeixinPay
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.WeixinPay",
                "Plugins/SmartStore.WeixinPay/{controller}/{action}",
                new { controller = "WeixinPay", action = "Index" },
                new[] { "SmartStore.WeixinPay.Controllers" }
            )
            .DataTokens["area"] = Plugin.SystemName;

            //routes.MapRoute("SmartStore.WeixinPay",
            //    "Plugins/SmartStore.WeixinPay/{controller}/{action}",
            //    new { controller = "PayPalDirect", action = "Index" },
            //    new[] { "SmartStore.WeixinPay.Controllers" }
            //)
            //.DataTokens["area"] = Plugin.SystemName;

            //routes.MapRoute("SmartStore.WeixinPay",
            //    "Plugins/SmartStore.WeixinPay/{controller}/{action}",
            //    new { controller = "PayPalStandard", action = "Index" },
            //    new[] { "SmartStore.WeixinPay.Controllers" }
            //)
            //.DataTokens["area"] = Plugin.SystemName;

            //routes.MapRoute("SmartStore.WeixinPay",
            //    "Plugins/SmartStore.WeixinPay/{controller}/{action}",
            //    new { controller = "PayPalPlus", action = "Index" },
            //    new[] { "SmartStore.WeixinPay.Controllers" }
            //)
            //.DataTokens["area"] = Plugin.SystemName;

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