using Autofac;
using Autofac.Integration.Mvc;
using SmartStore.Core.Infrastructure;
using SmartStore.Core.Infrastructure.DependencyManagement;
using SmartStore.Web.Controllers;
using SmartStore.WeixinPay.Filters;
using SmartStore.WeixinPay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.WeixinPay
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, bool isActiveModule)
        {
            builder.RegisterType<WeixinPayService>().As<IWeixinPayService>().InstancePerRequest();
            builder.RegisterType<WeixinPayApi>().As<IWeixinPayApi>().InstancePerRequest();

            if (isActiveModule)
            {
                builder.RegisterType<WeixinPayCheckoutFilter>().AsActionFilterFor<CheckoutController>().InstancePerRequest();
            }
        }

        public int Order
        {
            get { return 1; }
        }
    }
}