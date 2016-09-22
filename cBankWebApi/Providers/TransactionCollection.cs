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

        private static List<TransactionInfo> transaction;

        public string RegisterTransaction(Product product)
        {
            var transactionId = Guid.NewGuid().ToString();
            transaction.Add(new TransactionInfo
            {
                ProductToBuy = product,
                TransactionChecgTime = DateTime.Now,
                TransactionGuid = transactionId,
                TrasactionStatus = TransactionStatus.AuthWaiting
            });

            return transactionId;
        }

        private static void InitData()
        {
            transaction = new List<TransactionInfo>();
        }

        private TransactionCollection()
        {
        }
    }
}