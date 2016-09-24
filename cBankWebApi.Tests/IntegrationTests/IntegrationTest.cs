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
            var repo = new YaasPersistentRepo<Product>();
            var id = "57e61c923dc37c001ddcbc61";
            var product = repo.Get(id);

            Assert.IsNotNull(product?.Name);
            Assert.IsNotEmpty(product?.Name);
        }
    }
}
