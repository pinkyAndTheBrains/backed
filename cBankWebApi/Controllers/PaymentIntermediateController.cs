using cBankWebApi.Models;
using cBankWebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cBankWebApi.Push;

namespace cBankWebApi.Controllers
{
    public class PaymentIntermediateController : ApiController
    {
        private readonly IMerchantNotifier _merchantNotifier;

        public PaymentIntermediateController(IMerchantNotifier merchantNotifier)
        {
            _merchantNotifier = merchantNotifier;
        }
        public void Post([FromBody]TransactionAuth transactionAuth)
        {
            TransactionCollection.Instance.AuthTransaction(transactionAuth.TransactionId, transactionAuth.AuthCode);
            _merchantNotifier.NotifyMerchant(new MerchantNotificationMessage());
        }
        
    }
}
