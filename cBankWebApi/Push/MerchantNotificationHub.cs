using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace cBankWebApi.Push
{
    public class MerchantNotificationHub : Hub
    {

        public void NewContosoChatMessage(string name, string message)
        {
            Clients.All.addContosoChatMessageToPage(name, message);
        }


        public string Activate()
        {
            return "Monitor Activated";
        }

    }
}