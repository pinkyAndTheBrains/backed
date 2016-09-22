using System.Collections.Generic;
using cBankWebApi.Models;

namespace cBankWebApi.Providers
{
    public interface IProductCatalog
    {
        //Dictionary<string, List<Product>> CompanyProducts { get; set; }

        List<Product> GetProductsForCompany(string beaconId);
        Product GetProduct(string s, int productId);
    }
}