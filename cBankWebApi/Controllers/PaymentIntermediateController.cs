using cBankWebApi.Models;
using cBankWebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cBankWebApi.Controllers
{
    public class PaymentIntermediateController : ApiController
    {
        public void Post([FromBody]TransactionAuth transactionAuth)
        {
            TransactionCollection.Instance.AuthTransaction(transactionAuth.TransactionId, transactionAuth.AuthCode);
        }
        
    }
}
