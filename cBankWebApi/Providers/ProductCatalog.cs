using cBankWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Providers
{
    public class ProductCatalog
    {
        private static ProductCatalog _productCatalog;

        public static ProductCatalog Instance
        {
            get
            {
                if (_productCatalog == null)
                {
                    _productCatalog = new ProductCatalog();
                    InitData();
                }
                return _productCatalog;
            }
        }

        public Dictionary<string, List<Product>> CompanyProducts { get; set; }


        public List<Product> GetProductsForCompany(string beaconId)
        {
            return CompanyProducts["1"];
        }

        private static void InitData()
        {
            _productCatalog.CompanyProducts = new Dictionary<string, List<Product>>();


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
            _productCatalog.CompanyProducts.Add("1", products);
        }

        private ProductCatalog()
        {

        }
    }
}