using cBankWebApi.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cBankWebApi.Models;
using Newtonsoft.Json;

namespace cBankWebApi.Providers.Yaas
{
    public class YaasTransactionCollection : ITransactionSystem
    {
        private readonly YaasPersistentRepo<TransactionData> _transactionRepo;
        private readonly string _defaultTable;

        public YaasTransactionCollection()
        {
            _transactionRepo = new YaasPersistentRepo<TransactionData>();
            _defaultTable = "transactions";
        }

        public Product AuthTransaction(TransactionAuth transactionAuthData)
        {
            var transactionData = _transactionRepo.Get(transactionAuthData.TransactionId);
            var product = transactionData.ProductToBuy;
            transactionData.Authorize(transactionAuthData.AuthCode);

            _transactionRepo.Update(transactionData);
            return product;
        }

        public string RegisterTransaction(Product product)
        {
            var transactionData = new TransactionData(product);
            var response = _transactionRepo.Add<PostDataResponse>(transactionData);

            return response.id;
        }
    }
}