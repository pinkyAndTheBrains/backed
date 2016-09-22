using cBankWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBankWebApi.Providers.Interfaces
{
    interface IProductCatalogueProvider
    {
        List<Product> GetProductsForCompany(string beacon);
    }
}
