using cBankWebApi.Models;
using cBankWebApi.Providers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web;

namespace cBankWebApi.Tests.IntegrationTests
{
    [TestFixture]
    public class IntegrationTest
    {
        [TestCase]
        public void GettingProductFromYaasServiceWithoitException()
        {
            var repo = new YaasPersistentRepo();
            var id = "57e44653b8fd3f001dd65af6";
            var qId = HttpUtility.UrlEncode($"id:{id}");
            var product = repo.GetData<List<Product>>("products", $"?q={qId}");

            Assert.AreEqual(1, product.Count);
            Assert.IsNotEmpty(product[0].Name);
        }
    }
}
