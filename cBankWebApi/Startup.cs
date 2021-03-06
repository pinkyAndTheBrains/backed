﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(cBankWebApi.Startup))]

namespace cBankWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
