using System.Collections.Generic;
using cBankWebApi.Models;

namespace cBankWebApi.Providers.Interfaces
{
    public interface IProductCatalog
    {
        List<Product> GetProductsForCompany(string beaconId);

        Product GetProduct(string beaconId, string productId);
    }
}