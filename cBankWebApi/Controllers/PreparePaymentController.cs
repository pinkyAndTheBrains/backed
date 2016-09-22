using cBankWebApi.Providers;
using System.Linq;
using System.Web.Http;


namespace cBankWebApi.Controllers
{
    public class PreparePaymentController : ApiController
    {
        public string Get([FromUri]int ProductId)
        {
            var product = ProductCatalog.Instance.GetProductsForCompany("1").FirstOrDefault(s=>s.Id == ProductId);
            var transactionId = TransactionCollection.Instance.RegisterTransaction(product);

            return transactionId;
        }
    }
}
