using cBankWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Providers
{
    public class TransactionCollection
    {
        private static TransactionCollection _productCatalog;

        public static TransactionCollection Instance
        {
            get
            {
                if (_productCatalog == null)
                {
                    _productCatalog = new TransactionCollection();
                    InitData();
                }
                return _productCatalog;
            }
        }

        private static List<TransactionData> transactions;

        public string RegisterTransaction(Product product)
        {
            var transactionId = Guid.NewGuid().ToString();
            transactions.Add(new TransactionData
            {
                ProductToBuy = product,
                TransactionChangeTime = DateTime.Now,
                TransactionGuid = transactionId,
                TransactionStatus = TransactionStatusEnum.AuthWaiting,
                AuthCode = "1234",
            });

            return transactionId;
        }

        public void AuthTransaction(string transactionId, string authCode)
        {
            var transaction = transactions.FirstOrDefault(s => s.TransactionGuid == transactionId && s.TransactionStatus == TransactionStatusEnum.AuthWaiting);
            transaction.Authorize(authCode);
        }

        private static void InitData()
        {
            transactions = new List<TransactionData>();
        }

        private TransactionCollection()
        {
        }
    }
}