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
            TransactionStorage.Add(new TransactionData
            {
                ProductToBuy = product,
                TransactionChangeTime = DateTime.Now,
                TransactionGuid = transactionId,
                TransactionStatus = TransactionStatusEnum.AuthWaiting,
                AuthCode = "1234",
            });

            return transactionId;
        }

        public Product AuthTransaction(TransactionAuth transactionAuthData)
        {
            var transaction = TransactionStorage.FirstOrDefault(s => s.TransactionGuid == transactionAuthData.TransactionId && s.TransactionStatus == TransactionStatusEnum.AuthWaiting);

            if (transaction == null)
            {
                throw new ArgumentException("No transaction");
            }

            var product = transaction.ProductToBuy;
            transaction.Authorize(transactionAuthData.AuthCode);
            return product;
        }

        private static readonly List<TransactionData> TransactionStorage = new List<TransactionData>();
    }
}