using cBankWebApi.Providers;
using System.Linq;
using System.Web.Http;
using cBankWebApi.Providers.Interfaces;


namespace cBankWebApi.Controllers
{
    public class PreparePaymentController : ApiController
    {
        private readonly IProductCatalog _productCatalog;

        public PreparePaymentController(IProductCatalog productCatalog)
        {
            _productCatalog = productCatalog;
        }
        public string Get([FromUri]int ProductId)
        {
            var product = _productCatalog.GetProduct("1", ProductId);
            //.GetProductsForCompany("1").FirstOrDefault(s=>s.Id == ProductId);
            var transactionId = TransactionCollection.Instance.RegisterTransaction(product);

            return transactionId;
        }
    }
}
