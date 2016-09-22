using cBankWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Providers
{
    public class ProductCatalog : IProductCatalog
    {
       
        private Dictionary<string, List<Product>> CompanyProducts { get; set; }


        public List<Product> GetProductsForCompany(string beaconId)
        {
            return CompanyProducts["1"];
        }

        public Product GetProduct(string companyId, int productId)
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
                    Price = 8
                });
            products.Add(
                new Product()
                {
                    Id = 2,
                    Name = "Esspresso",
                    Price = 6
                });
            productCatalog.CompanyProducts.Add("1", products);
        }

        public ProductCatalog()
        {

        }
    }
}