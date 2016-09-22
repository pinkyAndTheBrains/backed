using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Models
{
    public class TransactionData
    {
        public string TransactionGuid { get; set; }
        public Product ProductToBuy { get; set; }
        public TransactionStatusEnum TransactionStatus { get; set; }
        public DateTime TransactionChangeTime { get; set; }
        public string AuthCode { get; set; }

        public void Authorize(string authCode)
        {
            if (TransactionStatus == TransactionStatusEnum.AuthWaiting && AuthCode == authCode)
            {
                TransactionStatus = TransactionStatusEnum.AuthReady;
                TransactionChangeTime = DateTime.Now;
            }
            else
            {
                throw new Exception("Can't authorize");
            }
        }
    }
}