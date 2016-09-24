using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBankWebApi.Providers.Interfaces
{
    interface IRepository<T>
    {
        T Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
