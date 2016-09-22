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

        public Product GetProduct(string beaconId, string productId)
        {
            return GetProductsForCompany("1").FirstOrDefault(s => s.ProductId == productId);
        }

        public static void InitData(ProductCatalog productCatalog)
        {
            productCatalog.CompanyProducts = new Dictionary<string, List<Product>>();


            var products = new List<Product>();
            products.Add(
                new Product()
                {
                    ProductId = "1",
                    Name = "Latte",
                    Price = 6m
                });
            products.Add(
                new Product()
                {
                    ProductId = "2",
                    Name = "Espresso",
                    Price = 3.6m
                });
            products.Add(
                new Product()
                {
                    ProductId = "3",
                    Name = "Cappucino",
                    Price = 4.7m
                });
            productCatalog.CompanyProducts.Add("1", products);
        }
    }
}