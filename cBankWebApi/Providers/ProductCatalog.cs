using cBankWebApi.Models;
using cBankWebApi.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Providers
{
    public class ProductCatalog : IProductCatalogueProvider
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

        public List<Product> GetProductsForCompany(string beacon)
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
            _productCatalog.CompanyProducts.Add("1", products);
        }

        private ProductCatalog()
        {

        }
    }
}