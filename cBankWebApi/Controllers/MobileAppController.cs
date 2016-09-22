﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cBankWebApi.Push;
using Microsoft.AspNet.SignalR;

namespace cBankWebApi.Controllers
{
    public class MobileAppController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            SendNotificationToMerchant();
        }

        private static void SendNotificationToMerchant()
        {
            GlobalHost
                .ConnectionManager
                .GetHubContext<MerchantNotificationHub>().Clients.All.addContosoChatMessageToPage("aaa", "something new!!");
        }


        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
