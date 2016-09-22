using cBankWebApi.Models;
using cBankWebApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cBankWebApi.Controllers
{
    public class GetSuggestionsController : ApiController
    {
        public List<Product> Get([FromUri]string beaconId)
        {
            return ProductCatalog.Instance.GetProductsForCompany(beaconId);
        }
    }
}
