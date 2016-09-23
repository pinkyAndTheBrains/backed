using cBankWebApi.Models;
using cBankWebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cBankWebApi.Providers.Interfaces;
using cBankWebApi.Push;

namespace cBankWebApi.Controllers
{
    //[Authorize]
    public class PaymentFinalController : ApiController
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IMerchantNotifier _merchantNotifier;

        public PaymentFinalController(
            ITransactionSystem transactionSystem, 
            IMerchantNotifier merchantNotifier)
        {
            _transactionSystem = transactionSystem;
            _merchantNotifier = merchantNotifier;
        }
        public void Post([FromBody]TransactionAuth transactionAuth)
        {
            var product = _transactionSystem.AuthTransaction(transactionAuth);
            _merchantNotifier.NotifyMerchant(new MerchantNotificationMessage() {ProductId = product.ProductId});
        }
    }
}
