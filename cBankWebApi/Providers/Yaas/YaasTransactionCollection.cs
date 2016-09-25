using cBankWebApi.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cBankWebApi.Models;
using Newtonsoft.Json;

namespace cBankWebApi.Providers.Yaas
{
    internal class YaasTransactionCollection : ITransactionSystem
    {
        private readonly IRepositoryReadWrite<TransactionData> _transactionRepo;
        private readonly string _defaultTable;

        public YaasTransactionCollection(IRepositoryReadWrite<TransactionData> transactionDataRepo)
        {
            _transactionRepo = transactionDataRepo;
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