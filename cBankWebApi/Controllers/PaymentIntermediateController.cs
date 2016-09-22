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
    public class PaymentIntermediateController : ApiController
    {
        private readonly ITransactionSystem _transactionSystem;
        private readonly IMerchantNotifier _merchantNotifier;

        public PaymentIntermediateController(
            ITransactionSystem transactionSystem, 
            IMerchantNotifier merchantNotifier)
        {
            _transactionSystem = transactionSystem;
            _merchantNotifier = merchantNotifier;
        }
        public void Post([FromBody]TransactionAuth transactionAuth)
        {
            _transactionSystem.AuthTransaction(transactionAuth);
            _merchantNotifier.NotifyMerchant(new MerchantNotificationMessage());
        }
    }
}
