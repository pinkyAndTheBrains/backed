using System.Collections.Generic;
using System.Linq;
using cBankWebApi.Models;
using cBankWebApi.Providers.Interfaces;

namespace cBankWebApi.Providers
{
    public class ProductCatalog: IProductCatalog
    {
        private Dictionary<string, List<Product>> CompanyProducts { get; set; }

        public List<Product> GetProductsForCompany(string beacon)
        {
            return CompanyProducts["1"];
        }

        public Product GetProduct(string beaconId, int productId)
        {
            return GetProductsForCompany("1").FirstOrDefault(s => s.Id == productId);
        }

        public static void InitData(ProductCatalog productCatalog)
        {
            productCatalog.CompanyProducts = new Dictionary<string, List<Product>>();


            var products = new List<Product>();
            products.Add(
                new Product()
                {
                    Id = 1,
                    Name = "Latte",
                    Price = 6m
                });
            products.Add(
                new Product()
                {
                    Id = 2,
                    Name = "Espresso",
                    Price = 3.6m
                });
            products.Add(
                new Product()
                {
                    Id = 3,
                    Name = "Cappucino",
                    Price = 4.7m
                });
            productCatalog.CompanyProducts.Add("1", products);
        }
    }
}