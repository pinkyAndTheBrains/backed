using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Models
{
    public class TransactionInfo
    {
        public string TransactionGuid { get; set; }
        public Product ProductToBuy { get; set; }
        public TransactionStatus TrasactionStatus { get; set; }
        public DateTime TransactionChecgTime { get; set; }
    }
}