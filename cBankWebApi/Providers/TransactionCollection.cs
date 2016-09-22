using cBankWebApi.Models;
using cBankWebApi.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Providers
{
    public class TransactionCollection : ITransactionSystem
    {
        private static TransactionCollection _productCatalog;

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

        public void AuthTransaction(TransactionAuth transactionAuthData)
        {
            var transaction = transactions.FirstOrDefault(s => s.TransactionGuid == transactionAuthData.TransactionId && s.TransactionStatus == TransactionStatusEnum.AuthWaiting);
            transaction.Authorize(transactionAuthData.AuthCode);
        }

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

        private static void InitData()
        {
            transactions = new List<TransactionData>();
        }

        private TransactionCollection()
        {
        }
    }
}