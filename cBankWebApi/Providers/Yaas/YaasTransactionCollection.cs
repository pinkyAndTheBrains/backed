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
        private readonly YaasPersistentRepo _repo;
        private readonly string _defaultTable;

        public YaasTransactionCollection()
        {
            _repo = new YaasPersistentRepo();
            _defaultTable = "transactions";
        }

        public void AuthTransaction(TransactionAuth transactionAuthData)
        {
            var qId = HttpUtility.UrlEncode($"id:{transactionAuthData.TransactionId}");            
            var transactionData = _repo.GetData<List<TransactionData>>(_defaultTable, $"?q={qId}").FirstOrDefault();
            transactionData.Authorize(transactionAuthData.AuthCode);

            var data = JsonConvert.SerializeObject(transactionData);
            _repo.PutData(_defaultTable, data);
        }

        public string RegisterTransaction(Product product)
        {
            var transactionInfo = new TransactionData(product);
            var data = JsonConvert.SerializeObject(transactionInfo);
            var response = _repo.PostData<PostDataResponse>(_defaultTable, data);

            return response.id;
        }
    }
}