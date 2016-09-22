using System.Web.Http;
using System.Web.Mvc;
using cBankWebApi.Providers;
using cBankWebApi.Providers.Interfaces;
using cBankWebApi.Push;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace cBankWebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IMerchantNotifier, MerchantNotifier>();

            var productCatalog = new ProductCatalog();
            ProductCatalog.InitData(productCatalog);
            container.RegisterInstance<IProductCatalog>(productCatalog);
            
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

        }
    }
}