﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace cBankWebApi.Push
{
    public class MerchantNotifier : IMerchantNotifier
    {
        public void NotifyMerchant(MerchantNotificationMessage message)
        {
            var merchantNotificationHub = GlobalHost.ConnectionManager
                .GetHubContext<MerchantNotificationHub>();

            var selectedClients = merchantNotificationHub.Clients.All;

            selectedClients.addContosoChatMessageToPage(message.TransactionId, message.ProductId);
        }
    }

    public class MerchantNotificationMessage
    {
        public string ProductId { get; set; }

        public string TransactionId { get; set; }
    }
}