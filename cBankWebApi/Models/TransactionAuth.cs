using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Models
{
    public class TransactionAuth
    {
        public string TransactionId { get; set; }
        public string AuthCode { get; set; }
    }
}