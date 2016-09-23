using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBankWebApi.Models
{
    public class Product
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}