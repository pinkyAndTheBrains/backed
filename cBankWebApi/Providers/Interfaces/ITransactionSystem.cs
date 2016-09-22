using cBankWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBankWebApi.Providers.Interfaces
{
    public interface ITransactionSystem
    {
        string RegisterTransaction(Product product);
        void AuthTransaction(TransactionAuth transactionAuthData);
    }
}
