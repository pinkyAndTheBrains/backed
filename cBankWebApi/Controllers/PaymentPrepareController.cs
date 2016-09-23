using cBankWebApi.Providers;
using System.Linq;
using System.Web.Http;
using cBankWebApi.Providers.Interfaces;


namespace cBankWebApi.Controllers
{
    //[Authorize]
    public class PaymentPrepareController : ApiController
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IProductCatalog _productCatalog;

        public PaymentPrepareController(ITransactionSystem transactionSystem, IProductCatalog productCatalog)
        {
            _transactionSystem = transactionSystem;
            _productCatalog = productCatalog;
        }
        public string Get([FromUri]int productId)
        {
            var product = _productCatalog.GetProduct("1", productId);
            //.GetProductsForCompany("1").FirstOrDefault(s=>s.Id == ProductId);
            var transactionId = _transactionSystem.RegisterTransaction(product);

            return transactionId;
        }
    }
}
