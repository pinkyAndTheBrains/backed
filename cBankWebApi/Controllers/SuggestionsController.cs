using cBankWebApi.Models;
using cBankWebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cBankWebApi.Providers.Interfaces;

namespace cBankWebApi.Controllers
{
    public class SuggestionsController : ApiController
    {
        private readonly IProductCatalog _catalog;

        public SuggestionsController(IProductCatalog catalog)
        {
            _catalog = catalog;
        }
        public List<Product> Get([FromUri]string beaconId)
        {
            return _catalog.GetProductsForCompany(beaconId);
        }
    }
}
